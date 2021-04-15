using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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


            HtmlWeb web = new HtmlWeb();
            HttpStatusCode statusCode = HttpStatusCode.OK;

            web.PostResponse += (request, response) =>
            {
                if (response != null)
                {
                    statusCode = response.StatusCode;
                }
            };
            //Parallel.For(1, 2000 + 1, i =>
            //{
            //    try
            //    {
            //        GetRecipeData(web, i);
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e.Message);
            //    }
            //});

            for (int i = 0; i < 200; i++)
            {
                try
                {
                    GetRecipeData(web, i);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }


        }

        private static void GetRecipeData(HtmlWeb web, int i)
        {
            var html = $"https://recepti.gotvach.bg/r-{i}";
            var htmlDoc = web.Load(html);

            var recipeName = GetRecipeName(htmlDoc);
            if (recipeName == null)
            {
                return;
            }
            Console.WriteLine("Recipe " + recipeName);
            var category = GetCategoryName(htmlDoc);
            if (category == null)
            {
                return;
            }
            Console.WriteLine("Categpry " + category);
            var author = GetAuthorName(htmlDoc);
            if (author == null)
            {
                return;
            }
            Console.WriteLine("Username " + author);
            var preparationTime = TimeSpan.Zero;
            var cookingTime = TimeSpan.Zero;
            GetTimes(htmlDoc, ref preparationTime, ref cookingTime);
            Console.WriteLine("Preparation time " + preparationTime);
            Console.WriteLine("Cooking time " + cookingTime);
            var portions = GetPortions(htmlDoc);
            Console.WriteLine("Portions " + portions);

            List<string> photoLinks = GetLinksOfPhotos(htmlDoc, web);
            Console.WriteLine("Links of pictures :" + string.Join(Environment.NewLine, photoLinks));

            List<string> ingredientsWithQuantities = GetIngredientsWithTheirQuantities(htmlDoc);
            Console.WriteLine("Ingedients: " + string.Join(Environment.NewLine, ingredientsWithQuantities));

            Console.WriteLine("Instructions " + GetInstructions(htmlDoc));
        }

        private static string GetRecipeName(HtmlDocument htmlDoc)
        {
            var name = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='combocolumn mr']/h1");

            if (name != null)
            {
                return name.Select(r => r.InnerText)
                    .FirstOrDefault().ToString();
            }

            return string.Empty;
        }

        private static string GetCategoryName(HtmlDocument htmlDoc)
        {
            var category = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='breadcrumb']/div/a/span");
            if (category != null)
            {
                return category.Skip(2)
                    .FirstOrDefault()
                    .InnerHtml
                    .ToString();
            }

            return string.Empty;
        }

        private static string GetAuthorName(HtmlDocument htmlDoc)
        {
            var author = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='autbox']/a");

            if (author != null)
            {
                return author.Select(x => x.InnerHtml).FirstOrDefault();
            }

            return string.Empty;
        }

        private static void GetTimes(HtmlDocument htmlDoc, ref TimeSpan preparationTime, ref TimeSpan cookingTime)
        {
            var times = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='feat small']");

            if (times == null)
            {
                return;
            }

            if (times.Count == 2)
            {
                var preparation = times[0].InnerText.Replace("Приготвяне", "").Split(" ").FirstOrDefault();

                preparationTime = TimeSpan.FromMinutes(int.Parse(preparation));

                var cooking = times[1].InnerText.Replace("Готвене", "").Split(" ").FirstOrDefault();
                cookingTime = TimeSpan.FromMinutes(int.Parse(cooking));
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

                }
            }
        }

        private static int GetPortions(HtmlDocument htmlDoc)
        {
            var portions = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='feat']/span");

            var portionsCount = 0;
            if (portions != null)
            {

                int.TryParse(portions.Select(x => x.InnerHtml)
                    .LastOrDefault().Replace("Порции", string.Empty)
                       .Replace("бр", string.Empty)
                       .Replace("бр.", string.Empty)
                       .Replace("броя", string.Empty)
                       .Replace("бройки", string.Empty), out portionsCount);
            }

            return portionsCount;
        }

        private static List<string> GetLinksOfPhotos(HtmlDocument htmlDoc, HtmlWeb web)
        {
            var photos = new List<string>();
            var photosUrls = htmlDoc
                .DocumentNode
                .SelectNodes(@"//a[@class='morebtn']");

            if (photosUrls != null)
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

        private static List<string> GetIngredientsWithTheirQuantities(HtmlDocument htmlDoc)
        {

            var ingredients = new List<string>();
            var ingredientSection = htmlDoc
                .DocumentNode
                .SelectNodes(@"//section[@class='products new']/ul/li");

            if (ingredientSection != null)
            {
                ingredients.AddRange(ingredientSection.Select(s => s.InnerHtml).ToList());
            }

            foreach (var ingredientInfo in ingredients)
            {
                var ingredient = ingredientInfo.Split(" - ").FirstOrDefault().Trim();

                var quantity = ingredientInfo.Split(" - ").LastOrDefault().Trim();
                if (ingredient == null || quantity == null)
                {
                    continue;
                }
                ingredients.Add(ingredient + ":" + quantity);

                Console.WriteLine(ingredient + ":" + quantity);
                //краве масло -  100 г меко, на стайна температура
            }

            return ingredients;
        }

        private static string GetInstructions(HtmlDocument htmlDoc)
        {
            var instrudctionsToLoad = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='text']/p");

            var instructions = new StringBuilder();
            if (instrudctionsToLoad != null)
            {
                instructions.AppendLine(string.Join(Environment.NewLine, instrudctionsToLoad.Select(x => x.InnerText)
                    .ToList()));
            }

            return instructions.ToString().Trim();
        }
    }
}
