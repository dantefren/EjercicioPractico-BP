#region Using

using WSMovimientos.Entidades.DTOS;
using WSMovimientos.Entidades.DTOS.Entrada;

#endregion Using

namespace WSMovimientos.Dominio.Cuentas
{
    public interface ICuentaRepositorio
    {
        Task<List<ECuentaConsulta>> Consultar(EEntradaConsultaCuenta entradaConsultaCuenta);
        Task<ECuentaId> Crear(ECuentaCrea cuentaCrea);
        Task<bool> Actualizar(ECuentaActualiza cuentaActualiza);
        Task<bool> Eliminar(ECuentaElimina cuentaElimina);

    }
}