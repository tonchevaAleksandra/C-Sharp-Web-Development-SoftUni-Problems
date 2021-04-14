using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
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

            var category = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='breadcrumb']/div/a/span")
                .Skip(2)
                .FirstOrDefault()
                .InnerHtml
                .ToString();

            Console.WriteLine(category);

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

            var ingredients = new List<string>();
            var ingredientSection = htmlDoc
                .DocumentNode
                .SelectNodes(@"//section[@class='products new']/ul/li");

            if (ingredientSection.Any())
            {
                ingredients.AddRange(ingredientSection.Select(s=>s.InnerText).ToList());
            }

            foreach (var ingredientInfo in ingredients)
            {
                var ingredient = ingredientInfo.Split(" - ").FirstOrDefault().Trim();
                Console.WriteLine(ingredient);
                var quantity = ingredientInfo.Split(" - ").LastOrDefault().Trim();
                Console.WriteLine(quantity);
                //краве масло -  100 г меко, на стайна температура
            }
            Console.WriteLine(string.Join(Environment.NewLine,ingredients));


            var instrudctionsToLoad = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='text']/p")
                .Select(x=>x.InnerText)
                .ToList();

            Console.WriteLine(string.Join(Environment.NewLine, instrudctionsToLoad));
            var instructions = new StringBuilder();
            if (instrudctionsToLoad.Any())
            {

                instructions.AppendLine(string.Join(Environment.NewLine, instrudctionsToLoad));
            }

            Console.WriteLine(instructions);
        }
    }
}
