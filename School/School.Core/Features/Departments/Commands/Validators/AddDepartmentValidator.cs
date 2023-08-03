using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Departments.Commands.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Departments.Commands.Validators
{
    public class AddDepartmentValidator : AbstractValidator<AddDepartmentCommand>
    {
        private readonly IDepartmentServices _departmentServices;
        private readonly IStringLocalizer<SharedResourses> _Localizer;
        public AddDepartmentValidator()
        {

        }

        public AddDepartmentValidator(IDepartmentServices departmentServices, IStringLocalizer<SharedResourses> localizer)
        {
            _departmentServices = departmentServices;
            _Localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValdationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.DNameAr)
                .NotEmpty().WithMessage(_Localizer[SharedResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResoursesKeys.Required])
                .MaximumLength(100).WithMessage(_Localizer[SharedResoursesKeys.MaxLengthIs100]);
            RuleFor(x => x.DNameEn)
                .NotEmpty().WithMessage(_Localizer[SharedResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResoursesKeys.Required])
                .MaximumLength(100).WithMessage(_Localizer[SharedResoursesKeys.MaxLengthIs100]);

        }
        public void ApplyCustomValdationRules()
        {
            RuleFor(x => x.DNameAr)
                .MustAsync(async (Key, CancellationToken) => !await _departmentServices.IsNameArExist(Key))
                .WithMessage(_Localizer[SharedResoursesKeys.IsExist]);
            RuleFor(x => x.DNameEn)
               .MustAsync(async (Key, CancellationToken) => !await _departmentServices.IsNameEnExist(Key))
               .WithMessage(_Localizer[SharedResoursesKeys.IsExist]);

        }
    }
}
