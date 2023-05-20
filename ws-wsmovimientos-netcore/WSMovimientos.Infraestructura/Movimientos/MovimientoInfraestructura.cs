#region Using 

using BP.API.Entidades;
using BP.API.Entidades.Excepciones;
using BP.Comun.Extensiones;
using BP.Comun.Logs.Base.Handlers;
using FluentValidation;
using System.Reflection;
using WSMovimientos.Dominio.Interfaces.Propiedades;
using WSMovimientos.Dominio.Movimientos;
using WSMovimientos.Entidades;
using WSMovimientos.Entidades.DTOS;
using WSMovimientos.Entidades.DTOS.Entrada;
using WSMovimientos.Entidades.DTOS.Salida;

#endregion Using

namespace WSMovimientos.Infraestructura.Movimientos
{
    public class MovimientoInfraestructura : IMovimientoInfraestructura
    {
        #region ReadOnly

        private readonly IPropiedadesApi _iPropiedadesApi;
        private readonly IMovimientoRepositorio _movimientoRepositorio;
        private readonly IValidator<EEntrada<EEntradaConsultaMovimiento>> _validatorEntradaConsulta;
        private readonly IValidator<EEntrada<EEntradaCreaMovimiento>> _validatorEntradaCrea;
        private readonly IValidator<EEntrada<EEntradaActualizaMovimiento>> _validatorEntradaActualiza;
        private readonly IValidator<EEntrada<EEntradaEliminaMovimiento>> _validatorEntradaElimina;

        private readonly IValidator<EEntrada<EEntradaConsultaMovimientoCuenta>> _validatorEntradaConsultaMovimiento;


        #endregion ReadOnly

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iPropiedadesApi"></param>
        /// <param name="movimientoRepositorio"></param>
        /// <param name="validatorEntradaMovimiento"></param>
        /// <param name="validatorEntradaCrea"></param>
        /// <param name="validatorEntradaActualiza"></param>
        /// <param name="validatorEntradaElimina"></param>
        public MovimientoInfraestructura
            (
            IPropiedadesApi iPropiedadesApi,
            IMovimientoRepositorio movimientoRepositorio,

            IValidator<EEntrada<EEntradaConsultaMovimiento>> validatorEntradaMovimiento,
            IValidator<EEntrada<EEntradaCreaMovimiento>> validatorEntradaCrea,
            IValidator<EEntrada<EEntradaActualizaMovimiento>> validatorEntradaActualiza,
            IValidator<EEntrada<EEntradaEliminaMovimiento>> validatorEntradaElimina,
            IValidator<EEntrada<EEntradaConsultaMovimientoCuenta>> validatorEntradaMovimientoCuenta
            

            )

        {
            _iPropiedadesApi = iPropiedadesApi;
            _movimientoRepositorio = movimientoRepositorio;
            _validatorEntradaConsulta = validatorEntradaMovimiento;
            _validatorEntradaCrea = validatorEntradaCrea;
            _validatorEntradaActualiza = validatorEntradaActualiza;
            _validatorEntradaElimina = validatorEntradaElimina;
            _validatorEntradaConsultaMovimiento = validatorEntradaMovimientoCuenta;
        }



        #endregion Constructor

        #region Methods

        #region CONSULTA
            /// <summary>
            /// 
            /// </summary>
            /// <param name="entrada"></param>
            /// <returns></returns>
            /// <exception cref="CoreNegocioError"></exception>
            [Loggable]
        public async Task<ERespuesta<ESalidaConsultaMovimiento>> Consultar(EEntrada<EEntradaConsultaMovimiento> entrada)
        {
            List<EMovimientoConsulta> resultadoConsulta = new List<EMovimientoConsulta>();

            var result = _validatorEntradaConsulta.Validate(entrada);
            if (!result.IsValid)
            {
                var falla = result.Errors.First();
                throw new CoreNegocioError(falla.ErrorCode, falla.ErrorMessage, this.GetFirstName(), MethodBase.GetCurrentMethod()?.DeclaringType?.Name, _iPropiedadesApi.BackendOpenShift());
            }


            resultadoConsulta = await _movimientoRepositorio.Consultar(entrada.BodyIn);


            if (resultadoConsulta.IsNull() || resultadoConsulta.Count < 1)
                throw new CoreNegocioError(EConstantes.ErrorCode4, EConstantes.ErrorCode4Descripcion, this.GetFirstName(), EConstantes.movimientos, _iPropiedadesApi.BackendOpenShift());

            return new ERespuesta<ESalidaConsultaMovimiento>()
            {
                HeaderOut = entrada.HeaderIn,
                BodyOut = new ESalidaConsultaMovimiento()
                {
                    Movimientos = resultadoConsulta
                },
                Error = new EError(this.GetFirstName(), EConstantes.movimientos, _iPropiedadesApi.BackendOpenShift()) { MensajeNegocio = EConstantes.OKGenericoDescripcion }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        /// <exception cref="CoreNegocioError"></exception>
        [Loggable]
        public async Task<ERespuesta<ESalidaConsultaMovimientoCuenta>> ConsultarMovimientosCuenta(EEntrada<EEntradaConsultaMovimientoCuenta> entrada)
        {
            List<EMovimientoCuentaConsulta> resultadoConsulta = new List<EMovimientoCuentaConsulta>();

            var result = _validatorEntradaConsultaMovimiento.Validate(entrada);
            if (!result.IsValid)
            {
                var falla = result.Errors.First();
                throw new CoreNegocioError(falla.ErrorCode, falla.ErrorMessage, this.GetFirstName(), MethodBase.GetCurrentMethod()?.DeclaringType?.Name, _iPropiedadesApi.BackendOpenShift());
            }


            resultadoConsulta = await _movimientoRepositorio.ConsultarMovimientosCuenta(entrada.BodyIn);


            if (resultadoConsulta.IsNull() || resultadoConsulta.Count < 1)
                throw new CoreNegocioError(EConstantes.ErrorCode4, EConstantes.ErrorCode4Descripcion, this.GetFirstName(), EConstantes.movimientos, _iPropiedadesApi.BackendOpenShift());

            return new ERespuesta<ESalidaConsultaMovimientoCuenta>()
            {
                HeaderOut = entrada.HeaderIn,
                BodyOut = new ESalidaConsultaMovimientoCuenta()
                {
                    Movimientos = resultadoConsulta
                },
                Error = new EError(this.GetFirstName(), EConstantes.movimientos, _iPropiedadesApi.BackendOpenShift()) { MensajeNegocio = EConstantes.OKGenericoDescripcion }
            };
        }





        #endregion CONSULTA

        #region Creacion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        /// <exception cref="CoreNegocioError"></exception>
        [Loggable]
        public async Task<ERespuesta<ESalidaCreaMovimiento>> Crear(EEntrada<EEntradaCreaMovimiento> entrada)
        {
            var result = _validatorEntradaCrea.Validate(entrada);
            if (!result.IsValid)
            {
                var falla = result.Errors.First();
                throw new CoreNegocioError(falla.ErrorCode, falla.ErrorMessage, this.GetFirstName(), MethodBase.GetCurrentMethod()?.DeclaringType?.Name, _iPropiedadesApi.BackendOpenShift());
            }

            var idCuenta = entrada.BodyIn.Movimiento.IdCuenta;
            var valorTransaccion = entrada.BodyIn.Movimiento.Valor;
            var tipoTransaccion = entrada.BodyIn.Movimiento.Tipo;
            decimal? saldo = 0;
            var movimientoCreaCompleto = new EMovimientoCreaCompleto();

            #region Consulta Saldo Ultima Tansaccion
            var entradaConsulta = new EEntradaConsultaMovimiento();
            entradaConsulta.IdCuenta = idCuenta;
            var untimaTransaccion = await _movimientoRepositorio.ConsultarUltimaTransaccion(entradaConsulta);

            if (untimaTransaccion.IsNull())
                throw new CoreNegocioError(EConstantes.ErrorCuentaCodigo, EConstantes.ErrorCuenta, this.GetFirstName(), EConstantes.crear, _iPropiedadesApi.BackendOpenShift());

            saldo = untimaTransaccion.Saldo;
            #endregion

            #region Calculo Transaccion
            movimientoCreaCompleto.IdCuenta = idCuenta;
            movimientoCreaCompleto.Tipo = tipoTransaccion;
            movimientoCreaCompleto.Valor = valorTransaccion;
            movimientoCreaCompleto.Saldo = saldo.GetValueOrDefault() + (tipoTransaccion.Equals("RET") ? (valorTransaccion * -1) : valorTransaccion);
    
            #endregion

            #region Validacion Transaccion
            if (movimientoCreaCompleto.Saldo < 0) throw new CoreNegocioError(EConstantes.ErrorSaldoCodigo, EConstantes.ErrorSaldo, this.GetFirstName(), EConstantes.crear, _iPropiedadesApi.BackendOpenShift());
            #endregion



            var resultadoCrea = await _movimientoRepositorio.Crear(movimientoCreaCompleto);

            if (resultadoCrea.IsNull() || resultadoCrea.Id < 1) throw new CoreNegocioError(EConstantes.ErrorCrearCode, EConstantes.ErrorCrearDescripcion, this.GetFirstName(), EConstantes.crear, _iPropiedadesApi.BackendOpenShift());

            return new ERespuesta<ESalidaCreaMovimiento>()
            {
                HeaderOut = entrada.HeaderIn,
                BodyOut = new ESalidaCreaMovimiento()
                {
                    Movimiento = resultadoCrea
                },
                Error = new EError(this.GetFirstName(), EConstantes.crear, _iPropiedadesApi.BackendOpenShift()) { MensajeNegocio = EConstantes.OKGenericoDescripcion }
            };
        }

        #endregion Creacion

        #region Actualizacion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        /// <exception cref="CoreNegocioError"></exception>
        [Loggable]
        public async Task<ERespuestaSimple> Actualizar(EEntrada<EEntradaActualizaMovimiento> entrada)
        {
            var result = _validatorEntradaActualiza.Validate(entrada);
            if (!result.IsValid)
            {
                var falla = result.Errors.First();
                throw new CoreNegocioError(falla.ErrorCode, falla.ErrorMessage, this.GetFirstName(), MethodBase.GetCurrentMethod()?.DeclaringType?.Name, _iPropiedadesApi.BackendOpenShift());
            }


            if (!(await _movimientoRepositorio.Actualizar(entrada.BodyIn.Movimiento)))
                throw new CoreNegocioError(EConstantes.ErrorActualizarCode, EConstantes.ErrorActualizarDescripcion, this.GetFirstName(), EConstantes.actualizar, _iPropiedadesApi.BackendOpenShift());

            return new ERespuestaSimple()
            {
                HeaderOut = entrada.HeaderIn,


                Error = new EError(this.GetFirstName(), EConstantes.actualizar, _iPropiedadesApi.BackendOpenShift()) { MensajeNegocio = EConstantes.OKGenericoDescripcion }
            };
        }

        #endregion Actualizacion

        #region Elimina

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        /// <exception cref="CoreNegocioError"></exception>
        public async Task<ERespuestaSimple> Eliminar(EEntrada<EEntradaEliminaMovimiento> entrada)
        {
            var result = _validatorEntradaElimina.Validate(entrada);
            if (!result.IsValid)
            {
                var falla = result.Errors.First();
                throw new CoreNegocioError(falla.ErrorCode, falla.ErrorMessage, this.GetFirstName(), MethodBase.GetCurrentMethod()?.DeclaringType?.Name, _iPropiedadesApi.BackendOpenShift());
            }


            if (!(await _movimientoRepositorio.Eliminar(entrada.BodyIn.Movimiento)))
                throw new CoreNegocioError(EConstantes.ErrorEliminarCode, EConstantes.ErrorEliminarDescripcion, this.GetFirstName(), EConstantes.eliminar, _iPropiedadesApi.BackendOpenShift());

            return new ERespuestaSimple()
            {
                HeaderOut = entrada.HeaderIn,


                Error = new EError(this.GetFirstName(), EConstantes.eliminar, _iPropiedadesApi.BackendOpenShift()) { MensajeNegocio = EConstantes.OKGenericoDescripcion }
            };
        }

        #endregion Elimina

        #endregion Methods
    }

}