using AdminDAeropuertos.Models;
using AdminDAeropuertos.Models.AdminCuentas;
using AdminDAeropuertos.Models.CuentasModels;
using AdminDAeropuertos.Models.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinePlusServices.Controllers
{
    
    public class CuentasController:Controller
   {

        private UserManager<Usuario> userManager;
        private SignInManager<Usuario> signInManager;
        


        public CuentasController( UserManager<Usuario> _userManager, SignInManager<Usuario> _signInManager)
        {
           
            userManager = _userManager;
            signInManager = _signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
       
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDCuentas model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, model.Password,true, false);
                    if (result.Succeeded)
                    {
                        if (await userManager.IsInRoleAsync(user, "Admin"))
                        {
                            //Redirecciona a donde se quiere que entren los Admin
                        }
                        else if (await userManager.IsInRoleAsync(user, "Partner"))
                        {
                          //Redirecciona a donde se quiere que entren los socios
                        }
                       
                       

                    }
                }
               
            }
            
              return View();
        }


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            //Se redirecciona a la cartelera                  return RedirectToAction("Login", "Cuentas");
        }

        

    }
}