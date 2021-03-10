using SUS.HTTP;
using SUS.MvcFramework;

namespace Chushka.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserSignedIn())
            {
                return this.IndexUser();
            }
            return this.View();
        }

        public HttpResponse IndexUser()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Index();
            }

            return this.View();
        }
    }
}
