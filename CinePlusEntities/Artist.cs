using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinePlus.Entities
{
    public class Artist
    {
        [Key]
        [Required]
        public int ArtistID { get; set; }

        [MaxLength(120)]
        public string Name { get; set; }

        public virtual ICollection<Director> Directors { get; set; }
        public virtual ICollection<Performer> Performers { get; set; }
        
        public Artist()
        {
            this.Directors =  new HashSet<Director>();
            this.Performers = new HashSet<Performer>();
        }
    }
}