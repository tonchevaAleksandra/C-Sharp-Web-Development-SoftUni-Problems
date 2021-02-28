using System;
using SIS.HTTP;
using SIS.HTTP.Response;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace SIS.MvcFramework
{
    public abstract class Controller
    {

        public HttpRequest Request { get; set; }

        protected HttpResponse View<T>(T viewModel = null, [CallerMemberName] string viewName = null)
        where T : class
        {
            var typeName = this.GetType().Name;
            var controllerName = typeName.Substring(0, typeName.Length - 10);
            var viewPath = "Views/" + controllerName + "/" + viewName + ".html";
            return this.ViewByName<T>(viewPath, viewModel);
        }
        protected HttpResponse View([CallerMemberName] string viewName = null)
        {
            return this.View<object>(null, viewName);

        }

        protected HttpResponse Error(string error)
        {
            return this.ViewByName<ErrorViewModel>( "Views/Shared/Error.html",new ErrorViewModel { Error = error });
        }

        protected HttpResponse Redirect(string url)
        {
            return new RedirectResponse(url);
        }

        protected string Hash(string input)
        {
            var crypt = new SHA256Managed();
            StringBuilder hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));

            foreach (byte cryptoByte in crypto)
            {
                hash .Append(cryptoByte.ToString("x2")); //255 => FF
            }

            return hash.ToString();
        }

        private HttpResponse ViewByName<T>(string viewPath, object viewModel)
        {
            IViewEngine viewEngine = new ViewEngine();

            var html = File.ReadAllText(viewPath);
            html = viewEngine.GetHtml(html, viewModel);

            var layout = File.ReadAllText("Views/Shared/layout.html");
            var bodyWithLayout = layout.Replace("@RenderBody()", html);
            bodyWithLayout = viewEngine.GetHtml(bodyWithLayout, viewModel);
            return new HtmlResponse(bodyWithLayout);
        }
    }
}
