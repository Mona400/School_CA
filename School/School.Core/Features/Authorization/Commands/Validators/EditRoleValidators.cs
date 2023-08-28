using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Authorization.Commands.Models;
using School.Core.Resources;

namespace School.Core.Features.Authorization.Commands.Validators
{
    public class EditRoleValidators : AbstractValidator<EditRoleCommand>
    {

        private readonly IStringLocalizer<SharedResourses> _Localizer;

        public EditRoleValidators(IStringLocalizer<SharedResourses> localizer)
        {
            _Localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValdationRules();
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(_Localizer[SharedResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResoursesKeys.Required]);

        }
        public void ApplyCustomValdationRules()
        {

        }

    }
}
