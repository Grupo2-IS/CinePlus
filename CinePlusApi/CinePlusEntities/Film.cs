using System;
using System.Collections.Generic;
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

        [Required]
        public int Rating { get; set; }

        [Required]
        [MaxLength(500)]
        public string Synopsis { get; set; }

        public virtual ICollection<Showing> Showings { get; set; }

        public Film()
        {
            this.Showings = new HashSet<Showing>();
        }


    }
}