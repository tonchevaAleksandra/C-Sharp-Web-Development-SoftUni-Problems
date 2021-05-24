using System.Threading.Tasks;
using Catstagram.Server.Data.Models;
using Catstagram.Server.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Catstagram.Server.Controllers
{

    public class IdentityController : ApiController
    {
        private readonly UserManager<User> userManager;

        public IdentityController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IActionResult> Register(RegisterUserRequestModel model)
        {
            var user = new User()
            {
                Email = model.Email,
                UserName = model.UserName
            };

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return this.Ok();
            }

            return this.BadRequest(result.Errors);
        }
    }
}
