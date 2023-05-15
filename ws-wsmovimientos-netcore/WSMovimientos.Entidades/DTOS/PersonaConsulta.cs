namespace WSMovimientos.Entidades.DTOS
{
    public class PersonaConsulta
    {
        public long Id { get; set; } = 0;
        public string Nombre { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public int Edad { get; set; } = 0;
        public string Identificacion { get; set; } = string.Empty;
        public string? Direccion { get; set; } = null;
        public int? Telefono { get; set; } = null;
    }
}