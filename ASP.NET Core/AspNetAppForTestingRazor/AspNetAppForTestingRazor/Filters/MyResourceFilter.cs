using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace AspNetAppForTestingRazor.Filters
{
    public class MyResourceFilter : IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
