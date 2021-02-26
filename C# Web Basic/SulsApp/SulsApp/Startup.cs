using System.Collections.Generic;
using SIS.HTTP;
using SIS.MvcFramework;
using SulsApp.Controllers;

namespace SulsApp
{
    public class Startup : IMvcApplication
    {
        public void Configure(IList<Route> routeTable)
        {
            routeTable.Add(new Route("/", HttpMethodType.Get, new HomeController().Index));
            routeTable.Add(new Route("/Users/Login", HttpMethodType.Get, new UsersController().Login));
            routeTable.Add(new Route("/Users/Register", HttpMethodType.Get, new UsersController().Register));
            routeTable.Add(new Route("/Submissions", HttpMethodType.Get, new SubmissionsController().Index));
            routeTable.Add(new Route("/css/bootstrap.min.css", HttpMethodType.Get, new StaticFilesController().Bootstrap));
            routeTable.Add(new Route("/css/site.css", HttpMethodType.Get, new StaticFilesController().Site));
            routeTable.Add(new Route("/css/reset-css.css", HttpMethodType.Get, new StaticFilesController().Reset));

        }

        public void ConfigureServices()
        {
            var db = new ApplicationDbContext();
            db.Database.EnsureCreated();
        }
    }
}
