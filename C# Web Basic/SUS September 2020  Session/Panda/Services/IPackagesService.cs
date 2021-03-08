using System.Collections.Generic;
using Panda.ViewModels.Packages;

namespace Panda.Services
{
    public interface IPackagesService
    {
        void Create(string description, decimal weight, string shippingAddress, string userId);

        ICollection<PackageViewModel> GetPendingPackages();
        ICollection<PackageViewModel> GetDeliveredPackages();

        string DeliverPackage(string id);
    }
}
