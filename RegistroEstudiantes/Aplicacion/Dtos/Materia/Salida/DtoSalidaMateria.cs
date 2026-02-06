namespace RegistroEstudiantes.Aplicacion.Dtos.Materia.Salida
{
    /// <summary>
    /// Dto de salida que representa una materia
    /// </summary>
    public class DtoSalidaMateria
    {
        /// <summary>
        /// Identificador de la materia
        /// </summary>
        public int MateriaId { get; set; }

        /// <summary>
        /// Nombre de la materia
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Nombre del profesor que dicta la materia
        /// </summary>
        public string Profesor { get; set; } = string.Empty;

        /// <summary>
        /// Indica si el estudiante está matriculado
        /// </summary>
        public bool Matriculada { get; set; }
    }
}
