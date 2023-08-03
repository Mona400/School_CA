using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Students.Commands.Models;
using School.Core.Resources;
using School.Data.Entities;
using School.Service.Abstracts;

namespace School.Core.Features.Students.Commands.Handelers
{
    public class StudentCommanHandler : ResponseHandler, IRequestHandler<AddStudentCommand, Response<string>>,
                                                         IRequestHandler<EditStudentCommand, Response<string>>,
                                                         IRequestHandler<DeleteStudentCommand, Response<string>>
    {
        private readonly IStudentServices _studentServices;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResourses> _Localizer;
        public StudentCommanHandler(IStudentServices studentServices, IMapper mapper, IStringLocalizer<SharedResourses> localizer) : base(localizer)
        {
            _studentServices = studentServices;
            _mapper = mapper;
            _Localizer = localizer;
        }

        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentMapper = _mapper.Map<Student>(request);
            var result = await _studentServices.AddAsync(studentMapper);
            if (result == "Success")
                return Created("");
            else
                return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            //Check the Id is Exist or not
            var student = await _studentServices.GetStudentByIdAsync(request.Id);
            //Return Not Found
            if (student == null)
                return NotFound<string>("Student Is Not Found");
            //Mapping Between request and student
            var studentMapper = _mapper.Map(request, student);
            //Calling Service that make Edit
            var result = await _studentServices.EditAsync(studentMapper);
            //return Response
            if (result == "Success")
                return Success((string)_Localizer[SharedResoursesKeys.Updated]);
            else
                return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {

            //Check the Id is Exist or not
            var student = _studentServices.GetStudentByIdAsync(request.Id);
            //Return Not Found
            if (student == null)
                return NotFound<string>("Student Is Not Found");
            //Mapping Between request and student
            var studentMapper = _mapper.Map<Student>(request);
            //Calling Service that make Edit
            var result = await _studentServices.DeleteAsync(studentMapper);
            //return Response
            if (result == "Success")
                return Success($"Delete Successfully {studentMapper.StudID}");
            else
                return BadRequest<string>();
        }
    }
}
