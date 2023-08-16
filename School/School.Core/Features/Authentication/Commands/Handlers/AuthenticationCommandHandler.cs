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
            var jwtToken = _authenticationService.ReadJwtToken(request.AccessToken);
            var userIdAndExpireDate = await _authenticationService.validateDetails(jwtToken, request.AccessToken, request.RefreshToken);

            var (status, expiryDate) = userIdAndExpireDate;

            switch (status)
            {
                case "AlgorithmIsWrong":
                    return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResoursesKeys.AlgorithmIsWrong]);
                case "TokenIsNotExpired":
                    return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResoursesKeys.TokenIsNotExpired]);
                case "RefreshTokenIsNotFound":
                    return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResoursesKeys.RefreshTokenIsNotFound]);
                case "RefreshTokenIsExpired":
                    return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResoursesKeys.RefreshTokenIsExpired]);
            }

            var userId = userIdAndExpireDate.Item1;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound<JwtAuthResult>();
            }

            var result = await _authenticationService.GetRefreshToken(user, jwtToken, expiryDate, request.RefreshToken);
            return Success(result);
        }

        //public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        //{
        //    var jwtToken = _authenticationService.ReadJwtToken(request.AccessToken);
        //    var userIdAndExpireDate = await _authenticationService.validateDetails(jwtToken, request.AccessToken, request.RefreshToken);

        //    switch (userIdAndExpireDate.ToString())
        //    {
        //        case ("AlgorithmIsWrong"): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResoursesKeys.AlgorithmIsWrong]);
        //        case ("TokenIsNotExpired"): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResoursesKeys.TokenIsNotExpired]);
        //        case ("RefreshTokenIsNotFound"): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResoursesKeys.RefreshTokenIsNotFound]);
        //        case ("RefreshTokenIsExpired"): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResoursesKeys.RefreshTokenIsExpired]);
        //    }
        //    var (userId, expiryDate) = userIdAndExpireDate;
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //    {
        //        return NotFound<JwtAuthResult>();
        //    }


        //    var result = await _authenticationService.GetRefreshToken(user, jwtToken, expiryDate, request.RefreshToken);
        //    return Success(result);
        //}
    }
}
