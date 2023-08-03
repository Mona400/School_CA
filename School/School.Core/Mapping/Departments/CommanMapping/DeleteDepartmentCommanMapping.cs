using School.Core.Features.Departments.Commands.Models;
using School.Data.Entities;

namespace School.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void DeleteDepartmentCommanMapping()
        {
            CreateMap<DeleteDepartmentCommand, Department>()
               .ForMember(des => des.DID, opt => opt.MapFrom(src => src.Id));
        }
    }
}
