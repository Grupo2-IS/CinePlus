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
    public class Roles
    {
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    } 
}
