using MediatR;
using School.Core.Features.Students.Queries.Results;

namespace School.Core.Features.Students.Queries.Models
{
    public class GetStudentListQuery : IRequest<Bases.Response<List<GetStudentListResponse>>>
    {

    }
}
