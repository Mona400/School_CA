using School.Core.Features.Students.Queries.Results;
using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetStudentPaginationMapping()
        {
            CreateMap<Student, GetStudentPaginatedListResponse>()
               .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Localize(src.Department.DNameAr, src.Department.DNameEn)))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)))
               .ForMember(dest => dest.StudID, opt => opt.MapFrom(src => src.StudID))
               .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

        }
    }
}
