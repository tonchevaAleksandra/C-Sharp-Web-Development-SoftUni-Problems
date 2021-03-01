using System.Drawing;
using SIS.HTTP;
using SIS.MvcFramework;
using SulsApp.Services;

namespace SulsApp.Controllers
{
    public class ProblemsController:Controller
    {
        private readonly IProblemsService problemService;

        public ProblemsController(IProblemsService problemsService)
        {
            this.problemService = problemsService;
        }

        public HttpResponse Create()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("Users/Login");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(string name, int points)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("Users/Login");
            }

            if (string.IsNullOrEmpty(name))
            {
                return this.Error("Invalid name");
            }

            if (points <= 0 || points > 100)
            {
                return this.Error("Points range [1-100]");
            }

            this.problemService.CreateProblem(name, points);

            return this.Redirect("/");

        }
    }
}
