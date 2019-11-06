using FluentValidation;
using MyPartyCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.FluentValidation
{
    public class CreatePartyViewModelValidator : AbstractValidator<PartyViewModel>
    {
        public CreatePartyViewModelValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Необходимо заполнить название вечеринки");

            RuleFor(x => x.Location)
                .NotEmpty()
                .WithMessage("Необходимо указать место проведение вечеринки");

            RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("Необходимо заполнит дату проведения вечеринки.")
                .GreaterThan(p => DateTime.Now)
                .WithMessage("Дата не может быть меньше текущей даты.");
        }
    }
}
