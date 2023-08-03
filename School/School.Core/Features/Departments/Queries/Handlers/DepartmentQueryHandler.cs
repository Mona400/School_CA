using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Departments.Queries.Models;
using School.Core.Features.Departments.Queries.Results;
using School.Core.Resources;
using School.Core.Wrappers;
using School.Data.Entities;
using School.Service.Abstracts;
using System.Linq.Expressions;

namespace School.Core.Features.Departments.Queries.Handlers
{
    public class DepartmentQueryHandler : ResponseHandler,

                                       IRequestHandler<GetDepartmentListQuery, Response<List<GetDepartmentListResponse>>>,
                                       IRequestHandler<GetDepartmentsByIdQuery, Response<GetDepartmentsByIdResponse>>,
                                       IRequestHandler<GetDepartmentPaginatedListQuery, PaginatedResult<GetDepartmentPaginatedListResponse>>
    {
        private readonly IStringLocalizer<SharedResourses> _stringLocalizer;
        private readonly IDepartmentServices _departmentServices;
        private readonly IStudentServices _studentServices;
        private readonly IMapper _mapper;

        public DepartmentQueryHandler(IStringLocalizer<SharedResourses> stringLocalizer, IDepartmentServices departmentServices, IMapper mapper, IStudentServices studentServices) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _departmentServices = departmentServices;
            _mapper = mapper;
            _studentServices = studentServices;
        }

        public async Task<Response<GetDepartmentsByIdResponse>> Handle(GetDepartmentsByIdQuery request, CancellationToken cancellationToken)
        {

            //service GetById include Student ,Sub, Ins
            var response = await _departmentServices.GetDepartmentByIdAsync(request.Id);
            //Check If Not Exist
            if (response == null)
            {
                return NotFound<GetDepartmentsByIdResponse>(_stringLocalizer[SharedResoursesKeys.NotFound]);
            }
            //Mapping
            var mapper = _mapper.Map<GetDepartmentsByIdResponse>(response);
            //pagination
            Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse(e.StudID, e.Localize(e.NameAr, e.NameEn));
            var studentQuerable = _studentServices.GetStudentsByDepartmentIdQuerable(request.Id);
            var paginatedList = await studentQuerable.Select(expression).ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);
            mapper.StudentList = paginatedList;
            //Return Response
            return Success(mapper);
        }

        public async Task<Response<List<GetDepartmentListResponse>>> Handle(GetDepartmentListQuery request, CancellationToken cancellationToken)
        {
            var StudentList = _departmentServices.GetListDepartmentQuerable();
            var studentListMapper = _mapper.Map<List<GetDepartmentListResponse>>(StudentList);
            var result = Success(studentListMapper);
            result.Meta = new { Count = studentListMapper.Count() };
            return result;
        }

        public async Task<PaginatedResult<GetDepartmentPaginatedListResponse>> Handle(GetDepartmentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Department, GetDepartmentPaginatedListResponse>> expression = e => new GetDepartmentPaginatedListResponse(e.DID, e.Localize(e.DNameAr, e.DNameEn));
            var FilterQuery = _departmentServices.FilterDepartmentPaginatedQuerable(request.OrderBy, request.search);
            var paginatedList = await FilterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            paginatedList.Meta = new { Count = paginatedList.Data.Count() };
            return paginatedList;


        }
    }
}
