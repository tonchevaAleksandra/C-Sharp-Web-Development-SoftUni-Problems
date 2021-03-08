using SUS.HTTP;
using SUS.MvcFramework;

namespace Panda.Controllers
{
    public class ReceiptsController : Controller
    {
        public HttpResponse Index()
        {
            return this.View();
        }
    }
}
