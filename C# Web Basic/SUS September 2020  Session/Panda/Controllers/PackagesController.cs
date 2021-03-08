using System;
using Panda.Services;
using Panda.ViewModels.Packages;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Panda.Controllers
{
    public class PackagesController : Controller
    {
        private IPackagesService packagesService;
        private IUsersService usersService;
        private readonly IReceiptsService receiptsService;


        public PackagesController(IPackagesService packagesService, IUsersService usersService, IReceiptsService receiptsService)
        {
            this.packagesService = packagesService;
            this.usersService = usersService;
            this.receiptsService = receiptsService;
        }
        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var viewModel = this.usersService.GetAllUsernames();
            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(PackageInputModel model)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (String.IsNullOrEmpty(model.Description) || model.Description.Length<5 || model.Description.Length>20)
            {
                return this.Redirect("/Packages/Create");
            }
            var userId = this.usersService.GetUserIdByUsername(model.RecipientName);
            this.packagesService.Create(model.Description,model.Weight, model.ShippingAddress,userId);
            return this.Redirect("/Packages/Pending");
        }

        public HttpResponse Delivered()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var viewModel = new PackagesModel()
            {
                Packages = this.packagesService.GetDeliveredPackages()
            };
            return this.View(viewModel);
        }

        public HttpResponse Pending()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var viewModel = new PackagesModel
            {
                Packages = this.packagesService.GetPendingPackages()
            };

            return this.View(viewModel);
        }

        public HttpResponse Deliver(string id)
        {
            var recipientId = this.packagesService.DeliverPackage(id);

            this.receiptsService.Create(id, recipientId);
            return this.Redirect("/Packages/Delivered");
        }
    }
}
