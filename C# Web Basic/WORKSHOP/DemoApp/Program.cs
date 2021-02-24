using SIS.HTTP;
using SIS.HTTP.Response;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DemoApp
{
    public static class Program
    {
        public static async Task Main()
        {
            var routeTable = new List<Route>
            {
                new Route("/", HttpMethodType.Get, Index),
                new Route("/users/login", HttpMethodType.Get, Login),
                new Route("/users/login", HttpMethodType.Post, DoLogin),
                new Route("/contact", HttpMethodType.Get, Contact),
                new Route("/favicon.ico", HttpMethodType.Get, FavIcon)
            };

            var httpServer = new HttpServer(80, routeTable);

            await httpServer.StartAsync();
        }

        private static HttpResponse FavIcon(HttpRequest request)
        {
            var byteContent = File.ReadAllBytes("wwwroot/favicon.ico");

            return new FileResponse(byteContent, "image/x-icon");
        }


        private static HttpResponse Contact(HttpRequest request) => new HtmlResponse("<h1>Contact</h1>");


        public static HttpResponse Index(HttpRequest request)
        {
            var username = request.SessionData.ContainsKey("Username") ? request.SessionData["Username"] : "Anonymous";
            return new HtmlResponse($"<h1>Home page. Hello, {username}</h1><a href='/users/login/'>Go to login</a>");
        }


        public static HttpResponse Login(HttpRequest request)
        {
            request.SessionData["Username"] = "Aleks";
            return new HtmlResponse("<h1>Login page</h1>");
        }


        public static HttpResponse DoLogin(HttpRequest request) => new HtmlResponse("<h1>Login page form</h1>");

    }
}
