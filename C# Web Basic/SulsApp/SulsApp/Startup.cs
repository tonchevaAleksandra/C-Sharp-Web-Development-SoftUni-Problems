using SIS.HTTP;
using SIS.MvcFramework;
using SulsApp.Services;
using System.Collections.Generic;

namespace SulsApp
{

    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IProblemsService, ProblemsService>();
        }

        public void Configure(IList<Route> routeTable)
        {
            var db = new ApplicationDbContext();
            //db.Database.Migrate();
        }
    }
}
