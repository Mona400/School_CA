using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.API.Base;
using School.Core.Features.Students.Commands.Models;
using School.Core.Features.Students.Queries.Models;
using Router = School.Data.AppMetaData.Router;

namespace School.API.Controllers
{

    [ApiController]
    [Authorize]
    public class StudentController : AppControllerBase
    {

        [HttpGet(Router.StudentRouting.List)]
        public async Task<IActionResult> GetStudentList()
        {
            var result = await Mediator.Send(new GetStudentListQuery());
            return Ok(result);
        }
        [HttpGet(Router.StudentRouting.Paginated)]
        [AllowAnonymous]
        public async Task<IActionResult> Paginated([FromQuery] GetStudentPaginatedListQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet(Router.StudentRouting.GetByID)]
        public async Task<IActionResult> GetStudentById([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetStudentByIdQuery(id));
            return NewResult(result);
        }
        [HttpPost(Router.StudentRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddStudentCommand addStudentCommand)
        {
            var result = await Mediator.Send(addStudentCommand);
            return NewResult(result);
        }

        [HttpPut(Router.StudentRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditStudentCommand editStudentCommand)
        {
            var result = await Mediator.Send(editStudentCommand);
            return NewResult(result);
        }

        [HttpDelete(Router.StudentRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await Mediator.Send(new DeleteStudentCommand());
            return NewResult(result);
        }
    }
}
