using FluentValidation;
using Microsoft.Extensions.Localization;
using MyPartyCore.ViewModels;
using System;


namespace MyPartyCore.FluentValidation
{
    public class CreatePartyViewModelValidator : AbstractValidator<CreatePartyViewModel>
    {
        public CreatePartyViewModelValidator(IStringLocalizer<CreatePartyViewModel> localizer)
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
