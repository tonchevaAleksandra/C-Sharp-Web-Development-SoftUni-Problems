using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetAppForTestingRazor.Filters
{
    public class MyResultFilterAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
        }
    }
}
