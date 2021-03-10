using SUS.MvcFramework;
using System.Threading.Tasks;

namespace Chushka
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await Host.CreateHostAsync(new Startup());
        }
    }
}
