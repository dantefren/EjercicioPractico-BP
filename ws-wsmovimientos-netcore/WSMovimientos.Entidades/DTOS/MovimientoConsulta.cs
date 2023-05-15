namespace WSMovimientos.Entidades.DTOS
{
    public class MovimientoConsulta
    {
        public long Id { get; set; } = 0;
        public long IdCuenta { get; set; }
        public CuentaConsulta Cuenta { get; set; } = null;
        public DateTime Fecha { get; set; } = DateTime.Today;
        public string Tipo { get; set; } = null!;
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }

    }
}