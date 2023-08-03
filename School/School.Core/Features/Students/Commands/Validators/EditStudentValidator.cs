using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.Students.Commands.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Students.Commands.Validators
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {
        private readonly IStudentServices _studentServices;
        private readonly IStringLocalizer<SharedResourses> _Localizer;
        public EditStudentValidator(IStudentServices studentServices, IStringLocalizer<SharedResourses> localizer)
        {
            _studentServices = studentServices;
            _Localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValdationRules();

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
        }
        public void ApplyCustomValdationRules()
        {
            RuleFor(x => x.NameAr)
               .MustAsync(async (model, Key, CancellationToken) => !await _studentServices.IsNameArExistExecuteSelf(Key, model.Id))
               .WithMessage(_Localizer[SharedResoursesKeys.IsExist]);
            RuleFor(x => x.NameEn)
               .MustAsync(async (model, Key, CancellationToken) => !await _studentServices.IsNameEnExistExecuteSelf(Key, model.Id))
               .WithMessage(_Localizer[SharedResoursesKeys.IsExist]);

        }
    }
}
