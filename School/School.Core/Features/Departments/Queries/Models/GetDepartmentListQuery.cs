using MediatR;
using School.Core.Features.Departments.Queries.Results;

namespace School.Core.Features.Departments.Queries.Models
{
    public class GetDepartmentListQuery : IRequest<Bases.Response<List<GetDepartmentListResponse>>>
    {

    }
}
