using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.API.Base;
using School.Core.Features.Authorization.Commands.Models;
using School.Core.Features.Authorization.Queries.Models;
using School.Data.AppMetaData;

namespace School.API.Controllers
{

    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuthorizationController : AppControllerBase
    {
        [HttpPost(Router.AuthorizationRouting.Create)]
        public async Task<IActionResult> Create([FromForm] AddRoleCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpPost(Router.AuthorizationRouting.Edit)]
        public async Task<IActionResult> Edit([FromForm] EditRoleCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpDelete(Router.AuthorizationRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            var response = await Mediator.Send(new DeleteRoleCommand(Id));
            return NewResult(response);

        }
        [HttpGet(Router.AuthorizationRouting.List)]
        public async Task<IActionResult> List()
        {
            var response = await Mediator.Send(new GetRolesListQuery());
            return NewResult(response);

        }
        [HttpGet(Router.AuthorizationRouting.GetByID)]
        public async Task<IActionResult> GetRoleById([FromRoute] int id)
        {
            var response = await Mediator.Send(new GetRoleByIdQuery() { Id = id });
            return NewResult(response);

        }

    }
}
