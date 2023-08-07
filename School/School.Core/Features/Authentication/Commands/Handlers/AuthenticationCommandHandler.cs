using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authentication.Commands.Models;
using School.Core.Resources;
using School.Data.Entities.Identity;
using School.Data.Helpers;
using School.Service.Abstracts;

namespace School.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<JwtAuthResult>>,
        IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>


    {
        private readonly IStringLocalizer _stringLocalizer;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationCommandHandler(IStringLocalizer<SharedResourses> stringLocalizer, UserManager<User> userManager, SignInManager<User> signInManager, IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }

        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {

            //check if user is exist or not
            var user = await _userManager.FindByNameAsync(request.UserName);
            //return the user not found
            if (user == null) { return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResoursesKeys.UserNameIsNotExist]); }
            //try to sign in
            var signInResult = _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            //if falied return password is wrong
            if (!signInResult.IsCompletedSuccessfully)
            {
                return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResoursesKeys.PasswordNotCorrect]);
            }
            //generate token
            var accessToken = await _authenticationService.GetJWTToken(user);
            //return token
            return Success(accessToken);
        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.GetRefreshToken(request.RefreshToken, request.AccessToken);
            return Success(result);
        }
    }
}
