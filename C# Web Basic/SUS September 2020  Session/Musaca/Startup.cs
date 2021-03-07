using Microsoft.EntityFrameworkCore;
using Musaca.Data;
using Musaca.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Collections.Generic;

namespace Musaca
{
    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IProductsService, ProductsService>();
            serviceCollection.Add<IOrdersService, OrdersService>();
        }

        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }
    }
}
