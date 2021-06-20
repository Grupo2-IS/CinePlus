using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinePlus.Entities
{
    public class Member
    {
        [Key]
        [Required]
        public int MemberID { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserID { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public string Email { get; set; }

        // public virtual ICollection<MemberPurchase> MemberPurchases { get; set; }

        public virtual User User { get; set; }

        public Member()
        {
            // this.MemberPurchases = new HashSet<MemberPurchase>();
        }


    }
}