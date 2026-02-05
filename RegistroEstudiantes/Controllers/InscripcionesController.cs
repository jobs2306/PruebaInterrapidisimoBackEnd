namespace RegistroEstudiantes.Controllers
{
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using RegistroEstudiantes.Aplicacion.Dtos.Materia;
    using RegistroEstudiantes.Aplicacion.Respuestas;
    using RegistroEstudiantes.Aplicacion.Servicios.Interface;
    using RegistroEstudiantes.Dominio.Excepciones;

    /// <summary>
    /// Controlador para las inscripciones 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class InscripcionesController : ControllerBase
    {
        private readonly IInscripcionService _inscripcionService;

        public InscripcionesController(IInscripcionService inscripcionService)
        {
            _inscripcionService = inscripcionService;
        }

        /// <summary>
        /// Crea una inscripcion de un estudiante a una materia
        /// </summary>
        /// <param name="dto">Dto con datos necesarios para crear la inscripcion</param>
        /// <returns> 
        /// Retorna una ApiRespuesta con StatusCodes
        ///   200OK Cuando crea la inscripcion correctamente
        ///   400BadRequest Si no hay un error en la validacion de datos de entrada.
        ///   404NotFound Si no encuentra la materia, el estudiante o el profesor relacionados al registros.
        ///   401Unauthorized Si no está autenticado.
        ///   500InternalServerError Si ocurrio una falla o errror NO controlado 
        /// </returns>
        /// <response code="200">Cuando crea la inscripcion correctamente</response>
        /// <response code="400">Si encuentra un error.</response>
        /// <response code="404">Si no encuentra alguna entidad.</response>
        /// <response code="401">Si no está autenticado.</response>
        /// <response code="500">Si ocurrio una falla o errror NO controlado</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiRespuesta<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiRespuesta<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiRespuesta<string>), (int)HttpStatusCode.NotFound)]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ApiRespuesta<string>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> InscribirMateria([FromBody] DtoEntradaInscribirMateria dto)
        {
            try
            {
                await _inscripcionService.InscribirMateriaAsync(dto);

                return ApiRespuestaUtil.Convertir(ApiRespuestaHttp<string>.RespuestaExitosa("Materia inscrita exitosamente"));
            }
            catch (BadRequestException ex)
            {
                return ApiRespuestaUtil.Convertir(ApiRespuestaHttp<string>.RespuestaFallida(ex.Message, HttpStatusCode.BadRequest));
            }
            catch (NotFoundException ex)
            {
                return ApiRespuestaUtil.Convertir(ApiRespuestaHttp<string>.RespuestaFallida(ex.Message, HttpStatusCode.NotFound));
            }
            catch (Exception)
            {
                return ApiRespuestaUtil.Convertir(ApiRespuestaHttp<string>.RespuestaFallida("Error interno del servidor"));
            }
        }

    }
}
