using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Abstracties;
using School.Infrastructure.Data;
using School.Infrastructure.InfrastructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Infrastructure.Repositories
{
    public class StudentRepositories : GenericRepositoryAsync<Student>, IStudentRepositories
    {
        #region fields
        private readonly DbSet<Student> _students;


        #endregion

        #region Constructor
        public StudentRepositories(ApplicationDbContext context):base(context)
        {
            _students = context.Set<Student>(); 
        }
        #endregion

        #region Handling Function
        public async Task<List<Student>> GetListStudentsAsync()
        {
            return await _students.Include(x=>x.Department).ToListAsync();
        }
        #endregion

    }
}
