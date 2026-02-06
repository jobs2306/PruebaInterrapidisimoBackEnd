namespace RegistroEstudiantes.Aplicacion.Dtos.Login.Entrada
{
    /// <summary>
    /// Dto para el login de un usuario
    /// </summary>
    public class DtoLogin
    {
        /// <summary>
        /// Email del usuario
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Constrasenia del usuario
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
