using System;
using System.Collections.Generic;
using System.Linq;
using Panda.Data;
using Panda.Models;
using Panda.Models.Enums;
using Panda.ViewModels.Packages;

namespace Panda.Services
{
    public class PackagesService : IPackagesService
    {
        private ApplicationDbContext db;

        public PackagesService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Create(string description, decimal weight, string shippingAddress, string userId)
        {
            var package = new Package()
            {
                Description = description,
                EstimatedDeliveryDate = DateTime.UtcNow,
                Weight = weight,
                ShippingAddress = shippingAddress,
                Status = PackageStatus.Pending,
                RecipientId = userId
            };

            this.db.Packages.Add(package);
            this.db.SaveChanges();
        }

        public ICollection<PackageViewModel> GetPendingPackages()
        {
            return this.db.Packages.Where(x => x.Status == PackageStatus.Pending).Select(x => new PackageViewModel()
            {
                Id=x.Id,
                Description = x.Description,
                RecipientName = x.Recipient.Username,
                ShippingAddress = x.ShippingAddress,
                Weight = x.Weight,
                Status = x.Status.ToString()
            }).ToList();
        }

        public ICollection<PackageViewModel> GetDeliveredPackages()
        {
            return this.db.Packages.Where(x => x.Status == PackageStatus.Delivered).Select(x => new PackageViewModel()
            {
                Id = x.Id,
                Description = x.Description,
                RecipientName = x.Recipient.Username,
                ShippingAddress = x.ShippingAddress,
                Weight = x.Weight,
                Status = x.Status.ToString()
            }).ToList();
        }

        public string DeliverPackage(string id)
        {
            var package = this.db.Packages.Find(id);

            package.Status = PackageStatus.Delivered;
            this.db.SaveChanges();

            return package.RecipientId;
        }
    }
}
