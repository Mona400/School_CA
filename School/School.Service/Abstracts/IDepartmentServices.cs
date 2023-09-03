using School.Data.Entities;
using School.Data.Enums;

namespace School.Service.Abstracts
{
    public interface IDepartmentServices
    {
        //public Task<Department> GetListDepartmentsAsync();
        public Task<Department> GetDepartmentByIdAsync(int id);
         public Task<bool> IsDepartmentIdNotExist(int id);
        
        public Task<string> AddAsync(Department department);
        public Task<bool> IsNameArExist(string nameAr);
        public Task<bool> IsNameEnExist(string nameEn);
        public Task<bool> IsNameArExistExecuteSelf(string nameAr, int id);
        public Task<bool> IsNameEnExistExecuteSelf(string nameEn, int id);
        public Task<string> EdisAsync(Department department);
        public Task<string> DeleteAsync(Department department);
        public IQueryable<Department> GetListDepartmentQuerable();
        public IQueryable<Department> FilterDepartmentPaginatedQuerable(DepartmentOrderingEnum departmentOrderingEnum, string search);
    }
}
