using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authorization.Queries.Models;
using School.Core.Resources;
using School.Data.Entities.Identity;
using School.Data.Results;
using School.Service.Abstracts;

namespace School.Core.Features.Authorization.Queries.Handlers
{
    public class ClaimsQueryHandler : ResponseHandler, IRequestHandler<ManageUserClaimsQuery, Response<MangeUserClaimResult>>
    {
        private readonly IStringLocalizer<SharedResourses> _stringLocalizer;
        private readonly IAuthorizationServices _authorizationServices;
        private readonly UserManager<User> _userManager;
        public ClaimsQueryHandler(IStringLocalizer<SharedResourses> stringLocalizer, IAuthorizationServices authorizationServices, UserManager<User> userManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationServices = authorizationServices;
            _userManager = userManager;
        }

        public async Task<Response<MangeUserClaimResult>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) { return NotFound<MangeUserClaimResult>(_stringLocalizer[SharedResoursesKeys.UserIsNotFound]); }
            var result = await _authorizationServices.ManageUserClaimData(user);
            return Success(result);
        }
    }
}
