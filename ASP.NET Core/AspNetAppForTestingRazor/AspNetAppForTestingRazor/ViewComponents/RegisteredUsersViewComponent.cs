using AspNetAppForTestingRazor.Data;
using AspNetAppForTestingRazor.ViewModels.ViewComponents;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AspNetAppForTestingRazor.ViewComponents
{
    public class RegisteredUsersViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _dbContext;

        public RegisteredUsersViewComponent(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
