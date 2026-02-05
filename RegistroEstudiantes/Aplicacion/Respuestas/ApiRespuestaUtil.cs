using Microsoft.AspNetCore.Mvc;

namespace RegistroEstudiantes.Aplicacion.Respuestas
{
    /// <summary>
    /// Utilidad para transformar ApiRespuesta en ActionResult.
    /// </summary>
    public static class ApiRespuestaUtil
    {
        /// <summary>
        /// Transforma una instancia de <see cref="ApiRespuestaHttp{T}"/> en un <see cref="IActionResult"/>.
        /// </summary>
        /// <typeparam name="T">El tipo de datos contenidos en la respuesta.</typeparam>
        /// <param name="respuestaHttp">La respuesta de la API a transformar.</param>
        /// <returns>Un ActionResult correspondiente a la respuesta.</returns>
        public static IActionResult Convertir<T>(ApiRespuestaHttp<T> respuestaHttp) where T : class
        {
            if (respuestaHttp == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            // Transformar a ApiRespuesta<T> eliminando el código HTTP
            var respuesta = new ApiRespuesta<T>
            {
                Exito = respuestaHttp.Exito,
                Datos = respuestaHttp.Datos,
                Errores = respuestaHttp.Errores
            };

            // Devolver el ActionResult con el código HTTP especificado
            return new ObjectResult(respuesta)
            {
                StatusCode = (int)respuestaHttp.CodigoHttp
            };
        }
    }
}
