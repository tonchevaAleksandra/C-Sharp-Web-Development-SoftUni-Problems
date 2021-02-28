using SIS.MvcFramework;
using System;
using System.Collections.Generic;

namespace SulsApp.Models
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public virtual ICollection<Submission> Submissions { get; set; } = new HashSet<Submission>();

    }
}
