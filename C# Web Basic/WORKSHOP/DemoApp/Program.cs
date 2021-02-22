using System.Threading.Tasks;
using SIS.HTTP;

namespace DemoApp
{

    public class Program
    {
        static async Task Main(string[] args)
        {
            // Actions:
            //  / => response IndexPage()
            //  /favicon.ico => favicon.ico
            // GET /Contact => response ShowContactForm()
            // POST /Contact =>response FillContactForm(request)
            // 
            // new HttpServer(80, actions)
            var httpServer = new HttpServer(80);
            // .Start()
            await httpServer.StartAsync();
        }
    }
}
