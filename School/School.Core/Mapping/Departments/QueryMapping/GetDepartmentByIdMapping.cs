using School.Core.Features.Departments.Queries.Results;
using School.Data.Entities;

namespace School.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentByIdMapping()
        {
            CreateMap<Department, GetDepartmentsByIdResponse>()
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Localize(src.DNameAr, src.DNameEn)))
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.DID))
                .ForMember(des => des.ManagerName, opt => opt.MapFrom(src => src.Instructor.Localize(src.Instructor.ENameAr, src.Instructor.ENameEn)))
                 .ForMember(des => des.SubjectList, opt => opt.MapFrom(src => src.DepartmentSubjects))
                 //.ForMember(des => des.StudentList, opt => opt.MapFrom(src => src.Students))
                 .ForMember(des => des.InstructorList, opt => opt.MapFrom(src => src.Instructors))
                ;
            CreateMap<DepartmentSubject, SubjectResponse>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.SubID))
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Subject.Localize(src.Subject.SubjectNameAr, src.Subject.SubjectNameEn)));

            //CreateMap<Student, StudentResponse>()
            //    .ForMember(des => des.Id, opt => opt.MapFrom(src => src.StudID))
            //    .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));

            CreateMap<Instructor, InstructorResponse>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.InsId))
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Localize(src.ENameAr, src.ENameEn)));

        }
    }
}
