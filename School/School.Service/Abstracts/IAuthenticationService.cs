using School.Data.Entities.Identity;
using School.Data.Helpers;

namespace School.Service.Abstracts
{
    public interface IAuthenticationService
    {
        Task<JwtAuthResult> GetJWTToken(User user);
        Task<JwtAuthResult> GetRefreshToken(string AccessToken, string RefreshToken);
        public Task<string> validateToken(string AccessToken);


    }
}
