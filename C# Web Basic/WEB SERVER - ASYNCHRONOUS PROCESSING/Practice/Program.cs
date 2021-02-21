using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Practice
{
    class Program
    {
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
            /*string responseText = "<h1>Hello Header</h>";*/
            //string responseText = "<form> <input type=text name='username' /> </form>";
            string responseText = @"<form action='/Account/Login' method='post'> 
                                        <input type=text name='username' /> 
                                        <input type=password name='password' /> 
                                        <input type=date name='date' /> 
                                        <input type=submit value='login' /> 
                                        </form>" + "<h1>" + DateTime.UtcNow + "</h1>";

            Thread.Sleep(10000);

            string response = "HTTP/1.0 307 OK" + NewLine +
                              "Server: SoftUniServer/1.0" + NewLine +
                              "Content-Type: text/html" + NewLine +
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
