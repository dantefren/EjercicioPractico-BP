#region Using 

using BP.API.Entidades;
using BP.API.Entidades.Excepciones;
using BP.Comun.Extensiones;
using BP.Comun.Logs.Base.Handlers;
using FluentValidation;
using System.Reflection;
using WSMovimientos.Dominio.Cuentas;
using WSMovimientos.Dominio.Interfaces.Propiedades;
using WSMovimientos.Entidades;
using WSMovimientos.Entidades.DTOS;
using WSMovimientos.Entidades.DTOS.Entrada;
using WSMovimientos.Entidades.DTOS.Salida;

#endregion Using

namespace WSMovimientos.Infraestructura.Cuentas
{
    public class CuentaInfraestructura : ICuentaInfraestructura
    {
        #region ReadOnly

        private readonly IPropiedadesApi _iPropiedadesApi;
        private readonly ICuentaRepositorio _cuentaRepositorio;
        private readonly IValidator<EEntrada<EEntradaConsultaCuenta>> _validatorEntradaConsulta;
        private readonly IValidator<EEntrada<EEntradaCreaCuenta>> _validatorEntradaCrea;
        private readonly IValidator<EEntrada<EEntradaActualizaCuenta>> _validatorEntradaActualiza;
        private readonly IValidator<EEntrada<EEntradaEliminaCuenta>> _validatorEntradaElimina;

        #endregion ReadOnly

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iPropiedadesApi"></param>
        /// <param name="cuentaRepositorio"></param>
        /// <param name="validatorEntradaConsulta"></param>
        /// <param name="validatorEntradaCrea"></param>
        /// <param name="validatorEntradaActualiza"></param>
        /// <param name="validatorEntradaElimina"></param>
        public CuentaInfraestructura
            (
            IPropiedadesApi iPropiedadesApi,
            ICuentaRepositorio cuentaRepositorio,
            IValidator<EEntrada<EEntradaConsultaCuenta>> validatorEntradaConsulta,
            IValidator<EEntrada<EEntradaCreaCuenta>> validatorEntradaCrea,
            IValidator<EEntrada<EEntradaActualizaCuenta>> validatorEntradaActualiza,
            IValidator<EEntrada<EEntradaEliminaCuenta>> validatorEntradaElimina
            )

        {
            _iPropiedadesApi = iPropiedadesApi;
            _cuentaRepositorio = cuentaRepositorio;
            _validatorEntradaConsulta = validatorEntradaConsulta;
            _validatorEntradaCrea = validatorEntradaCrea;
            _validatorEntradaActualiza = validatorEntradaActualiza;
            _validatorEntradaElimina = validatorEntradaElimina;

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
        public async Task<ERespuesta<ESalidaConsultaCuenta>> Consultar(EEntrada<EEntradaConsultaCuenta> entrada)
        {
            List<ECuentaConsulta> resultadoConsulta = new List<ECuentaConsulta>();

            var result = _validatorEntradaConsulta.Validate(entrada);
            if (!result.IsValid)
            {
                var falla = result.Errors.First();
                throw new CoreNegocioError(falla.ErrorCode, falla.ErrorMessage, this.GetFirstName(), MethodBase.GetCurrentMethod()?.DeclaringType?.Name, _iPropiedadesApi.BackendOpenShift());
            }


            resultadoConsulta = await _cuentaRepositorio.Consultar(entrada.BodyIn);


            if (resultadoConsulta.IsNull() || resultadoConsulta.Count < 1)
                throw new CoreNegocioError(EConstantes.ErrorCode4, EConstantes.ErrorCode4Descripcion, this.GetFirstName(), EConstantes.movimientos, _iPropiedadesApi.BackendOpenShift());

            return new ERespuesta<ESalidaConsultaCuenta>()
            {
                HeaderOut = entrada.HeaderIn,
                BodyOut = new ESalidaConsultaCuenta()
                {
                    Cuentas = resultadoConsulta
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
        public async Task<ERespuesta<ESalidaCreaCuenta>> Crear(EEntrada<EEntradaCreaCuenta> entrada)
        {
            var result = _validatorEntradaCrea.Validate(entrada);
            if (!result.IsValid)
            {
                var falla = result.Errors.First();
                throw new CoreNegocioError(falla.ErrorCode, falla.ErrorMessage, this.GetFirstName(), MethodBase.GetCurrentMethod()?.DeclaringType?.Name, _iPropiedadesApi.BackendOpenShift());
            }

            var resultadoCrea = await _cuentaRepositorio.Crear(entrada.BodyIn.Cuenta);

            if (resultadoCrea.IsNull() || resultadoCrea.Id < 1) throw new CoreNegocioError(EConstantes.ErrorCrearCode, EConstantes.ErrorCrearDescripcion, this.GetFirstName(), EConstantes.crear, _iPropiedadesApi.BackendOpenShift());

            return new ERespuesta<ESalidaCreaCuenta>()
            {
                HeaderOut = entrada.HeaderIn,
                BodyOut = new ESalidaCreaCuenta()
                {
                    Cuenta = resultadoCrea
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
        public async Task<ERespuestaSimple> Actualizar(EEntrada<EEntradaActualizaCuenta> entrada)
        {
            var result = _validatorEntradaActualiza.Validate(entrada);
            if (!result.IsValid)
            {
                var falla = result.Errors.First();
                throw new CoreNegocioError(falla.ErrorCode, falla.ErrorMessage, this.GetFirstName(), MethodBase.GetCurrentMethod()?.DeclaringType?.Name, _iPropiedadesApi.BackendOpenShift());
            }


            if (!(await _cuentaRepositorio.Actualizar(entrada.BodyIn.Cuenta)))
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
        public async Task<ERespuestaSimple> Eliminar(EEntrada<EEntradaEliminaCuenta> entrada)
        {
            var result = _validatorEntradaElimina.Validate(entrada);
            if (!result.IsValid)
            {
                var falla = result.Errors.First();
                throw new CoreNegocioError(falla.ErrorCode, falla.ErrorMessage, this.GetFirstName(), MethodBase.GetCurrentMethod()?.DeclaringType?.Name, _iPropiedadesApi.BackendOpenShift());
            }


            if (!(await _cuentaRepositorio.Eliminar(entrada.BodyIn.Cuenta)))
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