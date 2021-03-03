using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IRunes.Models
{
    public class Album
    {
        public Album()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Tracks = new HashSet<Track>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public string Cover { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }
    }
}
