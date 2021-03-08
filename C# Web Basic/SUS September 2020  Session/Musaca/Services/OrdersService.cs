using System;
using Musaca.Data;
using Musaca.Models;
using Musaca.Models.Enums;

namespace Musaca.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext db;

        public OrdersService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Create(string userId)
        {
            var order = new Order()
            {
                CashierId = userId,
                IssuedOn = DateTime.UtcNow,
                Status = OrderStatus.Active
            };

            this.db.Orders.Add(order);
            this.db.SaveChanges();
        }
    }
}
