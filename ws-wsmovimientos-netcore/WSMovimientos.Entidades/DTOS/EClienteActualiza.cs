namespace WSMovimientos.Entidades.DTOS
{
    public class EClienteActualiza
    {
        public long IdCliente { get; set; }
        public string Contrasenia { get; set; } = string.Empty;
        public bool? Estado { get; set; }
    }
}