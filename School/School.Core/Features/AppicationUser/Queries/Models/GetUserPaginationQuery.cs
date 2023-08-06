using MediatR;
using School.Core.Features.AppicationUser.Queries.Results;
using School.Core.Wrappers;

namespace School.Core.Features.AppicationUser.Queries.Models
{
    public class GetUserPaginationQuery : IRequest<PaginatedResult<GetUserPaginationListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
