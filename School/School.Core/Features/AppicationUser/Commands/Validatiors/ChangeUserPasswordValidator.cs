using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.AppicationUser.Commands.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.AppicationUser.Commands.Validatiors
{
    public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        private readonly IStudentServices _studentServices;
        private readonly IStringLocalizer<SharedResourses> _Localizer;
        private readonly IDepartmentServices _departmentServices;

        public ChangeUserPasswordValidator(IStudentServices studentServices, IStringLocalizer<SharedResourses> localizer, IDepartmentServices departmentServices)
        {
            _studentServices = studentServices;
            _Localizer = localizer;
            _departmentServices = departmentServices;
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(_Localizer[SharedResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResoursesKeys.Required]);


            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage(_Localizer[SharedResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResoursesKeys.Required]);
            RuleFor(x => x.NewPassword)

               .NotEmpty().WithMessage(_Localizer[SharedResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResoursesKeys.Required]);
            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword).WithMessage(_Localizer[SharedResoursesKeys.Required]);
        }
        public void ApplyCustomValdationRules()
        {


        }
    }
}