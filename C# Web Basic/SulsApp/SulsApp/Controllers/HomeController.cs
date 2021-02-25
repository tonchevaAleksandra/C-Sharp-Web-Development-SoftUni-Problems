using SIS.HTTP;
using SIS.HTTP.Response;

namespace SulsApp.Controllers
{
    public class HomeController
    {
        public HttpResponse Index(HttpRequest request)
        {
            return new HtmlResponse("<h1>Welcome to SULS Platform!</h1><p><a href='/Users/Login' target='_blanc'>Login</a> to use the Platform.</p><p><a href='/Users/Register' target='_blanc'>Register</a> if you don't have an account.</p>");
        }
    }
}
