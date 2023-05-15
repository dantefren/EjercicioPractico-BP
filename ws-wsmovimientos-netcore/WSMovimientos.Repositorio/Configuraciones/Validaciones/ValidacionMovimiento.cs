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
    public class ValidacionMovimiento : AbstractValidator<Entidades.DTOS.Movimiento>
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidacionMovimiento()
        {
            RuleFor(eEntidad => eEntidad)
                .Must(eEntidad => !eEntidad.Tipo.IsNull()).WithMessage(string.Format(EConstantes.ErrorCode1DescripcionNuloVacio, "Estado")).WithErrorCode(EConstantes.ErrorCode1)
                .Must(eEntidad => !(eEntidad.Valor == 0)).WithMessage(string.Format(EConstantes.ErrorCode1DescripcionNuloVacio, "Valor")).WithErrorCode(EConstantes.ErrorCode1);
                
           }
    }
}