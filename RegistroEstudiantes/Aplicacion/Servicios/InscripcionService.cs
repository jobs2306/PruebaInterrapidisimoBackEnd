namespace RegistroEstudiantes.Aplicacion.Servicios
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using RegistroEstudiantes.Aplicacion.Dtos.Materia.Entrada;
    using RegistroEstudiantes.Aplicacion.Dtos.Materia.Salida;
    using RegistroEstudiantes.Aplicacion.Util;
    using RegistroEstudiantes.Dominio.Entidades;
    using RegistroEstudiantes.Dominio.Excepciones;
    using RegistroEstudiantes.Infraestructura.Data;

    /// <summary>
    /// Interface para inscribir un estudiante a una materia
    /// </summary>
    public interface IInscripcionService
    {
        /// <summary>
        /// Metodo para inscribir un estudiante a una materia
        /// </summary>
        /// <param name="dto">Datos necesarios para la inscripcion</param>
        Task InscribirMateriaAsync(DtoEntradaInscribirMateria dto);

        /// <summary>
        /// Obtiene el listado de materias indicando si el estudiante las tiene matriculadas
        /// </summary>
        /// <returns>Retorna la lista de materias</returns>
        Task<List<DtoSalidaMateria>> ObtenerMateriasAsync();

        /// <summary>
        /// Obtiene la informacion las materias matriculadas
        /// </summary>
        /// <returns>Retorna la lista de materias matriculadas con la informacion de companeros de cada materia</returns>
        Task<List<DtoSalidaMateriaCompaneros>> ObtenerMisMateriasAsync();

        /// <summary>
        /// Cancela la inscripción del estudiante autenticado a una materia
        /// </summary>
        /// <param name="materiaId">Identificador de la materia</param>
        Task CancelarInscripcionAsync(int materiaId);
    }

    /// <summary>
    /// Implementacion de la interfaz IInscripcionService
    /// </summary>
    public class InscripcionService : IInscripcionService
    {
        private readonly RegistroEstudiantesDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="context">Contexto de la BD</param>
        /// <param name="httpContextAccessor">contexto http</param>
        public InscripcionService(RegistroEstudiantesDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        /// Heredado
        public async Task InscribirMateriaAsync(DtoEntradaInscribirMateria dto)
        {
            var estudianteId = ObtenerEstudianteIdSesion();

            // Validar estudiante
            var estudiante = await _context.Estudiantes.Include(e => e.EstudianteMaterias)
                .FirstOrDefaultAsync(e => e.EstudianteId == estudianteId);

            if (estudiante == null)
            {
                throw new NotFoundException("El estudiante no existe.");
            }

            // Validar máximo 3 materias
            if (estudiante.EstudianteMaterias!.Count >= 3)
            {
                throw new BadRequestException("El estudiante ya tiene el máximo de materias permitidas.");
            }

            // Validar materia
            var materia = await _context.Materias.Include(m => m.ProfesorMaterias)!
                .ThenInclude(pm => pm.Profesor)
                .FirstOrDefaultAsync(m => m.MateriaId == dto.MateriaId);

            if (materia == null)
            {
                throw new NotFoundException("La materia no existe.");
            }

            // Validar profesor
            var profesor = materia.ProfesorMaterias?.FirstOrDefault()?.Profesor;

            if (materia.ProfesorMaterias == null || materia.ProfesorMaterias.Count != 1)
            {
                throw new BadRequestException("Configuración inválida de la materia.");
            }

            // Validar que no esté inscrito en la misma materia
            bool yaInscrito = estudiante.EstudianteMaterias.Any(em => em.MateriaId == dto.MateriaId);

            if (yaInscrito)
            {
                throw new BadRequestException("El estudiante ya está inscrito en esta materia.");
            }

            // Validar que no repita profesor
            bool repiteProfesor = estudiante.EstudianteMaterias.Any(em => em.ProfesorId == profesor.ProfesorId);

            if (repiteProfesor)
            {
                throw new BadRequestException("El estudiante no puede repetir profesor.");
            }

            // Crear inscripción
            var estudianteMateria = new EstudianteMateria
            {
                EstudianteId = estudianteId,
                MateriaId = dto.MateriaId,
                ProfesorId = profesor.ProfesorId,
                FechaRegistro = ConvertidorZonaHoraria.ObtenerHoraActualColombia()
            };

            _context.EstudianteMaterias.Add(estudianteMateria);
            await _context.SaveChangesAsync();
        }

        // Heredado
        public async Task<List<DtoSalidaMateria>> ObtenerMateriasAsync()
        {
            var estudianteId = ObtenerEstudianteIdSesion();

            // Materias matriculadas por el estudiante
            var materiasMatriculadas = await _context.EstudianteMaterias
                .Where(em => em.EstudianteId == estudianteId)
                .Select(em => em.MateriaId)
                .ToListAsync();

            // Todas las materias con su profesor
            var materias = await _context.Materias
                .Select(m => new DtoSalidaMateria
                {
                    MateriaId = m.MateriaId,
                    Nombre = m.Nombre,

                    // Obtener nombre del profesor (único por materia)
                    Profesor = m.ProfesorMaterias!
                        .Select(pm => pm.Profesor!.Nombre)
                        .FirstOrDefault() ?? string.Empty,

                    Matriculada = materiasMatriculadas.Contains(m.MateriaId)
                })
                .OrderBy(m => m.Matriculada)
                .ToListAsync();

            return materias;
        }

        // Heredado
        public async Task<List<DtoSalidaMateriaCompaneros>> ObtenerMisMateriasAsync()
        {
            var estudianteId = ObtenerEstudianteIdSesion();

            var materias = await _context.EstudianteMaterias
                .Where(em => em.EstudianteId == estudianteId)
                .Select(em => em.Materia)
                .Select(m => new DtoSalidaMateriaCompaneros
                {
                    MateriaId = m.MateriaId,
                    Nombre = m.Nombre,
                    Matriculada = true,
                    Profesor = m.ProfesorMaterias!
                        .Select(pm => pm.Profesor!.Nombre)
                        .FirstOrDefault() ?? string.Empty,

                    Companeros = m.EstudianteMaterias!
                        .Where(em => em.EstudianteId != estudianteId)
                        .Select(em => em.Estudiante!.Nombre)
                        .Distinct()
                        .ToList()
                })
                .ToListAsync();

            return materias;
        }

        // Hereado
        public async Task CancelarInscripcionAsync(int materiaId)
        {
            var estudianteId = ObtenerEstudianteIdSesion();

            var inscripcion = await _context.EstudianteMaterias
                .FirstOrDefaultAsync(em =>
                    em.EstudianteId == estudianteId &&
                    em.MateriaId == materiaId);

            if (inscripcion == null)
            {
                throw new BadRequestException("El estudiante no está inscrito en la materia.");
            }

            _context.EstudianteMaterias.Remove(inscripcion);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Metodo para obtener el id de sesion del estudiante
        /// </summary>
        /// <returns>retorna el id de la sesion</returns>
        private int ObtenerEstudianteIdSesion()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity!.IsAuthenticated)
            {
                throw new UnauthorizedException("Usuario no autenticado.");
            }

            var claim = user.FindFirst("EstudianteId");

            if (claim == null)
            {
                throw new UnauthorizedException("No se pudo obtener el estudiante de la sesion.");
            }

            return int.Parse(claim.Value);
        }

    }
}
