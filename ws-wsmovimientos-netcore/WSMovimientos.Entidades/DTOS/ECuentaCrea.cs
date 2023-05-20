
namespace WSMovimientos.Entidades.DTOS
{
    public class ECuentaCrea
    {
        public long IdPersona { get; set; } = 0;
        public int NumeroCuenta { get; set; } = 0;
        public string TipoCuenta { get; set; } = null!;
        public decimal SaldoInicial { get; set; }

    }
}