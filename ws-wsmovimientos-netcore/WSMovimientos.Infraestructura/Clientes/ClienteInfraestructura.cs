#region Using 

using BP.API.Entidades;
using BP.API.Entidades.Excepciones;
using BP.Comun.Extensiones;
using BP.Comun.Logs.Base.Handlers;
using FluentValidation;
using System.Reflection;
using WSMovimientos.Dominio.Clientes;
using WSMovimientos.Dominio.Interfaces.Propiedades;
using WSMovimientos.Entidades;
using WSMovimientos.Entidades.DTOS;
using WSMovimientos.Entidades.DTOS.Entrada;
using WSMovimientos.Entidades.DTOS.Salida;

#endregion Using

namespace WSMovimientos.Infraestructura.Clientes
{
    public class ClienteInfraestructura : IClientesInfraestructura
    {
        #region ReadOnly

        private readonly IPropiedadesApi _iPropiedadesApi;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IValidator<EEntrada<EntradaConsultaCliente>> _validatorEntradaConsulta;
        private readonly IValidator<EEntrada<EntradaCreaCliente>> _validatorEntradaCrea;
        private readonly IValidator<EEntrada<EntradaActualizaCliente>> _validatorEntradaActualiza;
        private readonly IValidator<EEntrada<EntradaEliminaCliente>> _validatorEntradaElimina;

        #endregion ReadOnly

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iPropiedadesApi"></param>
        /// <param name="personaRepositorio"></param>
        /// <param name="validatorEntradaConsulta"></param>
        /// <param name="validatorEntradaCrea"></param>
        /// <param name="validatorEntradaActualiza"></param>
        /// <param name="validatorEntradaElimina"></param>
        public ClienteInfraestructura
            (
            IPropiedadesApi iPropiedadesApi,
            IClienteRepositorio personaRepositorio,
            IValidator<EEntrada<EntradaConsultaCliente>> validatorEntradaConsulta,
            IValidator<EEntrada<EntradaCreaCliente>> validatorEntradaCrea,
            IValidator<EEntrada<EntradaActualizaCliente>> validatorEntradaActualiza,
            IValidator<EEntrada<EntradaEliminaCliente>> validatorEntradaElimina
            )

        {
            _iPropiedadesApi = iPropiedadesApi;
            _clienteRepositorio = personaRepositorio;
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
        public async Task<ERespuesta<SalidaConsultaClientes>> Consulta(EEntrada<EntradaConsultaCliente> entrada)
        {
            List<ClienteConsulta> resultadoConsulta = new List<ClienteConsulta>();

            var result = _validatorEntradaConsulta.Validate(entrada);
            if (!result.IsValid)
            {
                var falla = result.Errors.First();
                throw new CoreNegocioError(falla.ErrorCode, falla.ErrorMessage, this.GetFirstName(), MethodBase.GetCurrentMethod()?.DeclaringType?.Name, _iPropiedadesApi.BackendOpenShift());
            }


            resultadoConsulta = await _clienteRepositorio.Consulta(entrada.BodyIn);


            if (resultadoConsulta.IsNull() || resultadoConsulta.Count < 1)
                throw new CoreNegocioError(EConstantes.ErrorCode4, EConstantes.ErrorCode4Descripcion, this.GetFirstName(), EConstantes.movimientos, _iPropiedadesApi.BackendOpenShift());

            return new ERespuesta<SalidaConsultaClientes>()
            {
                HeaderOut = entrada.HeaderIn,
                BodyOut = new SalidaConsultaClientes()
                {
                    Clientes = resultadoConsulta
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
        public async Task<ERespuesta<SalidaCreaCliente>> Crea(EEntrada<EntradaCreaCliente> entrada)
        {
            var result = _validatorEntradaCrea.Validate(entrada);
            if (!result.IsValid)
            {
                var falla = result.Errors.First();
                throw new CoreNegocioError(falla.ErrorCode, falla.ErrorMessage, this.GetFirstName(), MethodBase.GetCurrentMethod()?.DeclaringType?.Name, _iPropiedadesApi.BackendOpenShift());
            }

            var resultadoCrea = await _clienteRepositorio.Crea(entrada.BodyIn.Cliente);

            if (resultadoCrea.IsNull() || resultadoCrea.Id < 1) throw new CoreNegocioError(EConstantes.ErrorCrearCode, EConstantes.ErrorCrearDescripcion, this.GetFirstName(), EConstantes.crear, _iPropiedadesApi.BackendOpenShift());

            return new ERespuesta<SalidaCreaCliente>()
            {
                HeaderOut = entrada.HeaderIn,
                BodyOut = new SalidaCreaCliente()
                {
                    Cliente = resultadoCrea
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
        public async Task<ERespuestaSimple> Actualiza(EEntrada<EntradaActualizaCliente> entrada)
        {
            var result = _validatorEntradaActualiza.Validate(entrada);
            if (!result.IsValid)
            {
                var falla = result.Errors.First();
                throw new CoreNegocioError(falla.ErrorCode, falla.ErrorMessage, this.GetFirstName(), MethodBase.GetCurrentMethod()?.DeclaringType?.Name, _iPropiedadesApi.BackendOpenShift());
            }


            if (!(await _clienteRepositorio.Actualiza(entrada.BodyIn.Cliente)))
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
        public async Task<ERespuestaSimple> Elimina(EEntrada<EntradaEliminaCliente> entrada)
        {
            var result = _validatorEntradaElimina.Validate(entrada);
            if (!result.IsValid)
            {
                var falla = result.Errors.First();
                throw new CoreNegocioError(falla.ErrorCode, falla.ErrorMessage, this.GetFirstName(), MethodBase.GetCurrentMethod()?.DeclaringType?.Name, _iPropiedadesApi.BackendOpenShift());
            }


            if (!(await _clienteRepositorio.Elimina(entrada.BodyIn.Cliente)))
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