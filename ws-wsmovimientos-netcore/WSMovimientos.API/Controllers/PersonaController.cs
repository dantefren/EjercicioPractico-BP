#region Using

using BP.API.Constantes;
using BP.API.Entidades;
using BP.API.Entidades.Excepciones;
using BP.API.Entidades.Extensiones;
using BP.Comun.Extensiones;
using BP.Comun.Logs.Base.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WSMovimientos.Dominio.Personas;
using WSMovimientos.Entidades;
using WSMovimientos.Entidades.DTOS.Entrada;

#endregion Using

namespace WSMovimientos.API.Controllers
{

    [ApiVersion(EConstantes.ApiVersion)]
    [Route(Controlador.RutaVersion)]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        #region Readonly

        private readonly IPersonaInfraestructura _personaInfraestructura;

        #endregion Readonly

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personaInfraestructura"></param>
        public PersonaController(IPersonaInfraestructura personaInfraestructura)
        {
            _personaInfraestructura = personaInfraestructura;
        }

        #endregion Constructor

        #region M�todos

        #region HealthCheck
        /// <summary>
        /// Metodo para verificacion el estado de la api
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(EConstantes.HealthChecks)]
        [Loggable]
        public ActionResult HealthChecks()
        {
            return Ok();
        }
        #endregion HealthCheck

        #region Consulta
        /// <summary>
        /// Metodo para consultar las personas 
        /// </summary>
        /// <param name="eEntradaConsultaPersona"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(EConstantes.consulta)]
        [Loggable]
        public async Task<ActionResult> Consulta(EEntrada<EntradaConsultaPersona> eEntradaConsultaPersona)
        {
            var salidaError = new ERespuestaSimple(eEntradaConsultaPersona?.HeaderIn, new EError());

            try
            {
                return Ok(await _personaInfraestructura.Consulta(eEntradaConsultaPersona));

            }
            catch (CoreNegocioError coreNegocioError)
            {
                coreNegocioError.ProcesarError(salidaError);
                salidaError.Error.MensajeNegocio = coreNegocioError.Mensaje;
                return StatusCode(HttpStatusCode.BadRequest.ToInt(), salidaError);
            }
            catch (CoreExcepcion coreExcepcion)
            {
                coreExcepcion.ProcesarError(salidaError);
                salidaError.Error.MensajeNegocio = EConstantes.ErrorGenericoDescripcion;
                return StatusCode(HttpStatusCode.BadRequest.ToInt(), salidaError);
            }
            catch (Exception exception)
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt(), exception.ProcesarError(salidaError));
            }

        }
        #endregion Consulta

        #region Creacion
        /// <summary>
        /// Metodo para crear personas 
        /// </summary>
        /// <param name="entradaCreaPersona"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(EConstantes.crear)]
        [Loggable]
        public async Task<ActionResult> CreaPersonaAsync(EEntrada<EntradaCreaPersona> entradaCreaPersona)
        {
            var salidaError = new ERespuestaSimple(entradaCreaPersona?.HeaderIn, new EError());

            try
            {
                return Ok(await _personaInfraestructura.CrearAsync(entradaCreaPersona));

            }
            catch (CoreNegocioError coreNegocioError)
            {
                coreNegocioError.ProcesarError(salidaError);
                salidaError.Error.MensajeNegocio = coreNegocioError.Mensaje;
                return StatusCode(HttpStatusCode.BadRequest.ToInt(), salidaError);
            }
            catch (CoreExcepcion coreExcepcion)
            {
                coreExcepcion.ProcesarError(salidaError);
                salidaError.Error.MensajeNegocio = EConstantes.ErrorGenericoDescripcion;
                return StatusCode(HttpStatusCode.BadRequest.ToInt(), salidaError);
            }
            catch (Exception exception)
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt(), exception.ProcesarError(salidaError));
            }
        }
        #endregion Creacion

        #region Actualizacion
        /// <summary>
        /// Metodo para actualizar los datos de una Persona
        /// </summary>
        /// <param name="entradaActualizaPersona"></param>
        /// <returns></returns>
        [HttpPost]
        //[Route(EConstantes.Recurso003)]
        [Route(EConstantes.actualizar)]
        [Loggable]
        public async Task<ActionResult> ActualizaPersonaAsync(EEntrada<EntradaActualizaPersona> entradaActualizaPersona)
        {
            var salidaError = new ERespuestaSimple(entradaActualizaPersona?.HeaderIn, new EError());

            try
            {
                return Ok(await _personaInfraestructura.ActualizarAsync(entradaActualizaPersona));

            }
            catch (CoreNegocioError coreNegocioError)
            {
                coreNegocioError.ProcesarError(salidaError);
                salidaError.Error.MensajeNegocio = coreNegocioError.Mensaje;
                return StatusCode(HttpStatusCode.BadRequest.ToInt(), salidaError);
            }
            catch (CoreExcepcion coreExcepcion)
            {
                coreExcepcion.ProcesarError(salidaError);
                salidaError.Error.MensajeNegocio = EConstantes.ErrorGenericoDescripcion;
                return StatusCode(HttpStatusCode.BadRequest.ToInt(), salidaError);
            }
            catch (Exception exception)
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt(), exception.ProcesarError(salidaError));
            }
        }

        #endregion Actualizacion

        #region Eliminacion 
        /// <summary>
        /// Metodo para realizar la eliminacion de persona
        /// </summary>
        /// <param name="entradaEliminaPersona"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route(EConstantes.eliminar)]
        [Loggable]
        public async Task<ActionResult> EliminaPersona(EEntrada<EntradaEliminaPersona> entradaEliminaPersona)
        {
            var salidaError = new ERespuestaSimple(entradaEliminaPersona?.HeaderIn, new EError());

            try
            {
                return Ok(await _personaInfraestructura.EliminarAsync(entradaEliminaPersona));

            }
            catch (CoreNegocioError coreNegocioError)
            {
                coreNegocioError.ProcesarError(salidaError);
                salidaError.Error.MensajeNegocio = coreNegocioError.Mensaje;
                return StatusCode(HttpStatusCode.BadRequest.ToInt(), salidaError);
            }
            catch (CoreExcepcion coreExcepcion)
            {
                coreExcepcion.ProcesarError(salidaError);
                salidaError.Error.MensajeNegocio = EConstantes.ErrorGenericoDescripcion;
                return StatusCode(HttpStatusCode.BadRequest.ToInt(), salidaError);
            }
            catch (Exception exception)
            {
                return StatusCode(HttpStatusCode.InternalServerError.ToInt(), exception.ProcesarError(salidaError));
            }

        }

        #endregion Eliminacion

        #endregion M�todos
    }
}