namespace RegistroEstudiantes.Aplicacion.Dtos.Login.Entrada
{
    /// <summary>
    /// Dto que representa a un estudiante que se va a registrar
    /// </summary>
    public class DtoRegistrar : DtoLogin
    {
        /// <summary>
        /// Nombre del estudiante
        /// </summary>
        public string Nombre { get; set; } = string.Empty;
    }
}
