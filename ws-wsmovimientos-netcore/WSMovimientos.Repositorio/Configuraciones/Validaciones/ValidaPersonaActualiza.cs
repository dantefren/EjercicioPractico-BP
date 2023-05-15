#region Using

using BP.Comun.Extensiones;
using FluentValidation;
using WSMovimientos.Entidades;
using WSMovimientos.Entidades.DTOS;

#endregion Using

namespace WSMovimientos.Repositorio.Configuraciones.Validaciones
{
    public class ValidaPersonaActualiza : AbstractValidator<PersonaActualiza>
    {
        public ValidaPersonaActualiza()
        {

            RuleFor(ePersona => ePersona)
                .Must(ePersona => !ePersona
                .IsNull()).WithErrorCode(EConstantes.ErrorCode1).WithMessage(string.Format(EConstantes.ErrorCode1DescripcionNuloVacio, "Persona"));

            RuleFor(ePersona => ePersona.Nombre).Length(10, 150).WithErrorCode(EConstantes.ErrorCode2).WithMessage(string.Format(EConstantes.ErrorCode2DescripcionFueraRango, "Nombre"));
            RuleFor(ePersona => ePersona.Genero).Length(1).WithErrorCode(EConstantes.ErrorCode2).WithMessage(string.Format(EConstantes.ErrorCode2DescripcionFueraRango, "Genero"));
            RuleFor(ePersona => ePersona.Identificacion).Length(10, 15).WithErrorCode(EConstantes.ErrorCode2).WithMessage(string.Format(EConstantes.ErrorCode2DescripcionFueraRango, "Identificación"));
            RuleFor(ePersona => ePersona.Direccion).Length(16, 250).WithErrorCode(EConstantes.ErrorCode2).WithMessage(string.Format(EConstantes.ErrorCode2DescripcionFueraRango, "Dirección"));

        }
    }
}