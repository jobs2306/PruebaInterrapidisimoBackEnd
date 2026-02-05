using System.Net;

namespace RegistroEstudiantes.Aplicacion.Respuestas
{
    /// <summary>
    /// Representa una respuesta genérica de la API, que incluye un estado de éxito, los datos devueltos, posibles errores y un código HTTP.
    /// </summary>
    /// <typeparam name="T">El tipo de datos devueltos en la respuesta.</typeparam>
    public class ApiRespuesta<T> where T : class
    {
        /// <summary>
        /// Indica si la operación fue exitosa.
        /// </summary>
        public bool Exito { get; set; } = true;

        /// <summary>
        /// Los datos devueltos por la operación cuando la respuesta es exitosa.
        /// </summary>
        public T? Datos { get; set; }

        /// <summary>
        /// Lista de errores en caso de que la operación falle.
        /// </summary>
        public List<string>? Errores { get; set; }

        /// <summary>
        /// Crea una instancia de <see cref="ApiRespuesta{T}"/> que representa una respuesta exitosa.
        /// </summary>
        /// <param name="datos">Los datos a devolver en la respuesta.</param>
        /// <param name="codigoHttp">El código HTTP asociado. Predeterminado: 200 OK.</param>
        /// <returns>Una respuesta con éxito y los datos proporcionados.</returns>
        public static ApiRespuestaHttp<T> RespuestaExitosa(T datos, HttpStatusCode codigoHttp = HttpStatusCode.OK)
        {
            return new ApiRespuestaHttp<T>
            {
                Exito = true,
                Datos = datos,
                CodigoHttp = codigoHttp
            };
        }

        /// <summary>
        /// Crea una instancia de <see cref="ApiRespuesta{T}"/> que representa una respuesta fallida.
        /// </summary>
        /// <param name="errores">Una lista de errores que explican por qué la operación falló.</param>
        /// <param name="codigoHttp">El código HTTP asociado. Predeterminado: 500 Internal Server Error.</param>
        /// <returns>Una respuesta fallida con la lista de errores.</returns>
        public static ApiRespuestaHttp<T> RespuestaFallida(List<string> errores, HttpStatusCode codigoHttp = HttpStatusCode.InternalServerError)
        {
            return new ApiRespuestaHttp<T>
            {
                Exito = false,
                Errores = errores,
                CodigoHttp = codigoHttp
            };
        }

        /// <summary>
        /// Crea una respuesta fallida con un solo mensaje de error.
        /// </summary>
        /// <param name="error">El mensaje de error.</param>
        /// <param name="httpStatusCode">El código HTTP asociado. Predeterminado: 500 Internal Server Error.</param>
        /// <returns>Una respuesta fallida con un solo mensaje de error.</returns>
        public static ApiRespuestaHttp<T> RespuestaFallida(string error, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError)
        {
            return new ApiRespuestaHttp<T>
            {
                Exito = false,
                Errores = [error],
                CodigoHttp = httpStatusCode
            };
        }
    }

    public class ApiRespuestaHttp<T> : ApiRespuesta<T> where T : class
    {
        /// <summary>
        /// El código HTTP asociado con la respuesta.
        /// </summary>
        public HttpStatusCode CodigoHttp { get; set; } = HttpStatusCode.OK;
    }
}
