namespace WSMovimientos.Entidades.DTOS
{
    public class ECuenta
    {
        public long IdCuenta { get; set; } = 0;
        public decimal SaldoInicial { get; set; }
        public bool? Estado { get; set; } = null;
    }
}