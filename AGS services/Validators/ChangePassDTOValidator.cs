using AGS_Models.DTO;
using FluentValidation;

namespace AGS_services.Validators
{
    public class ChangePassDTOValidator : AbstractValidator<ChangePassDTO>
    {
        public ChangePassDTOValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("La contraseña no puede estar vacía")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres");

            RuleFor(x => x.ConfirmNewPassword)
                .Equal(x => x.NewPassword).WithMessage("Las contraseñas no coinciden");
        }
    }
}