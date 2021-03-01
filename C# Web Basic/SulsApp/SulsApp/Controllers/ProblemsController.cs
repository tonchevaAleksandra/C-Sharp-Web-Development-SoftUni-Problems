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
            return this.View();
        }

        [HttpPost("/Problems/Create")]
        public HttpResponse DoCreate(string name, int points)
        {
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
