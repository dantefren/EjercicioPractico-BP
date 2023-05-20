using AutoMapper;
using BP.API.Entidades.Excepciones;
using BP.Comun.Extensiones;
using BP.Comun.Logs.Base.Handlers;
using BP.Functional;
using Microsoft.EntityFrameworkCore;
using WSMovimientos.Dominio.Clientes;
using WSMovimientos.Dominio.Interfaces.Propiedades;
using WSMovimientos.Entidades;
using WSMovimientos.Entidades.DTOS;
using WSMovimientos.Entidades.DTOS.Entrada;
using WSMovimientos.Entidades.Modelo;
using WSMovimientos.Repositorio.Configuraciones.Context;

namespace WSMovimientos.Repositorio.Cliente
{
    public class ClienteRepositorio : IClienteRepositorio
    {
      
        #region ReadOnly

        private readonly IPropiedadesApi _iPropiedadesApi;
        private readonly BddContext _iBddContext;
        private readonly IMapper _mapper;

        #endregion ReadOnly

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iPropiedadesApi"></param>
        /// <param name="iBddContext"></param>
        /// <param name="mapper"></param>
        public ClienteRepositorio(IPropiedadesApi iPropiedadesApi, BddContext iBddContext, IMapper mapper)
        {
            _iPropiedadesApi = iPropiedadesApi;
            _iBddContext = iBddContext;
            _mapper = mapper;

        }

        #endregion Constructor


        #region Consulta
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entradaConsultaCliente"></param>
        /// <returns></returns>
        /// <exception cref="CoreExcepcion"></exception>
        public async Task<List<EClienteConsulta>> Consultar(EEntradaConsultaCliente entradaConsultaCliente)
        {
            try
            {
                /// VALIDAR DANILO 12/05/2023    .FirstOrDefaultAsync(m => m.IdCliente == id)
                var bmCliente = await _iBddContext.BmClientes
                    .Include(b => b.IdPersonaNavigation)
                    .Where(o => o.IdCliente == (entradaConsultaCliente.IdCliente.IsNull() ? o.IdCliente : entradaConsultaCliente.IdCliente))
                    .Where(o => o.IdPersona == (entradaConsultaCliente.IdPersona.IsNull() ? o.IdPersona : entradaConsultaCliente.IdPersona))
                    .ToListAsync();
                return _mapper.Map<List<EClienteConsulta>>(bmCliente);
            }
            catch (Exception ex)
            {
                throw new CoreExcepcion(EConstantes.ValExpCodigo, this.GetFirstName(), EConstantes.movimientos, _iPropiedadesApi.BackendOpenShift(), ex);
            }
        }
        #endregion Consulta

        #region Crear
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clienteCrea"></param>
        /// <returns></returns>
        /// <exception cref="CoreExcepcion"></exception>
        [Loggable]
        public async Task<EClienteId> Crear(EClienteCrea clienteCrea)
        {
            try
            {

                var bmCliente = _mapper.Map<BmCliente>(clienteCrea);
                _iBddContext.Add(bmCliente);
                await _iBddContext.SaveChangesAsync();
                return new EClienteId
                {

                    Id = bmCliente.IdCliente

                };

            }
            catch (Exception ex)
            {

                throw new CoreExcepcion(EConstantes.ValExpCodigo, this.GetFirstName(), EConstantes.crear, _iPropiedadesApi.BackendOpenShift(), ex);
            }


        }
        #endregion Crear

        #region Actualiza

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clienteActualiza"></param>
        /// <returns></returns>
        /// <exception cref="CoreExcepcion"></exception>
        [Loggable]
        public async Task<bool> Actualizar(EClienteActualiza clienteActualiza)
        {
            try
            {
                var bmCliente = await _iBddContext.BmClientes.FirstOrDefaultAsync(item => item.IdCliente == clienteActualiza.IdCliente);
                if (bmCliente.IsNull()) return false;

                bmCliente.Contrasenia = clienteActualiza.Contrasenia;
                bmCliente.Estado = clienteActualiza.Estado;

                _iBddContext.BmClientes.Update(bmCliente);
                await _iBddContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {

                throw new CoreExcepcion(EConstantes.ValExpCodigo, this.GetFirstName(), EConstantes.actualizar, _iPropiedadesApi.BackendOpenShift(), ex);
            }
        }

        #endregion Actualiza 

        #region Elimina
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clienteElimina"></param>
        /// <returns></returns>
        /// <exception cref="CoreExcepcion"></exception>
        public async Task<bool> Eliminar(EClienteElimina clienteElimina)
        {
            try
            {
                var bmCliente = await _iBddContext.BmClientes.FirstOrDefaultAsync(item => item.IdCliente == clienteElimina.Id );
                if (bmCliente.IsNull()) return false;

                //DANILO: SE RECOMIENDA HACER SOLO ELIMINACION LOGICA 11/05/2023
                _iBddContext.BmClientes.Remove(bmCliente);
                await _iBddContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw new CoreExcepcion(EConstantes.ValExpCodigo, this.GetFirstName(), EConstantes.eliminar, _iPropiedadesApi.BackendOpenShift(), ex);
            }
        }
        #endregion Elimina
    }
}
