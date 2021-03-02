using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SulsApp
{
    public class Startup : IMvcApplication
    {
        public void Configure(IList<Route> routeTable)
        {
            var db = new ApplicationDbContext();
            db.Database.MigrateAsync();
            Console.WriteLine("Created SULS database");
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {

        }
    }
}
