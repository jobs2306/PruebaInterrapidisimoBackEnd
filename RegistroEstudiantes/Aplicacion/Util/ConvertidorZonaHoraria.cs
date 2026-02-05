namespace RegistroEstudiantes.Aplicacion.Util
{
    public static class ConvertidorZonaHoraria
    {
        /// <summary>
        /// Convierte una fecha y hora a la zona horaria especificada.
        /// </summary>
        /// <param name="fechaHora">La fecha y hora a convertir.</param>
        /// <param name="idZonaHorariaDestino">El identificador de la zona horaria de destino.</param>
        /// <returns>La fecha y hora convertida a la zona horaria especificada.</returns>
        public static DateTime ConvertirAZonaHoraria(DateTime fechaHora, string idZonaHorariaDestino)
        {
            try
            {
                // Convertir a UTC si no lo está
                DateTime horaUtc = fechaHora.Kind == DateTimeKind.Utc
                    ? fechaHora
                    : fechaHora.ToUniversalTime();

                // Obtener la zona horaria de destino
                TimeZoneInfo zonaHorariaDestino = TimeZoneInfo.FindSystemTimeZoneById(idZonaHorariaDestino);

                // Convertir la hora UTC a la zona horaria de destino
                return TimeZoneInfo.ConvertTimeFromUtc(horaUtc, zonaHorariaDestino);
            }
            catch (TimeZoneNotFoundException)
            {
                throw new ArgumentException($"Zona horaria no encontrada: {idZonaHorariaDestino}", nameof(idZonaHorariaDestino));
            }
            catch (InvalidTimeZoneException)
            {
                throw new ArgumentException($"Zona horaria inválida: {idZonaHorariaDestino}", nameof(idZonaHorariaDestino));
            }
        }

        /// <summary>
        /// Obtiene la hora actual convertida a la zona horaria de Colombia.
        /// </summary>
        /// <returns>La hora actual en la zona horaria de Colombia.</returns>
        public static DateTime ObtenerHoraActualColombia()
        {
            return ConvertirAHoraColombia(DateTime.Now);
        }

        /// <summary>
        /// Convierte una fecha y hora a la zona horaria de Colombia (UTC-5).
        /// </summary>
        /// <param name="fechaHora">La fecha y hora a convertir.</param>
        /// <returns>La fecha y hora convertida a la zona horaria de Colombia.</returns>
        public static DateTime ConvertirAHoraColombia(DateTime fechaHora)
        {
            const string idZonaHorariaColombia = "SA Pacific Standard Time";
            return ConvertirAZonaHoraria(fechaHora, idZonaHorariaColombia);
        }
    }
}
