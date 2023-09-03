using MediatR;
using School.Core.Features.Departments.Queries.Results;
using School.Core.Wrappers;
using School.Data.Enums;

namespace School.Core.Features.Departments.Queries.Models
{
    public class GetDepartmentPaginatedListQuery : IRequest<PaginatedResult<GetDepartmentPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DepartmentOrderingEnum OrderBy { get; set; }
        public string? search { get; set; }
    }
}
