using School.Data.Entities.Identity;
using School.Infrastructure.InfrastructureBases;

namespace School.Infrastructure.Abstracties
{
    public interface IRefreshTokenRepository : IGenericRepositoryAsync<UserRefreshToken>
    {
    }
}
