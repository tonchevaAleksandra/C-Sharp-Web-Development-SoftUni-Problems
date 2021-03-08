using Microsoft.EntityFrameworkCore;
using Panda.Data;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Collections.Generic;
using Panda.Services;

namespace Panda
{
    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IPackagesService, PackagesService>();
            serviceCollection.Add<IReceiptsService, ReceiptsService>();
        }

        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }
    }
}
