using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Students.Commands.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Students.Commands.Validators
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        private readonly IStudentServices _studentServices;
        private readonly IStringLocalizer<SharedResourses> _Localizer;
        private readonly IDepartmentServices _departmentServices;
        public AddStudentValidator(IStudentServices studentServices, IStringLocalizer<SharedResourses> localizer, IDepartmentServices departmentServices)
        {
            _studentServices = studentServices;
            _Localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValdationRules();
            _departmentServices = departmentServices;
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage(_Localizer[SharedResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResoursesKeys.Required])
                .MaximumLength(100).WithMessage(_Localizer[SharedResoursesKeys.MaxLengthIs100]);
            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage(_Localizer[SharedResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResoursesKeys.Required])
                .MaximumLength(100).WithMessage(_Localizer[SharedResoursesKeys.MaxLengthIs100]);

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage(_Localizer[SharedResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResoursesKeys.Required])
                .MaximumLength(100).WithMessage(_Localizer[SharedResoursesKeys.MaxLengthIs100]);

            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage(_Localizer[SharedResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResoursesKeys.Required]);
                
        }
        public void ApplyCustomValdationRules()
        {
            RuleFor(x => x.NameAr)
                .MustAsync(async (Key, CancellationToken) => !await _studentServices.IsNameArExist(Key))
                .WithMessage(_Localizer[SharedResoursesKeys.IsExist]);
            RuleFor(x => x.NameEn)
               .MustAsync(async (Key, CancellationToken) => !await _studentServices.IsNameEnExist(Key))
               .WithMessage(_Localizer[SharedResoursesKeys.IsExist]);
             RuleFor(x => x.DepartmentId)
               .MustAsync(async (Key, CancellationToken) => await _departmentServices.IsDepartmentIdNotExist(Key))
               .WithMessage(_Localizer[SharedResoursesKeys.IsNotExist]);

        }
    }
}




