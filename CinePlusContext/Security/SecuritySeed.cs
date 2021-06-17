using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CinePlus.Entities;
using CinePlus.Context;

namespace CinePlus.Context.Security
{
    public class SecuritySeed
    {

        public static void SeedData(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,CinePlusDb context)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager,context);
        }

        private static void SeedUsers(UserManager<User> userManager,CinePlusDb context)
        {
            if(userManager.FindByNameAsync("Admin").Result == null)
            {
                var user = new User
                {
                    Email = "admin@mail.com",
                    NormalizedEmail = "ADMIN@MAIL.COM",
                    UserID = 4,
                    Name = "Admin",
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    Nick = "Peny",
                    Level = "3",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = false,
                };
                try
                {
                    IdentityResult identityResult = userManager.CreateAsync(user, "Password123!").Result;
                    if (identityResult.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Admin").Wait();
                    }
                }
                catch (Exception e)
                {
                   var d = e.InnerException;
                }
            }
        //     if (userManager.FindByNameAsync("Moderator").Result == null)
        //     {
        //         var s2 = shoppingCart.AddEntity(new ShoppingCart());
        //         var moderator = new User
        //         {
                  
        //             Email = "moderator@mail.com",
        //             Id = "5",
        //             Info = "Moderator",
        //             LastName = "Moderator",
        //             Name = "Moderator",
        //             NormalizedEmail = "MODERATOR@MAIL.COM",
        //             NormalizedUserName = "MODERATOR",
        //             PhoneNumber = "23456789",
        //             UserName = "Moderator",
        //             SecurityStamp = Guid.NewGuid().ToString(),
        //             LockoutEnabled = false,
        //             ShoppingCartId = s2.Id
        //         };

        //         try
        //         {
        //             IdentityResult identityResult = userManager.CreateAsync(moderator, "Password123!").Result;
        //             if (identityResult.Succeeded)
        //             {
        //                 userManager.AddToRoleAsync(moderator, "Moderator").Wait();
        //             }
        //         }
        //         catch (Exception e)
        //         {
        //             var d = e.InnerException;
        //         }
        //     }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult identityResult = roleManager.CreateAsync(role).Result;
            }
            // if (!roleManager.RoleExistsAsync("Moderator").Result)
            // {
            //     IdentityRole role = new IdentityRole();
            //     role.Name = "Moderator";
            //     role.NormalizedName = "MODERATOR";
            //     IdentityResult identityResult = roleManager.CreateAsync(role).Result;
            // }
        }

      

    }
}
