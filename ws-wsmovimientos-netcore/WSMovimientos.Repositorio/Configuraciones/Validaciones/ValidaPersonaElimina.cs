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
    public class ValidaPersonaElimina : AbstractValidator<PersonaElimina>
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidaPersonaElimina()
        {

            RuleFor(ePersona => ePersona)
                .Must(ePersona => !ePersona
                .IsNull()).WithErrorCode(EConstantes.ErrorCode1).WithMessage(string.Format(EConstantes.ErrorCode1DescripcionNuloVacio, "Persona"));

        }
    }
}