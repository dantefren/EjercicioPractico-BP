#region Using

using WSMovimientos.Entidades.DTOS;
using WSMovimientos.Entidades.DTOS.Entrada;

#endregion Using

namespace WSMovimientos.Dominio.Clientes
{
    public interface IClienteRepositorio
    {
        Task<List<ClienteConsulta>> Consulta(EntradaConsultaCliente entradaConsultaCliente);
        Task<ClienteId> Crea(ClienteCrea clienteCrea);
        Task<bool> Actualiza(ClienteActualiza clienteActualiza);
        Task<bool> Elimina(ClienteElimina clienteElimina);

    }
}