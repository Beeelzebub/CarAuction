using System;
using System.Linq;
using Entity;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Repositories;


namespace CarAuctionWebAPI.ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        private readonly IRepositoryManager _repositoryManager;

        public ValidationFilterAttribute(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var id = (int)context.ActionArguments["id"];
            
            if(id == 0)
            {
                context.Result = new BadRequestObjectResult("Bad id parameter");
                return;
            }
            var car = _repositoryManager.Car.GetAsync(id); 
            if (car == null)
            {
                context.Result = new BadRequestObjectResult("Object is null");
            }
            else
            {
                context.HttpContext.Items.Add("car", car);
            }
        }
    }
}
