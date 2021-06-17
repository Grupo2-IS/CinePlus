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

     public class CreateRole
    {  
        [Required]
        public string RoleName { get; set; }
    }



    public class EditRole
    {
        public EditRole()
        {
            Users = new List<string>();
        }

        public string RoleID { get; set; }

        [Required]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }

    }

    
}