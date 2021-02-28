using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SIS.HTTP;
using SIS.MvcFramework;
using SulsApp.Controllers;

namespace SulsApp
{
    public class Startup : IMvcApplication
    {
        public void ConfigureServices()
        {
            var db = new ApplicationDbContext();
            //db.Database.EnsureCreated();
        }

        public void Configure(IList<Route> routeTable)
        {
            
        }
    }
}
