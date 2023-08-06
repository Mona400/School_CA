using Microsoft.EntityFrameworkCore;
using School.Data.Entities.Identity;
using School.Infrastructure.Abstracties;
using School.Infrastructure.Data;
using School.Infrastructure.InfrastructureBases;

namespace School.Infrastructure.Repositories
{
    public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenRepository
    {
        private DbSet<UserRefreshToken> _usersRefreshToken;
        public RefreshTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _usersRefreshToken = dbContext.Set<UserRefreshToken>();
        }
    }
}