using System;
using SIS.HTTP;
using SIS.MvcFramework;
using SulsApp.ViewModels;

namespace SulsApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index(HttpRequest request)
        {
        
            return this.View();
        }
    }
}
