#region Using

using BP.API.Entidades;
using WSMovimientos.Entidades.DTOS.Entrada;
using WSMovimientos.Entidades.DTOS.Salida;

#endregion Using

namespace WSMovimientos.Dominio.Personas
{
    public interface IPersonaInfraestructura
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        Task<ERespuesta<ESalidaConsultaPersonas>> Consultar(EEntrada<EEntradaConsultaPersona> entrada);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        Task<ERespuesta<ESalidaCreaPersona>> Crear(EEntrada<EEntradaCreaPersona> entrada);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        Task<ERespuestaSimple> Actualizar(EEntrada<EEntradaActualizaPersona> entrada);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        Task<ERespuestaSimple> Eliminar(EEntrada<EEntradaEliminaPersona> entrada);
    }
}