using System.IO;
using SIS.HTTP;
using SIS.HTTP.Response;

namespace SIS.MvcFramework
{
    public abstract class Controller
    {
        protected HttpResponse View(string viewPath)
        {
            var layout = File.ReadAllText("Views/Shared/layout.html");
            var html = File.ReadAllText("Views/" + viewPath);
            var bodyWithLayout = layout.Replace("@RenderBody()", html);
            return new HtmlResponse(bodyWithLayout);
        }
    }
}
