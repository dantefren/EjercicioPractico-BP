#region Using

using WSMovimientos.Entidades.DTOS;
using WSMovimientos.Entidades.DTOS.Entrada;

#endregion Using

namespace WSMovimientos.Dominio.Movimientos
{
    public interface IMovimientoRepositorio
    {
        Task<List<EMovimientoConsulta>> Consultar(EEntradaConsultaMovimiento entradaConsultaMovimiento);
        Task<EMovimientoConsulta> ConsultarUltimaTransaccion(EEntradaConsultaMovimiento entradaConsultaMovimiento);
        Task<List<EMovimientoCuentaConsulta>> ConsultarMovimientosCuenta(EEntradaConsultaMovimientoCuenta entradaConsultaMovimiento);

        Task<EMovimiento> Crear(EMovimientoCreaCompleto movimientoCrea);
        Task<bool> Actualizar(EMovimientoActualiza movimientoActualiza);
        Task<bool> Eliminar(EMovimientoElimina movimientoElimina);

    }
}