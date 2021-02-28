using SIS.HTTP;
using SIS.MvcFramework;

namespace SulsApp.Controllers
{
    public class UsersController : Controller
    {
     
        public HttpResponse Login(HttpRequest request)
        {
            return this.View();

        }

        [HttpPost("/Users/Login")]
        public HttpResponse DoLogin(HttpRequest request)
        {
            return this.View();

        }

        public HttpResponse Register(HttpRequest request)
        {
            return this.View();

        }

        [HttpPost("/Users/Register")]
        public HttpResponse DoRegister(HttpRequest request)
        {
            return this.View();

        }
    }
}
