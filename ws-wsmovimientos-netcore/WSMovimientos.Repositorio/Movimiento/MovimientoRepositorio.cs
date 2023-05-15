using AutoMapper;
using BP.API.Entidades.Excepciones;
using BP.Comun.Extensiones;
using BP.Functional;
using Microsoft.EntityFrameworkCore;
using WSMovimientos.Dominio.Interfaces.Propiedades;
using WSMovimientos.Dominio.Movimientos;
using WSMovimientos.Entidades;
using WSMovimientos.Entidades.DTOS;
using WSMovimientos.Entidades.DTOS.Entrada;
using WSMovimientos.Entidades.Modelo;
using WSMovimientos.Repositorio.Configuraciones.Context;
using BP.Comun.Logs.Base.Handlers;

namespace WSMovimientos.Repositorio.Movimiento
{
    public class MovimientoRepositorio : IMovimientoRepositorio
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
        public MovimientoRepositorio(IPropiedadesApi iPropiedadesApi, BddContext iBddContext, IMapper mapper)
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
        /// <param name="entradaConsultaMovimiento"></param>
        /// <returns></returns>
        /// <exception cref="CoreExcepcion"></exception>
        public async Task<List<MovimientoConsulta>> Consulta(EntradaConsultaMovimiento entradaConsultaMovimiento)
        {
            try
            {

                /// VALIDAR DANILO 12/05/2023    .FirstOrDefaultAsync(m => m.IdCliente == id)
                var bmMovimiento = await _iBddContext.BmMovimientos
                    .Include(b => b.IdCuentaNavigation)
                    .Where(o => o.IdMovimientos == (entradaConsultaMovimiento.IdMovimiento.IsNull() ? o.IdMovimientos : entradaConsultaMovimiento.IdMovimiento))
                    .Where(o => o.IdCuenta == (entradaConsultaMovimiento.IdCuenta.IsNull() ? o.IdCuenta : entradaConsultaMovimiento.IdCuenta))
                    .Where(o => o.Fecha >= (entradaConsultaMovimiento.FechaInicio.IsNull() ? o.Fecha : DateTime.Parse(entradaConsultaMovimiento.FechaInicio)))
                    .Where(o => o.Fecha <= (entradaConsultaMovimiento.FechaFin.IsNull() ? o.Fecha : DateTime.Parse(entradaConsultaMovimiento.FechaFin).AddDays(1)))
                    .ToListAsync();
                return _mapper.Map<List<MovimientoConsulta>>(bmMovimiento);
            }
            catch (Exception ex)
            {
                throw new CoreExcepcion(EConstantes.ValExpCodigo, this.GetFirstName(), EConstantes.movimientos, _iPropiedadesApi.BackendOpenShift(), ex);
            }
        }

        #region Consulta Ultima Transaccion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entradaConsultaMovimiento"></param>
        /// <returns></returns>
        /// <exception cref="CoreExcepcion"></exception>
        public async Task<MovimientoConsulta> ConsultaUltimaTransaccion(EntradaConsultaMovimiento entradaConsultaMovimiento)
        {
            try
            {
                var bmMovimiento = _iBddContext.BmMovimientos.Where(item => item.IdCuenta == entradaConsultaMovimiento.IdCuenta).OrderBy(x => x.Fecha).LastOrDefault();

                if (bmMovimiento.IsNull())
                {
                   var cuenta = _iBddContext.BmCuenta.Where(item => item.IdCuenta == entradaConsultaMovimiento.IdCuenta).OrderBy(x => x.IdCuenta).LastOrDefault();
                   if (!cuenta.IsNull())
                        bmMovimiento = new BmMovimiento() { IdCuentaNavigation = cuenta, Saldo = cuenta.SaldoInicial};
                }

                return _mapper.Map<MovimientoConsulta>(bmMovimiento);
            }
            catch (Exception ex)
            {
                throw new CoreExcepcion(EConstantes.ValExpCodigo, this.GetFirstName(), EConstantes.movimientos, _iPropiedadesApi.BackendOpenShift(), ex);
            }
        }
        #endregion

        #endregion Consulta

        #region Reportes

        #region Consulta Movimientos Cuenta
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entradaConsultaMovimiento"></param>
        /// <returns></returns>
        /// <exception cref="CoreExcepcion"></exception>
        public async Task<List<MovimientoCuentaConsulta>> ConsultaMovimientosCuenta(EntradaConsultaMovimientoCuenta entradaConsultaMovimiento)
        {
            try
            {
                var fechaInicio = DateTime.Parse(entradaConsultaMovimiento.FechaInicio);
                var fechafin = DateTime.Parse(entradaConsultaMovimiento.FechaFin).AddDays(1);


                var consulta = await (from _persona in _iBddContext.BmPersonas
                                      join _cuenta in _iBddContext.BmCuenta on _persona.IdPersona equals _cuenta.IdPersona
                                      join _movimientos in _iBddContext.BmMovimientos on _cuenta.IdCuenta equals _movimientos.IdCuenta
                                      where _persona.Identificacion == entradaConsultaMovimiento.Identificacion && _movimientos.Fecha >= fechaInicio && _movimientos.Fecha <= fechafin
                                      select new {
                                          _movimientos.Fecha,
                                          _persona.Nombre,
                                          _cuenta.NumeroCuenta,
                                          _cuenta.TipoCuenta,
                                          _cuenta.SaldoInicial,
                                          _cuenta.Estado,
                                          _movimientos.Tipo,
                                          _movimientos.Valor,
                                          _movimientos.Saldo
                                      }).ToListAsync();


                //DANILO: Pendiente validar el Mapero en la clase Mapper
                List<MovimientoCuentaConsulta> respuesta = new List<MovimientoCuentaConsulta>();
                
                for (int i = 0; i< consulta.Count; i++)
                {
                    respuesta.Add(new MovimientoCuentaConsulta()
                    {
                        Fecha = consulta[i].Fecha,
                        Cliente = consulta[i].Nombre,
                        NumeroCuenta = consulta[i].NumeroCuenta,
                        Tipo = consulta[i].TipoCuenta,
                        SaldoInicial = consulta[i].SaldoInicial,
                        Estado = consulta[i].Estado,
                        Movimiento = consulta[i].Valor * (consulta[i].Tipo.Equals("RET") ? -1 : 1),
                        SaldoDisponible = consulta[i].Saldo,

                    }) ;
                }

                

                return respuesta;
            }
            catch (Exception ex)
            {
                throw new CoreExcepcion(EConstantes.ValExpCodigo, this.GetFirstName(), EConstantes.movimientos, _iPropiedadesApi.BackendOpenShift(), ex);
            }
        }
        #endregion

        #endregion

        #region Crear
        /// <summary>
        /// 
        /// </summary>
        /// <param name="movimientoCrea"></param>
        /// <returns></returns>
        /// <exception cref="CoreExcepcion"></exception>
        [Loggable]
        public async Task<Entidades.DTOS.Movimiento> Crea(MovimientoCreaCompleto movimientoCrea)
        {
            try
            {

                var bmMovimiento = _mapper.Map<BmMovimiento>(movimientoCrea);
                _iBddContext.Add(bmMovimiento);
                await _iBddContext.SaveChangesAsync();

                var respuesta = _mapper.Map<Entidades.DTOS.Movimiento>(movimientoCrea);
                respuesta.Id = bmMovimiento.IdCuenta;
                respuesta.Fecha = bmMovimiento.Fecha;

                return  respuesta;
                  
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
        /// <param name="movimientoActualiza"></param>
        /// <returns></returns>
        /// <exception cref="CoreExcepcion"></exception>
        [Loggable]
        public async Task<bool> Actualiza(MovimientoActualiza movimientoActualiza)
        {
            try
            {
                var bmMovimiento = await _iBddContext.BmMovimientos.FirstOrDefaultAsync(item => item.IdMovimientos == movimientoActualiza.Id);
                if (bmMovimiento.IsNull()) return false;

                //DANILO 13 May 2023: No de deberia poder modificar la información de Movimientos - Información Transaccional
                bmMovimiento.Tipo = movimientoActualiza.Tipo;
                bmMovimiento.Saldo = movimientoActualiza.Saldo;

                _iBddContext.BmMovimientos.Update(bmMovimiento);
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
        /// <param name="movimientoElimina"></param>
        /// <returns></returns>
        /// <exception cref="CoreExcepcion"></exception>
        public async Task<bool> Elimina(MovimientoElimina movimientoElimina)
        {
            try
            {
                var bmMovimiento = await _iBddContext.BmMovimientos.FirstOrDefaultAsync(item => item.IdMovimientos == movimientoElimina.Id );
                if (bmMovimiento.IsNull()) return false;

                //DANILO 13 May 2023: No de deberia poder Eliminar la información de Movimientos - Información Transaccional
                _iBddContext.BmMovimientos.Remove(bmMovimiento);
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
