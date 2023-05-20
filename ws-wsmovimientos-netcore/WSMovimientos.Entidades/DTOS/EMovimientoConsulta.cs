namespace WSMovimientos.Entidades.DTOS
{
    public class EMovimientoConsulta
    {
        public long Id { get; set; } = 0;
        public long IdCuenta { get; set; }
        public ECuentaConsulta Cuenta { get; set; } = null;
        public DateTime Fecha { get; set; } = DateTime.Today;
        public string Tipo { get; set; } = null!;
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }

    }
}