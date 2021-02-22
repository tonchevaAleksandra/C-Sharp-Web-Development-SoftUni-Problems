using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIS.HTTP
{
    public class HttpServer : IHttpServer
    {
        private readonly TcpListener tcpListener;

        // TODO: actions
        public HttpServer(int port)
        {
            this.tcpListener = new TcpListener(IPAddress.Loopback, port);

        }
        public async Task StartAsync()
        {
            this.tcpListener.Start();
            while (true)
            {
                TcpClient client = await tcpListener.AcceptTcpClientAsync();
                Task.Run(() => ProcessClientAsync(client));

            }
        }

        public async Task ResetAsync()
        {
            this.Stop();
            await this.StartAsync();
        }

        public void Stop()
        {
            this.tcpListener.Stop();
        }

        private static async Task ProcessClientAsync(TcpClient client)
        {
           
            using NetworkStream networkStream = client.GetStream();
            byte[] requestBytes = new byte[1000000]; //TODO: Use buffer
            int bytesRead = await networkStream.ReadAsync(requestBytes, 0, requestBytes.Length);
            string request = Encoding.UTF8.GetString(requestBytes, 0, bytesRead);
            byte[] fileContent = Encoding.UTF8.GetBytes("<form method='post'><input name='username' /><input type='submit' /></form><h1>Hello, World!</h1>");

            var match = Regex.Match(request, @"Cookie: user=[^\n]*");
            var username = match.ToString().Split('=').Last();
            /*string responseText = "<h1>Hello Header</h>";*/
            //string responseText = "<form> <input type=text name='username' /> </form>";
            string responseText = @"<h1>" + username + "</h1>" + "<h1>" + DateTime.UtcNow + "</h1>";

            //Thread.Sleep(10000);

            string headers = "HTTP/1.0 307 OK" + HttpConstants.NewLine +
                             "Server: SoftUniServer/1.0" + HttpConstants.NewLine +
                             "Content-Type: text/html" + HttpConstants.NewLine +
                             "Content-Length: " +
                             fileContent.Length + HttpConstants.NewLine +
                             HttpConstants.NewLine;
            byte[] headersBytes = Encoding.UTF8.GetBytes(headers);
            await networkStream.WriteAsync(headersBytes, 0, headersBytes.Length);
            await networkStream.WriteAsync(fileContent, 0, fileContent.Length);

            Console.WriteLine(request);
            Console.WriteLine(new string('=', 60));
        }
    }
}
