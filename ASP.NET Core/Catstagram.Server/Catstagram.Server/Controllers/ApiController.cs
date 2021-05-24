using Microsoft.AspNetCore.Mvc;

namespace Catstagram.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ApiController:ControllerBase
    {
    }
}
