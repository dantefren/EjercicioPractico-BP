#region Using

using BP.Comun.Extensiones;
using FluentValidation;
using WSMovimientos.Entidades;
using WSMovimientos.Entidades.DTOS;

#endregion Using

namespace WSMovimientos.Repositorio.Configuraciones.Validaciones
{
    public class ValidaCuentaCrea : AbstractValidator<ECuentaCrea>
    {
        public ValidaCuentaCrea()
        {

            RuleFor(eEntidad => eEntidad)
                .Must(eEntidad => !eEntidad.NumeroCuenta.IsNull()).WithMessage(string.Format(EConstantes.ErrorCode1DescripcionNuloVacio, "NumeroCuenta")).WithErrorCode(EConstantes.ErrorCode1)
                .Must(eEntidad => !eEntidad.IdPersona.IsNull()).WithMessage(string.Format(EConstantes.ErrorCode1DescripcionNuloVacio, "IdPersona")).WithErrorCode(EConstantes.ErrorCode1)
                .Must(eEntidad => !eEntidad.TipoCuenta.IsNull()).WithMessage(string.Format(EConstantes.ErrorCode1DescripcionNuloVacio, "TipoCuenta")).WithErrorCode(EConstantes.ErrorCode1);

            RuleFor(eEntidad => eEntidad.NumeroCuenta.ToString()).Length(6).WithMessage(string.Format(EConstantes.ErrorCode2DescripcionFueraRango, "NumeroCuenta")).WithErrorCode(EConstantes.ErrorCode2);
            RuleFor(eEntidad => eEntidad.TipoCuenta).Length(3).WithMessage(string.Format(EConstantes.ErrorCode2DescripcionFueraRango, "TipoCuenta")).WithErrorCode(EConstantes.ErrorCode2);

        }
    }
}