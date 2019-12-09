using FluentValidation;
using Microsoft.Extensions.Localization;
using MyPartyCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.FluentValidation
{
    public class EditUserViewModelValidator : AbstractValidator<EditUserViewModel>
    {
        public EditUserViewModelValidator(IStringLocalizer<EditUserViewModel> localizer)
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage(localizer["UserNameRequired"]);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(localizer["EmailNameRequired"])
                .EmailAddress()
                .WithMessage(localizer["EmailValidator"]);

            RuleFor(x => x.Birthday)
                .NotEmpty()
                .WithMessage(localizer["BirthdayValidator"])
                .LessThan(p => DateTime.Now)
                .WithMessage(localizer["BirthdayMore"]);

            RuleFor(x => x.Sex)
                .NotEmpty()
                .WithMessage(localizer["SexRequired"]);
        }
    }
}
