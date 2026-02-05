namespace RegistroEstudiantes.Aplicacion.Servicios.Interface
{
    using Microsoft.EntityFrameworkCore;
    using RegistroEstudiantes.Aplicacion.Dtos.Materia;
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
    }

    /// <summary>
    /// Implementacion de la interfaz IInscripcionService
    /// </summary>
    public class InscripcionService : IInscripcionService
    {
        private readonly RegistroEstudiantesDbContext _context;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="context">Contexto de la BD</param>
        public InscripcionService(RegistroEstudiantesDbContext context)
        {
            _context = context;
        }

        /// Heredado
        public async Task InscribirMateriaAsync(DtoEntradaInscribirMateria dto)
        {
            // Validar estudiante
            var estudiante = await _context.Estudiantes
                .Include(e => e.EstudianteMaterias)
                .FirstOrDefaultAsync(e => e.EstudianteId == dto.EstudianteId);

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
            var materia = await _context.Materias.FirstOrDefaultAsync(m => m.MateriaId == dto.MateriaId);

            if (materia == null)
            {
                throw new NotFoundException("La materia no existe.");
            }

            // Validar profesor
            var profesor = await _context.Profesores
                .FirstOrDefaultAsync(p => p.ProfesorId == dto.ProfesorId);

            if (profesor == null)
            {
                throw new NotFoundException("El profesor no existe.");
            }

            // Validar que el profesor dicta la materia
            bool dictaMateria = await _context.ProfesorMaterias
                .AnyAsync(pm =>
                    pm.ProfesorId == dto.ProfesorId &&
                    pm.MateriaId == dto.MateriaId);

            if (!dictaMateria)
            {
                throw new BadRequestException("El profesor no dicta la materia seleccionada.");
            }

            // Validar que no repita profesor
            bool repiteProfesor = estudiante.EstudianteMaterias
                .Any(em => em.ProfesorId == dto.ProfesorId);

            if (repiteProfesor)
            {
                throw new BadRequestException("El estudiante no puede repetir profesor.");
            }

            // Validar que no esté inscrito en la misma materia
            bool yaInscrito = estudiante.EstudianteMaterias
                .Any(em => em.MateriaId == dto.MateriaId);

            if (yaInscrito)
            {
                throw new BadRequestException("El estudiante ya está inscrito en esta materia.");
            }

            // Crear inscripción
            var estudianteMateria = new EstudianteMateria
            {
                EstudianteId = dto.EstudianteId,
                MateriaId = dto.MateriaId,
                ProfesorId = dto.ProfesorId,
                FechaRegistro = DateTime.UtcNow
            };

            _context.EstudianteMaterias.Add(estudianteMateria);
            await _context.SaveChangesAsync();
        }
    }
}
