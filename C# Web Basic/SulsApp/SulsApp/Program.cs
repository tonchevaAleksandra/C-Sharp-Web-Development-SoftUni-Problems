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

            };

            var httpServer = new HttpServer(80, routeTable);

            await httpServer.StartAsync();
        }
    }
}
