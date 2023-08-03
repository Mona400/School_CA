using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Core.Bases;
using System.Net;

namespace School.API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppControllerBase : ControllerBase
    {
        private  IMediator _mediatorInstance;
        protected IMediator Mediator =>_mediatorInstance ??=HttpContext.RequestServices.GetService<IMediator>();

        public ObjectResult NewResult<T>(Response<T>response)
        {
            switch (response.statusCode)
            {
                case HttpStatusCode.OK:
                    return new ObjectResult(response);
                case HttpStatusCode.Created:
                    return new CreatedResult(string.Empty,response);
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedObjectResult(response);
                case HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(response);
                case HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(response);
                case HttpStatusCode.Accepted:
                    return new AcceptedResult(string.Empty,response);
                case HttpStatusCode.UnprocessableEntity:
                    return new UnprocessableEntityObjectResult(response);
               default:
                    return new BadRequestObjectResult(response);

            }

        }
      

    }
}
