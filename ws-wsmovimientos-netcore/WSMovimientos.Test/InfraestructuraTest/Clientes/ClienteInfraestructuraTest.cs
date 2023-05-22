#region Using

using BP.API.Entidades;
using BP.API.Entidades.Excepciones;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using WSMovimientos.Dominio.Clientes;
using WSMovimientos.Dominio.Interfaces.Propiedades;
using WSMovimientos.Entidades.DTOS;
using WSMovimientos.Entidades.DTOS.Entrada;
using WSMovimientos.Infraestructura.Clientes;

#endregion Using

namespace WSMovimientos.Test.InfraestructuraTest.Clientes
{
    public class ClienteInfraestructuraTest
    {
        #region MOCKS

        
        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IPropiedadesApi> _mockIPropiedadesApi = new Mock<IPropiedadesApi>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IClienteRepositorio> _mockRepositorio = new Mock<IClienteRepositorio>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IValidator<EEntrada<EEntradaConsultaCliente>>> _mockValidatorEntradaConsulta = new Mock<IValidator<EEntrada<EEntradaConsultaCliente>>>();
        
        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IValidator<EEntrada<EEntradaCreaCliente>>> _mockValidatorEntradaCrea = new Mock<IValidator<EEntrada<EEntradaCreaCliente>>>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IValidator<EEntrada<EEntradaActualizaCliente>>> _mockvalidatorEntradaActualiza = new Mock<IValidator<EEntrada<EEntradaActualizaCliente>>>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IValidator<EEntrada<EEntradaEliminaCliente>>> _mockvalidatorEntradaElimina = new Mock<IValidator<EEntrada<EEntradaEliminaCliente>>>();

        #endregion MOCKS

        #region Propiedades

        /// <summary>
        /// 
        /// </summary>
        private EEntrada<EEntradaConsultaCliente> entradaConsultar = new EEntrada<EEntradaConsultaCliente>();
        private EEntrada<EEntradaCreaCliente> entradaCrear = new EEntrada<EEntradaCreaCliente>();
        private EEntrada<EEntradaActualizaCliente> entradaActualizar = new EEntrada<EEntradaActualizaCliente>();
        private EEntrada<EEntradaEliminaCliente> entradaEliminar = new EEntrada<EEntradaEliminaCliente>();

        /// <summary>
        /// 
        /// </summary>
        private List<EClienteConsulta> lista = new List<EClienteConsulta>();

        /// <summary>
        /// 
        /// </summary>
        private EClienteConsulta informacionConsulta = new EClienteConsulta();

        /// <summary>
        /// 
        /// </summary>
        private EClienteId identificadorRegistro = new EClienteId();

        /// <summary>
        /// 
        /// </summary>
        private IClientesInfraestructura _iClientesInfraestructura;

        #endregion Propiedades


        #region SETUP

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void Setup()
        {
            string strEntrada = "{\"headerIn\":{\"dispositivo\":\"SOFGHyjguVVZrWyOQhxK2R1ULX3d76XrXpBLVwJj\",\"empresa\":\"0010\",\"canal\":\"02\",\"medio\":\"020007\",\"aplicacion\":\"00664\",\"agencia\":\"0162\",\"tipoTransaccion\":\"201021303\",\"geolocalizacion\":\"0.6814,0.1515\",\"usuario\":\"USINTERT\",\"unicidad\":\"mBMVucAHA4auns86xXqgb8eNJECRUqFTRDvvbQAG\",\"guid\":\"52f66830535f92FFdNVGXBK7Xn1FG8KY\",\"fechaHora\":\"202208111710001063\",\"filler\":\"filler\",\"idioma\":\"es-EC\",\"sesion\":\"TCqnfSnMAlVlrJ2A6KKSpbmfoTra917zDuDe6uqe\",\"ip\":\"10.223.15.237\",\"idCliente\":\"0503364242\",\"tipoIdCliente\":\"0001\"}";
                
            entradaConsultar = JsonConvert.DeserializeObject<EEntrada<EEntradaConsultaCliente>>(strEntrada + ",\"bodyIn\":{\"idCliente\": 1}}}");
            entradaCrear = JsonConvert.DeserializeObject<EEntrada<EEntradaCreaCliente>>(strEntrada + ",\"bodyIn\":{\"cliente\": {\"idPersona\": 1,\"contrasenia\": \"Prueba12345\"}}}}");
            entradaActualizar = JsonConvert.DeserializeObject<EEntrada<EEntradaActualizaCliente>>(strEntrada + ",\"bodyIn\":{\"cliente\": {\"idCliente\": 3,\"contrasenia\": \"Actualiza12345\",\"estado\": true}}}}");
            entradaEliminar = JsonConvert.DeserializeObject<EEntrada<EEntradaEliminaCliente>>(strEntrada + ",\"bodyIn\":{\"cliente\": {\"id\": 3}}}}");


            informacionConsulta = new EClienteConsulta
            {
                Id = 1,
                Estado = true,
                IdPersona = 1,
                Persona = null,
                Contrasenia = "12345678"

            };

            lista.Add(informacionConsulta);
            lista.Add(informacionConsulta);
            lista.Add(informacionConsulta);
            lista.Add(informacionConsulta);


            identificadorRegistro = new EClienteId
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
            _mockRepositorio.Setup(ent => ent.Consultar(entradaConsultar.BodyIn)).ReturnsAsync(new List<Entidades.DTOS.EClienteConsulta>());
            _iClientesInfraestructura = new ClienteInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.ThrowsAsync<CoreNegocioError>(() => _iClientesInfraestructura.Consultar(entradaConsultar));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void ConsultarTest_DadaUnaSolicitudConFiltro_CuandoExistanRegistrosEnElRepositorio_RetornaDatosDeRespuesta()
        {
            _mockValidatorEntradaConsulta.Setup(v => v.Validate(entradaConsultar)).Returns(new ValidationResult());
            _mockRepositorio.Setup(ent => ent.Consultar(entradaConsultar.BodyIn)).ReturnsAsync(lista);
            _iClientesInfraestructura = new ClienteInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.DoesNotThrowAsync(() => _iClientesInfraestructura.Consultar(entradaConsultar));
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
            _mockRepositorio.Setup(ent => ent.Crear(entradaCrear.BodyIn.Cliente)).ReturnsAsync(new Entidades.DTOS.EClienteId());
            _iClientesInfraestructura = new ClienteInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.ThrowsAsync<CoreNegocioError>(() => _iClientesInfraestructura.Crear(entradaCrear));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void CrearTest_DadaUnaRegistro_CuandoRegistraLaInformacionEnElRepositorio_RetornaIdentificador()
        {
            _mockValidatorEntradaCrea.Setup(v => v.Validate(entradaCrear)).Returns(new ValidationResult());
            _mockRepositorio.Setup(ent => ent.Crear(entradaCrear.BodyIn.Cliente)).ReturnsAsync(identificadorRegistro);
            _iClientesInfraestructura = new ClienteInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.DoesNotThrowAsync(() => _iClientesInfraestructura.Crear(entradaCrear));
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
            _mockRepositorio.Setup(ent => ent.Actualizar(entradaActualizar.BodyIn.Cliente)).ReturnsAsync(false);
            _iClientesInfraestructura = new ClienteInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.ThrowsAsync<CoreNegocioError>(() => _iClientesInfraestructura.Actualizar(entradaActualizar));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void ActualizarTest_DadUnRegistro_CuandoRegistraLaInformacionEnElRepositorio_RetornaIdentificador()
        {
            _mockvalidatorEntradaActualiza.Setup(v => v.Validate(entradaActualizar)).Returns(new ValidationResult());
            _mockRepositorio.Setup(ent => ent.Actualizar(entradaActualizar.BodyIn.Cliente)).ReturnsAsync(true);
            _iClientesInfraestructura = new ClienteInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.DoesNotThrowAsync(() => _iClientesInfraestructura.Actualizar(entradaActualizar));
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
            _mockRepositorio.Setup(ent => ent.Eliminar(entradaEliminar.BodyIn.Cliente)).ReturnsAsync(false);
            _iClientesInfraestructura = new ClienteInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.ThrowsAsync<CoreNegocioError>(() => _iClientesInfraestructura.Eliminar(entradaEliminar));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void EliminarTest_DadoUnIdentificador_CuandoExistaElRegistroEnElRepositorio_RetornaVerdadero()
        {
            _mockvalidatorEntradaElimina.Setup(v => v.Validate(entradaEliminar)).Returns(new ValidationResult());
            _mockRepositorio.Setup(ent => ent.Eliminar(entradaEliminar.BodyIn.Cliente)).ReturnsAsync(true);
            _iClientesInfraestructura = new ClienteInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.DoesNotThrowAsync(() => _iClientesInfraestructura.Eliminar(entradaEliminar));
        }
        #endregion

        #endregion TEST

    }
}