using IRunes.Data;
using Microsoft.EntityFrameworkCore;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Collections.Generic;

namespace Git
{
    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            //serviceCollection.Add<IUsersService, UsersService>();
            //serviceCollection.Add<ITracksService, TracksService>();
            //serviceCollection.Add<IAlbumsService, AlbumsService>();
        }

        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }
    }
}
