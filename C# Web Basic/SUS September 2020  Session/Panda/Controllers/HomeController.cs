using Panda.Services;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Panda.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersService usersService;

        public HomeController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        [HttpGet("/")]
        public HttpResponse IndexSlash()
        {
            return this.Index();
        }

        public HttpResponse Index()
        {
            if (this.IsUserSignedIn())
            {
                return this.IndexLoggedIn();
            }
            return this.View();
        }

        public HttpResponse IndexLoggedIn()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.GetUserId();
            var model = this.usersService.GetUsername(userId);
            return this.View(model);
        }
    }
}
