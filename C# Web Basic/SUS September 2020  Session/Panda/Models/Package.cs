using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Panda.Models.Enums;

namespace Panda.Models
{
  public  class Package
    {
        public Package()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public string SippingAddress { get; set; }
        public PackageStatus Status { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }

        [Required]
        public string RecipientId  { get; set; }
        public virtual User Recipient  { get; set; }

    }
}
