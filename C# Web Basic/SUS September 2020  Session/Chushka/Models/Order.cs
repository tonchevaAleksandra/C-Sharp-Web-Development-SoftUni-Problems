using System;
using System.ComponentModel.DataAnnotations;

namespace Chushka.Models
{
    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        public string ClientId { get; set; }

        public User Client { get; set; }

        public DateTime OrderedOn { get; set; }

    }
}
