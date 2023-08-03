using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Departments.Commands.Models
{
    public class AddDepartmentCommand : IRequest<Response<string>>
    {
        public string DNameAr { get; set; }

        public string DNameEn { get; set; }
        public int InsManager { get; set; }
    }
}
