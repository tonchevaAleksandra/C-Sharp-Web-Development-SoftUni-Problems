using SUS.HTTP;
using SUS.MvcFramework;

namespace Musaca.Controllers
{
    public class ProductsController : Controller
    {

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            return this.View();
        }

        //[HttpPost]
        //public HttpResponse Create()
        //{

        //}

        public HttpResponse All()
        {
            return this.View();
        }
    }
}
