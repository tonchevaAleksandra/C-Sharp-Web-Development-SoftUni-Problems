using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Io;
using HtmlAgilityPack;

namespace Scrapping
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var url = new Url("https://recepti.gotvach.bg/r-174963");


            //var config = Configuration.Default.WithDefaultLoader();
            //var document = await AngleSharp.BrowsingContext.New(config).OpenAsync(url);



            //Console.WriteLine("Serializing the document again:");
            //Console.WriteLine(document.DocumentElement.OuterHtml);

            //HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            //myRequest.Method = "GET";
            //WebResponse myResponse = myRequest.GetResponse();
            //StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            //string result = sr.ReadToEnd();
            //Console.WriteLine(result);
            //sr.Close();
            //myResponse.Close();

            //using (WebClient web1 = new WebClient())
            //{
            //    string data = web1.DownloadString(url);
            //    Console.WriteLine(data);
            //}

            var html = @"https://recepti.gotvach.bg/r-174963";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var portions = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='feat']/span")
                .LastOrDefault()
                .InnerHtml;

            Console.WriteLine(portions);

        }
    }
}
