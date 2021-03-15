using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetAppForTestingRazor.Filters
{
    public class MyExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
        }
    }
}
