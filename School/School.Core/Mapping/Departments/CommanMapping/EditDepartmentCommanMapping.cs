using School.Core.Features.Departments.Commands.Models;
using School.Data.Entities;

namespace School.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void EditDepartmentCommanMapping()
        {

            CreateMap<EditDepartmentCommand, Department>()
                    .ForMember(des => des.InsManager, opt => opt.MapFrom(src => src.InsManager))
                    .ForMember(des => des.DID, opt => opt.MapFrom(src => src.Id))
                    .ForMember(des => des.DNameEn, opt => opt.MapFrom(src => src.DNameEn))
                    .ForMember(des => des.DNameAr, opt => opt.MapFrom(src => src.DNameAr));
        }
    }
}
