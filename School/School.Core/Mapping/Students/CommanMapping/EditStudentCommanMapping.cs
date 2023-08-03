using School.Core.Features.Students.Commands.Models;
using School.Data.Entities;

namespace School.Core.Mapping.Students
{
    partial class StudentProfile
    {
        public void EditStudentCommanMapping()
        {
            CreateMap<EditStudentCommand, Student>()
                .ForMember(des => des.DID, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(des => des.StudID, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.NameEn, opt => opt.MapFrom(src => src.NameEn))
                .ForMember(des => des.NameAr, opt => opt.MapFrom(src => src.NameAr));
        }

    }
}

