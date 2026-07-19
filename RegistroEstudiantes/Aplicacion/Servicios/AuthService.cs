using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RegistroEstudiantes.Aplicacion.Dtos.Login.Entrada;
using RegistroEstudiantes.Aplicacion.Dtos.Login.salida;
using RegistroEstudiantes.Aplicacion.Util;
using RegistroEstudiantes.Dominio.Entidades;
using RegistroEstudiantes.Dominio.Excepciones;
using RegistroEstudiantes.Infraestructura.Data;

namespace RegistroEstudiantes.Aplicacion.Servicios
{
    /// <summary>
    /// Interface para operaciones de autorizacion
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Metodo para iniciar sesion
        /// </summary>
        /// <param name="dto">Dto con los datos para iniciar sesion</param>
        Task<DtoRespuestaAuth> LoginAsync(DtoLogin dto);

        /// <summary>
        /// Metodo para registrar un estudiante
        /// </summary>
        /// <param name="dto">Dto con los datos para registrar el estudiante</param>
        Task RegistrarAsync(DtoRegistrar dto);
    }

    /// <summary>
    /// Implementacion de la interfaz IAuthService
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly RegistroEstudiantesDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<object> _passwordHasher = new();

        public AuthService(
            RegistroEstudiantesDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public async Task RegistrarAsync(DtoRegistrar dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                throw new BadRequestException("El email es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(dto.Nombre))
            {
                throw new BadRequestException("El nombre es obligatorio");
            }

            if (!EsEmailValido(dto.Email))
            {
                throw new BadRequestException("El email no tiene un formato válido.");
            }

            if (string.IsNullOrWhiteSpace(dto.Password))
            {
                throw new BadRequestException("La contraseña es obligatoria.");
            }

            if (!EsPasswordValido(dto.Password))
            {
                throw new BadRequestException("La contraseña debe cumplir los siguiente requisitos: \n " +
                    "Debe tener de 8 a 16 caracteres. \n " +
                    "Debe tener al menos una letra, un número y un simbolo");
            }

            var existeEmail = await _context.Estudiantes.AnyAsync(e => e.Email == dto.Email);

            if (existeEmail)
            {
                throw new BadRequestException("El email ya se encuentra registrado.");
            }

            var passwordHash = _passwordHasher.HashPassword(null!, dto.Password);

            var estudiante = new Estudiante
            {
                Email = dto.Email,
                Nombre = dto.Nombre,
                PasswordHash = passwordHash,
                FechaRegistro = ConvertidorZonaHoraria.ObtenerHoraActualColombia()
            };

            await _context.Estudiantes.AddAsync(estudiante);
            await _context.SaveChangesAsync();
        }
        public async Task<DtoRespuestaAuth> LoginAsync(DtoLogin dto)
        {
            var estudiante = await _context.Estudiantes.FirstOrDefaultAsync(e => e.Email == dto.Email);

            if (estudiante == null)
            {
                throw new UnauthorizedException("Credenciales inválidas.");
            }

            var result = _passwordHasher.VerifyHashedPassword(
                null!,
                estudiante.PasswordHash,
                dto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedException("Credenciales inválidas.");
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, estudiante.EstudianteId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, estudiante.Email),
                new Claim("EstudianteId", estudiante.EstudianteId.ToString())
            };

            var expireMinutes = int.Parse(_configuration["Jwt:ExpireMinutes"]!);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: creds);

            return new DtoRespuestaAuth
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Nombre = estudiante.Nombre,
                Expira = token.ValidTo
            };
        }
        private static bool EsEmailValido(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private static bool EsPasswordValido(string password)
        {
            var regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,16}$");
            return regex.IsMatch(password);
        }
    }
}

