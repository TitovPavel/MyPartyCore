using FluentValidation;
using Microsoft.Extensions.Localization;
using MyPartyCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.FluentValidation
{
    public class EditPartyViewModelValidator : AbstractValidator<EditPartyViewModel>
    {
        public EditPartyViewModelValidator(IStringLocalizer<EditPartyViewModel> localizer)
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage(localizer["TitleRequired"]);

            RuleFor(x => x.Location)
                .NotEmpty()
                .WithMessage(localizer["LocationRequired"]);

            RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage(localizer["DateRequired"])
                .GreaterThan(p => DateTime.Now)
                .WithMessage(localizer["DateLess"]);
        }
    }
}
