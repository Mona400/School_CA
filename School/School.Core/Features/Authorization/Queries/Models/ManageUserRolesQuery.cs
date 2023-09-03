using MediatR;
using School.Core.Bases;
using School.Data.Results;

namespace School.Core.Features.Authorization.Queries.Models
{
    public class ManageUserRolesQuery : IRequest<Response<UpdateUserRolesResult>>
    {
        public int UserId { get; set; }
    }
}
