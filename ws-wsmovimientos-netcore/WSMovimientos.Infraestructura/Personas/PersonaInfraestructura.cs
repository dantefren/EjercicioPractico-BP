#region Using 

using BP.API.Entidades;
using BP.API.Entidades.Excepciones;
using BP.Comun.Extensiones;
using BP.Comun.Logs.Base.Handlers;
using FluentValidation;
using System.Reflection;
using WSMovimientos.Dominio.Interfaces.Propiedades;
using WSMovimientos.Dominio.Personas;
using WSMovimientos.Entidades;
using WSMovimientos.Entidades.DTOS;
using WSMovimientos.Entidades.DTOS.Entrada;
using WSMovimientos.Entidades.DTOS.Salida;

#endregion Using

namespace WSMovimientos.Infraestructura.Personas
{
    public class PersonaInfraestructura : IPersonaInfraestructura
    {
        #region ReadOnly

        private readonly IPropiedadesApi _iPropiedadesApi;
        private readonly IPersonaRepositorio _personaRepositorio;
        private readonly IValidator<EEntrada<EntradaConsultaPersona>> _validatorEntradaConsulta;
        private readonly IValidator<EEntrada<EntradaCreaPersona>> _validatorEntradaCrea;
        private readonly IValidator<EEntrada<EntradaActualizaPersona>> _validatorEntradaActualiza;
        private readonly IValidator<EEntrada<EntradaEliminaPersona>> _validatorEntradaElimina;

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
        public PersonaInfraestructura
            (
            IPropiedadesApi iPropiedadesApi,
            IPersonaRepositorio personaRepositorio,
            IValidator<EEntrada<EntradaConsultaPersona>> validatorEntradaConsulta,
            IValidator<EEntrada<EntradaCreaPersona>> validatorEntradaCrea,
            IValidator<EEntrada<EntradaActualizaPersona>> validatorEntradaActualiza,
            IValidator<EEntrada<EntradaEliminaPersona>> validatorEntradaElimina
            )

        {
            _iPropiedadesApi = iPropiedadesApi;
            _personaRepositorio = personaRepositorio;
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
        public async Task<ERespuesta<SalidaConsultaPersonas>> Consulta(EEntrada<EntradaConsultaPersona> entrada)
        {
            List<PersonaConsulta> resultadoConsulta = new List<PersonaConsulta>();

            var result = _validatorEntradaConsulta.Validate(entrada);
            if (!result.IsValid)
            {
                var falla = result.Errors.First();
                throw new CoreNegocioError(falla.ErrorCode, falla.ErrorMessage, this.GetFirstName(), MethodBase.GetCurrentMethod()?.DeclaringType?.Name, _iPropiedadesApi.BackendOpenShift());
            }

            
            resultadoConsulta = await _personaRepositorio.Consulta(entrada.BodyIn);
         

            if (resultadoConsulta.IsNull() || resultadoConsulta.Count < 1)
                throw new CoreNegocioError(EConstantes.ErrorCode4, EConstantes.ErrorCode4Descripcion, this.GetFirstName(), EConstantes.movimientos, _iPropiedadesApi.BackendOpenShift());

            return new ERespuesta<SalidaConsultaPersonas>()
            {
                HeaderOut = entrada.HeaderIn,
                BodyOut = new SalidaConsultaPersonas()
                {
                    Personas = resultadoConsulta
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
        public async Task<ERespuesta<SalidaCreaPersona>> CrearAsync(EEntrada<EntradaCreaPersona> entrada)
        {
            var result = _validatorEntradaCrea.Validate(entrada);
            if (!result.IsValid)
            {
                var falla = result.Errors.First();
                throw new CoreNegocioError(falla.ErrorCode, falla.ErrorMessage, this.GetFirstName(), MethodBase.GetCurrentMethod()?.DeclaringType?.Name, _iPropiedadesApi.BackendOpenShift());
            }

            var resultadoCrea = await _personaRepositorio.CrearAsync(entrada.BodyIn.Persona);

            if (resultadoCrea.IsNull() || resultadoCrea.Id < 1) throw new CoreNegocioError(EConstantes.ErrorCrearCode, EConstantes.ErrorCrearDescripcion, this.GetFirstName(), EConstantes.crear, _iPropiedadesApi.BackendOpenShift());

            return new ERespuesta<SalidaCreaPersona>()
            {
                HeaderOut = entrada.HeaderIn,
                BodyOut = new SalidaCreaPersona()
                {
                    Persona = resultadoCrea
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
        public async Task<ERespuestaSimple> ActualizarAsync(EEntrada<EntradaActualizaPersona> entrada)
        {
            var result = _validatorEntradaActualiza.Validate(entrada);
            if (!result.IsValid)
            {
                var falla = result.Errors.First();
                throw new CoreNegocioError(falla.ErrorCode, falla.ErrorMessage, this.GetFirstName(), MethodBase.GetCurrentMethod()?.DeclaringType?.Name, _iPropiedadesApi.BackendOpenShift());
            }


            if (!(await _personaRepositorio.ActualizarAsync(entrada.BodyIn.Persona)))
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
        public async Task<ERespuestaSimple> EliminarAsync(EEntrada<EntradaEliminaPersona> entrada)
        {
            var result = _validatorEntradaElimina.Validate(entrada);
            if (!result.IsValid)
            {
                var falla = result.Errors.First();
                throw new CoreNegocioError(falla.ErrorCode, falla.ErrorMessage, this.GetFirstName(), MethodBase.GetCurrentMethod()?.DeclaringType?.Name, _iPropiedadesApi.BackendOpenShift());
            }


            if (!(await _personaRepositorio.EliminarAsync(entrada.BodyIn.Persona)))
                throw new CoreNegocioError(EConstantes.ErrorEliminarCode, EConstantes.ErrorEliminarDescripcion, this.GetFirstName(), EConstantes.eliminar, _iPropiedadesApi.BackendOpenShift());

            return new ERespuestaSimple()
            {
                HeaderOut = entrada.HeaderIn,


                Error = new EError(this.GetFirstName(), EConstantes.eliminar, _iPropiedadesApi.BackendOpenShift()) { MensajeNegocio = EConstantes.OKGenericoDescripcion }
            };
        }

        #endregion Elimina
    }



    #endregion Methods

}