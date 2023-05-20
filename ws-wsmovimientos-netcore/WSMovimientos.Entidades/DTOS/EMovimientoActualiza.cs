namespace WSMovimientos.Entidades.DTOS
{
    public class EMovimientoActualiza
    {
        public long Id { get; set; } = 0;
        public string Tipo { get; set; } = null!;
        public decimal Saldo { get; set; }

    }
}