using Panda.Services;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Panda.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly IReceiptsService _receiptsService;

        public ReceiptsController(IReceiptsService receiptsService)
        {
            _receiptsService = receiptsService;
        }
        public HttpResponse Index()
        {
            var userId = this.GetUserId();
            var viewModel = this._receiptsService.GetAllReceipts(userId);
            return this.View(viewModel);
        }
    }
}
