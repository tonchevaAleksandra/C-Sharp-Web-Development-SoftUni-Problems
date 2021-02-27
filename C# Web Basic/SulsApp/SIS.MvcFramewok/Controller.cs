using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using SIS.HTTP;
using SIS.HTTP.Response;

namespace SIS.MvcFramework
{
    public abstract class Controller
    {
        protected HttpResponse View<T>(T viewModel = null, [CallerMemberName] string viewName = null)
        where T : class
        {
            IViewEngine viewEngine = new ViewEngine();
            var typeName = this.GetType().Name;
            var controllerName = typeName.Substring(0, typeName.Length - 10);
            var html = File.ReadAllText("Views/" + controllerName + "/" + viewName + ".html");
            html = viewEngine.GetHtml(html, null);

            var layout = File.ReadAllText("Views/Shared/_Layout.html");
            var bodyWithLayout = layout.Replace("@RenderBody()", html);
            bodyWithLayout = viewEngine.GetHtml(bodyWithLayout, viewModel);
            return new HtmlResponse(bodyWithLayout);
        }
        protected HttpResponse View([CallerMemberName] string viewName = null)
        {
            return this.View<object>(null, viewName);

        }
    }
}
