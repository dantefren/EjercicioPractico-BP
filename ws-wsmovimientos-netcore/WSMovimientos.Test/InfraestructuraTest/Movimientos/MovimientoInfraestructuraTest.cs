#region Using

using BP.API.Entidades;
using BP.API.Entidades.Excepciones;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using WSMovimientos.Dominio.Movimientos;
using WSMovimientos.Dominio.Interfaces.Propiedades;
using WSMovimientos.Entidades.DTOS;
using WSMovimientos.Entidades.DTOS.Entrada;
using WSMovimientos.Infraestructura.Movimientos;

#endregion Using

namespace WSMovimientos.Test.InfraestructuraTest.Movimientos
{
    public class MovimientoInfraestructuraTest
    {
        #region MOCKS

        
        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IPropiedadesApi> _mockIPropiedadesApi = new Mock<IPropiedadesApi>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IMovimientoRepositorio> _mockRepositorio = new Mock<IMovimientoRepositorio>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IValidator<EEntrada<EEntradaConsultaMovimiento>>> _mockValidatorEntradaConsulta = new Mock<IValidator<EEntrada<EEntradaConsultaMovimiento>>>();
        private readonly Mock<IValidator<EEntrada<EEntradaConsultaMovimientoCuenta>>> _mockValidatorEntradaMovimientoConsulta = new Mock<IValidator<EEntrada<EEntradaConsultaMovimientoCuenta>>>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IValidator<EEntrada<EEntradaCreaMovimiento>>> _mockValidatorEntradaCrea = new Mock<IValidator<EEntrada<EEntradaCreaMovimiento>>>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IValidator<EEntrada<EEntradaActualizaMovimiento>>> _mockvalidatorEntradaActualiza = new Mock<IValidator<EEntrada<EEntradaActualizaMovimiento>>>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IValidator<EEntrada<EEntradaEliminaMovimiento>>> _mockvalidatorEntradaElimina = new Mock<IValidator<EEntrada<EEntradaEliminaMovimiento>>>();

        #endregion MOCKS

        #region Propiedades

        /// <summary>
        /// 
        /// </summary>
        private EEntrada<EEntradaConsultaMovimiento> entradaConsultar = new EEntrada<EEntradaConsultaMovimiento>();
        private EEntrada<EEntradaCreaMovimiento> entradaCrear = new EEntrada<EEntradaCreaMovimiento>();
        private EEntrada<EEntradaActualizaMovimiento> entradaActualizar = new EEntrada<EEntradaActualizaMovimiento>();
        private EEntrada<EEntradaEliminaMovimiento> entradaEliminar = new EEntrada<EEntradaEliminaMovimiento>();

        /// <summary>
        /// 
        /// </summary>
        private List<EMovimientoConsulta> lista = new List<EMovimientoConsulta>();

        /// <summary>
        /// 
        /// </summary>
        private EMovimientoConsulta informacionConsulta = new EMovimientoConsulta();

        /// <summary>
        /// 
        /// </summary>
        private EMovimientoId identificadorRegistro = new EMovimientoId();

        /// <summary>
        /// 
        /// </summary>
        private EMovimiento movimientoRegistro = new EMovimiento();

        /// <summary>
        /// 
        /// </summary>
        private IMovimientoInfraestructura _iMovimientoInfraestructura;

        #endregion Propiedades


        #region SETUP

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void Setup()
        {
            string strEntrada = "{\"headerIn\":{\"dispositivo\":\"SOFGHyjguVVZrWyOQhxK2R1ULX3d76XrXpBLVwJj\",\"empresa\":\"0010\",\"canal\":\"02\",\"medio\":\"020007\",\"aplicacion\":\"00664\",\"agencia\":\"0162\",\"tipoTransaccion\":\"201021303\",\"geolocalizacion\":\"0.6814,0.1515\",\"usuario\":\"USINTERT\",\"unicidad\":\"mBMVucAHA4auns86xXqgb8eNJECRUqFTRDvvbQAG\",\"guid\":\"52f66830535f92FFdNVGXBK7Xn1FG8KY\",\"fechaHora\":\"202208111710001063\",\"filler\":\"filler\",\"idioma\":\"es-EC\",\"sesion\":\"TCqnfSnMAlVlrJ2A6KKSpbmfoTra917zDuDe6uqe\",\"ip\":\"10.223.15.237\",\"idCliente\":\"0503364242\",\"tipoIdCliente\":\"0001\"}";
                
            entradaConsultar = JsonConvert.DeserializeObject<EEntrada<EEntradaConsultaMovimiento>>(strEntrada + ",\"bodyIn\":{\"idCuenta\": 1,\"fechaInicio\": \"2023-05-13\",\"fechaFin\": \"2023-05-15\"}}}");
            entradaCrear = JsonConvert.DeserializeObject<EEntrada<EEntradaCreaMovimiento>>(strEntrada + ",\"bodyIn\":{\"movimiento\": {\"idCuenta\": 1,\"tipo\": \"RET\",\"valor\": 290.00}}}}");
            entradaActualizar = JsonConvert.DeserializeObject<EEntrada<EEntradaActualizaMovimiento>>(strEntrada + ",\"bodyIn\":{\"movimiento\": {\"id\": 2,=\"tipo\": \"RET\",\"saldo\": 99}}}}");
            entradaEliminar = JsonConvert.DeserializeObject<EEntrada<EEntradaEliminaMovimiento>>(strEntrada + ",\"bodyIn\":{\"movimiento\": {\"id\": 18}}}}");


            informacionConsulta = new EMovimientoConsulta
            {
                Id = 1,
                IdCuenta = 1,
                Cuenta = null,
                Saldo = 1000,
                Tipo = "DEP",
                Valor = 10

            };

            lista.Add(informacionConsulta);
            lista.Add(informacionConsulta);
            lista.Add(informacionConsulta);
            lista.Add(informacionConsulta);


            identificadorRegistro = new EMovimientoId
            {
                Id = 2
            };

            movimientoRegistro = new EMovimiento
            {
                Id = 2,
                Saldo = 300,
                Fecha = DateTime.Now,
                Tipo = "DEP",
                Valor = 10
            };
        }

        #endregion SETUP

        #region TEST

        #region Consulta

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void ConsultarTest_DadaUnaEntrada_CuandoNoExistaDatosDeRespuesta_RetornaUnaExepcion()
        {
            _mockValidatorEntradaConsulta.Setup(v => v.Validate(entradaConsultar)).Returns(new ValidationResult());
            _mockRepositorio.Setup(ent => ent.Consultar(entradaConsultar.BodyIn)).ReturnsAsync(new List<Entidades.DTOS.EMovimientoConsulta>());
            _iMovimientoInfraestructura = new MovimientoInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object, _mockValidatorEntradaMovimientoConsulta.Object);
            Assert.ThrowsAsync<CoreNegocioError>(() => _iMovimientoInfraestructura.Consultar(entradaConsultar));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void ConsultarTest_DadaUnaSolicitudConFiltro_CuandoExistanRegistrosEnElRepositorio_RetornaDatosDeRespuesta()
        {
            _mockValidatorEntradaConsulta.Setup(v => v.Validate(entradaConsultar)).Returns(new ValidationResult());
            _mockRepositorio.Setup(ent => ent.Consultar(entradaConsultar.BodyIn)).ReturnsAsync(lista);
            _iMovimientoInfraestructura = new MovimientoInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object, _mockValidatorEntradaMovimientoConsulta.Object);
            Assert.DoesNotThrowAsync(() => _iMovimientoInfraestructura.Consultar(entradaConsultar));
        }
        #endregion

        #region Crear

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void CrearTest_DadoUnRegistro_CuandoNoRegistraLaInformacion_RetornaUnaExepcion()
        {
            _mockValidatorEntradaCrea.Setup(v => v.Validate(entradaCrear)).Returns(new ValidationResult());
            EMovimientoCreaCompleto movimientoCompleto = new EMovimientoCreaCompleto() { 
            IdCuenta = entradaCrear.BodyIn.Movimiento.IdCuenta,
            Tipo = entradaCrear.BodyIn.Movimiento.Tipo,
            Valor = entradaCrear.BodyIn.Movimiento.Valor,
            Saldo = 200
            };

            _mockRepositorio.Setup(ent => ent.Crear(movimientoCompleto)).ReturnsAsync(new Entidades.DTOS.EMovimiento());
            _iMovimientoInfraestructura = new MovimientoInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object, _mockValidatorEntradaMovimientoConsulta.Object);
            Assert.ThrowsAsync<CoreNegocioError>(() => _iMovimientoInfraestructura.Crear(entradaCrear));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void CrearTest_DadaUnaRegistro_CuandoRegistraLaInformacionEnElRepositorio_RetornaIdentificador()
        {
            _mockValidatorEntradaCrea.Setup(v => v.Validate(entradaCrear)).Returns(new ValidationResult());
            EMovimientoCreaCompleto movimientoCompleto = new EMovimientoCreaCompleto()
            {
                IdCuenta = entradaCrear.BodyIn.Movimiento.IdCuenta,
                Tipo = entradaCrear.BodyIn.Movimiento.Tipo,
                Valor = entradaCrear.BodyIn.Movimiento.Valor,
                Saldo = 200
            };
            _mockRepositorio.Setup(ent => ent.Crear(movimientoCompleto)).ReturnsAsync(movimientoRegistro);
            _iMovimientoInfraestructura = new MovimientoInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object, _mockValidatorEntradaMovimientoConsulta.Object);
            Assert.DoesNotThrowAsync(() => _iMovimientoInfraestructura.Crear(entradaCrear));
        }
        #endregion

        #region Actualizar

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void ActualizarTest_DadoUnRegistro_CuandoNoActualizaLaInformacion_RetornaUnaExepcion()
        {
            _mockvalidatorEntradaActualiza.Setup(v => v.Validate(entradaActualizar)).Returns(new ValidationResult());
            _mockRepositorio.Setup(ent => ent.Actualizar(entradaActualizar.BodyIn.Movimiento)).ReturnsAsync(false);
            _iMovimientoInfraestructura = new MovimientoInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object, _mockValidatorEntradaMovimientoConsulta.Object);
            Assert.ThrowsAsync<CoreNegocioError>(() => _iMovimientoInfraestructura.Actualizar(entradaActualizar));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void ActualizarTest_DadUnRegistro_CuandoRegistraLaInformacionEnElRepositorio_RetornaIdentificador()
        {
            _mockvalidatorEntradaActualiza.Setup(v => v.Validate(entradaActualizar)).Returns(new ValidationResult());
            _mockRepositorio.Setup(ent => ent.Actualizar(entradaActualizar.BodyIn.Movimiento)).ReturnsAsync(true);
            _iMovimientoInfraestructura = new MovimientoInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object, _mockValidatorEntradaMovimientoConsulta.Object);
            Assert.DoesNotThrowAsync(() => _iMovimientoInfraestructura.Actualizar(entradaActualizar));
        }
        #endregion

        #region Eliminar

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void EliminarTest_DadoUnIdentificador_CuandoNoExistaElRegistro_RetornaUnaExepcion()
        {
            _mockvalidatorEntradaElimina.Setup(v => v.Validate(entradaEliminar)).Returns(new ValidationResult());
            _mockRepositorio.Setup(ent => ent.Eliminar(entradaEliminar.BodyIn.Movimiento)).ReturnsAsync(false);
            _iMovimientoInfraestructura = new MovimientoInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object, _mockValidatorEntradaMovimientoConsulta.Object);
            Assert.ThrowsAsync<CoreNegocioError>(() => _iMovimientoInfraestructura.Eliminar(entradaEliminar));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void EliminarTest_DadoUnIdentificador_CuandoExistaElRegistroEnElRepositorio_RetornaVerdadero()
        {
            _mockvalidatorEntradaElimina.Setup(v => v.Validate(entradaEliminar)).Returns(new ValidationResult());
            _mockRepositorio.Setup(ent => ent.Eliminar(entradaEliminar.BodyIn.Movimiento)).ReturnsAsync(true);
            _iMovimientoInfraestructura = new MovimientoInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object, _mockValidatorEntradaMovimientoConsulta.Object);
            Assert.DoesNotThrowAsync(() => _iMovimientoInfraestructura.Eliminar(entradaEliminar));
        }
        #endregion

        #endregion TEST

    }
}