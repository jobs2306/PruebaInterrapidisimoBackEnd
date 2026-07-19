namespace RegistroEstudiantes.Controllers
{
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using RegistroEstudiantes.Aplicacion.Dtos.Login.Entrada;
    using RegistroEstudiantes.Aplicacion.Dtos.Login.salida;
    using RegistroEstudiantes.Aplicacion.Respuestas;
    using RegistroEstudiantes.Aplicacion.Servicios;
    using RegistroEstudiantes.Dominio.Excepciones;

    /// <summary>
    /// Controlador para manejar la sesion de un usuario
    /// </summary>
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Crea a un usuario
        /// </summary>
        /// <param name="dto">Dto con datos necesarios para crear al usuario</param>
        /// <returns>
        /// Retorna una ApiRespuesta con StatusCodes
        ///   200OK Cuando crea al usuario
        ///   400BadRequest Si hay un error en la validacion de datos de entrada.
        ///   500InternalServerError Si ocurrio una falla o errror NO controlado
        /// </returns>
        /// <response code="200">Cuando crea al usuario correctamente</response>
        /// <response code="400">Si encuentra un error.</response>
        /// <response code="500">Si ocurrio una falla o errror NO controlado</response>
        [HttpPost("Registrar")]
        [ProducesResponseType(typeof(ApiRespuesta<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiRespuesta<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiRespuesta<string>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Registrar([FromBody] DtoRegistrar dto)
        {
            try
            {
                await _authService.RegistrarAsync(dto);
                return ApiRespuestaUtil.Convertir(ApiRespuestaHttp<string>.RespuestaExitosa("Usuario registrado exitosamente"));
            }
            catch (BadRequestException ex)
            {
                return ApiRespuestaUtil.Convertir(ApiRespuestaHttp<string>.RespuestaFallida(ex.Message, HttpStatusCode.BadRequest));
            }
            catch (Exception)
            {
                return ApiRespuestaUtil.Convertir(ApiRespuestaHttp<string>.RespuestaFallida(
                        "Error interno del servidor",
                        HttpStatusCode.InternalServerError
                    )
                );
            }
        }

        /// <summary>
        /// Inicia la sesion de un usuario
        /// </summary>
        /// <param name="dto">Dto con datos necesarios para iniciar sesion</param>
        /// <returns> 
        /// Retorna una ApiRespuesta con StatusCodes
        ///   200OK Cuando crea inicia sesion
        ///   401Unauthorized Si no inicia la sesion.
        ///   500InternalServerError Si ocurrio una falla o errror NO controlado 
        /// </returns>
        /// <response code="200">Cuando inicia sesión correctamente</response>
        /// <response code="401">Si no está autenticado.</response>
        /// <response code="500">Si ocurrio una falla o errror NO controlado</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiRespuesta<DtoRespuestaAuth>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ApiRespuesta<string>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Login([FromBody] DtoLogin dto)
        {
            try
            {
                var result = await _authService.LoginAsync(dto);
                return ApiRespuestaUtil.Convertir(ApiRespuestaHttp<DtoRespuestaAuth>.RespuestaExitosa(result));
            }
            catch (UnauthorizedException ex)
            {
                return ApiRespuestaUtil.Convertir(ApiRespuestaHttp<string>.RespuestaFallida(ex.Message, HttpStatusCode.Unauthorized));
            }
            catch (Exception)
            {
                return ApiRespuestaUtil.Convertir(ApiRespuestaHttp<string>.RespuestaFallida(
                        "Error interno del servidor",
                        HttpStatusCode.InternalServerError
                    )
                );
            }
        }
    }
}
