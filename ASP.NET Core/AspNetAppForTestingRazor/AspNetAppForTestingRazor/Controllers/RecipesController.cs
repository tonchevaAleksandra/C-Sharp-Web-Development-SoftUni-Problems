using AspNetAppForTestingRazor.ViewModels.Recipes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AspNetAppForTestingRazor.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RecipesController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Add()
        {
            var model = new AddRecipeInputModel()
            {
                Date = DateTime.UtcNow,
                Time = new RecipeTimeInputModel()
                {
                    CookingTime = 20,
                    PreparationTime = 30
                },
                Name = "Banica",
                FirstCooked = DateTime.UtcNow.AddYears(-130)
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(/*[FromForm]*/ /*[FromQuery]*/ AddRecipeInputModel input)
        {
            if (!input.Image.FileName.EndsWith(".png") /*|| !input.Image.ContentDisposition.EndsWith(".jpg")*/)
            {
                this.ModelState.AddModelError("Image", "Invalid file type.");
            }
            if (input.Image.Length > (10 * 1024 * 1024))
            {
                this.ModelState.AddModelError("Image", "File is too big.");
            }

            if (!ModelState.IsValid)
            {
                return this.View(input);
            }

            using (FileStream fs = new FileStream(this._webHostEnvironment.WebRootPath + "/user.png", FileMode.Create, FileAccess.Write))
            {
                await input.Image.CopyToAsync(fs);
            }

            //input.Image.ContentType => BindNeverAttribute use this
            //IImageFormat format;

            //using (var image = Image.Load(input.Image as Stream, out format))
            //{
            //    image.Mutate(c => c.Resize(30, 30));
            //    image.Save(input.Image as Stream, format);
            //}


            //var count= this.HttpContext.Request.Cookies.Keys.Count;
            return this.RedirectToAction(nameof(ThankYou));
        }

        public IActionResult ThankYou()
        {
            return this.View();
        }

        public IActionResult Image()
        {
            return this.PhysicalFile(this._webHostEnvironment.WebRootPath + "/user.png" , "image/png");
        }
    }
}
