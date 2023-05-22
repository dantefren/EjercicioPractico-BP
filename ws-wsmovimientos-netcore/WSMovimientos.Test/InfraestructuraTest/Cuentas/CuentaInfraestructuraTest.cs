#region Using

using BP.API.Entidades;
using BP.API.Entidades.Excepciones;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using WSMovimientos.Dominio.Cuentas;
using WSMovimientos.Dominio.Interfaces.Propiedades;
using WSMovimientos.Entidades.DTOS;
using WSMovimientos.Entidades.DTOS.Entrada;
using WSMovimientos.Infraestructura.Cuentas;

#endregion Using

namespace WSMovimientos.Test.InfraestructuraTest.Cuentas
{
    public class CuentaInfraestructuraTest
    {
        #region MOCKS

        
        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IPropiedadesApi> _mockIPropiedadesApi = new Mock<IPropiedadesApi>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<ICuentaRepositorio> _mockRepositorio = new Mock<ICuentaRepositorio>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IValidator<EEntrada<EEntradaConsultaCuenta>>> _mockValidatorEntradaConsulta = new Mock<IValidator<EEntrada<EEntradaConsultaCuenta>>>();
        
        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IValidator<EEntrada<EEntradaCreaCuenta>>> _mockValidatorEntradaCrea = new Mock<IValidator<EEntrada<EEntradaCreaCuenta>>>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IValidator<EEntrada<EEntradaActualizaCuenta>>> _mockvalidatorEntradaActualiza = new Mock<IValidator<EEntrada<EEntradaActualizaCuenta>>>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IValidator<EEntrada<EEntradaEliminaCuenta>>> _mockvalidatorEntradaElimina = new Mock<IValidator<EEntrada<EEntradaEliminaCuenta>>>();

        #endregion MOCKS

        #region Propiedades

        /// <summary>
        /// 
        /// </summary>
        private EEntrada<EEntradaConsultaCuenta> entradaConsultar = new EEntrada<EEntradaConsultaCuenta>();
        private EEntrada<EEntradaCreaCuenta> entradaCrear = new EEntrada<EEntradaCreaCuenta>();
        private EEntrada<EEntradaActualizaCuenta> entradaActualizar = new EEntrada<EEntradaActualizaCuenta>();
        private EEntrada<EEntradaEliminaCuenta> entradaEliminar = new EEntrada<EEntradaEliminaCuenta>();

        /// <summary>
        /// 
        /// </summary>
        private List<ECuentaConsulta> lista = new List<ECuentaConsulta>();

        /// <summary>
        /// 
        /// </summary>
        private ECuentaConsulta informacionConsulta = new ECuentaConsulta();

        /// <summary>
        /// 
        /// </summary>
        private ECuentaId identificadorRegistro = new ECuentaId();

        /// <summary>
        /// 
        /// </summary>
        private ICuentaInfraestructura _iCuentaInfraestructura;

        #endregion Propiedades


        #region SETUP

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void Setup()
        {
            string strEntrada = "{\"headerIn\":{\"dispositivo\":\"SOFGHyjguVVZrWyOQhxK2R1ULX3d76XrXpBLVwJj\",\"empresa\":\"0010\",\"canal\":\"02\",\"medio\":\"020007\",\"aplicacion\":\"00664\",\"agencia\":\"0162\",\"tipoTransaccion\":\"201021303\",\"geolocalizacion\":\"0.6814,0.1515\",\"usuario\":\"USINTERT\",\"unicidad\":\"mBMVucAHA4auns86xXqgb8eNJECRUqFTRDvvbQAG\",\"guid\":\"52f66830535f92FFdNVGXBK7Xn1FG8KY\",\"fechaHora\":\"202208111710001063\",\"filler\":\"filler\",\"idioma\":\"es-EC\",\"sesion\":\"TCqnfSnMAlVlrJ2A6KKSpbmfoTra917zDuDe6uqe\",\"ip\":\"10.223.15.237\",\"idCliente\":\"0503364242\",\"tipoIdCliente\":\"0001\"}";
                
            entradaConsultar = JsonConvert.DeserializeObject<EEntrada<EEntradaConsultaCuenta>>(strEntrada + ",\"bodyIn\":{\"idCuenta\": 1}}}");
            entradaCrear = JsonConvert.DeserializeObject<EEntrada<EEntradaCreaCuenta>>(strEntrada + ",\"bodyIn\":{\"cuenta\": {\"idPersona\": 1,\"numeroCuenta\": 124324,\"tipoCuenta\": \"AHO\",\"saldoInicial\": 200}}}}");
            entradaActualizar = JsonConvert.DeserializeObject<EEntrada<EEntradaActualizaCuenta>>(strEntrada + ",\"bodyIn\":{\"cuenta\": {\"idCuenta\": 2,\"estado\": false}}}}");
            entradaEliminar = JsonConvert.DeserializeObject<EEntrada<EEntradaEliminaCuenta>>(strEntrada + ",\"bodyIn\":{\"cuenta\": {\"id\": 2}}}}");


            informacionConsulta = new ECuentaConsulta
            {
                Id = 1,
                Estado = true,
                IdPersona = 1,
                Persona = null,
               NumeroCuenta = 234567,
               SaldoInicial = 12,
               TipoCuenta = "AHO"

            };

            lista.Add(informacionConsulta);
            lista.Add(informacionConsulta);
            lista.Add(informacionConsulta);
            lista.Add(informacionConsulta);


            identificadorRegistro = new ECuentaId
            {
                Id = 2
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
            _mockRepositorio.Setup(ent => ent.Consultar(entradaConsultar.BodyIn)).ReturnsAsync(new List<Entidades.DTOS.ECuentaConsulta>());
            _iCuentaInfraestructura = new CuentaInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.ThrowsAsync<CoreNegocioError>(() => _iCuentaInfraestructura.Consultar(entradaConsultar));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void ConsultarTest_DadaUnaSolicitudConFiltro_CuandoExistanRegistrosEnElRepositorio_RetornaDatosDeRespuesta()
        {
            _mockValidatorEntradaConsulta.Setup(v => v.Validate(entradaConsultar)).Returns(new ValidationResult());
            _mockRepositorio.Setup(ent => ent.Consultar(entradaConsultar.BodyIn)).ReturnsAsync(lista);
            _iCuentaInfraestructura = new CuentaInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.DoesNotThrowAsync(() => _iCuentaInfraestructura.Consultar(entradaConsultar));
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
            _mockRepositorio.Setup(ent => ent.Crear(entradaCrear.BodyIn.Cuenta)).ReturnsAsync(new Entidades.DTOS.ECuentaId());
            _iCuentaInfraestructura = new CuentaInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.ThrowsAsync<CoreNegocioError>(() => _iCuentaInfraestructura.Crear(entradaCrear));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void CrearTest_DadaUnaRegistro_CuandoRegistraLaInformacionEnElRepositorio_RetornaIdentificador()
        {
            _mockValidatorEntradaCrea.Setup(v => v.Validate(entradaCrear)).Returns(new ValidationResult());
            _mockRepositorio.Setup(ent => ent.Crear(entradaCrear.BodyIn.Cuenta)).ReturnsAsync(identificadorRegistro);
            _iCuentaInfraestructura = new CuentaInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.DoesNotThrowAsync(() => _iCuentaInfraestructura.Crear(entradaCrear));
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
            _mockRepositorio.Setup(ent => ent.Actualizar(entradaActualizar.BodyIn.Cuenta)).ReturnsAsync(false);
            _iCuentaInfraestructura = new CuentaInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.ThrowsAsync<CoreNegocioError>(() => _iCuentaInfraestructura.Actualizar(entradaActualizar));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void ActualizarTest_DadUnRegistro_CuandoRegistraLaInformacionEnElRepositorio_RetornaIdentificador()
        {
            _mockvalidatorEntradaActualiza.Setup(v => v.Validate(entradaActualizar)).Returns(new ValidationResult());
            _mockRepositorio.Setup(ent => ent.Actualizar(entradaActualizar.BodyIn.Cuenta)).ReturnsAsync(true);
            _iCuentaInfraestructura = new CuentaInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.DoesNotThrowAsync(() => _iCuentaInfraestructura.Actualizar(entradaActualizar));
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
            _mockRepositorio.Setup(ent => ent.Eliminar(entradaEliminar.BodyIn.Cuenta)).ReturnsAsync(false);
            _iCuentaInfraestructura = new CuentaInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.ThrowsAsync<CoreNegocioError>(() => _iCuentaInfraestructura.Eliminar(entradaEliminar));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void EliminarTest_DadoUnIdentificador_CuandoExistaElRegistroEnElRepositorio_RetornaVerdadero()
        {
            _mockvalidatorEntradaElimina.Setup(v => v.Validate(entradaEliminar)).Returns(new ValidationResult());
            _mockRepositorio.Setup(ent => ent.Eliminar(entradaEliminar.BodyIn.Cuenta)).ReturnsAsync(true);
            _iCuentaInfraestructura = new CuentaInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.DoesNotThrowAsync(() => _iCuentaInfraestructura.Eliminar(entradaEliminar));
        }
        #endregion

        #endregion TEST

    }
}