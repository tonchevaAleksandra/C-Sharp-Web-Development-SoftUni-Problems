using System;
using System.Collections.Generic;
using System.Globalization;
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

            var recipeName = GetRecipeName(htmlDoc);

            var category = GetCategoryName(htmlDoc);

            var author = GetAuthorName(htmlDoc);

            var preparationTime = TimeSpan.MinValue;
            var cookingTime = TimeSpan.MinValue;
            GetTimes(htmlDoc, ref preparationTime, ref cookingTime);


            var portions = GetPortions(htmlDoc);


            List<string> photoLinks = GetLinksOfPhotos(htmlDoc, web);


            Dictionary<string, string> ingredientsWithQuantities = GetIngredientsWithTheirQuantities(htmlDoc);


            Console.WriteLine(GetInstructions(htmlDoc));

        }

        private static string GetRecipeName(HtmlDocument htmlDoc)
        {
            var name = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='combocolumn mr']/h1")
                .FirstOrDefault()
                .InnerHtml
                .ToString();

            Console.WriteLine(name);

            return name;
        }

        private static string GetCategoryName(HtmlDocument htmlDoc)
        {
            var category = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='breadcrumb']/div/a/span")
                .Skip(2)
                .FirstOrDefault()
                .InnerHtml
                .ToString();

            Console.WriteLine(category);

            return category;
        }

        private static string GetAuthorName(HtmlDocument htmlDoc)
        {
            var author = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='autbox']/a")
                .FirstOrDefault()
                .InnerHtml;

            Console.WriteLine(author);

            return author;
        }

        private static void GetTimes(HtmlDocument htmlDoc, ref TimeSpan preparationTime, ref TimeSpan cookingTime)
        {
            var times = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='feat small']");

            if (!times.Any())
            {
                return;
            }

            if (times.Count == 2)
            {
                var preparation = times[0].InnerText.Replace("Приготвяне", "").Split(" ").FirstOrDefault();

                preparationTime = TimeSpan.FromMinutes(int.Parse(preparation));
                Console.WriteLine(preparationTime);

                var cooking = times[1].InnerText.Replace("Готвене", "").Split(" ").FirstOrDefault();
                cookingTime = TimeSpan.FromMinutes(int.Parse(cooking));
                Console.WriteLine(cookingTime);

            }

            else if (times.Count == 1)
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
        }

        private static int GetPortions(HtmlDocument htmlDoc)
        {
            var portions = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='feat']/span")
                .LastOrDefault()
                .InnerHtml
                .ToString();
            var portionsCount = 0;
            int.TryParse(portions, out portionsCount);
            return portionsCount;
        }

        private static List<string> GetLinksOfPhotos(HtmlDocument htmlDoc, HtmlWeb web)
        {
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

                if (photosUrlsToLoad != null)
                {
                    var picturesUrls = photosUrlsToLoad.ToList();

                    if (picturesUrls[0].GetAttributeValue("src", "unknown") ==
                        "https://recepti.gotvach.bg/files/recipes/photos/")
                    {
                        picturesUrls.Clear();
                    }
                    else
                    {
                        photos.AddRange(picturesUrls.Select(p => p.GetAttributeValue("src", "unknown")));
                    }
                }
            }

            return photos;
        }

        private static Dictionary<string, string> GetIngredientsWithTheirQuantities(HtmlDocument htmlDoc)
        {
            var ingredientsQuantities = new Dictionary<string, string>();
            var ingredients = new List<string>();
            var ingredientSection = htmlDoc
                .DocumentNode
                .SelectNodes(@"//section[@class='products new']/ul/li");

            if (ingredientSection.Any())
            {
                ingredients.AddRange(ingredientSection.Select(s => s.InnerText).ToList());
            }

            foreach (var ingredientInfo in ingredients)
            {
                var ingredient = ingredientInfo.Split(" - ").FirstOrDefault().Trim();

                var quantity = ingredientInfo.Split(" - ").LastOrDefault().Trim();

                ingredientsQuantities.Add(ingredient, quantity);
                //краве масло -  100 г меко, на стайна температура
            }

            return ingredientsQuantities;
        }

        private static string GetInstructions(HtmlDocument htmlDoc)
        {
            var instrudctionsToLoad = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='text']/p")
                .Select(x => x.InnerText)
                .ToList();

            var instructions = new StringBuilder();
            if (instrudctionsToLoad.Any())
            {
                instructions.AppendLine(string.Join(Environment.NewLine, instrudctionsToLoad));
            }

            return instructions.ToString().Trim();
        }
    }
}
