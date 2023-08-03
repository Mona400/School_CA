using Azure;
using MediatR;

namespace School.Core.Features.Departments.Commands.Models
{
    public class DeleteDepartmentCommand : IRequest<Response<string>>
    {
        public DeleteDepartmentCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

    }
}
