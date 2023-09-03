using MediatR;
using School.Core.Bases;
using School.Data.Dtos;

namespace School.Core.Features.Authorization.Commands.Models
{
    public class UpdatetUserRolesCommand : UpdateUserRolesRequest, IRequest<Response<string>>
    {

    }
}
