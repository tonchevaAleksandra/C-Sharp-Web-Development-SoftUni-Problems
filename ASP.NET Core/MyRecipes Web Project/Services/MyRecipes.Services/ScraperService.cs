namespace MyRecipes.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    using Data.Common.Repositories;
    using HtmlAgilityPack;
    using Models;
    using MyRecipes.Data.Models;

    public class ScraperService : IScraperService
    {
        private readonly HtmlWeb web;
        private readonly ConcurrentBag<RecipeDto> concurrentBag;

        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IRepository<RecipeIngredient> recipeIngredientsRepository;
        private readonly IRepository<Image> imagesRepository;
        private HttpStatusCode statusCode;

        public ScraperService(HtmlWeb web,
            IDeletableEntityRepository<Category> categoriesRepository,
            IDeletableEntityRepository<Ingredient> ingredientsRepository,
            IDeletableEntityRepository<Recipe> recipeRepository,
            IRepository<RecipeIngredient> recipeIngredientsRepository,
            IRepository<Image> imagesRepository)
        {
            this.web = web;
            this.web.PostResponse += (request, response) =>
            {
                if (response != null)
                {
                    this.statusCode = response.StatusCode;
                }
            };

            this.concurrentBag = new ConcurrentBag<RecipeDto>();

            this.categoriesRepository = categoriesRepository;
            this.ingredientsRepository = ingredientsRepository;
            this.recipesRepository = recipeRepository;
            this.recipeIngredientsRepository = recipeIngredientsRepository;
            this.imagesRepository = imagesRepository;

            this.statusCode = HttpStatusCode.OK;
        }

        public async Task PopulateDbWithRecipes()
        {
            web.PostResponse += (request, response) =>
            {
                if (response != null)
                {
                    this.statusCode = response.StatusCode;
                }
            };

            Parallel.For(1, 2000 + 1, i =>
            {
                try
                {
                    RecipeDto recipe = GetRecipeData(web, i);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            });
        }

        private static RecipeDto GetRecipeData(HtmlWeb web, int i)
        {
            string html = $"https://recepti.gotvach.bg/r-{i}";
            HtmlDocument htmlDoc = web.LoadFromWebAsync(html).GetAwaiter().GetResult();
            if (web.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            RecipeDto recipe = new RecipeDto();
            recipe.OriginalUrl = html;

            recipe.RecipeName = GetRecipeName(htmlDoc);
            if (recipe.RecipeName == null)
            {
                return null;
            }

            recipe.CategoryName = GetCategoryName(htmlDoc);
            if (recipe.CategoryName == null)
            {
                return null;
            }

            recipe.PreparationTime = TimeSpan.Zero;
            recipe.CookingTime = TimeSpan.Zero;
            GetTimes(htmlDoc, recipe);
            recipe.PortionsCount = GetPortions(htmlDoc);
            recipe.Instructions = GetInstructions(htmlDoc);
            List<string> photoLinks = GetLinksOfPhotos(htmlDoc, web);
            List<string> ingredientsWithQuantities = GetIngredientsWithTheirQuantities(htmlDoc);

            return recipe;
        }

        private static string GetRecipeName(HtmlDocument htmlDoc)
        {
            var name = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='combocolumn mr']/h1");

            if (name != null)
            {
                return name.Select(r => r.InnerText)
                    .FirstOrDefault()
                    ?.ToString();
            }

            return string.Empty;
        }

        private static string GetCategoryName(HtmlDocument htmlDoc)
        {
            var category = htmlDoc
                .DocumentNode
                .SelectNodes(@"//div[@class='breadcrumb']");
            if (category != null)
            {
                return category
                    .Select(x => x.InnerText)
                    .FirstOrDefault()
                   ?.Split(" »")
                    .Reverse()
                    .ToList()[1];
            }

            return string.Empty;
        }

        private static void GetTimes(HtmlDocument htmlDoc, RecipeDto recipe)
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
                var preparation = times[0].InnerText.Replace("Приготвяне", string.Empty).Split(" ").FirstOrDefault();

                recipe.PreparationTime = TimeSpan.FromMinutes(int.Parse(preparation));

                var cooking = times[1].InnerText.Replace("Готвене", string.Empty).Split(" ").FirstOrDefault();
                recipe.CookingTime = TimeSpan.FromMinutes(int.Parse(cooking));
            }
            else if (times.Count == 1)
            {
                if (times[0].InnerText.Contains("Приготвяне"))
                {
                    var preparation = times[0].InnerText.Replace("Приготвяне", string.Empty).Split(" ").FirstOrDefault();

                    recipe.PreparationTime = TimeSpan.FromMinutes(int.Parse(preparation));
                }
                else
                {
                    var cooking = times[1].InnerText.Replace("Готвене", string.Empty).Split(" ").FirstOrDefault();
                    recipe.CookingTime = TimeSpan.FromMinutes(int.Parse(cooking));
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
                    .LastOrDefault()
                    ?.Replace("Порции", string.Empty)
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

            var ingredientsParse = htmlDoc
                .DocumentNode
                .SelectNodes(@"//section[@class='products new']/ul/li");

            if (ingredientsParse != null)
            {
                ingredients.AddRange(ingredientsParse
                    .Select(li => li.InnerText)
                    .ToList());
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
