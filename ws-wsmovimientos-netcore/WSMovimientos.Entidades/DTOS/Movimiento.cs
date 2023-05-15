namespace WSMovimientos.Entidades.DTOS
{
    public class Movimiento
    {
        public long Id { get; set; } = 0;
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; } = null!;
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
    }
}