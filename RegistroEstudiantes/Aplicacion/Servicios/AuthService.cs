using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RegistroEstudiantes.Aplicacion.Dtos.Login;
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
                Expira = token.ValidTo
            };
        }
    }
}
