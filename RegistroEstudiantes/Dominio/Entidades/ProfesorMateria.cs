namespace RegistroEstudiantes.Dominio.Entidades
{
    /// <summary>
    /// Modelo para la entidad ProfesorMateria
    /// </summary>
    public class ProfesorMateria
    {
        /// <summary>
        /// Identificador del profesorMateria
        /// </summary>
        public int ProfesorMateriaId { get; set; }

        /// <summary>
        /// Identificador del profesor
        /// </summary>
        public int ProfesorId { get; set; }

        /// <summary>
        /// Identificador de la materia
        /// </summary>
        public int MateriaId { get; set; }

        /// <summary>
        /// Profesor relacionado
        /// </summary>
        public virtual Profesor? Profesor { get; set; }

        /// <summary>
        /// Materia relacionada
        /// </summary>
        public virtual Materia? Materia { get; set; }
    }
}
