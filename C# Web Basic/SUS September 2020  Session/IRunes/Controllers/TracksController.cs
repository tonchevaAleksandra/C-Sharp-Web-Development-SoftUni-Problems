using SUS.HTTP;
using SUS.MvcFramework;

namespace IRunes.Controllers
{
    public class TracksController : Controller
    {
        public HttpResponse Create()
        {
            return this.View();
        }

        public HttpResponse Details()
        {
            return this.View();
        }
    }
}
