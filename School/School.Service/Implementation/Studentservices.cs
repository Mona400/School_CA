using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Data.Helpers;
using School.Infrastructure.Abstracties;
using School.Service.Abstracts;

namespace School.Service.Implementation
{
    public class Studentservices : IStudentServices
    {
        private readonly IStudentRepositories _studentRepositories;

        public Studentservices(IStudentRepositories studentRepositories)
        {
            _studentRepositories = studentRepositories;
        }

        public async Task<List<Student>> GetListStudentsAsync()
        {
            return await _studentRepositories.GetListStudentsAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var student = _studentRepositories.GetTableNoTracking()
                                             .Include(x => x.Department)
                                             .Where(s => s.StudID == id)
                                             .FirstOrDefault();
            return student;

        }
        public async Task<string> AddAsync(Student student)
        {
            var studentResult = _studentRepositories.GetTableNoTracking()
                                                  .Where(x => x.NameAr.Equals(student.NameAr))
                                                  .FirstOrDefault();
            if (studentResult != null)
            {
                return "Exist";
            }
            await _studentRepositories.AddAsync(student);
            return "Add Successfully";
        }
        public async Task<bool> IsNameArExist(string nameAr)
        {
            //Check if the name is exist or Not
            var student = _studentRepositories.GetTableNoTracking().Where(x => x.NameAr.Equals(nameAr)).FirstOrDefault();
            if (student == null)
                return false;
            return true;

        }
        public async Task<bool> IsNameEnExist(string nameEn)
        {
            //Check if the name is exist or Not
            var student = _studentRepositories.GetTableNoTracking().Where(x => x.NameEn.Equals(nameEn)).FirstOrDefault();
            if (student == null)
                return false;
            return true;

        }
        public async Task<bool> IsNameArExistExecuteSelf(string nameAr, int id)
        {
            //Check if the name is exist or Not
            var student = await _studentRepositories.GetTableNoTracking().Where(x => x.NameAr.Equals(nameAr) & !x.StudID.Equals(id)).FirstOrDefaultAsync();
            if (student == null)
                return false;
            return true;
        }
        public async Task<bool> IsNameEnExistExecuteSelf(string nameEn, int id)
        {
            //Check if the name is exist or Not
            var student = await _studentRepositories.GetTableNoTracking().Where(x => x.NameEn.Equals(nameEn) & !x.StudID.Equals(id)).FirstOrDefaultAsync();
            if (student == null)
                return false;
            return true;
        }
        public async Task<string> EditAsync(Student student)
        {
            await _studentRepositories.UpdateAsync(student);
            return "Success";

        }
        public async Task<string> DeleteAsync(Student student)
        {
            var trans = _studentRepositories.BeginTransaction();
            try
            {
                await _studentRepositories.DeleteAsync(student);
                await trans.CommitAsync();
                return "Success";
            }
            catch
            {
                await trans.RollbackAsync();
                return "Faild";
            }

        }
        public IQueryable<Student> GetListStudentsQuerable()
        {
            return _studentRepositories.GetTableNoTracking().Include(x => x.Department).AsQueryable();
        }
        public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum orderingEnum, string search)
        {
            var querable = _studentRepositories.GetTableNoTracking().Include(x => x.Department).AsQueryable();
            if (search != null)
            {
                querable = querable.Where(x => x.NameAr.Contains(search) || x.Address.Contains(search));
            }
            switch (orderingEnum)
            {
                case StudentOrderingEnum.StudID:
                    querable = querable.OrderBy(x => x.StudID);
                    break;
                case StudentOrderingEnum.Name:
                    querable = querable.OrderBy(x => x.NameAr);
                    break;
                case StudentOrderingEnum.Address:
                    querable = querable.OrderBy(x => x.Address);
                    break;
                case StudentOrderingEnum.DepartmentName:
                    querable = querable.OrderBy(x => x.Department.DNameAr);
                    break;
                default:
                    querable = querable.OrderBy(x => x.StudID);
                    break;
            }
            return querable;
        }

        public IQueryable<Student> GetStudentsByDepartmentIdQuerable(int DID)
        {
            return _studentRepositories.GetTableNoTracking().Where(x => x.DID.Equals(DID)).AsQueryable();
        }
    }
}
