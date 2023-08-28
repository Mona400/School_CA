using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authorization.Commands.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Authorization.Commands.Handlers
{
    public class RoleCommandHandler : ResponseHandler,
        IRequestHandler<AddRoleCommand, Response<string>>,
        IRequestHandler<EditRoleCommand, Response<string>>,
        IRequestHandler<DeleteRoleCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResourses> _localization;
        private readonly IAuthorizationServices _authorizationServices;
        public RoleCommandHandler(IStringLocalizer<SharedResourses> stringLocalizer, IAuthorizationServices authorizationServices) : base(stringLocalizer)
        {
            _localization = stringLocalizer;
            _authorizationServices = authorizationServices;
        }

        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationServices.AddRoleAsync(request.RoleName);
            if (result == "Success")
            {
                return Success("");
            }
            return BadRequest<string>(_localization[SharedResoursesKeys.AddFaild]);

        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationServices.EditRoleAsync(request);
            if (result == "notFound")
            {
                return NotFound<string>();
            }
            else if (result == "Success")
            {
                return Success((string)_localization[SharedResoursesKeys.Updated]);
            }
            else
            {
                return BadRequest<string>(result);
            }

        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationServices.DeleteRoleAsync(request.Id);
            if (result == "NotFound")
            {
                return NotFound<string>();
            }
            else if (result == "Used")
            {
                return BadRequest<string>(_localization[SharedResoursesKeys.RoleIsUsed]);
            }

            else if (result == "Success")
            {
                return Success((string)_localization[SharedResoursesKeys.Deleted]);
            }
            else
            {
                return BadRequest<string>(result);
            }
        }
    }
}
