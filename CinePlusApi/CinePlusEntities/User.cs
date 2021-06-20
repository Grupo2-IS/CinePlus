using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace CinePlus.Entities
{

    public class User
    {
        [Key]
        [Required]
        public int UserID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Nick { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Role { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }

        // public virtual ICollection<NormalPurchase> NormalPurchases { get; set; }
        public virtual Member Member { get; set; }

        public User()
        {
            this.Purchases = new HashSet<Purchase>();
        }

    }

}
