using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HTTP_Requester
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            const string NewLine = "\r\n";
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 80);
            tcpListener.Start();
            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();

                using NetworkStream networkStream = client.GetStream();
                byte[] requestBytes = new byte[1000000]; //TODO: Use buffer
                int bytesRead = networkStream.Read(requestBytes, 0, requestBytes.Length);
                string request = Encoding.UTF8.GetString(requestBytes, 0, bytesRead);
                /*string responseText = "<h1>Hello Header</h>";*/ 
                //string responseText = "<form> <input type=text name='username' /> </form>";
                string responseText = @"<form action='/Account/Login' method='post'> 
                                        <input type=text name='username' /> 
                                        <input type=password name='password' /> 
                                        <input type=date name='date' /> 
                                        <input type=submit value='login' /> 
                                        </form>";
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
                networkStream.Write(responseBytes, 0, responseBytes.Length);
              

                Console.WriteLine(request);
                Console.WriteLine(new string('=', 60));
            }


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
