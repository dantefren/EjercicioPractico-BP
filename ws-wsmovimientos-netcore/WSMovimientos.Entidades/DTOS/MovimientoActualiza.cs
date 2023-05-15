namespace WSMovimientos.Entidades.DTOS
{
    public class MovimientoActualiza
    {
        public long Id { get; set; } = 0;
        public string Tipo { get; set; } = null!;
        public decimal Saldo { get; set; }

    }
}