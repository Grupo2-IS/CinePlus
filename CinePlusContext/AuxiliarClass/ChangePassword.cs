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
    public class ChangePassword
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = 
            "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        // public ChangePassword( string current, string newPassword, string confirm)
        // {   
        //     this.CurrentPassword = current;
        //     this.NewPassword = newPassword;
        //     this.ConfirmPassword = confirm;

        // }

    }
       
       
    
}