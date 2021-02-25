using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SulsApp.Models
{
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }
     
        public string Id { get; set; }

        [Required]
        //[MinLength(5)]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        //[EmailAddress]
        public string Email { get; set; }

        [Required]
        //[MinLength(6)]
        //[MaxLength(20)]
        public string Password { get; set; }

        public virtual ICollection<Submission> Submissions { get; set; } = new HashSet<Submission>();

    }
}
