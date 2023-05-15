namespace WSMovimientos.Entidades.DTOS
{
    public class CuentaConsulta
    {
        public long Id { get; set; } = 0;
        public long IdPersona { get; set; } = 0;
        public PersonaConsulta Persona { get; set; } = null;
        public int NumeroCuenta { get; set; } = 0;
        public string TipoCuenta { get; set; } = null!;
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }

    }
}