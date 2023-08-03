using MediatR;
using School.Core.Features.Students.Queries.Results;
using School.Core.Wrappers;
using School.Data.Helpers;

namespace School.Core.Features.Students.Queries.Models
{
    public class GetStudentPaginatedListQuery : IRequest<PaginatedResult<GetStudentPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public StudentOrderingEnum OrderBy { get; set; }
        public string? search { get; set; }
    }
}
