using MediatR;
using School.Core.Bases;
using School.Core.Features.Departments.Queries.Results;

namespace School.Core.Features.Departments.Queries.Models
{
    public class GetDepartmentsByIdQuery : IRequest<Response<GetDepartmentsByIdResponse>>
    {
        public int Id { get; set; }
        public int StudentPageNumber { get; set; }
        public int StudentPageSize { get; set; }


    }
}
