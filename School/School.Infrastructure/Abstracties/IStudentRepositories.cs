using School.Data.Entities;
using School.Infrastructure.InfrastructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Infrastructure.Abstracties
{
    public interface IStudentRepositories:IGenericRepositoryAsync<Student>
    {
        public Task<List<Student>> GetListStudentsAsync();
        //public Task<Student> GetStudentByIdAsync(int id );
    }
}
