
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace GotvachBgScrapping
{
    class Program
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

            // to 88K is working.
            Parallel.For(1,15, (i) =>
            {
                try
                {
                    GetRecipesHtml(web, i);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            });

        }

        private static void GetRecipesHtml(HtmlWeb web, int i)
        {
            var html = $"https://recepti.gotvach.bg/r-{i}";

            var htmlDoc = web.Load(html);

            if (web.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine($"{i} not found.");

                return;
            }

            //Console.WriteLine("########## RECIPE ID ##########");
            var recipeRealId = html.Split("r-", 2)[1];
            //Console.WriteLine(recipeRealId + Environment.NewLine);

            //Console.WriteLine("########## RECIPE ORIGINAL LINK ##########");
            var originalLink = html;
            //Console.WriteLine(html + Environment.NewLine);

            //Console.WriteLine("########## RECIPE NAME ##########");
            var recipeParse = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='combocolumn mr']/h1");

            if (recipeParse != null)
            {
                var recipeName = recipeParse
                    .Select(r => r.InnerText)
                    .FirstOrDefault();

                //Console.WriteLine(recipeName + Environment.NewLine);
            }

            //Console.WriteLine("########## RECIPE CATEGORY ##########");
            var recipeCategoryParse = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='breadcrumb']");

            if (recipeCategoryParse != null)
            {
                var recipeCategory = recipeCategoryParse
                    .Select(c => c.InnerText)
                    .FirstOrDefault()
                    .Split(" »")
                    .Reverse()
                    .ToList()[1];

                //Console.WriteLine(recipeCategory + Environment.NewLine);
            }

            //Console.WriteLine("########## RECIPE DATE ##########");
            var recipeDateParse = htmlDoc.DocumentNode
                .SelectNodes(@"//span[@class='date']");

            if (recipeDateParse != null)
            {
                var recipeDate = recipeDateParse
                    .Select(r => r.InnerText)
                    .FirstOrDefault();

                //Console.WriteLine(recipeDate + Environment.NewLine);
            }

            //Console.WriteLine("########## RECIPE PICTURES ##########");
            var allPicturesUrlParse = htmlDoc.DocumentNode
                .SelectNodes(@"//a[@class='morebtn']");

            if (allPicturesUrlParse != null)
            {
                var allPicturesUrlParsed = allPicturesUrlParse
                    .FirstOrDefault()
                    ?.GetAttributeValue("href", "unknown");

                var link = web.Load(allPicturesUrlParsed);

                var allPicturesUrlsParse = link
                    .DocumentNode
                    .SelectNodes(@"//div[@class='main']/div/img");

                if (allPicturesUrlsParse != null)
                {
                    var allPicturesUrl = allPicturesUrlsParse.ToList();

                    if (allPicturesUrl[0].GetAttributeValue("src", "unknown") == "https://recepti.gotvach.bg/files/recipes/photos/")
                    {
                        //Console.WriteLine("No pictures." + Environment.NewLine);

                        allPicturesUrl.Clear();
                    }
                    else
                    {
                        foreach (var picture in allPicturesUrl)
                        {
                            //Console.WriteLine(picture.GetAttributeValue("src", "unknown"));
                        }

                        //Console.WriteLine();
                    }
                }
            }

            //Console.WriteLine("########## PREPARATION AND COOKING TIME ##########");
            var timesParse = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='feat small']");

            if (timesParse != null)
            {
                var preparationTime = 0;
                var cookingTime = 0;

                var times = timesParse.Select(pt => pt.InnerText)
                    .ToList();

                if (times[0].Contains("Приготвяне"))
                {
                    preparationTime = int.Parse(times[0].Substring(10, times[0].Length - 15));

                    //Console.WriteLine(preparationTime + "m preparation time.");
                }

                if (times.Count == 2)
                {
                    if (times[0].Contains("Готвене"))
                    {
                        cookingTime = int.Parse(times[1].Substring(7, times[1].Length - 12));

                        //Console.WriteLine(cookingTime + "m cooking time.");
                    }
                }
            }

            // Console.WriteLine("########## PORTIONS COUNT ##########");
            var portionsParse = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='feat']");

            if (portionsParse != null)
            {
                var portionsCount = portionsParse.LastOrDefault()
                    ?.InnerText
                    .Substring(6);

                // Console.WriteLine(portionsCount + " portions");
            }

            // Console.WriteLine("########## INGREDIENTS => QUANTITY ##########");
            var ingredientsAndQuantityParse = htmlDoc
                .DocumentNode
                .SelectNodes(@"//section[@class='products new']/ul/li");

            if (ingredientsAndQuantityParse != null)
            {
                var ingredientsAndQuantity = ingredientsAndQuantityParse
                    .Select(li => li.InnerText);

                var ingredient = "";
                var quantity = "";

                foreach (var body in ingredientsAndQuantity)
                {
                    var split = body
                        .Split(" - ", StringSplitOptions.RemoveEmptyEntries)
                        .Take(2)
                        .ToList();

                    if (split.Count >= 2)
                    {
                        ingredient = split[0];
                        quantity = split[1];

                        //Console.WriteLine(ingredient + " => " + quantity);
                    }
                    else
                    {
                        ingredient = split[0];

                        //Console.WriteLine(ingredient);
                    }
                }
            }

            //Console.WriteLine("########## RECIPE DESCRIPTION ##########");
            var descriptionParse = htmlDoc
                .DocumentNode
                .SelectNodes(@"//p[@class='desc']");

            if (descriptionParse != null)
            {
                var description = descriptionParse
                    .Select(d => d.InnerText)
                    .ToList();

                var fullDescription = new StringBuilder();

                foreach (var desc in description)
                {
                    fullDescription.AppendLine(desc);
                }

                //Console.WriteLine(fullDescription);
            }

            Console.WriteLine($"{i} found.");
        }
    }
    
}
