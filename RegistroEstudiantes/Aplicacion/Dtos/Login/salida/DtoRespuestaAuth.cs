namespace RegistroEstudiantes.Aplicacion.Dtos.Login.salida
{
    /// <summary>
    /// Dto de respuesta de autorizacion
    /// </summary>
    public class DtoRespuestaAuth
    {
        /// <summary>
        /// Token de respuesta auth
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de expiracion del token
        /// </summary>
        public DateTime Expira { get; set; }
    }
}
