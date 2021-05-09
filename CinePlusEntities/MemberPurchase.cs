using System;
using System.ComponentModel.DataAnnotations;

namespace CinePlus.Entities
{
    public class MemberPurchase
    {

        [Required]
        public int MemberId { get; set; }
        
        [Required]
        public int SeatID { get; set; }
        
        [Required]
        public int FilmID { get; set; }

        [Required]
        public int RoomID { get; set; }

        [Required]
        public DateTime ShowingStart { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public bool PayWithPoints { get; set; }

        [Required]
        public int UsedPoints { get; set; } 
        
        [Required]
        public string PurchaseCode { get; set; }

        public virtual Seat Seat{ get; set; }
        public virtual Showing Showing { get; set; }

        public virtual Member Member { get; set; }

    }
}