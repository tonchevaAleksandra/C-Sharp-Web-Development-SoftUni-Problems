using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;

namespace SulsApp
{
    public class Startup : IMvcApplication
    {
        public void Configure(IList<Route> routeTable)
        {
            var db = new ApplicationDbContext();
            db.Database.EnsureCreated();
            Console.WriteLine("Created SULS database");
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {

        }
    }
}
