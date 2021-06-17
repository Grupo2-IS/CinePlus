using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CinePlus.Entities;
using CinePlus.Context.Repositories;
using CinePlus.Context.AuxiliarClass;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;




namespace CinePlusServices.Controllers
{
   // [AllowAnonymous]
  [Authorize(Roles = "Admin")]
    public class AdministrationController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly IUserRepository repository;

        // base address: api/administrationController
	    [Route("api/[controller]")]
        [ApiController]

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IUserRepository repository)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.repository = repository;
        }


        // Get: api/administrationController/
        // BODY: Film (JSON)
        [HttpGet]
        [ProducesResponseType(404)]
        [Authorize(Policy = "ManageRolesAndClaimsPolicy")]
        public async Task<IActionResult> ManageUserRoles( [FromBody] UserRole userRole, string Id)
        {
            userRole.UserRoleID = Id;

            var user = await userManager.FindByIdAsync(Id);

            if(user == null)
            {
              return NotFound();  // 404 Resource not found
            }

            var model = new List<Roles>();
            foreach(var role in roleManager.Roles)
            {
                var roles = new Roles
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                }; 

                if (await userManager.IsInRoleAsync(user, role.Name))
                    Roles.IsSelected = true;
                else
                    Roles.IsSelected = false;
                model.Add(roles);
            }
            return ok(model);
        }
        //el mismo Manage pero pasandole la lista d roles



        // Get: api/administrationController/
        // BODY: Film (JSON)
        [HttpGet]
        [ProducesResponseType(404)]
        [Authorize(Policy = "ManageRolesAndClaimsPolicy")]
        public async Task<IActionResult> ManageUserClaims([FromBody] UserRole userRole,string Id)
        {
            userRole.UserRoleID= Id;

            var user = await userManager.FindByIdAsync(Id);

            if (user == null)
            {
               return NotFound();  // 404 Resource not found
            }

            var existingUserClaims = await userManager.GetClaimsAsync(user);
            var model = new List<UserClaims>();

            foreach(Claim claim in ClaimsStore.AllClaims)
            {
                UserClaimsViewModel userClaim = new UserClaimsViewModel
                {
                    ClaimType = claim.Type
                };
                if (existingUserClaims.Any(x => x.Type == claim.Type && x.Value == "true"))               
                    userClaim.IsSelected = true; 

                model.Add(userClaim);
            }
            return ok(model);
        }


        // Post: api/administrationController/
        // BODY: Film (JSON)
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Authorize(Policy = "CreateRolePolicy")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRole model)
        {
           if (! ModelState.IsValid)
			{
				return BadRequest(ModelState);  // 400 Bad Request
			}
           
            var r = await roleManager.FindByNameAsync(model.RoleName);

            if (r is null)
            {
               return NotFound();  // 404 Resource not found
            }

            IdentityRole role = new IdentityRole
            {
                Name = model.RoleName
            };

            IdentityResult result = await roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            //Hacer algo aki

            // Artist added = await repository.CreateAsync(artist);
            // return CreatedAtRoute( // 201 Created
            //     routeName: nameof(this.GetArtist),
            //     routeValues: new { id = added.ArtistID },
            //     value: added
            // );
            
        }

        [HttpPost]
        [ProducesResponseType(404)]
        [Authorize(Policy = "ManageRolePolicy")]
        public async Task<IActionResult> EditRole([FromBody] EditRole model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role is null)
            {
                return NotFound();  // 404 Resource not found
            }
                
            role.Name = model.RoleName;
            var result = await roleManager.UpdateAsync(role);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);

            }               
           // return RedirectToAction("AllRoles", "Administration");
           //ALLROlles retorna una lista d roles roleManager.Roles
        }
    

        [HttpGet]
        [ProducesResponseType(404)]
        [Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
           if (role is null)
            {
                return NotFound();  // 404 Resource not found
            }
            var model = new EditRole
            {
                RoleID = role.Id,
                RoleName = role.Name
            };

            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return ok(model);
        }

        [HttpPost] 
        [ProducesResponseType(404)]
        [Authorize(Policy = "DeleteRolePolicy")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role is null)
              {
                return NotFound();  // 404 Resource not found
              } 

            var result = await roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
                
            return RedirectToAction("AllRoles");
                
        }

        [HttpPost]
        [ProducesResponseType(404)]
        [Authorize(Policy = "EditUserPolicy")]
        public async Task<IActionResult> EditUsersInRole([FromBody] List<UserRole> model, string id)
        {
            var role = await roleManager.FindByIdAsync(id);

           if (role is null)
            {
                return NotFound();  // 404 Resource not found
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserRoleID);
                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                    continue;

                if (result.Succeeded && i == model.Count - 1)
                {
                    return EditRole( id);
                }
            }
            return EditRole(id);
        }


        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string id)
        {
            ViewBag.RoleId = id;
            //List<UserRole> list =

            var role = await roleManager.FindByIdAsync(id);

            if (role is null)
                //Poner pagina o mesnsaje para mostrar los errores
                return RedirectToAction("AllRoles", "Administration");

            var model = new List<UserRoleViewModel>();

            foreach (var user in userManager.Users)
            {
                if (!user.Active) continue;
                var userrole = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                    userrole.IsSelected = true;
                else
                    userrole.IsSelected = false;
                model.Add(userrole);
            }
            return View(model);
        }

        [HttpPost]
        [ProducesResponseType(404)]
        public async Task<IActionResult> EditUser([FromBody] EditUser model)
        {
            var user = await userManager.FindByIdAsync(model.UserID);
            if (user is null)
            {
                return NotFound();  // 404 Resource not found
            } 

            user.Email = model.Email;
            user.UserName = model.UserName;
            user.Name = model.Name;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            
            return RedirectToAction("Users");//List<User>metodo q hay q resolver
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user is null)
            {
                return NotFound();  // 404 Resource not found
            }
            bool? deleted = await this.repository.DeleteAsync((int)id);
            if (deleted.HasValue && deleted.Value)
            {
                return new NoContentResult();   // 204 No Content
            }
            else
            {
                return BadRequest(  // 400 Bad Request
                    $"User with id {id} was found but failed to delete."
                );
            }

        }
    
    }
}