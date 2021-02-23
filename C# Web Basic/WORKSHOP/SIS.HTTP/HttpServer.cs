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
            string requestAsString = Encoding.UTF8.GetString(requestBytes, 0, bytesRead);
           
            var request = new HttpRequest(requestAsString);
            string content = "<h1>random page</h1>";
            if (request.Path=="/")
            {
                content= "<h1>Home page</h1>";
            }
            else if (request.Path=="/users/login")
            {
                content = "<h1>Login page</h1>";
            }
            
            
            byte[] stringContent = Encoding.UTF8.GetBytes(content);
            var response = new HttpResponse(HttpResponseCode.Ok, stringContent);
            response.Headers.Add(new Header("Sever", "SoftUniServer/1.0"));
            response.Headers.Add(new Header("Content-Type", "text/html"));
           
            byte[] responseBytes = Encoding.UTF8.GetBytes(response.ToString());
            await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
            await networkStream.WriteAsync(response.Body, 0, response.Body.Length);


            Console.WriteLine(request);
            Console.WriteLine(new string('=', 60));
        }
    }
}
