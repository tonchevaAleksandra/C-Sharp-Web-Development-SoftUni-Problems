using SIS.HTTP;
using SIS.HTTP.Logging;
using SIS.MvcFramework;
using SulsApp.Services;
using System.Collections.Generic;
using ILogger = SIS.HTTP.Logging.ILogger;

namespace SulsApp
{

    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<ILogger, ConsoleLogger>();
        }

        public void Configure(IList<Route> routeTable)
        {
            var db = new ApplicationDbContext();
            //db.Database.Migrate();
        }
    }
}
