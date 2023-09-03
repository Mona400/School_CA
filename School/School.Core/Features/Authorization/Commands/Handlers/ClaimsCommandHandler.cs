using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authorization.Commands.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Authorization.Commands.Handlers
{
    public class ClaimsCommandHandler : ResponseHandler, IRequestHandler<UpdateUserClaimsCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResourses> _stringLocalizer;
        private readonly IAuthorizationServices _authorizationServices;
        public ClaimsCommandHandler(IStringLocalizer<SharedResourses> stringLocalizer, IAuthorizationServices authorizationServices) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationServices = authorizationServices;
        }

        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationServices.UpdateUserClaims(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>(_stringLocalizer[SharedResoursesKeys.UserIsNotFound]);
                case "FailedToRemmoveOldClaims": return NotFound<string>(_stringLocalizer[SharedResoursesKeys.FailedToRemmoveOldClaims]);
                case "FailedToAddNewClaims": return NotFound<string>(_stringLocalizer[SharedResoursesKeys.FailedToAddNewClaims]);
                case "FailedToUpdateClaims": return NotFound<string>(_stringLocalizer[SharedResoursesKeys.FailedToUpdateClaims]);

            }
            return Success<string>(_stringLocalizer[SharedResoursesKeys.Success]);
        }
    }
}
