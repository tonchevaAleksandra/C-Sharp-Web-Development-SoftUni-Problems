using System.Collections;
using System.Collections.Generic;
using SIS.HTTP;

namespace SIS.MvcFramework
{
    public interface IMvcApplication
    {
        void Configure(IList<Route> routeTable);
        void ConfigureServices();
    }
}
