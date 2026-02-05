namespace RegistroEstudiantes.Dominio.Entidades
{
    /// <summary>
    /// Modelo que representa la entidad EstudianteMateria
    /// </summary>
    public class EstudianteMateria
    {
        /// <summary>
        /// Identificador del estudiante materia
        /// </summary>
        public int EstudianteMateriaId { get; set; }

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

        /// <summary>
        /// Fecha del registro
        /// </summary>
        public DateTime FechaRegistro { get; set; }

        /// <summary>
        /// Materia relacionada
        /// </summary>
        public virtual Materia? Materia { get; set; }

        /// <summary>
        /// Estudiante relacionado
        /// </summary>
        public virtual Estudiante? Estudiante { get; set; }

        /// <summary>
        /// Profesor relacionado al estudiante y materia
        /// </summary>
        public virtual Profesor? Profesor { get; set; }
    }
}
