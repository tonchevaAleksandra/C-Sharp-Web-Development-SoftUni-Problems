using SUS.HTTP;
using SUS.MvcFramework;

namespace Musaca.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Products/All");
            }
            return this.View();
        }
        [HttpGet("/Home/Index")]
        public HttpResponse IndexPage()
        {
            return this.Redirect("/");
        }
    }
}
