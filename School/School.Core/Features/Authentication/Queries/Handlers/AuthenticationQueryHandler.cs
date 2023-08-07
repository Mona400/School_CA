using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authentication.Queries.Models;
using School.Core.Resources;
using School.Service.Abstracts;

namespace School.Core.Features.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler : ResponseHandler,

        IRequestHandler<AuthorizeUserQuery, Response<string>>


    {
        private readonly IStringLocalizer<SharedResourses> _stringLocalizer;

        private readonly IAuthenticationService _authenticationService;
        public AuthenticationQueryHandler(IStringLocalizer<SharedResourses> stringLocalizer, IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            _authenticationService = authenticationService;
        }





        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.validateToken(request.AccessToken);
            if (result == "NotExpired")
            {
                return Success(result);
            }
            return BadRequest<string>("Expired");
        }
    }
}
