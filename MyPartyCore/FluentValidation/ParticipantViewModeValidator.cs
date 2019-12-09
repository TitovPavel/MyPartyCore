using FluentValidation;
using Microsoft.Extensions.Localization;
using MyPartyCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.FluentValidation
{
    public class ParticipantViewModeValidator : AbstractValidator<ParticipantViewModel>
    {
        public ParticipantViewModeValidator(IStringLocalizer<ParticipantViewModel> localizer)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(localizer["NameRequired"]);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(localizer["EmailRequired"])
                .EmailAddress()
                .WithMessage(localizer["EmailValidator"]);
        }
    }
}

