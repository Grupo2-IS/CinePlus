using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System;


namespace CinePlus.Context.AuxiliarClass
{
    public class EditUser
    {
        public EditUser()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }
        public string UserID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public List<string> Claims { get; set; }
        public List<string> Roles { get; set; }
    }

}