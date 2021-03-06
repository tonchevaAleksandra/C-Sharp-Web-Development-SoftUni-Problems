using SUS.HTTP;
using SUS.MvcFramework;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SharedTrip.Data;

namespace SharedTrip
{
    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {

        }

        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }
    }
}
