using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinePlus.Entities
{
    public class Showing
    {
        [Required]
        public int FilmID { get; set; }

        [Required]
        public int RoomID { get; set; }

        [Required]
        public DateTime ShowingStart { get; set; }

        [Required]
        public DateTime ShowingEnd { get; set; }
        
        public virtual Film Film { get; set; }
        public virtual Room Room { get; set; }
        public virtual ICollection<NormalPurchase> NormalPurchases { get; set; }
        public virtual ICollection<MemberPurchase> MemberPurchases { get; set; }

        public Showing()
        {
            this.NormalPurchases = new HashSet<NormalPurchase>();
            this.MemberPurchases = new HashSet<MemberPurchase>();
        }
        
    }
}