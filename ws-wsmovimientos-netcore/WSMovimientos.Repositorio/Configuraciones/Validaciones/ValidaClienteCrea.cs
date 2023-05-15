#region Using

using BP.Comun.Extensiones;
using FluentValidation;
using WSMovimientos.Entidades;
using WSMovimientos.Entidades.DTOS;

#endregion Using

namespace WSMovimientos.Repositorio.Configuraciones.Validaciones
{
    public class ValidaClienteCrea : AbstractValidator<ClienteCrea>
    {
        public ValidaClienteCrea()
        {

            RuleFor(eEntidad => eEntidad)
                .Must(eEntidad => !eEntidad.Contrasenia.IsNullEmpty()).WithMessage(string.Format(EConstantes.ErrorCode1DescripcionNuloVacio, "Contraseña")).WithErrorCode(EConstantes.ErrorCode1)
                .Must(eEntidad => !eEntidad.IdPersona.IsNull()).WithMessage(string.Format(EConstantes.ErrorCode1DescripcionNuloVacio, "IdPersona")).WithErrorCode(EConstantes.ErrorCode1);

            RuleFor(eEntidad => eEntidad.Contrasenia).Length(4, 50).WithMessage(string.Format(EConstantes.ErrorCode2DescripcionFueraRango, "Contraseña")).WithErrorCode(EConstantes.ErrorCode2);
            
     }
    }
}