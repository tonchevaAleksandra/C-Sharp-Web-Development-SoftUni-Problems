using System.IO;
using SIS.HTTP;
using SIS.HTTP.Response;

namespace SulsApp.Controllers
{
    public class HomeController
    {
        public HttpResponse Index(HttpRequest request)
        {
            var layout = File.ReadAllText("Views/Shared/layout.html");
            var html = File.ReadAllText("Views/Home/index.html");
            var bodyWithLayout = layout.Replace("@RenderBody()", html);
            return new HtmlResponse(bodyWithLayout);
        }
    }
}
