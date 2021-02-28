using SIS.HTTP;
using SIS.MvcFramework;

namespace SulsApp.Controllers
{
    public class SubmissionsController : Controller
    {
        public HttpResponse Index()
        {
            return this.View();
        }
    }
}
