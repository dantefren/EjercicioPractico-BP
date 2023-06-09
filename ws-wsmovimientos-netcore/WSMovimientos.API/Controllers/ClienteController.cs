#region Using

using BP.API.Constantes;
using BP.API.Entidades;
using BP.API.Entidades.Excepciones;
using BP.API.Entidades.Extensiones;
using BP.Comun.Extensiones;
using BP.Comun.Logs.Base.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WSMovimientos.Dominio.Clientes;
using WSMovimientos.Entidades;
using WSMovimientos.Entidades.DTOS.Entrada;

#endregion Using

namespace WSMovimientos.API.Controllers
{

    [ApiVersion(EConstantes.ApiVersion)]
    [Route(Controlador.RutaVersion)]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        #region Readonly

        private readonly IClientesInfraestructura _clientesInfraestructura;

        #endregion Readonly

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personaInfraestructura"></param>
        public ClienteController(IClientesInfraestructura clienteInfraestructura)
        {
            _clientesInfraestructura = clienteInfraestructura;
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
        /// <param name="eEntradaConsultaCliente"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(EConstantes.consultar)]
        [Loggable]
        public async Task<ActionResult> Consultar(EEntrada<EEntradaConsultaCliente> eEntradaConsultaCliente)
        {
            var salidaError = new ERespuestaSimple(eEntradaConsultaCliente?.HeaderIn, new EError());

            try
            {
                return Ok(await _clientesInfraestructura.Consultar(eEntradaConsultaCliente));

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
        /// <param name="entradaCreaCliente"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(EConstantes.crear)]
        [Loggable]
        public async Task<ActionResult> Crear(EEntrada<EEntradaCreaCliente> entradaCreaCliente)
        {
            var salidaError = new ERespuestaSimple(entradaCreaCliente?.HeaderIn, new EError());

            try
            {
                return Ok(await _clientesInfraestructura.Crear(entradaCreaCliente));

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
        /// <param name="entradaActualizaCliente"></param>
        /// <returns></returns>
        [HttpPost]
        //[Route(EConstantes.Recurso003)]
        [Route(EConstantes.actualizar)]
        [Loggable]
        public async Task<ActionResult> Actualizar(EEntrada<EEntradaActualizaCliente> entradaActualizaCliente)
        {
            var salidaError = new ERespuestaSimple(entradaActualizaCliente?.HeaderIn, new EError());

            try
            {
                return Ok(await _clientesInfraestructura.Actualizar(entradaActualizaCliente));

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
        /// <param name="entradaEliminaCliente"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route(EConstantes.eliminar)]
        [Loggable]
        public async Task<ActionResult> Eliminar(EEntrada<EEntradaEliminaCliente> entradaEliminaCliente)
        {
            var salidaError = new ERespuestaSimple(entradaEliminaCliente?.HeaderIn, new EError());

            try
            {
                return Ok(await _clientesInfraestructura.Eliminar(entradaEliminaCliente));

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