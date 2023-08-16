using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Authentication.Commands.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Authentication.Commands.Validators
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        private readonly IStudentServices _studentServices;
        private readonly IStringLocalizer<SharedResourses> _Localizer;
        private readonly IDepartmentServices _departmentServices;
        public SignInValidator(IStudentServices studentServices, IStringLocalizer<SharedResourses> localizer, IDepartmentServices departmentServices)
        {
            _Localizer = localizer;
            _studentServices = studentServices;
            _departmentServices = departmentServices;
            ApplyValidationRules();
            ApplyCustomValdationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(_Localizer[SharedResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResoursesKeys.Required]);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(_Localizer[SharedResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResoursesKeys.Required]);


        }
        public void ApplyCustomValdationRules()
        {

        }
    }
}



