using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Authorization.Commands.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Authorization.Commands.Validators
{
    public class DeleteRoleValidators : AbstractValidator<DeleteRoleCommand>
    {
        private readonly IStringLocalizer<SharedResourses> _stringlocalization;
        private readonly IAuthorizationServices _authorizationServices;
        public DeleteRoleValidators(IStringLocalizer<SharedResourses> stringlocalization, IAuthorizationServices authorizationServices)
        {
            _stringlocalization = stringlocalization;
            _authorizationServices = authorizationServices;
            ApplyValidationRules();
            ApplyCustomValdationRules();

        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(_stringlocalization[SharedResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_stringlocalization[SharedResoursesKeys.Required]);

        }
        public void ApplyCustomValdationRules()
        {
            //    RuleFor(x => x.Id)
            //       .MustAsync(async (Key, CancellationToken) => await _authorizationServices.IsRoleExistById(Key))
            //       .WithMessage(_stringlocalization [SharedResoursesKeys.RoleNotExist]);
        }
    }
}
