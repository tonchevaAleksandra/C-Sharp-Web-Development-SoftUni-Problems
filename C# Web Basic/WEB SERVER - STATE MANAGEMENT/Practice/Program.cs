using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Practice
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


            var match = Regex.Match(request, @"Cookie: user=[^\n]*");
            var username = match.ToString().Split("=").Last();
            /*string responseText = "<h1>Hello Header</h>";*/
            //string responseText = "<form> <input type=text name='username' /> </form>";
            string responseText = @"<h1>" + username + "</h1>" + "<h1>" + DateTime.UtcNow + "</h1>";

            //Thread.Sleep(10000);

            string response = "HTTP/1.0 307 OK" + NewLine +
                              "Server: SoftUniServer/1.0" + NewLine +
                              "Content-Type: text/html" + NewLine +
                              "Set-Cookie: user=Aleks; Max-Age=3600; SameSite=Strict" +/* new DateTime(2021, 3, 1).ToString("R") +*/ NewLine +
                              //"Location: https://github.com/tonchevaAleksandra" + NewLine +
                              //"Content-Disposition: attachment; filenmae=aleks.html" + NewLine +
                              "Content-Length: " +
                              responseText.Length + NewLine +
                              NewLine +
                              responseText;
            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);


            Console.WriteLine(request);
            Console.WriteLine(new string('=', 60));
        }
    }
}
