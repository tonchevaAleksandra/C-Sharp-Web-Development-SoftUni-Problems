using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace AspNetAppForTestingRazor.Filters
{
    public class MyAuthFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
