using School.Data.Entities.Identity;

namespace School.Service.Abstracts
{
    public interface IAuthenticationService
    {
        Task<string> GetJWTToken(User user);
    }
}
