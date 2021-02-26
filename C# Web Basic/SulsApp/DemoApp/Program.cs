using System;
using SIS.HTTP;
using SIS.HTTP.Response;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIS.MvcFramework;

namespace DemoApp
{
    public static class Program
    {
        public static async Task Main()
        {
            await WebHost.StartAsync(new Startup());
        }
    }
}
