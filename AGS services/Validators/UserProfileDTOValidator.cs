using AGS_models.DTO;
using AGS_Models.DTO;
using FluentValidation;

namespace AGS_services.Validators
{
    public class UserProfileDTOValidator : AbstractValidator<UserProfileDTO>
    {
        public UserProfileDTOValidator()
        {
            RuleFor(u => u.nombre)
                .MaximumLength(50).WithMessage("El nombre no debe tener mas de 50 caracteres")
                .When(u => !string.IsNullOrEmpty(u.nombre)); 

            RuleFor(u => u.apellido)
                .MaximumLength(50).WithMessage("El apellido no debe tener mas de 50 caracteres")
                .When(u => !string.IsNullOrEmpty(u.apellido)); 

            RuleFor(u => u.mail)
                .EmailAddress().WithMessage("Formato invalido")
                .When(u => !string.IsNullOrEmpty(u.mail)); 

            RuleFor(u => u.telefono)
                .Matches(@"^\d+$").WithMessage("Debe ser numerico")
                .Length(10).WithMessage("Debe tener 10 digitos")
                .When(u => !string.IsNullOrEmpty(u.telefono)); 
        }
    }
}