using SIS.HTTP;
using SIS.MvcFramework;

namespace SulsApp.Controllers
{
    public class SubmissionsController : Controller
    {
        public HttpResponse Index(HttpRequest request)
        {
            return this.View();
        }
    }
}
