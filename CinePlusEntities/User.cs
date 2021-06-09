using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CinePlus.Entities
{
    
    public class User: IdentityUser
    {
        [Key]
        [Required]
        public int UserID { get; set; }

        [Required]
        [MaxLength(20)]
        public int Nick { get; set; }

         [Required]
         public int Password { get; set; }

           [Required]
         public int PhoneNumber { get; set; }

        public virtual ICollection<NormalPurchase> NormalPurchases { get; set; }
        public virtual Member Member { get; set; }
        
        public User()
        {
            this.NormalPurchases = new HashSet<NormalPurchase>();
        }

    }

}
