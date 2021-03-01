using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SIS.HTTP;
using SIS.MvcFramework;
using SulsApp.Controllers;
using SulsApp.Services;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace SulsApp
{
    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
           
        }

        public void Configure(IList<Route> routeTable)
        {
            var db = new ApplicationDbContext();
            //db.Database.Migrate();
        }
    }
}
