using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Abstracties;
using School.Infrastructure.Data;
using School.Infrastructure.InfrastructureBases;

namespace School.Infrastructure.Repositories
{
    public class SubjectRepositories : GenericRepositoryAsync<Subject>, ISubjectRepositories
    {
        private readonly DbSet<Subject> _subjects;
        public SubjectRepositories(ApplicationDbContext dbContext) : base(dbContext)
        {
            _subjects = dbContext.Set<Subject>();
        }
    }
}
