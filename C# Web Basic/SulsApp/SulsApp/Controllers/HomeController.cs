using System;
using System.Linq;
using SIS.HTTP;
using SIS.MvcFramework;
using SulsApp.ViewModels;

namespace SulsApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
        {
            var viewModel = new IndexViewModel
            {
                Message = this.Request.Headers.FirstOrDefault(x=>x.Name=="User-Agent").Value,
                //Message = "Welcome to SULS Platform!",
                Year = DateTime.UtcNow.Year,
            };
            return this.View(viewModel);
         
        }
    }
}
