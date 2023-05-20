#region Using

using WSMovimientos.Entidades.DTOS;
using WSMovimientos.Entidades.DTOS.Entrada;

#endregion Using

namespace WSMovimientos.Dominio.Personas
{
    public interface IPersonaRepositorio
    {
        Task<List<EPersonaConsulta>> Consultar(EEntradaConsultaPersona entradaConsultaPersona);
        Task<EPersonaId> Crear(EPersonaCrea personaCrea);
        Task<bool> Actualizar(EPersonaActualiza personaActualiza);
        Task<bool> Eliminar(EPersonaElimina personaElimina);

    }
}