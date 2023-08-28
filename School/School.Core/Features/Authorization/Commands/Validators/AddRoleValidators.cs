using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Authorization.Commands.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Authorization.Commands.Validators
{
    public class AddRoleValidators : AbstractValidator<AddRoleCommand>
    {


        private readonly IStringLocalizer<SharedResourses> _Localizer;
        private readonly IAuthorizationServices _authorizationServices;

        public AddRoleValidators(IStringLocalizer<SharedResourses> localizer, IAuthorizationServices authorizationServices)
        {
            _Localizer = localizer;
            _authorizationServices = authorizationServices;
            ApplyValidationRules();
            ApplyCustomValdationRules();

        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage(_Localizer[SharedResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResoursesKeys.Required]);

        }
        public void ApplyCustomValdationRules()
        {
            RuleFor(x => x.RoleName)
                .MustAsync(async (Key, CancellationToken) => !await _authorizationServices.IsRoleExistByName(Key))
                .WithMessage(_Localizer[SharedResoursesKeys.IsExist]);
        }

    }
}
