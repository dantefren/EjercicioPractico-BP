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
    public class PersonaInfraestructuraTest
    {
        #region MOCKS

        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IValidator<EEntrada<EntradaConsultaPersona>>> _mockValidatorEntradaConsulta = new Mock<IValidator<EEntrada<EntradaConsultaPersona>>>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IPropiedadesApi> _mockIPropiedadesApi = new Mock<IPropiedadesApi>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IPersonaRepositorio> _mockPersonaRepositorio = new Mock<IPersonaRepositorio>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IValidator<EEntrada<EntradaCreaPersona>>> _mockValidatorEntradaCrea = new Mock<IValidator<EEntrada<EntradaCreaPersona>>>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IValidator<EEntrada<EntradaActualizaPersona>>> _mockvalidatorEntradaActualiza = new Mock<IValidator<EEntrada<EntradaActualizaPersona>>>();

        /// <summary>
        /// 
        /// </summary>
        private readonly Mock<IValidator<EEntrada<EntradaEliminaPersona>>> _mockvalidatorEntradaElimina = new Mock<IValidator<EEntrada<EntradaEliminaPersona>>>();

        #endregion MOCKS

        #region Propiedades

        /// <summary>
        /// 
        /// </summary>
        private EEntrada<EntradaConsultaPersona> entrada = new EEntrada<EntradaConsultaPersona>();

        /// <summary>
        /// 
        /// </summary>
        private List<PersonaConsulta> lstPersona = new List<PersonaConsulta>();

        /// <summary>
        /// 
        /// </summary>
        private PersonaConsulta personaInfo = new PersonaConsulta();

        /// <summary>
        /// 
        /// </summary>
        private PersonaId personaId = new PersonaId();

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
            string strEntrada = "{\"headerIn\":{\"dispositivo\":\"SOFGHyjguVVZrWyOQhxK2R1ULX3d76XrXpBLVwJj\",\"empresa\":\"0010\",\"canal\":\"02\",\"medio\":\"020007\",\"aplicacion\":\"00664\",\"agencia\":\"0162\",\"tipoTransaccion\":\"201021303\",\"geolocalizacion\":\"0.6814,0.1515\",\"usuario\":\"USINTERT\",\"unicidad\":\"mBMVucAHA4auns86xXqgb8eNJECRUqFTRDvvbQAG\",\"guid\":\"52f66830535f92FFdNVGXBK7Xn1FG8KY\",\"fechaHora\":\"202208111710001063\",\"filler\":\"filler\",\"idioma\":\"es-EC\",\"sesion\":\"TCqnfSnMAlVlrJ2A6KKSpbmfoTra917zDuDe6uqe\",\"ip\":\"10.223.15.237\",\"idCliente\":\"0503364242\",\"tipoIdCliente\":\"0001\"},\"bodyIn\":{\"filtro\":0,\"ordenante\":{\"identificacion\":\"1717720872\",\"tipoIdentificacion\":\"0001\"}}}";
            entrada = JsonConvert.DeserializeObject<EEntrada<EntradaConsultaPersona>>(strEntrada);


            personaInfo = new PersonaConsulta
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


            personaId = new PersonaId
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
        public void ConsultarPorOrdenanteAsyncTest_DadaUnCliente_CuandoNoExistaDatosDelCLiente_RetornaUnaExepcion()
        {
            _mockValidatorEntradaConsulta.Setup(v => v.Validate(entrada)).Returns(new ValidationResult());
            _mockPersonaRepositorio.Setup(ent => ent.Consulta(entrada.BodyIn)).ReturnsAsync(new List<Entidades.DTOS.PersonaConsulta>());
            _iPersonaInfraestructura = new PersonaInfraestructura(_mockIPropiedadesApi.Object, _mockPersonaRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.ThrowsAsync<CoreNegocioError>(() => _iPersonaInfraestructura.Consulta(entrada));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void ConsultarPorOrdenanteAsyncTest_DadaUnaSolicitudDeContatosConFiltroDeEstado_CuandoExistanRegistrosEnElRepositorio_RetornaDatosDelCliente()
        {
            _mockValidatorEntradaConsulta.Setup(v => v.Validate(entrada)).Returns(new ValidationResult());
            _mockPersonaRepositorio.Setup(ent => ent.Consulta(entrada.BodyIn)).ReturnsAsync(lstPersona);
            _iPersonaInfraestructura = new PersonaInfraestructura(_mockIPropiedadesApi.Object, _mockPersonaRepositorio.Object, _mockValidatorEntradaConsulta.Object, _mockValidatorEntradaCrea.Object, _mockvalidatorEntradaActualiza.Object, _mockvalidatorEntradaElimina.Object);
            Assert.DoesNotThrowAsync(() => _iPersonaInfraestructura.Consulta(entrada));
        }
        #endregion

        #endregion TEST

    }
}