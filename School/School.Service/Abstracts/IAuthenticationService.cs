using School.Data.Entities.Identity;
using School.Data.Helpers;

namespace School.Service.Abstracts
{
    public interface IAuthenticationService
    {
        Task<JwtAuthResult> GetJWTToken(User user);
    }
}
