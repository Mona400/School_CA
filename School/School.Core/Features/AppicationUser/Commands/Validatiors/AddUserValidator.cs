using FluentValidation;
using Microsoft.Extensions.Localization;
using School.Core.Features.AppicationUser.Commands.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.AppicationUser.Commands.Validatiors
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        private readonly IStudentServices _studentServices;
        private readonly IStringLocalizer<SharedResourses> _Localizer;
        private readonly IDepartmentServices _departmentServices;

        public AddUserValidator(IStudentServices studentServices, IStringLocalizer<SharedResourses> localizer, IDepartmentServices departmentServices)
        {
            _studentServices = studentServices;
            _Localizer = localizer;
            _departmentServices = departmentServices;
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage(_Localizer[SharedResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResoursesKeys.Required])
                .MaximumLength(100).WithMessage(_Localizer[SharedResoursesKeys.MaxLengthIs100]);
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(_Localizer[SharedResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResoursesKeys.Required])
                .MaximumLength(100).WithMessage(_Localizer[SharedResoursesKeys.MaxLengthIs100]);
            RuleFor(x => x.Country)
                .NotEmpty().WithMessage(_Localizer[SharedResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResoursesKeys.Required])
                .MaximumLength(100).WithMessage(_Localizer[SharedResoursesKeys.MaxLengthIs100]);

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage(_Localizer[SharedResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResoursesKeys.Required])
                .MaximumLength(100).WithMessage(_Localizer[SharedResoursesKeys.MaxLengthIs100]);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(_Localizer[SharedResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResoursesKeys.Required]);
            RuleFor(x => x.ConfirmPassword)

               .Equal(x => x.Password)
               .WithMessage(_Localizer[SharedResoursesKeys.PasswordNotEqualTheConfirmPassword]);
            ;
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_Localizer[SharedResoursesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResoursesKeys.Required]);

        }
        public void ApplyCustomValdationRules()
        {


        }
    }
}
