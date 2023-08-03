using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Abstracties;
using School.Infrastructure.Data;
using School.Infrastructure.InfrastructureBases;

namespace School.Infrastructure.Repositories
{
    public class DepartmentRepositories : GenericRepositoryAsync<Department>, IDepartmentRepositories
    {
        private readonly DbSet<Department> _department;
        public DepartmentRepositories(ApplicationDbContext dbContext) : base(dbContext)
        {
            _department = dbContext.Set<Department>();
        }
    }
}
