using System;
using SIS.HTTP;

namespace SIS.HTTP
{
    public class Route
    {
        public Route(string path, HttpMethodType method, Func<HttpRequest, HttpResponse> action)
        {
            this.Path = path;
            this.HttpMethod = method;
            this.Action = action;
        }
        public string Path { get; set; }
        public HttpMethodType HttpMethod { get; set; }
        public Func<HttpRequest, HttpResponse> Action { get; set; }

    }
}
