namespace WSMovimientos.Entidades.DTOS
{
    public class MovimientoCreaCompleto
    {
        public long IdCuenta { get; set; }
        public string Tipo { get; set; } = null!;
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }

    }
}