namespace RegistroEstudiantes.Aplicacion.Dtos.Materia.Salida
{
    /// <summary>
    /// Dto de salida de una materia con la informacion de los companeros de estudiante
    /// </summary>
    public class DtoSalidaMateriaCompaneros : DtoSalidaMateria
    {
        /// <summary>
        /// Companeros de la materia, solo en caso de que se encuentre matriculada
        /// </summary>
        public List<string>? Companeros { get; set; }
    }
}
