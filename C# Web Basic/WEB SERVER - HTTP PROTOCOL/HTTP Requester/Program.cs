using System;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace HTTP_Requester
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            
            //TcpListener 
        }

        public static async Task HttpRequest()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "MyConsoleBrowser/1.0");
            HttpResponseMessage response = await client.GetAsync("https://softuni.bg/");
            //HttpResponseMessage response = await client.GetAsync("https://softuni.bg/", new FormUrlEncodedContent());
            string result = await response.Content.ReadAsStringAsync();
            File.WriteAllText("index.html", result);
            //Console.WriteLine(result);
        }
    }
}
