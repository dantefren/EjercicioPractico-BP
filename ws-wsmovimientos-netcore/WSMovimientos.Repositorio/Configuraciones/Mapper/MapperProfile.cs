#region Using

using AutoMapper;
using WSMovimientos.Entidades.DTOS;
using WSMovimientos.Entidades.Modelo;

#endregion Using

namespace WSMovimientos.Repositorio.Configuraciones.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region Persona
            CreateMap<BmPersona, PersonaConsulta>()
                .ForMember(dest => dest.Id, orig => orig.MapFrom(src => src.IdPersona))
                .ForMember(dest => dest.Nombre, orig => orig.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Genero, orig => orig.MapFrom(src => src.Genero))
                .ForMember(dest => dest.Edad, orig => orig.MapFrom(src => src.Edad))
                .ForMember(dest => dest.Identificacion, orig => orig.MapFrom(src => src.Identificacion))
                .ForMember(dest => dest.Direccion, orig => orig.MapFrom(src => src.Direccion))
                .ForMember(dest => dest.Telefono, orig => orig.MapFrom(src => src.Telefono));


            CreateMap<PersonaCrea, BmPersona>()
                    .ForMember(dest => dest.Identificacion, orig => orig.MapFrom(src => src.Identificacion));
            #endregion

            #region Cliente
            CreateMap<BmCliente, ClienteConsulta>()
                .ForMember(dest => dest.Id, orig => orig.MapFrom(src => src.IdCliente))
                .ForMember(dest => dest.IdPersona, orig => orig.MapFrom(src => src.IdPersona))
                .ForMember(dest => dest.Persona, orig => orig.MapFrom(src => src.IdPersonaNavigation))
                .ForMember(dest => dest.Contrasenia, orig => orig.MapFrom(src => src.Contrasenia))
                .ForMember(dest => dest.Estado, orig => orig.MapFrom(src => src.Estado));

            CreateMap<ClienteCrea, BmCliente > ()
                     .ForMember(dest => dest.IdPersona, orig => orig.MapFrom(src => src.IdPersona))
                     .ForMember(dest => dest.Contrasenia, orig => orig.MapFrom(src => src.Contrasenia));

            CreateMap<BmCliente, ClienteCrea>()
                     .ForMember(dest => dest.IdPersona, orig => orig.MapFrom(src => src.IdPersona))
                     .ForMember(dest => dest.Contrasenia, orig => orig.MapFrom(src => src.Contrasenia));


            #endregion

            #region Cuenta
            CreateMap<BmCuentum, CuentaConsulta>()
                .ForMember(dest => dest.Id, orig => orig.MapFrom(src => src.IdCuenta))
                .ForMember(dest => dest.IdPersona, orig => orig.MapFrom(src => src.IdPersona))
                .ForMember(dest => dest.Persona, orig => orig.MapFrom(src => src.IdPersonaNavigation))
                .ForMember(dest => dest.NumeroCuenta, orig => orig.MapFrom(src => src.NumeroCuenta))
                .ForMember(dest => dest.TipoCuenta, orig => orig.MapFrom(src => src.TipoCuenta))
                .ForMember(dest => dest.SaldoInicial, orig => orig.MapFrom(src => src.SaldoInicial))
                .ForMember(dest => dest.Estado, orig => orig.MapFrom(src => src.Estado));

            CreateMap<CuentaCrea, BmCuentum>()
                     .ForMember(dest => dest.IdPersona, orig => orig.MapFrom(src => src.IdPersona))
                     .ForMember(dest => dest.NumeroCuenta, orig => orig.MapFrom(src => src.NumeroCuenta))
                     .ForMember(dest => dest.TipoCuenta, orig => orig.MapFrom(src => src.TipoCuenta))
                     .ForMember(dest => dest.SaldoInicial, orig => orig.MapFrom(src => src.SaldoInicial));

            CreateMap<BmCuentum, CuentaCrea>()
                     .ForMember(dest => dest.IdPersona, orig => orig.MapFrom(src => src.IdPersona))
                     .ForMember(dest => dest.NumeroCuenta, orig => orig.MapFrom(src => src.NumeroCuenta))
                     .ForMember(dest => dest.TipoCuenta, orig => orig.MapFrom(src => src.TipoCuenta))
                     .ForMember(dest => dest.SaldoInicial, orig => orig.MapFrom(src => src.SaldoInicial));

            #endregion

            #region Movimiento
            CreateMap<BmMovimiento, MovimientoConsulta>()
                .ForMember(dest => dest.Id, orig => orig.MapFrom(src => src.IdMovimientos))
                .ForMember(dest => dest.IdCuenta, orig => orig.MapFrom(src => src.IdCuenta))
                .ForMember(dest => dest.Cuenta, orig => orig.MapFrom(src => src.IdCuentaNavigation))
                .ForMember(dest => dest.Fecha, orig => orig.MapFrom(src => src.Fecha))
                .ForMember(dest => dest.Tipo, orig => orig.MapFrom(src => src.Tipo))
                .ForMember(dest => dest.Valor, orig => orig.MapFrom(src => src.Valor))
                .ForMember(dest => dest.Saldo, orig => orig.MapFrom(src => src.Saldo));

            CreateMap<MovimientoCrea, BmMovimiento>()
                     .ForMember(dest => dest.IdCuenta, orig => orig.MapFrom(src => src.IdCuenta))
                     .ForMember(dest => dest.Tipo, orig => orig.MapFrom(src => src.Tipo))
                     .ForMember(dest => dest.Valor, orig => orig.MapFrom(src => src.Valor));

  
            CreateMap<BmMovimiento, MovimientoCrea>()
                     .ForMember(dest => dest.IdCuenta, orig => orig.MapFrom(src => src.IdCuenta))
                     .ForMember(dest => dest.Tipo, orig => orig.MapFrom(src => src.Tipo))
                     .ForMember(dest => dest.Valor, orig => orig.MapFrom(src => src.Valor));

            CreateMap<MovimientoCreaCompleto, BmMovimiento>()
                     .ForMember(dest => dest.IdCuenta, orig => orig.MapFrom(src => src.IdCuenta))
                     .ForMember(dest => dest.Tipo, orig => orig.MapFrom(src => src.Tipo))
                     .ForMember(dest => dest.Valor, orig => orig.MapFrom(src => src.Valor))
                     .ForMember(dest => dest.Saldo, orig => orig.MapFrom(src => src.Saldo));


            CreateMap<BmMovimiento, MovimientoCreaCompleto>()
                     .ForMember(dest => dest.IdCuenta, orig => orig.MapFrom(src => src.IdCuenta))
                     .ForMember(dest => dest.Tipo, orig => orig.MapFrom(src => src.Tipo))
                     .ForMember(dest => dest.Valor, orig => orig.MapFrom(src => src.Valor))
                     .ForMember(dest => dest.Saldo, orig => orig.MapFrom(src => src.Saldo));


            CreateMap<MovimientoCreaCompleto, Entidades.DTOS.Movimiento>()
                .ForMember(dest => dest.Tipo, orig => orig.MapFrom(src => src.Tipo))
                .ForMember(dest => dest.Valor, orig => orig.MapFrom(src => src.Valor))
                .ForMember(dest => dest.Saldo, orig => orig.MapFrom(src => src.Saldo));

            CreateMap<Entidades.DTOS.Movimiento, MovimientoCreaCompleto>()
                .ForMember(dest => dest.Tipo, orig => orig.MapFrom(src => src.Tipo))
                .ForMember(dest => dest.Valor, orig => orig.MapFrom(src => src.Valor))
                .ForMember(dest => dest.Saldo, orig => orig.MapFrom(src => src.Saldo));
            #endregion

        }
        
    }
}