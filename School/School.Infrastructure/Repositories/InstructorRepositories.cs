using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Abstracties;
using School.Infrastructure.Data;
using School.Infrastructure.InfrastructureBases;

namespace School.Infrastructure.Repositories
{
    public class InstructorRepositories : GenericRepositoryAsync<Instructor>, IInstructorRepositories
    {
        private readonly DbSet<Instructor> _instructors;
        public InstructorRepositories(ApplicationDbContext dbContext) : base(dbContext)
        {
            _instructors = dbContext.Set<Instructor>();
        }
    }
}
