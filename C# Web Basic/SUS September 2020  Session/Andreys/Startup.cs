using System;
using System.Collections.Generic;
using System.Text;
using Andreys.Data;
using Andreys.Services;
using Microsoft.EntityFrameworkCore;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Andreys
{
    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            //serviceCollection.Add<IPackagesService, PackagesService>();
            //serviceCollection.Add<IReceiptsService, ReceiptsService>();
        }

        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }
    }
}
