using FluentValidation;
using MyPartyCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.FluentValidation
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Необходимо заполнить имя");


            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Необходимо заполнить пароль");

        }
    }
}
