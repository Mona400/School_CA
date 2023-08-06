using MediatR;
using School.Core.Bases;
using School.Core.Features.AppicationUser.Queries.Results;

namespace School.Core.Features.AppicationUser.Queries.Models
{
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdResponse>>
    {
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
