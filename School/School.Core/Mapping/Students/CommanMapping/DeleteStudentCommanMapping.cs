using School.Core.Features.Students.Commands.Models;
using School.Data.Entities;


namespace School.Core.Mapping.Students
{
    partial class StudentProfile
    {
        public void DeleteStudentCommanMapping()
        {
            CreateMap<DeleteStudentCommand, Student>()
            .ForMember(des => des.StudID, opt => opt.MapFrom(src => src.Id));
        }
    }
}

