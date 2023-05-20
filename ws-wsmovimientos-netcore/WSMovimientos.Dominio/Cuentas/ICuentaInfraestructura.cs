#region Using

using BP.API.Entidades;
using WSMovimientos.Entidades.DTOS.Entrada;
using WSMovimientos.Entidades.DTOS.Salida;

#endregion Using

namespace WSMovimientos.Dominio.Cuentas
{
    public interface ICuentaInfraestructura
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        Task<ERespuesta<ESalidaConsultaCuenta>> Consultar(EEntrada<EEntradaConsultaCuenta> entrada);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        Task<ERespuesta<ESalidaCreaCuenta>> Crear(EEntrada<EEntradaCreaCuenta> entrada);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        Task<ERespuestaSimple> Actualizar(EEntrada<EEntradaActualizaCuenta> entrada);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        Task<ERespuestaSimple> Eliminar(EEntrada<EEntradaEliminaCuenta> entrada);

    }
}