using Microsoft.AspNetCore.Mvc;
using School.API.Base;
using School.Core.Features.Authentication.Commands.Models;
using School.Data.AppMetaData;

namespace School.API.Controllers
{

    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        [HttpPost(Router.AuthenticationRouting.SignIn)]
        public async Task<IActionResult> Create([FromForm] SignInCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
    }
}
