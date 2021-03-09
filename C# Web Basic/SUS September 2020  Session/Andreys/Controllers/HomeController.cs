using SUS.HTTP;
using SUS.MvcFramework;

namespace Andreys.App.Controllers
{

    public class HomeController : Controller
    {
        public HttpResponse Index()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpGet("/")]
        public HttpResponse Home()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Home/Index");
            }

            return this.View();
        }
    }
}
