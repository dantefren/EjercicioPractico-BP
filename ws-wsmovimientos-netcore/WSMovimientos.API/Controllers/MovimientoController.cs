#region Using

using BP.API.Constantes;
using BP.API.Entidades;
using BP.API.Entidades.Excepciones;
using BP.API.Entidades.Extensiones;
using BP.Comun.Extensiones;
using BP.Comun.Logs.Base.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WSMovimientos.Dominio.Movimientos;
using WSMovimientos.Entidades;
using WSMovimientos.Entidades.DTOS.Entrada;

#endregion Using

namespace WSMovimientos.API.Controllers
{

    [ApiVersion(EConstantes.ApiVersion)]
    [Route(Controlador.RutaVersion)]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        #region Readonly

        private readonly IMovimientoInfraestructura _cuentasInfraestructura;

        #endregion Readonly

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personaInfraestructura"></param>
        public MovimientoController(IMovimientoInfraestructura cuentaInfraestructura)
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
        /// <param name="eEntradaConsultaMovimiento"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(EConstantes.consultar)]
        [Loggable]
        public async Task<ActionResult> Consultar(EEntrada<EEntradaConsultaMovimiento> eEntradaConsultaMovimiento)
        {
            var salidaError = new ERespuestaSimple(eEntradaConsultaMovimiento?.HeaderIn, new EError());

            try
            {
                return Ok(await _cuentasInfraestructura.Consultar(eEntradaConsultaMovimiento));

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


        /// <summary>
        /// Metodo para consultar las personas 
        /// </summary>
        /// <param name="eEntradaConsultaMovimientoCuenta"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(EConstantes.consultaMovimientos)]
        [Loggable]
        public async Task<ActionResult> ConsultarMovimientosCuenta(EEntrada<EEntradaConsultaMovimientoCuenta> eEntradaConsultaMovimientoCuenta)
        {
            var salidaError = new ERespuestaSimple(eEntradaConsultaMovimientoCuenta?.HeaderIn, new EError());

            try
            {
                return Ok(await _cuentasInfraestructura.ConsultarMovimientosCuenta(eEntradaConsultaMovimientoCuenta));

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
        /// <param name="entradaCreaMovimiento"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(EConstantes.crear)]
        [Loggable]
        public async Task<ActionResult> Crear(EEntrada<EEntradaCreaMovimiento> entradaCreaMovimiento)
        {
            var salidaError = new ERespuestaSimple(entradaCreaMovimiento?.HeaderIn, new EError());

            try
            {
                return Ok(await _cuentasInfraestructura.Crear(entradaCreaMovimiento));

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
        public async Task<ActionResult> Actualizar(EEntrada<EEntradaActualizaMovimiento> entradaActualizaMovimiento)
        {
            var salidaError = new ERespuestaSimple(entradaActualizaMovimiento?.HeaderIn, new EError());

            try
            {
                return Ok(await _cuentasInfraestructura.Actualizar(entradaActualizaMovimiento));

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
        /// <param name="entradaEliminaMovimiento"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route(EConstantes.eliminar)]
        [Loggable]
        public async Task<ActionResult> Eliminar(EEntrada<EEntradaEliminaMovimiento> entradaEliminaMovimiento)
        {
            var salidaError = new ERespuestaSimple(entradaEliminaMovimiento?.HeaderIn, new EError());

            try
            {
                return Ok(await _cuentasInfraestructura.Eliminar(entradaEliminaMovimiento));

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