using SIS.HTTP;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{

    public class Program
    {
        static async Task Main(string[] args)
        {

            var routeTable = new List<Route>();
            routeTable.Add(new Route("/", HttpMethodType.Get, Index));
            routeTable.Add(new Route("/users/login", HttpMethodType.Get, Login));
            routeTable.Add(new Route("/users/login", HttpMethodType.Post, DoLogin));
            routeTable.Add(new Route("/contact", HttpMethodType.Get, Contact));
            routeTable.Add(new Route("/favicon.ico", HttpMethodType.Get, FavIcon));

            var httpServer = new HttpServer(80, routeTable);

            await httpServer.StartAsync();
        }

        private static HttpResponse FavIcon(HttpRequest request)
        {
            var content = "<img scr='/images/img.jpeg' />";
            byte[] stringContent = Encoding.UTF8.GetBytes(content);
            var response = new HttpResponse(HttpResponseCode.Ok, stringContent);
            response.Headers.Add(new Header("Content-Type", "image/png"));
            return response;
        }

        private static HttpResponse Contact(HttpRequest request)
        {
            var content = "<h1>Contact</h1>";
            byte[] stringContent = Encoding.UTF8.GetBytes(content);
            var response = new HttpResponse(HttpResponseCode.Ok, stringContent);
            response.Headers.Add(new Header("Content-Type", "text/html"));
            return response;
        }

        public static HttpResponse Index(HttpRequest request)
        {
            var content = "<h1>Home page</h1>";
            byte[] stringContent = Encoding.UTF8.GetBytes(content);
            var response = new HttpResponse(HttpResponseCode.Ok, stringContent);
            response.Headers.Add(new Header("Content-Type", "text/html"));
            return response;
        }

        public static HttpResponse Login(HttpRequest request)
        {
            var content = "<h1>Login page</h1>";
            byte[] stringContent = Encoding.UTF8.GetBytes(content);
            var response = new HttpResponse(HttpResponseCode.Ok, stringContent);
            response.Headers.Add(new Header("Content-Type", "text/html"));
            return response;
        }

        public static HttpResponse DoLogin(HttpRequest request)
        {
            var content = "<h1>Login page</h1>";
            byte[] stringContent = Encoding.UTF8.GetBytes(content);
            var response = new HttpResponse(HttpResponseCode.Ok, stringContent);
            response.Headers.Add(new Header("Content-Type", "text/html"));
            return response;
        }
    }


}
