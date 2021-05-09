using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CinePlus.Entities
{
    
    public class User
    {
        [Key]
        [Required]
        public int UserID { get; set; }

        [Required]
        [MaxLength(20)]
        public int Nick { get; set; }

        public virtual ICollection<NormalPurchase> NormalPurchases { get; set; }
        public virtual Member Member { get; set; }
        
        public User()
        {
            this.NormalPurchases = new HashSet<NormalPurchase>();
        }

    }

}
