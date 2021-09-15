using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Enums;

namespace DTO.Response
{
    public class Response<T> : BaseResponse
    {
        public T Data { get; }

        public Response(ErrorCode errorCode, T data) : base(errorCode)
        {
            Data = data;
        }
    }

    public class Response : BaseResponse
    {
        public Response(ErrorCode errorCode) : base(errorCode)
        {

        }
    }
}
