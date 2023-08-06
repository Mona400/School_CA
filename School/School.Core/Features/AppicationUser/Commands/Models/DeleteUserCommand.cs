using MediatR;
using School.Core.Bases;

namespace School.Core.Features.AppicationUser.Commands.Models
{
    public class DeleteUserCommand : IRequest<Response<string>>
    {
        public DeleteUserCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
