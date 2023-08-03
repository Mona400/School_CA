using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Departments.Commands.Models
{
    public class EditDepartmentCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string DNameAr { get; set; }
        public string DNameEn { get; set; }
        public int InsManager { get; set; }
    }
}
