using System;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using Services.Exceptions;
using Microsoft.AspNetCore.Http;

namespace CarAuctionWebAPI.Filters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;
            var httpContext = context.HttpContext;

            httpContext.Response.StatusCode = exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            return httpContext.Response.WriteAsync(JsonSerializer.Serialize(new { error = exception.Message }));
        }
    }
}
