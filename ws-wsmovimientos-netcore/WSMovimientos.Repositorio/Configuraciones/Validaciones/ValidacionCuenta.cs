#region Using

using BP.Comun.Extensiones;
using FluentValidation;
using WSMovimientos.Entidades;
using WSMovimientos.Entidades.DTOS;

#endregion Using

namespace WSMovimientos.Repositorio.Configuraciones.Validaciones
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidacionCuenta : AbstractValidator<Entidades.DTOS.Cuenta>
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidacionCuenta()
        {
            RuleFor(eEntidad => eEntidad)
                .Must(eEntidad => !eEntidad.Estado.IsNull()).WithMessage(string.Format(EConstantes.ErrorCode1DescripcionNuloVacio, "Estado")).WithErrorCode(EConstantes.ErrorCode1);
                
           }
    }
}