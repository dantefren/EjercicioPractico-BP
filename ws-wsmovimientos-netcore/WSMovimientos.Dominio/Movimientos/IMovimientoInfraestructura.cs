#region Using

using BP.API.Entidades;
using WSMovimientos.Entidades.DTOS.Entrada;
using WSMovimientos.Entidades.DTOS.Salida;

#endregion Using

namespace WSMovimientos.Dominio.Movimientos
{
    public interface IMovimientoInfraestructura
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        Task<ERespuesta<ESalidaConsultaMovimiento>> Consultar(EEntrada<EEntradaConsultaMovimiento> entrada);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        Task<ERespuesta<ESalidaConsultaMovimientoCuenta>> ConsultarMovimientosCuenta(EEntrada<EEntradaConsultaMovimientoCuenta> entrada);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        Task<ERespuesta<ESalidaCreaMovimiento>> Crear(EEntrada<EEntradaCreaMovimiento> entrada);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        Task<ERespuestaSimple> Actualizar(EEntrada<EEntradaActualizaMovimiento> entrada);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        Task<ERespuestaSimple> Eliminar(EEntrada<EEntradaEliminaMovimiento> entrada);

    }
}