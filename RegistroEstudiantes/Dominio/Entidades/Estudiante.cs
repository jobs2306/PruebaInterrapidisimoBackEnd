namespace RegistroEstudiantes.Dominio.Entidades
{
    /// <summary>
    /// Modelo de la entidad estudiante
    /// </summary>
    public class Estudiante
    {
        /// <summary>
        /// Identificador del estudiante
        /// </summary>
        public int EstudianteId { get; set; }

        /// <summary>
        /// Nombre del estudiante
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Email del estudiante
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Contrasenia del estudiante
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de registro del estudiante
        /// </summary>
        public DateTime FechaRegistro { get; set; }

        /// <summary>
        /// Coleccion de EstudianteMateria relacionadas al estudiante
        /// </summary>
        public virtual ICollection<EstudianteMateria>? EstudianteMaterias { get; set; }
    }
}
