using IRunes.Services;
using IRunes.ViewModels.Home;
using SUS.HTTP;
using SUS.MvcFramework;

namespace IRunes.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersService usersService;

        public HomeController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserSignedIn())
            {
                var viewModel = new IndexLoggedInViewModel();
                var userId = this.GetUserId();
                var username = this.usersService.GetUsername(userId);
                viewModel.Username = username;
                return this.View(viewModel, "Home");
            }

            return this.View();
        }

        [HttpGet("/Home/Index")]
        public HttpResponse IndexFullPage()
        {
            return this.Index();
        }

        //public HttpResponse Home(IndexLoggedInViewModel model)
        //{
        //    return this.View(model);
        //}
    }
}
