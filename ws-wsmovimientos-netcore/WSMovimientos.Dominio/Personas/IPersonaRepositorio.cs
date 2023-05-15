#region Using

using WSMovimientos.Entidades.DTOS;
using WSMovimientos.Entidades.DTOS.Entrada;

#endregion Using

namespace WSMovimientos.Dominio.Personas
{
    public interface IPersonaRepositorio
    {
        Task<List<PersonaConsulta>> Consulta(EntradaConsultaPersona entradaConsultaPersona);
        Task<PersonaId> CrearAsync(PersonaCrea personaCrea);
        Task<bool> ActualizarAsync(PersonaActualiza personaActualiza);
        Task<bool> EliminarAsync(PersonaElimina personaElimina);

    }
}