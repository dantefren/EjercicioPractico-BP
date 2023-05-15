#region Using

using BP.Comun.Extensiones;
using FluentValidation;
using WSMovimientos.Entidades;

#endregion Using

namespace WSMovimientos.Repositorio.Configuraciones.Validaciones
{
    public class ValidaPersonaCrea : AbstractValidator<Entidades.DTOS.PersonaCrea>
    {
        public ValidaPersonaCrea()
        {

            RuleFor(eOrdenate => eOrdenate)
                .Must(eOrdenate => !eOrdenate.Identificacion.IsNullEmpty()).WithMessage(string.Format(EConstantes.ErrorCode1DescripcionNuloVacio, "identificacion")).WithErrorCode(EConstantes.ErrorCode1);

            RuleFor(eOrdenate => eOrdenate.Identificacion).Length(10, 13).WithMessage(string.Format(EConstantes.ErrorCode2DescripcionFueraRango, "identificacion")).WithErrorCode(EConstantes.ErrorCode2);
            RuleFor(ePersona => ePersona.Nombre).Length(10, 150).WithErrorCode(EConstantes.ErrorCode2).WithMessage(string.Format(EConstantes.ErrorCode2DescripcionFueraRango, "Nombre"));
            RuleFor(ePersona => ePersona.Genero).Length(1).WithErrorCode(EConstantes.ErrorCode2).WithMessage(string.Format(EConstantes.ErrorCode2DescripcionFueraRango, "Genero"));
            RuleFor(ePersona => ePersona.Identificacion).Length(10, 15).WithErrorCode(EConstantes.ErrorCode2).WithMessage(string.Format(EConstantes.ErrorCode2DescripcionFueraRango, "Identificación"));
            RuleFor(ePersona => ePersona.Direccion).Length(16, 250).WithErrorCode(EConstantes.ErrorCode2).WithMessage(string.Format(EConstantes.ErrorCode2DescripcionFueraRango, "Dirección"));
     }
    }
}