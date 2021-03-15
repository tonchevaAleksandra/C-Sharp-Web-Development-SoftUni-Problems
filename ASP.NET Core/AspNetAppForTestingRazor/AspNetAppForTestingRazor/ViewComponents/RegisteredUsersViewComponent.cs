using AspNetAppForTestingRazor.Data;
using AspNetAppForTestingRazor.ViewModels.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AspNetAppForTestingRazor.Services;

namespace AspNetAppForTestingRazor.ViewComponents
{
    public class RegisteredUsersViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IInstanceCounter _instanceCounter;

        public RegisteredUsersViewComponent(ApplicationDbContext dbContext, IInstanceCounter instanceCounter)
        {
            _dbContext = dbContext;
            _instanceCounter = instanceCounter;
        }
        public IViewComponentResult Invoke(string title)
        {
            var users = this._dbContext.Users.Count();
            var viewModel = new RegisteredUsersViewModel()
            {
                Title = title,
                ResiteredUsers = users
            };
            return this.View(viewModel);
        }
    }
}
