namespace WSMovimientos.Entidades.DTOS
{
    public class ClienteConsulta
    {
        public long Id { get; set; } = 0;
        public long IdPersona { get; set; } = 0;
        public PersonaConsulta Persona { get; set; } = null;
        public string Contrasenia { get; set; } = string.Empty;
        public bool Estado { get; set; } = true;

    }
}