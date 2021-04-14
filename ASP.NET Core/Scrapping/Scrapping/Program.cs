using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
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
            var html = @"https://recepti.gotvach.bg/r-174963";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var name = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='combocolumn mr']/h1")
                .FirstOrDefault()
                .InnerHtml
                .ToString();

            Console.WriteLine(name);

            var author = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='autbox']/a")
                .FirstOrDefault()
                .InnerHtml;

            Console.WriteLine(author);

            var times = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='feat small']");
            var preparationTime = TimeSpan.MinValue;
            var cookingTime = TimeSpan.MinValue;
            if (!times.Any())
            {
                ;
            }
            if (times.Count==2)
            {
                var preparation = times[0].InnerText.Replace("Приготвяне","").Split(" ").FirstOrDefault();

                preparationTime = TimeSpan.FromMinutes(int.Parse(preparation));
                Console.WriteLine(preparationTime);

                var cooking = times[1].InnerText.Replace("Готвене", "").Split(" ").FirstOrDefault();
                cookingTime = TimeSpan.FromMinutes(int.Parse(cooking));
                Console.WriteLine(cookingTime);
            }
            else if (times.Count==1)
            {
                if (times[0].InnerText.Contains("Приготвяне"))
                {
                    var preparation = times[0].InnerText.Replace("Приготвяне", "").Split(" ").FirstOrDefault();

                   preparationTime = TimeSpan.FromMinutes(int.Parse(preparation));
                }
                else
                {
                    var cooking = times[1].InnerText.Replace("Готвене", "").Split(" ").FirstOrDefault();
                    cookingTime = TimeSpan.FromMinutes(int.Parse(cooking));
                    Console.WriteLine(cookingTime);
                }
            }
          

            var portions = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='feat']/span")
                .LastOrDefault()
                .InnerHtml
                .ToString();

            Console.WriteLine(portions);

          
            var photos = new List<string>();
            var photosUrls = htmlDoc
                .DocumentNode
                .SelectNodes(@"//a[@class='morebtn']");

            if (photosUrls.Any())
            {
                var urlPhoros = photosUrls
                    .FirstOrDefault()
                    ?.GetAttributeValue("href", "unknown");

                var link = web.Load(urlPhoros);
               var photosUrlsToLoad = link
                   .DocumentNode
                   .SelectNodes(@"//div[@class='main']/div/img");

               if (photosUrlsToLoad!=null)
               {
                   var picturesUrls = photosUrlsToLoad.ToList();

                   if (picturesUrls[0].GetAttributeValue("src", "unknown")== "https://recepti.gotvach.bg/files/recipes/photos/")
                   {
                       picturesUrls.Clear();
                   }
                   else
                   {
                       photos.AddRange(picturesUrls.Select(p=>p.GetAttributeValue("src", "unknown")));
                   }
               }
            }

            foreach (var photo in photos)
            {
                Console.WriteLine(photo);
            }

            var ingredientSection = htmlDoc
                .DocumentNode
                .SelectNodes(@"//section[@class='products new']/ul")
                .Select(s=>s.InnerHtml);

            Console.WriteLine(string.Join(Environment.NewLine,ingredientSection));
        }
    }
}
