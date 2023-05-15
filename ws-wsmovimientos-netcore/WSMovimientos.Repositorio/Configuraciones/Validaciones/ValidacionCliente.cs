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
    public class ValidacionCliente : AbstractValidator<Entidades.DTOS.Cliente>
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidacionCliente()
        {
            RuleFor(eEntidad => eEntidad)
                .Must(eEntidad => !eEntidad.Contrasenia.IsNullEmpty()).WithMessage(string.Format(EConstantes.ErrorCode1DescripcionNuloVacio, "Contraseña")).WithErrorCode(EConstantes.ErrorCode1);
                
            RuleFor(eEntidad => eEntidad.Contrasenia).Length(4, 50).WithMessage(string.Format(EConstantes.ErrorCode2DescripcionFueraRango, "Contraseña")).WithErrorCode(EConstantes.ErrorCode2);
         }
    }
}