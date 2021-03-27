using System;
using System.Threading.Tasks;
using AngleSharp;

namespace TestApi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = Configuration.Default.WithDefaultLoader();

            var context = BrowsingContext.New(config);

            var document = await context.OpenAsync("https://recepti.gotvach.bg/");

            Console.WriteLine(document.DocumentElement.OuterHtml);
        }
    }
}
