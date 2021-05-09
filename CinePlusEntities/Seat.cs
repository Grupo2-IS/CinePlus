using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinePlus.Entities
{
    public class Seat
    {
        [Required]
        public int SeatID { get; set; }

        [Required]
        public int RoomID { get; set; }

        [Required]
        public int Row { get; set; }

        public virtual Room Room { get; set; }
        
        public virtual ICollection<NormalPurchase> NormalPurchases { get; set; }
        public virtual ICollection<MemberPurchase> MemberPurchases { get; set; }

        public Seat()
        {
            this.NormalPurchases = new HashSet<NormalPurchase>();
            this.MemberPurchases = new HashSet<MemberPurchase>();
        }

        
    }
}