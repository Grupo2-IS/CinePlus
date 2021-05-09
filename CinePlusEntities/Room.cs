using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinePlus.Entities
{
    public class Room
    {
        [Key]
        [Required]
        public int RoomID { get; set; }

        [MaxLength(40)]
        public string RoomName { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }

        public virtual ICollection<Showing> Showings { get; set; }

        public Room()
        {
            this.Seats = new HashSet<Seat>();
            this.Showings = new HashSet<Showing>();
        }
        
        
    }
}