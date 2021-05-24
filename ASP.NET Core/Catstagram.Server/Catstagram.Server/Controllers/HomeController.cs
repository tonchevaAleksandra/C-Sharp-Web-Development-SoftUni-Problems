using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catstagram.Server.Controllers
{

    public class HomeController : ApiController
    {
        [Authorize]

        public ActionResult Get()
        {
            return this.Ok("Works");
        }
    }
}
