#region Using

using BP.API.Entidades;
using BP.Comun.Extensiones;
using FluentValidation;
using WSMovimientos.Entidades;
using WSMovimientos.Entidades.DTOS.Entrada;

#endregion Using

namespace WSMovimientos.Repositorio.Configuraciones.Validaciones
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidaEntradaCreaCliente : AbstractValidator<EEntrada<EEntradaCreaCliente>>
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidaEntradaCreaCliente()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(entrada => entrada.HeaderIn).NotNull().WithMessage(EConstantes.HeaderInNullDescription).WithErrorCode(EConstantes.HeaderInNullCode);
            When(entrada => entrada.HeaderIn.IsNotNull(), () =>
            {
                RuleFor(entrada => entrada.HeaderIn).SetValidator(new ValidaHeaderIn());
            });
            RuleFor(entrada => entrada.BodyIn).NotNull().WithMessage(EConstantes.BodyNullDescription).WithErrorCode(EConstantes.BodyNullCode);
            When(entrada => entrada.BodyIn.IsNotNull(), () =>
            {
                RuleFor(entrada => entrada.BodyIn.Cliente).SetValidator(new ValidaClienteCrea());
            });
        }
    }
}