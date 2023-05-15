namespace WSMovimientos.Entidades.Modelo
{
    public partial class BmMovimiento
    {
        public long IdMovimientos { get; set; }
        public long IdCuenta { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; } = null!;
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }

        public virtual BmCuentum IdCuentaNavigation { get; set; } = null!;
    }
}
