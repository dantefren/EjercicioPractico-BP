namespace WSMovimientos.Entidades.Modelo
{
    public partial class BmCuentum
    {
        public BmCuentum()
        {
            BmMovimientos = new HashSet<BmMovimiento>();
        }

        public long IdCuenta { get; set; }
        public long IdPersona { get; set; }
        public int NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; } = null!;
        public decimal SaldoInicial { get; set; }
        public bool? Estado { get; set; }

        public virtual BmPersona IdPersonaNavigation { get; set; } = null!;
        public virtual ICollection<BmMovimiento> BmMovimientos { get; set; }
    }
}
