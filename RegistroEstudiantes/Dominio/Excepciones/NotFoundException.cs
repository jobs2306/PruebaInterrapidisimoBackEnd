namespace RegistroEstudiantes.Dominio.Excepciones
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string mensaje)
            : base(mensaje)
        {
        }
    }
}
