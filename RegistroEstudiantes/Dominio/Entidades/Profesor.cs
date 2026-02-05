namespace RegistroEstudiantes.Dominio.Entidades
{
    /// <summary>
    /// Modelo de la entidad Profesor
    /// </summary>
    public class Profesor
    {
        /// <summary>
        /// Identificador del profesor
        /// </summary>
        public int ProfesorId { get; set; }

        /// <summary>
        /// Nombre del profesor
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Coleccion de profesorMaterias
        /// </summary>
        public virtual ICollection<ProfesorMateria>? ProfesorMaterias { get; set; }

        /// <summary>
        /// Coleccion de EstudianteMaterias
        /// </summary>
        public virtual ICollection<EstudianteMateria>? EstudianteMaterias { get; set; }
    }
}
