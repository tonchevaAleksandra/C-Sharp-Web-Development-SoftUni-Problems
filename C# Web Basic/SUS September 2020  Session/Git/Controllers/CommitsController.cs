using Git.Services;
using Git.ViewModels.Commits;
using SUS.HTTP;
using SUS.MvcFramework;
using System;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly ICommitsService commitsService;
        private readonly IRepositoriesService repositoriesService;

        public CommitsController(ICommitsService commitsService, IRepositoriesService repositoriesService)
        {
            this.commitsService = commitsService;
            this.repositoriesService = repositoriesService;
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var repositoryName = this.repositoriesService.GetRepositoryName(id);
            var viewModel = new CreateCommitToRepoViewModel()
            {
                Id = id,
                Name = repositoryName
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(CreateCommitInputModel model, string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (String.IsNullOrEmpty(model.Description) || model.Description.Length < 5)
            {
                return this.Redirect("/Commits/Create");
            }

            var userId = this.GetUserId();
            this.commitsService.Create(model.Description, id, userId);

            return this.Redirect("/Commits/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.GetUserId();
            var viewModel = this.commitsService.GetAllCommitsFromUser(userId);
            return this.View(viewModel);
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.GetUserId();
            if (!this.commitsService.CanUserDeleteThisCommit(userId, id))
            {
                return this.Redirect("/Commits/All");
            }

            this.commitsService.DeleteCommmit(id);
            return this.Redirect("/Repositories/All");
        }
    }
}
