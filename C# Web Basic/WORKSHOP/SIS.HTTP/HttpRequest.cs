using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace SIS.HTTP
{
    public class HttpRequest
    {
        public HttpRequest(string httpRequestAsString)
        {
            this.Headers = new List<Header>();
            StringReader reader = new StringReader(httpRequestAsString);
            reader.ReadLine();
        }
        public HttpMethodType Method { get; set; }
        public string Path { get; set; }
        public HttpVersionType Version { get; set; }

        public IEnumerable<Header> Headers { get; set; }

        public string Body { get; set; }
    }

}
