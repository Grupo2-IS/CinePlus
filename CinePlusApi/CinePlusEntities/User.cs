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

        //esto muy probable se use para las autorizaciones.
        //[Required]
        [MaxLength(100)]
        public string Level { get; set; }

        public string Role { get; set; }

        // [MaxLength(120)]
        // [RegularExpression(@"[a-zA-Z0-9\.-_]+@[a-zA-Z0-9\.-_]+")]
        //[Required]
        //public string Email { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }


        public virtual ICollection<NormalPurchase> NormalPurchases { get; set; }
        public virtual Member Member { get; set; }

        public User()
        {
            this.NormalPurchases = new HashSet<NormalPurchase>();
        }

    }

}
