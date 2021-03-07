using Musaca.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Musaca.Models
{
    public class Order
    {

        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Products = new HashSet<Product>();
        }

        [Key]
        public string Id { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime IssuedOn { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string CashierId { get; set; }
        public virtual User Cashier { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
