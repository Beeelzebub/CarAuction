using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using DTO.Response;
using Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Services.Exceptions;

namespace CarAuctionWebAPI.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<Exception> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlingMiddleware(ILogger<Exception> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                await ResponseAsync(context, exception);
            }
        }

        private static Task ResponseAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            
            return httpContext.Response.WriteAsync(JsonSerializer.Serialize(new Response(ErrorCode.InternalServerError)));
        }
    }
}
