namespace RegistroEstudiantes.Aplicacion.Dtos.Materia
{
    /// <summary>
    /// DTO para inscripción de materias
    /// </summary>
    public class DtoEntradaInscribirMateria
    {
        /// <summary>
        /// Identificador del estudiante
        /// </summary>
        public int EstudianteId { get; set; }

        /// <summary>
        /// Identificador de la materia
        /// </summary>
        public int MateriaId { get; set; }

        /// <summary>
        /// Identificador del profesor
        /// </summary>
        public int ProfesorId { get; set; }
    }
}
