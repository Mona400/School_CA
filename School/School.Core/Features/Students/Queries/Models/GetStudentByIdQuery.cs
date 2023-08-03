using MediatR;
using School.Core.Bases;
using School.Core.Features.Students.Queries.Results;
using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.Features.Students.Queries.Models
{
    public class GetStudentByIdQuery:IRequest<Response<GetSingleStudentResponse>>
    {
        public int Id { get; set; }
        public GetStudentByIdQuery(int id)
        {
            Id=id;
        }
    }
}
