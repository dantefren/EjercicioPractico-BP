#region Using

using WSMovimientos.Entidades.DTOS;
using WSMovimientos.Entidades.DTOS.Entrada;

#endregion Using

namespace WSMovimientos.Dominio.Cuentas
{
    public interface ICuentaRepositorio
    {
        Task<List<CuentaConsulta>> Consulta(EntradaConsultaCuenta entradaConsultaCuenta);
        Task<CuentaId> Crea(CuentaCrea cuentaCrea);
        Task<bool> Actualiza(CuentaActualiza cuentaActualiza);
        Task<bool> Elimina(CuentaElimina cuentaElimina);

    }
}