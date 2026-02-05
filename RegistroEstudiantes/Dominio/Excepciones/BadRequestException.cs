namespace RegistroEstudiantes.Dominio.Excepciones
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string mensaje)
            : base(mensaje)
        {
        }
    }
}
