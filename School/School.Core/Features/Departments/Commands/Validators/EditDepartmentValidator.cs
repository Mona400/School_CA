using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Departments.Commands.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Departments.Commands.Validators
{
    public class EditDepartmentValidator : AbstractValidator<EditDepartmentCommand>
    {
        private readonly IDepartmentServices _departmentServices;
        private readonly IStringLocalizer<SharedResourses> _Localizer;


        public EditDepartmentValidator(IDepartmentServices departmentServices, IStringLocalizer<SharedResourses> localizer)
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
               .MustAsync(async (model, Key, CancellationToken) => !await _departmentServices.IsNameArExistExecuteSelf(Key, model.Id))
               .WithMessage(_Localizer[SharedResoursesKeys.IsExist]);
            RuleFor(x => x.DNameEn)
               .MustAsync(async (model, Key, CancellationToken) => !await _departmentServices.IsNameEnExistExecuteSelf(Key, model.Id))
               .WithMessage(_Localizer[SharedResoursesKeys.IsExist]);

        }
    }
}
