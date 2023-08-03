using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.Bases
{
    public class Response<T>
    {
        public HttpStatusCode statusCode{ get; set; }
        public object Meta { get; set; }
        public bool Successed { get; set; }
        public string Message { get; set; }
        public List<string>Errors { get; set; }
        public T Data { get; set; }
        public Response()
        {
            
        }
        public Response(T data, string message =null)
        {
            Successed = true;
            Message = message;
            Data = data;
        }
        public Response(string message)
        {
            Message = message;
            Successed=false;
        }
        public Response(string message,bool succeeded)
        {
            Message = message;
            Successed = succeeded;
        }
    }
}
