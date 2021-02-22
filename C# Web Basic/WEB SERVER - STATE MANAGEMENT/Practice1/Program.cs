using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Practice1
{
    class Program
    {
        //static Dictionary<string, int> sessionStore = new Dictionary<string, int>();
        const string NewLine = "\r\n";
        static async Task Main(string[] args)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 80);
            tcpListener.Start();
            while (true)
            {
                TcpClient client = await tcpListener.AcceptTcpClientAsync();
                Task.Run(() => ProcessClientAsync(client));

            }
        }

        private static async Task ProcessClientAsync(TcpClient client)
        {
            await using NetworkStream networkStream = client.GetStream();
            byte[] requestBytes = new byte[1000000]; //TODO: Use buffer
            int bytesRead = await networkStream.ReadAsync(requestBytes, 0, requestBytes.Length);
            string request = Encoding.UTF8.GetString(requestBytes, 0, bytesRead);
            byte[] fileContent = File.ReadAllBytes("pexels-photo-617278.jpeg");

            var match = Regex.Match(request, @"Cookie: user=[^\n]*");
            var username = match.ToString().Split("=").Last();
            /*string responseText = "<h1>Hello Header</h>";*/
            //string responseText = "<form> <input type=text name='username' /> </form>";
            string responseText = @"<h1>" + username + "</h1>" + "<h1>" + DateTime.UtcNow + "</h1>";

            //Thread.Sleep(10000);

            string headers = "HTTP/1.0 307 OK" + NewLine +
                              "Server: SoftUniServer/1.0" + NewLine +
                              "Content-Type: image/jpeg" + NewLine +
                              "Content-Length: " +
                              fileContent.Length + NewLine +
                              NewLine ;
            byte[] headersBytes = Encoding.UTF8.GetBytes(headers);
            await networkStream.WriteAsync(headersBytes);
            await networkStream.WriteAsync(fileContent);

            Console.WriteLine(request);
            Console.WriteLine(new string('=', 60));
        }
    }
}
