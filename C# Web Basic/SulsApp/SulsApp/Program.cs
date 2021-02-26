using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SIS.HTTP;
using SulsApp.Controllers;

namespace SulsApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var db = new ApplicationDbContext();
            db.Database.EnsureCreated();

            var routeTable = new List<Route>
            {
                new Route("/", HttpMethodType.Get, new HomeController().Index),
                new Route("/Users/Login", HttpMethodType.Get, new UsersController().Login),
                new Route("/Users/Register", HttpMethodType.Get, new UsersController().Register),
                new Route("/Submissions", HttpMethodType.Get, new SubmissionsController().Index),
                new Route("/css/bootstrap.min.css", HttpMethodType.Get, new StaticFilesController().Bootstrap),
                new Route("/css/site.css", HttpMethodType.Get,new StaticFilesController().Site),
                new Route("/css/reset-css.css", HttpMethodType.Get, new StaticFilesController().Reset),
             

            };

            var httpServer = new HttpServer(80, routeTable);

            await httpServer.StartAsync();
        }
    }
}
