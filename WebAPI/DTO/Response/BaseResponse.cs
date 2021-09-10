using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Enums;

namespace DTO.Response
{
    public class BaseResponse<T>
    {
        public int HttpStatusCode { get; set; } 
        public InternalCode Code { get; set; }
        public T Data { get; set; }

        protected BaseResponse(int httpStatusCode, InternalCode code, T data)
        {
            HttpStatusCode = httpStatusCode;
            Code = code;
            Data = data;
        }

        //public static BaseResponse<T> Created() => new OperationResult(201, InternalCode.Success, );
    }
}
