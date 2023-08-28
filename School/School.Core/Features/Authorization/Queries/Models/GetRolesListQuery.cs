using MediatR;
using School.Core.Bases;
using School.Core.Features.Authorization.Queries.Results;

namespace School.Core.Features.Authorization.Queries.Models
{
    public class GetRolesListQuery : IRequest<Response<List<GetRolesListResult>>>
    {

    }
}
