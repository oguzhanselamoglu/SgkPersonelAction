using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgkPersonelActionApp.Models
{
    public class ActionResponse<T>
    {
        public T Data { get; set; }

        public string Message { get; set; }

        public int StatusCode { get; set; }
        public ResponseType ResponseType { get; set; }

        public static ActionResponse<T> Success( int statusCode)
        {
            return new ActionResponse<T> { Data = default, StatusCode = statusCode, ResponseType = ResponseType.Ok };
        }

    }


    public enum ResponseType
    {
        Ok = 1,
        Warning = 2,
        Error = 3
    }
}
