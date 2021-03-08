using System;
using System.Collections.Generic;
using System.Linq;
using Panda.Data;
using Panda.Models;
using Panda.ViewModels;

namespace Panda.Services
{
    public class ReceiptsService : IReceiptsService
    {
        private ApplicationDbContext db;

        public ReceiptsService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Create(string packageId, string recipientId)
        {
            var packageWeight = this.db.Packages.Where(x => x.Id == packageId).Select(x => x.Weight).FirstOrDefault();
            var receipt = new Receipt()
            {
                Fee = 2.67M* packageWeight,
                IssuedOn = DateTime.UtcNow,
                PackageId = packageId,
                RecipientId = recipientId
            };

            this.db.Receipts.Add(receipt);
            this.db.SaveChanges();
        }

        public ICollection<ReceiptViewModel> GetAllReceipts(string userId)
        {
            var receipts = this.db.Receipts.Where(x => x.RecipientId == userId).Select(x => new ReceiptViewModel()
            {
                Fee = x.Fee,
                Id = x.Id,
                IssuedOn = x.IssuedOn,
                RecipientName = x.Recipient.Username
            }).ToList();

            return receipts;
        }
    }
}
