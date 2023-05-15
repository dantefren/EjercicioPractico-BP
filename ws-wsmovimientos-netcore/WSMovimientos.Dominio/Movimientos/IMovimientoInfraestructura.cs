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
        Task<ERespuesta<SalidaConsultaMovimiento>> Consulta(EEntrada<EntradaConsultaMovimiento> entrada);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        Task<ERespuesta<SalidaConsultaMovimientoCuenta>> ConsultaMovimientosCuenta(EEntrada<EntradaConsultaMovimientoCuenta> entrada);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        Task<ERespuesta<SalidaCreaMovimiento>> Crea(EEntrada<EntradaCreaMovimiento> entrada);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        Task<ERespuestaSimple> Actualiza(EEntrada<EntradaActualizaMovimiento> entrada);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        Task<ERespuestaSimple> Elimina(EEntrada<EntradaEliminaMovimiento> entrada);

    }
}