using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catstagram.Server.Controllers
{

    public class HomeController : ApiController
    {
         [Authorize]
       
        public IActionResult Get()
        {
            return this.Ok("Works");
        }
    }
}
