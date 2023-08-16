using School.Data.Entities.Identity;
using School.Data.Helpers;
using System.IdentityModel.Tokens.Jwt;

namespace School.Service.Abstracts
{
    public interface IAuthenticationService
    {
        Task<JwtAuthResult> GetJWTToken(User user);
        JwtSecurityToken ReadJwtToken(string accessToken);
        Task<(string, DateTime?)> validateDetails(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken);

        Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? ExpiryDate, string refreshToken);
        Task<string> validateToken(string AccessToken);


    }
}
