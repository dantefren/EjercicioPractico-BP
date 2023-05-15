#region Using

using BP.Comun.Extensiones;
using FluentValidation;
using WSMovimientos.Entidades;
using WSMovimientos.Entidades.DTOS;

#endregion Using

namespace WSMovimientos.Repositorio.Configuraciones.Validaciones
{
    public class ValidaMovimientoActualiza : AbstractValidator<MovimientoActualiza>
    {
        public ValidaMovimientoActualiza()
        {

            RuleFor(eEntidad => eEntidad)
                .Must(eEntidad => !eEntidad
                .IsNull()).WithErrorCode(EConstantes.ErrorCode1).WithMessage(string.Format(EConstantes.ErrorCode1DescripcionNuloVacio, "Cuenta"))
                .Must(eEntidad => !eEntidad.Tipo.IsNull()).WithMessage(string.Format(EConstantes.ErrorCode1DescripcionNuloVacio, "Estado")).WithErrorCode(EConstantes.ErrorCode1);

                  
        }
    }
}