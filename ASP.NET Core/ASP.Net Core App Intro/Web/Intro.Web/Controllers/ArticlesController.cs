namespace Intro.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ArticlesController : Controller
    {
        [RequireHttps]
        public IActionResult ById()
        {
            return this.View();
        }
    }
}
