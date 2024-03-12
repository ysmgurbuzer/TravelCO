using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Travel.CustomErrorHandler
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public static Response<T> Fail(string errorMessage)
        {
            return new Response<T> { Succeeded = false, Message = errorMessage };
        }
        public static Response<T> Success(T data)
        {
            return new Response<T> { Succeeded = true, Data = data };
        }
        public static Response<T> Unauthorized(string errorMessage)
        {
            return new Response<T> { Succeeded = false, Message = errorMessage };
        }
        public static Response<T> Success(string successMessage)
        {
            return new Response<T> { Succeeded = true, Message = successMessage };
        }

    }
}
