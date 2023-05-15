#region Using

using BP.Comun.Extensiones;
using FluentValidation;
using WSMovimientos.Entidades;
using WSMovimientos.Entidades.DTOS;

#endregion

namespace WSMovimientos.Repositorio.Configuraciones.Validaciones
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidaCuentaElimina : AbstractValidator<CuentaElimina>
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidaCuentaElimina()
        {

            RuleFor(eEntidad => eEntidad)
               .Must(eEntidad => !eEntidad
               .IsNull()).WithErrorCode(EConstantes.ErrorCode1).WithMessage(string.Format(EConstantes.ErrorCode1DescripcionNuloVacio, "Cuenta"));

        }
    }
}