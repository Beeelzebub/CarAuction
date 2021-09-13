using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Enums;

namespace DTO.Response
{
    public abstract class BaseResponse
    {

        public ErrorCode ErrorCode { get; }

        protected BaseResponse(ErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        public static BaseResponse Success() =>
            new Response(ErrorCode.Success);

        public static BaseResponse Success<T>(T data) =>
            new Response<T>(ErrorCode.Success, data);

        public static BaseResponse Fail(ErrorCode errorCode) =>
            new Response(errorCode);

        public static BaseResponse Fail<T>(ErrorCode errorCode, T data) =>
            new Response<T>(errorCode, data);
    }
}
