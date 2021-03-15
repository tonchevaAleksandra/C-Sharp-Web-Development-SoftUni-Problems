using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using AspNetAppForTestingRazor.Services;

namespace AspNetAppForTestingRazor.Filters
{
    public class AddHeaderActionFilterAttribute :/*Attribute, IActionFilter*/ ActionFilterAttribute
    {
        //private readonly IShortStringService _shortStringService;

        //public AddHeaderActionFilterAttribute(IShortStringService shortStringService)
        //{
        //    _shortStringService = shortStringService;
        //}
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("X-Info-Action+Name", context.ActionDescriptor.DisplayName);
            //context.HttpContext.Response.WriteAsync("BEFORE!!!");
        }

        //public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //{
        //   //before
        //   if(true)
        //   {
        //       next(); // => this is the Action method
        //    }
          
        //   //after
          
        //}

        public override void  OnActionExecuted(ActionExecutedContext context)
        {

            context.HttpContext.Response.Headers.Add("X-Info-Result-Type", context.Result.GetType().Name);
        }

    }
}
