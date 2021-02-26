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
        protected HttpResponse View([CallerMemberName] string viewFileName=null)
        {
           //Environment.StackTrace() => First option
           //var stackTrace = new StackTrace();
           //stackTrace.GetFrames().Skip().Take(); => Second option
           //try
           //{
           //    throw new Exception();
           //}
           //catch (Exception e)
           //{
           //   e.StackTrace  => Third option for dummies :D
           //}
            var layout = File.ReadAllText("Views/Shared/layout.html");
            var controllerName = this.GetType().Name.Replace("Controller", string.Empty);
            var html = File.ReadAllText("Views/" + controllerName + "/" + viewFileName + ".html");
            var bodyWithLayout = layout.Replace("@RenderBody()", html);
            return new HtmlResponse(bodyWithLayout);
        }
    }
}
