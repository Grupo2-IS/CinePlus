using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CinePlus.Entities
{
    public class Film
    {
        [Key]
        [Required]
        public int FilmID { get; set; }

        [Required]
        [MaxLength(120)]
        public string Name { get; set; }

        [Required]
        public TimeSpan FilmLength { get; set; }

        [MaxLength(20)]
        public string Country { get; set; }

        [MaxLength(40)]
        public string Genre { get; set; }

        public virtual ICollection<Director> Directors { get; set; }

        public virtual ICollection<Performer> Performers { get; set; }

        public virtual ICollection<Showing> Showings { get; set; }

        public Film()
        {
            this.Directors = new HashSet<Director>();
            this.Performers = new HashSet<Performer>();
            this.Showings = new HashSet<Showing>();
        }

        
    }
}