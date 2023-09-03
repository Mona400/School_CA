using MediatR;
using School.Core.Bases;
using School.Data.Results;

namespace School.Core.Features.Authorization.Queries.Models
{
    public class ManageUserClaimsQuery : IRequest<Response<MangeUserClaimResult>>
    {
        public int UserId { get; set; }
    }
}
