using FluentValidation;
using MyPartyCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.FluentValidation
{
    public class CreateUserViewModelValidator : AbstractValidator<CreateUserViewModel>
    {
        public CreateUserViewModelValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Необходимо указать имя");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Необходимо заполнить адрес")
                .EmailAddress()
                .WithMessage("Некорректный адрес");

            RuleFor(x => x.Birthday)
                .NotEmpty()
                .WithMessage("Необходимо заполнит дату рождения.")
                .LessThan(p => DateTime.Now)
                .WithMessage("Дата рождения не может быть больше текущей даты.");

            RuleFor(x => x.Sex)
                .NotEmpty()
                .WithMessage("Необходимо заполнить пол");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Необходимо заполнить пароль");

        }
    }
}
