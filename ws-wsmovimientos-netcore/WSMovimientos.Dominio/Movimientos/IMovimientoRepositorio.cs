#region Using

using WSMovimientos.Entidades.DTOS;
using WSMovimientos.Entidades.DTOS.Entrada;

#endregion Using

namespace WSMovimientos.Dominio.Movimientos
{
    public interface IMovimientoRepositorio
    {
        Task<List<MovimientoConsulta>> Consulta(EntradaConsultaMovimiento entradaConsultaMovimiento);
        Task<MovimientoConsulta> ConsultaUltimaTransaccion(EntradaConsultaMovimiento entradaConsultaMovimiento);
        Task<List<MovimientoCuentaConsulta>> ConsultaMovimientosCuenta(EntradaConsultaMovimientoCuenta entradaConsultaMovimiento);

        Task<Movimiento> Crea(MovimientoCreaCompleto movimientoCrea);
        Task<bool> Actualiza(MovimientoActualiza movimientoActualiza);
        Task<bool> Elimina(MovimientoElimina movimientoElimina);

    }
}