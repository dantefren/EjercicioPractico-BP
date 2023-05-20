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
    public class ValidacionPersona : AbstractValidator<Entidades.DTOS.EPersona>
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidacionPersona()
        {
            RuleFor(eOrdenate => eOrdenate)
                .Must(eOrdenate => !eOrdenate.Identificacion.IsNullEmpty()).WithMessage(string.Format(EConstantes.ErrorCode1DescripcionNuloVacio, "identificacion")).WithErrorCode(EConstantes.ErrorCode1);

            RuleFor(eOrdenate => eOrdenate.Identificacion).Length(10, 13).WithMessage(string.Format(EConstantes.ErrorCode2DescripcionFueraRango, "identificacion")).WithErrorCode(EConstantes.ErrorCode2);
            RuleFor(eOrdenate => eOrdenate.Identificacion).Matches(EConstantes.identificacionCode9Expresion).WithMessage(string.Format(EConstantes.ErrorCode9SoloNumero, "identificacion")).WithErrorCode(EConstantes.ErrorCode9);
        }
    }
}