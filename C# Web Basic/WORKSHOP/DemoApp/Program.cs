using SIS.HTTP;
using SIS.HTTP.Response;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{

    public class Program
    {
        static async Task Main(string[] args)
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

        }


        private static HttpResponse Contact(HttpRequest request) => new HtmlResponse("<h1>Contact</h1>");


        public static HttpResponse Index(HttpRequest request) => new HtmlResponse("<h1>Home page</h1>");


        public static HttpResponse Login(HttpRequest request) => new HtmlResponse("<h1>Login page</h1>");


        public static HttpResponse DoLogin(HttpRequest request) => new HtmlResponse("<h1>Login page form</h1>");

    }
}
