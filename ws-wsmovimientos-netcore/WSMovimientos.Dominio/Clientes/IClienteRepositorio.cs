#region Using

using WSMovimientos.Entidades.DTOS;
using WSMovimientos.Entidades.DTOS.Entrada;

#endregion Using

namespace WSMovimientos.Dominio.Clientes
{
    public interface IClienteRepositorio
    {
        Task<List<EClienteConsulta>> Consultar(EEntradaConsultaCliente entradaConsultaCliente);
        Task<EClienteId> Crear(EClienteCrea clienteCrea);
        Task<bool> Actualizar(EClienteActualiza clienteActualiza);
        Task<bool> Eliminar(EClienteElimina clienteElimina);

    }
}