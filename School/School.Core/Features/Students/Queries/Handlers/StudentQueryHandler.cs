using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Students.Queries.Models;
using School.Core.Features.Students.Queries.Results;
using School.Core.Resources;
using School.Core.Wrappers;
using School.Data.Entities;
using School.Service.Abstracts;
using System.Linq.Expressions;

namespace School.Core.Features.Students.Queries.Handlers
{
    public class StudentHandler : ResponseHandler,
                                IRequestHandler<GetStudentListQuery, Response<List<GetStudentListResponse>>>,
                                IRequestHandler<GetStudentByIdQuery, Response<GetSingleStudentResponse>>,
                                IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentPaginatedListResponse>>

    {
        private readonly IStudentServices _studentServices;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResourses> _stringLocalizer;
        public StudentHandler(IStudentServices studentServices, IMapper mapper, IStringLocalizer<SharedResourses> stringLocalizer) : base(stringLocalizer)
        {
            _studentServices = studentServices;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var StudentList = await _studentServices.GetListStudentsAsync();
            var StudentListMapper = _mapper.Map<List<GetStudentListResponse>>(StudentList);
            var result = Success(StudentListMapper);
            result.Meta = new { Count = StudentListMapper.Count() };
            return result;
        }

        public async Task<Response<GetSingleStudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentServices.GetStudentByIdAsync(request.Id);
            if (student == null)
            {
                return NotFound<GetSingleStudentResponse>(_stringLocalizer[SharedResoursesKeys.NotFound]);
            }
            var result = _mapper.Map<GetSingleStudentResponse>(student);
            return Success(result);
        }



        public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            //Expression<Func<Student, GetStudentPaginatedListResponse>> expression = e => new GetStudentPaginatedListResponse(e.StudID, e.Localize(e.NameAr, e.NameEn), e.Address, e.Department.Localize(e.Department.DNameAr, e.Department.DNameEn));
           
            var FilterQuery = _studentServices.FilterStudentPaginatedQuerable(request.OrderBy, request.search);
            var paginatedList = await FilterQuery.Select(x=>new GetStudentPaginatedListResponse(x.StudID, x.Localize(x.NameAr, x.NameEn), x.Address, x.Department.Localize(x.Department.DNameAr, x.Department.DNameEn))).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            paginatedList.Meta = new { Count = paginatedList.Data.Count() };
            return paginatedList;
        }
    }
}
