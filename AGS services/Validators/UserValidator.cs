using AGS_models.DTO;
using AGS_Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGS_services.Validators
{
    public class UserValidator : AbstractValidator<UserCreateDTO>
    {
        public UserValidator()
        {
            RuleFor(u => u.nombre)
                .MaximumLength(50).WithMessage("El nombre no debe tener mas de 50 caracteres");

            RuleFor(u => u.apellido)
                .MaximumLength(50).WithMessage("El apellido no debe tener mas de 50 caracteres");

            RuleFor(u => u.mail)
                .EmailAddress().WithMessage("Formato invalido");

            RuleFor(u => u.contrasena)
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caraceres");
            //.Matches("[^a-zA-Z0-9]").WithMessage("Debe tener al menos un caracter especial");
            //esto cuando valide que cambies la contraseña

            RuleFor(u => u.telefono)
                .Matches(@"^\d+$").WithMessage("El telefono debe ser numerico")
                .Length(10).WithMessage("El telefono debe tener 10 digitos");
        }

    }
}

