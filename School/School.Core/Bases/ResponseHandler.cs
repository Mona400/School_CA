using Microsoft.Extensions.Localization;
using School.Core.Resources;

namespace School.Core.Bases
{
    public class ResponseHandler
    {
        private readonly IStringLocalizer<SharedResourses> _stringLocalizer;

        public ResponseHandler(IStringLocalizer<SharedResourses> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }


        public Response<T> Delete<T>(string Message = null)
        {
            return new Response<T>()
            {
                statusCode = System.Net.HttpStatusCode.OK,
                Successed = true,

                Message = Message == null ? _stringLocalizer[SharedResoursesKeys.Deleted] : Message
            };

        }
        public Response<T> Success<T>(T entity, object meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                statusCode = System.Net.HttpStatusCode.OK,
                Message = _stringLocalizer[SharedResoursesKeys.Success],
                Meta = meta
            };
        }
        public Response<T> Unauthorized<T>(string Message = null)
        {
            return new Response<T>()
            {
                statusCode = System.Net.HttpStatusCode.Unauthorized,
                Successed = true,
                Message = Message == null ? _stringLocalizer[SharedResoursesKeys.UnAuthorized] : Message
            };
        }
        public Response<T> BadRequest<T>(string Message = null)
        {
            return new Response<T>()
            {
                statusCode = System.Net.HttpStatusCode.BadRequest,
                Successed = false,
                Message = Message == null ? _stringLocalizer[SharedResoursesKeys.BadRequest] : Message
            };
        }
        public Response<T> UnprocessableEntity<T>(string Message = null)
        {
            return new Response<T>()
            {
                statusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                Successed = false,
                Message = Message == null ? _stringLocalizer[SharedResoursesKeys.UnprocessableEntity] : Message
            };
        }
        public Response<T> NotFound<T>(string message = null)
        {
            return new Response<T>()
            {
                statusCode = System.Net.HttpStatusCode.NotFound,
                Successed = false,
                Message = message == null ? _stringLocalizer[SharedResoursesKeys.NotFound] : message

            };
        }



        public Response<T> Created<T>(T entity, object meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                statusCode = System.Net.HttpStatusCode.Created,
                Successed = true,
                Message = _stringLocalizer[SharedResoursesKeys.Created],
                Meta = meta
            };
        }
    }
}
