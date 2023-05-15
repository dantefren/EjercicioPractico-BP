#region USING

using BP.API.Entidades;
using BP.Comun.Logs;
using FluentValidation;
using Microsoft.OpenApi.Models;
using WSMovimientos.Dominio.Clientes;
using WSMovimientos.Dominio.Cuentas;
using WSMovimientos.Dominio.Interfaces.Propiedades;
using WSMovimientos.Dominio.Movimientos;
using WSMovimientos.Dominio.Personas;
using WSMovimientos.Entidades.DTOS.Entrada;
using WSMovimientos.Infraestructura.Clientes;
using WSMovimientos.Infraestructura.Cuentas;
using WSMovimientos.Infraestructura.Movimientos;
using WSMovimientos.Infraestructura.Personas;
using WSMovimientos.Repositorio.Cliente;
using WSMovimientos.Repositorio.Configuraciones.Api;
using WSMovimientos.Repositorio.Configuraciones.Context;
using WSMovimientos.Repositorio.Configuraciones.Validaciones;
using WSMovimientos.Repositorio.Cuenta;
using WSMovimientos.Repositorio.Movimiento;
using WSMovimientos.Repositorio.Persona;


#endregion

#region MAIN API

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

#region REPOSITORIOS

builder.Services.AddTransient<IPersonaRepositorio, PersonaRepositorio>();
builder.Services.AddTransient<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddTransient<ICuentaRepositorio, CuentaRepositorio>();
builder.Services.AddTransient<IMovimientoRepositorio, MovimientoRepositorio>();

builder.Services.AddDbContext<BddContext>();
builder.Services.AddScoped<IValidator<EEntrada<EntradaConsultaPersona>>, ValidaEntradaConsultaPersona>();
builder.Services.AddScoped<IValidator<EEntrada<EntradaCreaPersona>>, ValidaEntradaCreaPersona>();
builder.Services.AddScoped<IValidator<EEntrada<EntradaActualizaPersona>>, ValidaEntradaActualizaPersona>();
builder.Services.AddScoped<IValidator<EEntrada<EntradaEliminaPersona>>, ValidaEntradaEliminaPersona>();

builder.Services.AddScoped<IValidator<EEntrada<EntradaConsultaCliente>>, ValidaEntradaConsultaCliente>();
builder.Services.AddScoped<IValidator<EEntrada<EntradaCreaCliente>>, ValidaEntradaCreaCliente>();
builder.Services.AddScoped<IValidator<EEntrada<EntradaActualizaCliente>>, ValidaEntradaActualizaCliente>();
builder.Services.AddScoped<IValidator<EEntrada<EntradaEliminaCliente>>, ValidaEntradaEliminaCliente>();

builder.Services.AddScoped<IValidator<EEntrada<EntradaConsultaCuenta>>, ValidaEntradaConsultaCuenta>();
builder.Services.AddScoped<IValidator<EEntrada<EntradaCreaCuenta>>, ValidaEntradaCreaCuenta>();
builder.Services.AddScoped<IValidator<EEntrada<EntradaActualizaCuenta>>, ValidaEntradaActualizaCuenta>();
builder.Services.AddScoped<IValidator<EEntrada<EntradaEliminaCuenta>>, ValidaEntradaEliminaCuenta>();

builder.Services.AddScoped<IValidator<EEntrada<EntradaConsultaMovimiento>>, ValidaEntradaConsultaMovimiento>();
builder.Services.AddScoped<IValidator<EEntrada<EntradaCreaMovimiento>>, ValidaEntradaCreaMovimiento>();
builder.Services.AddScoped<IValidator<EEntrada<EntradaActualizaMovimiento>>, ValidaEntradaActualizaMovimiento>();
builder.Services.AddScoped<IValidator<EEntrada<EntradaEliminaMovimiento>>, ValidaEntradaEliminaMovimiento>();

builder.Services.AddScoped<IValidator<EEntrada<EntradaConsultaMovimientoCuenta>>, ValidaEntradaConsultaMovimientoCuenta>();
#endregion REPOSITORIOS

#region INFRAESTRUCTURA
builder.Services.AddTransient<IPersonaInfraestructura, PersonaInfraestructura>();
builder.Services.AddTransient<IClientesInfraestructura, ClienteInfraestructura>();
builder.Services.AddTransient<ICuentaInfraestructura, CuentaInfraestructura>();
builder.Services.AddTransient<IMovimientoInfraestructura, MovimientoInfraestructura>();
#endregion INFRAESTRUCTURA

#region CONFIGURADORES PROPIEDADES

builder.Services.AddSingleton<IPropiedadesApi, PropiedadesApi>();

#endregion

#region POLITICA PARA DOMINIO CRUZADO 

builder.Services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                   .AllowAnyMethod()
                                                   .AllowAnyHeader()));
#endregion POLITICA PARA DOMINIO CRUZADO

#region SWAGGER HEADER

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.1.0",
        Title = "WSMovimientos",
        Description = "Servicio base para invocar servicios externos",
        Contact = new OpenApiContact
        {
            Name = "Banco Pichincha",
            Email = "devops@pichincha.com",
            Url = new Uri("https://www.pichincha.com"),
        }
    });

});

#endregion SWAGGER

#region MANEJO DE VERSIONES DE API

builder.Services.AddApiVersioning(options => options.UseApiBehavior = true);
builder.Services.AddApiVersioning(options => options.AssumeDefaultVersionWhenUnspecified = true);

#endregion MANEJO DE VERSIONES DE API FIN

#region AutoMapper

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#endregion AutoMapper

#region INICIALIZAR COMPONENTE DE LOGS

builder.Services.AddLogs();

#endregion INICIALIZAR COMPONENTE DE LOGS

builder.Services.AddHealthChecks();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{

}
app.UseHealthChecks("/api/v1/WSMovimientos/HealthChecks");
app.UseHealthChecks("/api/v1/WSMovimientos/HealthChecksServicio");

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowAll");

#region SWAGGER DOCUMENT

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WSMovimientos.API V1");
});
#endregion Swagger

app.MapControllers();
app.Run();