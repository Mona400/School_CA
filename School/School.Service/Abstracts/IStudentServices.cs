using School.Data.Entities;
using School.Data.Helpers;

namespace School.Service.Abstracts
{
    public interface IStudentServices
    {
        public Task<List<Student>> GetListStudentsAsync();
        public Task<Student> GetStudentByIdAsync(int id);
        public Task<string> AddAsync(Student student);
        public Task<bool> IsNameArExist(string name);
        public Task<bool> IsNameEnExist(string name);
        // public Task<bool> IsNameExistExecuteSelf(string name, int id);
        public Task<bool> IsNameArExistExecuteSelf(string name, int id);
        public Task<bool> IsNameEnExistExecuteSelf(string name, int id);
        public Task<string> EditAsync(Student student);
        public Task<string> DeleteAsync(Student student);
        public IQueryable<Student> GetListStudentsQuerable();
        public IQueryable<Student> GetStudentsByDepartmentIdQuerable(int DID);
        public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum orderingEnum, string search);

    }
}
