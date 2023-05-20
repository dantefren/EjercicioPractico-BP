#region Using

using BP.Comun.Extensiones;
using FluentValidation;
using WSMovimientos.Entidades;
using WSMovimientos.Entidades.DTOS;

#endregion Using

namespace WSMovimientos.Repositorio.Configuraciones.Validaciones
{
    public class ValidaClienteActualiza : AbstractValidator<EClienteActualiza>
    {
        public ValidaClienteActualiza()
        {

            RuleFor(eEntidad => eEntidad)
                .Must(eEntidad => !eEntidad
                .IsNull()).WithErrorCode(EConstantes.ErrorCode1).WithMessage(string.Format(EConstantes.ErrorCode1DescripcionNuloVacio, "Cliente"));

            RuleFor(eEntidad => eEntidad.Contrasenia).Length(4, 50).WithErrorCode(EConstantes.ErrorCode2).WithMessage(string.Format(EConstantes.ErrorCode2DescripcionFueraRango, "Contraseña"));
            
        }
    }
}