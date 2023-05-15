namespace WSMovimientos.Entidades.Modelo
{
    public partial class BmPersona
    {
        public BmPersona()
        {
            BmClientes = new HashSet<BmCliente>();
            BmCuenta = new HashSet<BmCuentum>();
        }

        public long IdPersona { get; set; }
        public string Nombre { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public int Edad { get; set; }
        public string Identificacion { get; set; } = null!;
        public string? Direccion { get; set; }
        public int? Telefono { get; set; }
        public virtual ICollection<BmCliente> BmClientes { get; set; }
        public virtual ICollection<BmCuentum> BmCuenta { get; set; }
    }
}
