namespace RegistroEstudiantes.Dominio.Excepciones
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string mensaje)
            : base(mensaje)
        {
        }
    }
}
