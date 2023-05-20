#region Using

using BP.API.Constantes;
using BP.API.Entidades;
using BP.API.Entidades.Excepciones;
using BP.API.Entidades.Extensiones;
using BP.Comun.Extensiones;
using BP.Comun.Logs.Base.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WSMovimientos.Dominio.Cuentas;
using WSMovimientos.Entidades;
using WSMovimientos.Entidades.DTOS.Entrada;

#endregion Using

namespace WSMovimientos.API.Controllers
{

    [ApiVersion(EConstantes.ApiVersion)]
    [Route(Controlador.RutaVersion)]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        #region Readonly

        private readonly ICuentaInfraestructura _cuentasInfraestructura;

        #endregion Readonly

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personaInfraestructura"></param>
        public CuentaController(ICuentaInfraestructura cuentaInfraestructura)
        {
            _cuentasInfraestructura = cuentaInfraestructura;
        }

        #endregion Constructor

        #region Métodos

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
        public async Task<ActionResult> Consultar(EEntrada<EEntradaConsultaCuenta> eEntradaConsultaCuenta)
        {
            var salidaError = new ERespuestaSimple(eEntradaConsultaCuenta?.HeaderIn, new EError());

            try
            {
                return Ok(await _cuentasInfraestructura.Consultar(eEntradaConsultaCuenta));

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
        /// <param name="entradaCreaCuenta"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(EConstantes.crear)]
        [Loggable]
        public async Task<ActionResult> Crear(EEntrada<EEntradaCreaCuenta> entradaCreaCuenta)
        {
            var salidaError = new ERespuestaSimple(entradaCreaCuenta?.HeaderIn, new EError());

            try
            {
                return Ok(await _cuentasInfraestructura.Crear(entradaCreaCuenta));

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
        /// <param name="entradaActualizaCuenta"></param>
        /// <returns></returns>
        [HttpPost]
        //[Route(EConstantes.Recurso003)]
        [Route(EConstantes.actualizar)]
        [Loggable]
        public async Task<ActionResult> Actualizar(EEntrada<EEntradaActualizaCuenta> entradaActualizaCuenta)
        {
            var salidaError = new ERespuestaSimple(entradaActualizaCuenta?.HeaderIn, new EError());

            try
            {
                return Ok(await _cuentasInfraestructura.Actualizar(entradaActualizaCuenta));

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
        /// <param name="entradaEliminaCuenta"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route(EConstantes.eliminar)]
        [Loggable]
        public async Task<ActionResult> Eliminar(EEntrada<EEntradaEliminaCuenta> entradaEliminaCuenta)
        {
            var salidaError = new ERespuestaSimple(entradaEliminaCuenta?.HeaderIn, new EError());

            try
            {
                return Ok(await _cuentasInfraestructura.Eliminar(entradaEliminaCuenta));

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

        #endregion Métodos
    }
}