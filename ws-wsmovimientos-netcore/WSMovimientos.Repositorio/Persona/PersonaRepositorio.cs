#region Using

using AutoMapper;
using BP.API.Entidades.Excepciones;
using BP.Comun.Extensiones;
using BP.Comun.Logs.Base.Handlers;
using BP.Functional;
using Microsoft.EntityFrameworkCore;
using WSMovimientos.Dominio.Personas;
using WSMovimientos.Dominio.Interfaces.Propiedades;
using WSMovimientos.Entidades;
using WSMovimientos.Entidades.DTOS;
using WSMovimientos.Entidades.DTOS.Entrada;
using WSMovimientos.Entidades.Modelo;
using WSMovimientos.Repositorio.Configuraciones.Context;

#endregion Using

namespace WSMovimientos.Repositorio.Persona
{
    public class PersonaRepositorio : IPersonaRepositorio
    {
        #region ReadOnly

        private readonly IPropiedadesApi _iPropiedadesApi;
        private readonly BddContext _iBddContext;
        private readonly IMapper _mapper;

        #endregion ReadOnly

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iPropiedadesApi"></param>
        /// <param name="iBddContext"></param>
        /// <param name="mapper"></param>
        public PersonaRepositorio(IPropiedadesApi iPropiedadesApi, BddContext iBddContext, IMapper mapper)
        {
            _iPropiedadesApi = iPropiedadesApi;
            _iBddContext = iBddContext;
            _mapper = mapper;

        }





        #endregion Constructor

        #region Methods

        #region Consulta 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entradaConsultaPersona"></param>
        /// <returns></returns>
        [Loggable]
        public async Task<List<EPersonaConsulta>> Consultar(EEntradaConsultaPersona entradaConsultaPersona)
        {
            try
            {
                var resultado = await _iBddContext.BmPersonas
               .Where(o => o.Identificacion == entradaConsultaPersona.Identificacion).ToListAsync();
                return _mapper.Map<List<EPersonaConsulta>>(resultado);
            }
            catch (Exception ex)
            {
                throw new CoreExcepcion(EConstantes.ValExpCodigo, this.GetFirstName(), EConstantes.movimientos, _iPropiedadesApi.BackendOpenShift(), ex);
            }


        }
        #endregion Consulta

        #region Crea 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personaCrea"></param>
        /// <returns></returns>
        /// <exception cref="CoreExcepcion"></exception>
        [Loggable]
        public async Task<EPersonaId> Crear(EPersonaCrea personaCrea)
        {
            try
            {

                var bmPersona = _mapper.Map<BmPersona>(personaCrea);
                await _iBddContext.BmPersonas.AddAsync(bmPersona);
                await _iBddContext.SaveChangesAsync();
                return new EPersonaId
                {

                    Id = bmPersona.IdPersona

                };

            }
            catch (Exception ex)
            {

                throw new CoreExcepcion(EConstantes.ValExpCodigo, this.GetFirstName(), EConstantes.crear, _iPropiedadesApi.BackendOpenShift(), ex);
            }


        }

        #endregion Crea

        #region Actualiza

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personaActualiza"></param>
        /// <returns></returns>
        /// <exception cref="CoreExcepcion"></exception>
        [Loggable]
        public async Task<bool> Actualizar(EPersonaActualiza personaActualiza)
        {
            try
            {
                var bmPersona = await _iBddContext.BmPersonas.FirstOrDefaultAsync(item => item.IdPersona == personaActualiza.Id);
                if (bmPersona.IsNull()) return false;

                bmPersona = _mapper.Map<BmPersona>(personaActualiza);
                await _iBddContext.BmPersonas.AddAsync(bmPersona);
                await _iBddContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw new CoreExcepcion(EConstantes.ValExpCodigo, this.GetFirstName(), EConstantes.actualizar, _iPropiedadesApi.BackendOpenShift(), ex);
            }
        }

        #endregion Actualiza 

        #region Elimina
        public async Task<bool> Eliminar(EPersonaElimina personaElimina)
        {
            try
            {
                var bmPersona = await _iBddContext.BmPersonas.FirstOrDefaultAsync(item => item.IdPersona == personaElimina.Id && item.Identificacion == personaElimina.Identificacion);
                if (bmPersona.IsNull()) return false;

                //DANILO: SE RECOMIENDA HACER SOLO ELIMINACION LOGICA 11/05/2023
                _iBddContext.BmPersonas.Remove(bmPersona); 
                await _iBddContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw new CoreExcepcion(EConstantes.ValExpCodigo, this.GetFirstName(), EConstantes.eliminar, _iPropiedadesApi.BackendOpenShift(), ex);
            }
        }
        #endregion Elimina
    }



}

#endregion Methods




