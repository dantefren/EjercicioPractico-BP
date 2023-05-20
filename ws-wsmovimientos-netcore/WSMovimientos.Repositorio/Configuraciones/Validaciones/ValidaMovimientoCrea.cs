#region Using

using BP.Comun.Extensiones;
using FluentValidation;
using WSMovimientos.Entidades;
using WSMovimientos.Entidades.DTOS;

#endregion Using

namespace WSMovimientos.Repositorio.Configuraciones.Validaciones
{
    public class ValidaMovimientoCrea : AbstractValidator<EMovimientoCrea>
    {
        public ValidaMovimientoCrea()
        {

            RuleFor(eEntidad => eEntidad)
                .Must(eEntidad => !eEntidad.IdCuenta.IsNull()).WithMessage(string.Format(EConstantes.ErrorCode1DescripcionNuloVacio, "IdCuenta")).WithErrorCode(EConstantes.ErrorCode1)
                .Must(eEntidad => !eEntidad.Tipo.IsNull()).WithMessage(string.Format(EConstantes.ErrorCode1DescripcionNuloVacio, "Tipo")).WithErrorCode(EConstantes.ErrorCode1)
                .Must(eEntidad => !eEntidad.Valor.IsNull()).WithMessage(string.Format(EConstantes.ErrorCode1DescripcionNuloVacio, "Valor")).WithErrorCode(EConstantes.ErrorCode1)
                .Must(eEntidad => !(eEntidad.Valor <= 0)).WithMessage(string.Format(EConstantes.ErrorCode1DescripcionNuloVacio, "Valor")).WithErrorCode(EConstantes.ErrorCode1);

            RuleFor(eEntidad => (eEntidad.Tipo.Equals("DEP") || eEntidad.Tipo.Equals("RET") ? true:false)).Equal(true).WithMessage(string.Format(EConstantes.ErrorCodeTipoDescripcion, "Tipo")).WithErrorCode(EConstantes.ErrorCodeTipo);

    }
    }
}