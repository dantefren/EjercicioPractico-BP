namespace WSMovimientos.Entidades.DTOS
{
    public class ECuentaConsulta
    {
        public long Id { get; set; } = 0;
        public long IdPersona { get; set; } = 0;
        public EPersonaConsulta Persona { get; set; } = null;
        public int NumeroCuenta { get; set; } = 0;
        public string TipoCuenta { get; set; } = null!;
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }

    }
}