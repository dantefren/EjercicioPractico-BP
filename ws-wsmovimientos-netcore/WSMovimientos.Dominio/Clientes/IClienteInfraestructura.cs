#region Using

using BP.API.Entidades;
using WSMovimientos.Entidades.DTOS.Entrada;
using WSMovimientos.Entidades.DTOS.Salida;

#endregion Using

namespace WSMovimientos.Dominio.Clientes
{
    public interface IClientesInfraestructura
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        Task<ERespuesta<ESalidaConsultaClientes>> Consultar(EEntrada<EEntradaConsultaCliente> entrada);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        Task<ERespuesta<ESalidaCreaCliente>> Crear(EEntrada<EEntradaCreaCliente> entrada);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        Task<ERespuestaSimple> Actualizar(EEntrada<EEntradaActualizaCliente> entrada);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        Task<ERespuestaSimple> Eliminar(EEntrada<EEntradaEliminaCliente> entrada);

    }
}