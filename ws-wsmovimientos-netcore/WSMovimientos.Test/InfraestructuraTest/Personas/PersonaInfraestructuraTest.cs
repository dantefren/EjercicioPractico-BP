#region Using

using BP.API.Entidades;
using BP.API.Entidades.Excepciones;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using WSMovimientos.Dominio.Personas;
using WSMovimientos.Dominio.Interfaces.Propiedades;
using WSMovimientos.Entidades.DTOS;
using WSMovimientos.Entidades.DTOS.Entrada;
using WSMovimientos.Infraestructura.Personas;

#endregion Using

namespace WSMovimientos.Test.InfraestructuraTest.Personas
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
        private readonly Mock<IPersonaRepositorio> _mockRepositorio = new Mock<IPersonaRepositorio>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IValidator<EEntrada<EEntradaConsultaPersona>>> _mockValidatorEntradaConsulta = new Mock<IValidator<EEntrada<EEntradaConsultaPersona>>>();
        
        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IValidator<EEntrada<EEntradaCreaPersona>>> _mockValidatorEntradaCrea = new Mock<IValidator<EEntrada<EEntradaCreaPersona>>>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IValidator<EEntrada<EEntradaActualizaPersona>>> _mockvalidatorEntradaActualiza = new Mock<IValidator<EEntrada<EEntradaActualizaPersona>>>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IValidator<EEntrada<EEntradaEliminaPersona>>> _mockvalidatorEntradaElimina = new Mock<IValidator<EEntrada<EEntradaEliminaPersona>>>();

        #endregion MOCKS

        #region Propiedades

        /// <summary>
        /// 
        /// </summary>
        private EEntrada<EEntradaConsultaPersona> entradaConsultar = new EEntrada<EEntradaConsultaPersona>();
        private EEntrada<EEntradaCreaPersona> entradaCrear = new EEntrada<EEntradaCreaPersona>();
        private EEntrada<EEntradaActualizaPersona> entradaActualizar = new EEntrada<EEntradaActualizaPersona>();
        private EEntrada<EEntradaEliminaPersona> entradaEliminar = new EEntrada<EEntradaEliminaPersona>();

        /// <summary>
        /// 
        /// </summary>
        private List<EPersonaConsulta> lstPersona = new List<EPersonaConsulta>();

        /// <summary>
        /// 
        /// </summary>
        private EPersonaConsulta personaInfo = new EPersonaConsulta();

        /// <summary>
        /// 
        /// </summary>
        private EPersonaId personaId = new EPersonaId();

        /// <summary>
        /// 
        /// </summary>
        private IPersonaInfraestructura _iPersonaInfraestructura;

        #endregion Propiedades


        #region SETUP

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void Setup()
        {
            string strEntrada = "{\"headerIn\":{\"dispositivo\":\"SOFGHyjguVVZrWyOQhxK2R1ULX3d76XrXpBLVwJj\",\"empresa\":\"0010\",\"canal\":\"02\",\"medio\":\"020007\",\"aplicacion\":\"00664\",\"agencia\":\"0162\",\"tipoTransaccion\":\"201021303\",\"geolocalizacion\":\"0.6814,0.1515\",\"usuario\":\"USINTERT\",\"unicidad\":\"mBMVucAHA4auns86xXqgb8eNJECRUqFTRDvvbQAG\",\"guid\":\"52f66830535f92FFdNVGXBK7Xn1FG8KY\",\"fechaHora\":\"202208111710001063\",\"filler\":\"filler\",\"idioma\":\"es-EC\",\"sesion\":\"TCqnfSnMAlVlrJ2A6KKSpbmfoTra917zDuDe6uqe\",\"ip\":\"10.223.15.237\",\"idCliente\":\"0503364242\",\"tipoIdCliente\":\"0001\"}";
                
            entradaConsultar = JsonConvert.DeserializeObject<EEntrada<EEntradaConsultaPersona>>(strEntrada + ",\"bodyIn\":{\"identificacion\":\"1717720872\"}}}");
            entradaCrear = JsonConvert.DeserializeObject<EEntrada<EEntradaCreaPersona>>(strEntrada + ",\"bodyIn\":{\"identificacion\":\"1717720872\",\"edad\":30, \"genero\":\"M\", \"direccion\":\"Monjas\", \"nombre\":\"Danilo\", \"telefono\":2601583}}}");
            entradaActualizar = JsonConvert.DeserializeObject<EEntrada<EEntradaActualizaPersona>>(strEntrada + ",\"bodyIn\":{\"id\":1, \"identificacion\":\"1717720872\",\"edad\":30, \"genero\":\"M\", \"direccion\":\"Monjas\", \"nombre\":\"Danilo\", \"telefono\":2601583}}}");
            entradaEliminar = JsonConvert.DeserializeObject<EEntrada<EEntradaEliminaPersona>>(strEntrada + ",\"bodyIn\":{\"id\":1}}}");


            personaInfo = new EPersonaConsulta
            {
                Identificacion = "17025799728",
                Edad = 30,
                Genero = "M",
                Direccion = "Monjas",
                Id = 1,
                Nombre = "Danilo",
                Telefono = 2601538

            };

            lstPersona.Add(personaInfo);
            lstPersona.Add(personaInfo);
            lstPersona.Add(personaInfo);
            lstPersona.Add(personaInfo);


            personaId = new EPersonaId
            {
                Id = 2,
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
            _mockRepositorio.Setup(ent => ent.Consultar(entradaConsultar.BodyIn)).ReturnsAsync(new List<Entidades.DTOS.EPersonaConsulta>());
            _iPersonaInfraestructura = new PersonaInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.ThrowsAsync<CoreNegocioError>(() => _iPersonaInfraestructura.Consultar(entradaConsultar));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void ConsultarTest_DadaUnaSolicitudConFiltro_CuandoExistanRegistrosEnElRepositorio_RetornaDatosDeRespuesta()
        {
            _mockValidatorEntradaConsulta.Setup(v => v.Validate(entradaConsultar)).Returns(new ValidationResult());
            _mockRepositorio.Setup(ent => ent.Consultar(entradaConsultar.BodyIn)).ReturnsAsync(lstPersona);
            _iPersonaInfraestructura = new PersonaInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.DoesNotThrowAsync(() => _iPersonaInfraestructura.Consultar(entradaConsultar));
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
            _mockRepositorio.Setup(ent => ent.Crear(entradaCrear.BodyIn.Persona)).ReturnsAsync(new Entidades.DTOS.EPersonaId());
            _iPersonaInfraestructura = new PersonaInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.ThrowsAsync<CoreNegocioError>(() => _iPersonaInfraestructura.Crear(entradaCrear));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void CrearTest_DadaUnaRegistro_CuandoRegistraLaInformacionEnElRepositorio_RetornaIdentificador()
        {
            _mockValidatorEntradaCrea.Setup(v => v.Validate(entradaCrear)).Returns(new ValidationResult());
            _mockRepositorio.Setup(ent => ent.Crear(entradaCrear.BodyIn.Persona)).ReturnsAsync(personaId);
            _iPersonaInfraestructura = new PersonaInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.DoesNotThrowAsync(() => _iPersonaInfraestructura.Crear(entradaCrear));
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
            _mockRepositorio.Setup(ent => ent.Actualizar(entradaActualizar.BodyIn.Persona)).ReturnsAsync(false);
            _iPersonaInfraestructura = new PersonaInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.ThrowsAsync<CoreNegocioError>(() => _iPersonaInfraestructura.Actualizar(entradaActualizar));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void ActualizarTest_DadUnRegistro_CuandoRegistraLaInformacionEnElRepositorio_RetornaIdentificador()
        {
            _mockvalidatorEntradaActualiza.Setup(v => v.Validate(entradaActualizar)).Returns(new ValidationResult());
            _mockRepositorio.Setup(ent => ent.Actualizar(entradaActualizar.BodyIn.Persona)).ReturnsAsync(true);
            _iPersonaInfraestructura = new PersonaInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.DoesNotThrowAsync(() => _iPersonaInfraestructura.Actualizar(entradaActualizar));
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
            _mockRepositorio.Setup(ent => ent.Eliminar(entradaEliminar.BodyIn.Persona)).ReturnsAsync(false);
            _iPersonaInfraestructura = new PersonaInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.ThrowsAsync<CoreNegocioError>(() => _iPersonaInfraestructura.Eliminar(entradaEliminar));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void EliminarTest_DadoUnIdentificador_CuandoExistaElRegistroEnElRepositorio_RetornaVerdadero()
        {
            _mockvalidatorEntradaElimina.Setup(v => v.Validate(entradaEliminar)).Returns(new ValidationResult());
            _mockRepositorio.Setup(ent => ent.Eliminar(entradaEliminar.BodyIn.Persona)).ReturnsAsync(true);
            _iPersonaInfraestructura = new PersonaInfraestructura(_mockIPropiedadesApi.Object, _mockRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.DoesNotThrowAsync(() => _iPersonaInfraestructura.Eliminar(entradaEliminar));
        }
        #endregion

        #endregion TEST

    }
}