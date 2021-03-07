using SUS.MvcFramework;
using System.Threading.Tasks;

namespace Musaca
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await Host.CreateHostAsync(new Startup());
        }
    }
}
