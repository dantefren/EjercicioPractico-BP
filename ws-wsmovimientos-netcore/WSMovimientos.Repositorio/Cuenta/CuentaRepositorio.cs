using AutoMapper;
using BP.API.Entidades.Excepciones;
using BP.Comun.Extensiones;
using BP.Comun.Logs.Base.Handlers;
using BP.Functional;
using Microsoft.EntityFrameworkCore;
using WSMovimientos.Dominio.Cuentas;
using WSMovimientos.Dominio.Interfaces.Propiedades;
using WSMovimientos.Entidades;
using WSMovimientos.Entidades.DTOS;
using WSMovimientos.Entidades.DTOS.Entrada;
using WSMovimientos.Entidades.Modelo;
using WSMovimientos.Repositorio.Configuraciones.Context;

namespace WSMovimientos.Repositorio.Cuenta
{
    public class CuentaRepositorio : ICuentaRepositorio
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
        public CuentaRepositorio(IPropiedadesApi iPropiedadesApi, BddContext iBddContext, IMapper mapper)
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
        /// <param name="entradaConsultaCuenta"></param>
        /// <returns></returns>
        /// <exception cref="CoreExcepcion"></exception>
        public async Task<List<ECuentaConsulta>> Consultar(EEntradaConsultaCuenta entradaConsultaCuenta)
        {
            try
            {
                /// VALIDAR DANILO 12/05/2023    .FirstOrDefaultAsync(m => m.IdCliente == id)
                var bmCuenta = await _iBddContext.BmCuenta
                    .Include(b => b.IdPersonaNavigation)
                    .Where(o => o.IdCuenta == (entradaConsultaCuenta.IdCuenta.IsNull() ? o.IdCuenta : entradaConsultaCuenta.IdCuenta))
                    .Where(o => o.IdPersona == (entradaConsultaCuenta.IdPersona.IsNull() ? o.IdPersona : entradaConsultaCuenta.IdPersona))
                    .ToListAsync();
                return _mapper.Map<List<ECuentaConsulta>>(bmCuenta);
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
        /// <param name="cuentaCrea"></param>
        /// <returns></returns>
        /// <exception cref="CoreExcepcion"></exception>
        [Loggable]
        public async Task<ECuentaId> Crear(ECuentaCrea cuentaCrea)
        {
            try
            {

                var bmCuenta = _mapper.Map<BmCuentum>(cuentaCrea);
                _iBddContext.Add(bmCuenta);
                await _iBddContext.SaveChangesAsync();
                return new ECuentaId
                {

                    Id = bmCuenta.IdCuenta

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
        /// <param name="cuentaActualiza"></param>
        /// <returns></returns>
        /// <exception cref="CoreExcepcion"></exception>
        [Loggable]
        public async Task<bool> Actualizar(ECuentaActualiza cuentaActualiza)
        {
            try
            {
                var bmCuenta = await _iBddContext.BmCuenta.FirstOrDefaultAsync(item => item.IdCuenta == cuentaActualiza.IdCuenta);
                if (bmCuenta.IsNull()) return false;

                bmCuenta.Estado = cuentaActualiza.Estado;

                _iBddContext.BmCuenta.Update(bmCuenta);
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
        /// <param name="cuentaElimina"></param>
        /// <returns></returns>
        /// <exception cref="CoreExcepcion"></exception>
        public async Task<bool> Eliminar(ECuentaElimina cuentaElimina)
        {
            try
            {
                var bmCuenta = await _iBddContext.BmCuenta.FirstOrDefaultAsync(item => item.IdCuenta == cuentaElimina.Id );
                if (bmCuenta.IsNull()) return false;

                //DANILO: SE RECOMIENDA HACER SOLO ELIMINACION LOGICA 11/05/2023
                _iBddContext.BmCuenta.Remove(bmCuenta);
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
