
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Data.Entity;
using CinePlus.Entities

namespace CinePlusServices.Controllers
{
    
       [Authorization(Roles="Admin")]
    public class CuentaDAdminController : Controller
    {
        private readonly UserManager<Usuario> userManager;
        public CinePlusDB context;
        
        
        public CuentaDSocioController(UserManager<Usuario> _userManager,CinePlusDB _context)
        {
            
            userManager = _userManager;
            context = _context;
        }

        
        public IActionResult Create()
        {
            
            return View();
        }

        [Authorization(Roles="Admin")]
        public IActionResult Index()
        {
            
            return View(context);
           
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAccount model)
        {


            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserID = model.Identidad,
                    Nick = model.UserName,
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber,
                    
                };
                
                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                await userManager.AddToRoleAsync(user,"Admin");

                if (result.Succeeded)
                {
                     //Redirecciona hacia la cartelera
                    
                }

            }
            return View();
        }

          
        
        public IActionResult Delete(string Username)
        {
            
            foreach (var user in context.Users)
            {
                if (user.UserName == Username)
                {
                    return View(user);
                }
            }

            
                return NotFound();
            

          //  return View(user);

          //  var user = await context.Users.FindAsync( (m=>m.UserName==Username);
            

            
        }

        // POST: Aeropuertos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> DeleteConfirmed(string Username)
        {
            var user = await context.Users.FindAsync(Username);

            foreach (var item in context.Users)
            {
                if (item.UserName == Username)
                {
                      context.Users.Remove(item);
                }

            }
            
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        

        

    }
}