using Microsoft.AspNetCore.Mvc;
using School.API.Base;
using School.Core.Features.AppicationUser.Commands.Models;
using School.Core.Features.AppicationUser.Queries.Models;
using School.Data.AppMetaData;

namespace School.API.Controllers
{

    [ApiController]
    public class ApplicationUserController : AppControllerBase
    {
        [HttpPost(Router.ApplicationUserRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddUserCommand addUserCommand)
        {
            var result = await Mediator.Send(addUserCommand);
            return NewResult(result);
        }
        [HttpGet(Router.ApplicationUserRouting.Paginated)]
        public async Task<IActionResult> Paginated([FromQuery] GetUserPaginationQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        [HttpGet(Router.ApplicationUserRouting.GetByID)]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetUserByIdQuery(id));
            return NewResult(result);
        }
        [HttpPut(Router.ApplicationUserRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditUserCommand editUserCommand)
        {
            var result = await Mediator.Send(editUserCommand);
            return NewResult(result);
        }
        [HttpPut(Router.ApplicationUserRouting.ChangePassword)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
        [HttpDelete(Router.ApplicationUserRouting.Delete)]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var result = await Mediator.Send(new DeleteUserCommand(id));
            return NewResult(result);
        }
    }
}
