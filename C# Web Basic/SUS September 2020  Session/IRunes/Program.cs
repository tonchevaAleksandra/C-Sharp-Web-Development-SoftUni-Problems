using System;
using System.Threading.Tasks;
using SUS.MvcFramework;

namespace IRunes
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await Host.CreateHostAsync(new Startup());
        }
    }
}
