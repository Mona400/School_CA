using Microsoft.AspNetCore.Mvc;
using School.API.Base;
using School.Core.Features.Departments.Queries.Models;
using School.Data.AppMetaData;

namespace School.API.Controllers
{

    [ApiController]
    public class DepartmentController : AppControllerBase
    {
        [HttpGet(Router.DepartmentRouting.GetByID)]
        public async Task<IActionResult> DepartmentById([FromQuery] GetDepartmentsByIdQuery query)
        {
            var result = await Mediator.Send(query);
            return NewResult(result);
        }
        [HttpGet(Router.DepartmentRouting.List)]
        public async Task<IActionResult> GetDepartmentList()
        {
            var result = await Mediator.Send(new GetDepartmentListQuery());
            return Ok(result);
        }
        [HttpGet(Router.DepartmentRouting.Paginated)]
        public async Task<IActionResult> Paginated([FromQuery] GetDepartmentPaginatedListQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
