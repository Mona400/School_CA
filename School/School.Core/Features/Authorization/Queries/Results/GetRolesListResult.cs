using MediatR;
using School.Core.Bases;
using School.Core.Features.Authorization.Queries.Models;

namespace School.Core.Features.Authorization.Queries.Results
{
    public class GetRolesListResult : IRequest<Response<List<GetRolesListQuery>>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
