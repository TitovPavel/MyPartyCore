using FluentValidation;
using MyPartyCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.FluentValidation
{
    public class ParticipantViewModeValidator : AbstractValidator<ParticipantViewModel>
    {
        public ParticipantViewModeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Необходимо заполнить имя");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Необходимо заполнить адрес")
                .EmailAddress()
                .WithMessage("Некорректный адрес");
        }
    }
}

