using System;
using System.Linq;
using Entity;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Repositories;


namespace CarAuctionWebAPI.Filters
{
    public class ValidationFilterAttribute<T> : IActionFilter where T : class, IEntity, new()
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
            
            if(id < 1)
            {
                context.Result = new BadRequestObjectResult("Bad id parameter");
                return;
            }
            
            var entity = _repositoryManager.GetRepositoryByEntity<T>().Get(id); 
            if (entity == null)
            {
                context.Result = new NotFoundObjectResult("Object is null");
            }
            else
            {
                context.HttpContext.Items.Add("entity", entity);
            }
        }
    }
}
