using SUS.HTTP;
using SUS.MvcFramework;

namespace Panda.Controllers
{
    public class PackagesController : Controller
    {
        public HttpResponse Create()
        {
            return this.View();
        }

        public HttpResponse Delivered()
        {
            return this.View();
        }

        public HttpResponse Pending()
        {
            return this.View();
        }
    }
}
