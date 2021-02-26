using SIS.HTTP;
using SIS.MvcFramework;

namespace SulsApp.Controllers
{
    public class HomeController : Controller
    {
        public HttpResponse Index(HttpRequest request)
        {
            return this.View("Home/index.html");
        }
    }
}
