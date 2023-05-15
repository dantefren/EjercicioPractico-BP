namespace WSMovimientos.Entidades.DTOS
{
    public class Cliente
    {
        public long idCliente { get; set; } = 0;
        public bool? Estado { get; set; }
        public string Contrasenia { get; set; } = String.Empty;
    }
}