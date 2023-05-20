namespace WSMovimientos.Entidades.DTOS
{
    public class EMovimientoCreaCompleto
    {
        public long IdCuenta { get; set; }
        public string Tipo { get; set; } = null!;
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }

    }
}