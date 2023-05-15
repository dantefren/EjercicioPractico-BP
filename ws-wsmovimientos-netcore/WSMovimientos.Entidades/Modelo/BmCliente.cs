namespace WSMovimientos.Entidades.Modelo
{
    public partial class BmCliente
    {
        public long? IdCliente { get; set; }
        public long? IdPersona { get; set; }
        public string Contrasenia { get; set; } = null!;
        public bool? Estado { get; set; }

        public virtual BmPersona IdPersonaNavigation { get; set; } = null!;
    }
}
