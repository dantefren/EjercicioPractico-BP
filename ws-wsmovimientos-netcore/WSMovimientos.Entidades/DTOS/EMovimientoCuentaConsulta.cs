namespace WSMovimientos.Entidades.DTOS
{
    public class EMovimientoCuentaConsulta
    {
        public DateTime Fecha { get; set; } = DateTime.Today;
        public string Cliente { get; set; } = null!;
        public int NumeroCuenta { get; set; }
        public string Tipo { get; set; } = null!;
        public decimal SaldoInicial { get; set; }
        public bool? Estado { get; set; }
        public decimal Movimiento { get; set; }
        public decimal SaldoDisponible { get; set; }

    }
}