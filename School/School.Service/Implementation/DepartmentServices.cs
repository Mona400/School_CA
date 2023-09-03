using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Data.Enums;
using School.Infrastructure.Abstracties;
using School.Service.Abstracts;

namespace School.Service.Implementation
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IDepartmentRepositories _departmentRepositories;

        public DepartmentServices(IDepartmentRepositories departmentRepositories)
        {
            _departmentRepositories = departmentRepositories;
        }

        public async Task<string> AddAsync(Department department)
        {

            var studentResult = _departmentRepositories.GetTableNoTracking()
                                                     .Where(x => x.DNameAr.Equals(department.DNameAr))
                                                     .FirstOrDefaultAsync();
            if (studentResult != null)
            {
                return "Exist";
            }
            await _departmentRepositories.AddAsync(department);
            return "Add Successfully";
        }

        public async Task<string> DeleteAsync(Department department)
        {
            var trans = _departmentRepositories.BeginTransaction();
            try
            {
                await _departmentRepositories.DeleteAsync(department);
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception)
            {
                await trans.RollbackAsync();
                return "Faild";
            }
        }

        public async Task<string> EdisAsync(Department department)
        {
            await _departmentRepositories.UpdateAsync(department);
            return "Success";
        }

        public IQueryable<Department> FilterDepartmentPaginatedQuerable(DepartmentOrderingEnum departmentOrderingEnum, string search)
        {
            var querable = _departmentRepositories.GetTableNoTracking().Include(x => x.DepartmentSubjects).ThenInclude(x => x.Subject)
                                                                 .Include(x => x.Students)
                                                                 .Include(x => x.Instructors)
                                                                 .Include(x => x.Instructor).AsQueryable();
            if (search != null)
            {
                querable = querable.Where(x => x.DNameAr.Contains(search) || x.DNameEn.Contains(search));
            }
            switch (departmentOrderingEnum)
            {
                case DepartmentOrderingEnum.DeptID:
                    querable = querable.OrderBy(x => x.DID);
                    break;
                case DepartmentOrderingEnum.Name:
                    querable = querable.OrderBy(x => x.DNameAr);
                    break;
                default:
                    querable = querable.OrderBy(x => x.DID);
                    break;
            }

            return querable;

        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            var student = await _departmentRepositories.GetTableNoTracking().Where(x => x.DID.Equals(id))
                                                          .Include(x => x.DepartmentSubjects).ThenInclude(x => x.Subject)
                                                          .Include(x => x.Instructors)
                                                          .Include(x => x.Instructor)
                                                          .FirstOrDefaultAsync();
            return student;
        }

        public IQueryable<Department> GetListDepartmentQuerable()
        {
            return _departmentRepositories.GetTableNoTracking().Include(x => x.DepartmentSubjects).ThenInclude(x => x.Subject)
                                                               .Include(x => x.Students)
                                                               .Include(x => x.Instructors)
                                                               .Include(x => x.Instructor).AsQueryable();
        }

        //public Task<Department> GetListDepartmentsAsync()
        //{
        //    return _departmentRepositories.get
        //}

        public async Task<bool> IsNameArExist(string nameAr)
        {
            var department = _departmentRepositories.GetTableNoTracking().Where(x => x.DNameAr.Equals(nameAr)).FirstOrDefaultAsync();
            if (department == null)
                return false;
            return true;
        }

        public async Task<bool> IsNameArExistExecuteSelf(string nameAr, int id)
        {
            var department = await _departmentRepositories.GetTableNoTracking().Where(x => x.DNameAr.Equals(nameAr) & !x.DID.Equals(id)).FirstOrDefaultAsync();
            if (department == null)
                return false;
            return true;
        }

        public async Task<bool> IsNameEnExist(string nameEn)
        {
            var department = _departmentRepositories.GetTableNoTracking().Where(x => x.DNameEn.Equals(nameEn)).FirstOrDefaultAsync();
            if (department == null)
                return false;
            return true;
        }

        public async Task<bool> IsNameEnExistExecuteSelf(string nameEn, int id)
        {
            var department = await _departmentRepositories.GetTableNoTracking().Where(x => x.DNameEn.Equals(nameEn) & !x.DID.Equals(id)).FirstOrDefaultAsync();
            if (department == null)
                return false;
            return true;
        }
        public async Task<bool> IsDepartmentIdNotExist(int id)
        {
            return await _departmentRepositories.GetTableNoTracking().AnyAsync(x=>x.DID.Equals(id));
        }
    }
}
