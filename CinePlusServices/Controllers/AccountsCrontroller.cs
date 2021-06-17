using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CinePlus.Entities;
using CinePlus.Context.Repositories;
using CinePlus.Context.AuxiliarClass;
using Microsoft.Extensions.Logging;   //esto es con logger q no se usa se puede borrar
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;




namespace CinePlusServices.Controllers
{
	// base address: api/accountsController
	[Route("api/[controller]")]
    [ApiController]
    public class AccountsCrontroller :ControllerBase
    {
    
		private Usermanager<User> userManager;
		private SingInmanager<User> singInmanager;
		public Ilogger<AccountsCrontroller> logger{get;}

		public AccountsCrontroller( Usermanager<User> usermanager, SingInmanager<User> singInmanager, Ilogger<AccountsCrontroller> logger)
		{
			this.userManager = usermanager;
			this.singInmanager = singInmanager;
			this.logger = logger;
		}

		
        // Get: api/accountsController/accesDenied
        // BODY: Film (JSON)
        [HttpGet()]
		[ProducesResponseType(403)]
		public async Task<IActionResult> AccessDenied ()
		{
		return  Forbidden(); //Error 403 Acces Denied
		}



    	// Post: api/accountsController/[userId]/[token]
        // BODY: Film (JSON)
        [HttpGet("{userId:string}/{token:string}")]
		[AllowAnonymous]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> ConfirmEmail(string userId, string token)
		{
 			if (userId == null || token == null)
                return BadRequest(ModelState); //400 Bad Request

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
              return NotFound();  // 404 Resource not found
            }

            var result = await userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
				return Json($"You have been successfully registered");

            }
          	return Json($"Email cannot be confirmed");
		}
		
		// POST: api/accountsController
        // BODY: Film (JSON)
        [HttpPost]
		[ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody] Register model)
		{
			if (! ModelState.IsValid)
			{
				return BadRequest(ModelState);  // 400 Bad Request
			}
			var user = new User()
			{
				UserName = model.Username,
				UserId= model.UserId,
				Email = model.Email,
				Nick = model.FirstName,
				Level = 3,
			};
			// este result es d tipo IdentutyResult 
			var result = await userManager.CreateAsync(user, model.Password);
		
			if (!result.Succeeded)
			{
				return BadRequest(ModelState);  // 400 Bad Request
			}
		
			//ESTE REGISTER ES UN CREATED BASICAMENTE, NO ESTOY SEGURA SI TENGO  Q HaacRE EL METODO D ABAJO	LLAMANDO AL CREATEASYNC DE IUSEREPOSITORY
			//  IUserRepository repository = UserRepository; (se pasa en el constructor en todo caso)
			//User added = await repository.CreateAsync(user);
			// return CreatedAtRoute( // 201 Created
            //     routeName: nameof(this.GetArtist),
            //     routeValues: new { id = added.ArtistID },
            //     value: added
            // );

			await signInManager.SignInAsync(user, isPersistent: false);
			return ok(model);
			}

		
		// Get: api/accountsController/logOut
        // BODY: Film (JSON)
        [HttpGet("{logOut}")]
		[ProducesResponseType(204)]
		public async Task<IActionResult> LogOut()
		{
			await singInmanager.SignOutAsync();
			return new NoContentResult();   // 204 No Content

			// Resumen: RedirectToAction(actionname, routeValue)
			//     Redirects (Microsoft.AspNetCore.Http.StatusCodes.Status302Found) to the specified
			//     action using the actionName.
			//return RedirectToAction("index", "home"); //habriaq poner la direccion del home

		}

		// Get: api/accountsController/logIn
        // BODY: Film (JSON)
		[HttpGet("{logIn}")]
		[AllowAnonymous]
		[ProducesResponseType(400)]
		public async Task<IActionResult> LogIn( [FromBody] Login  login)
		{  
			if (! ModelState.IsValid)
			{
				return BadRequest(ModelState); //400 Bad Request
			}
			var result = await signInManager.PasswordSignInAsync(login.UserName, login.Password, true, false) ;

			if (!result.Succeeded)
			{
				return BadRequest(ModelState); //400 Bad Request
			}
			
			return ok(login);
			//NO ESTOY SEGURA SI PONER UN NO CONTENT RESPONSE 

			//se pasa como parametro la url a la q se kiere ir si el logIn es succesfull      
			// if (string.IsNullOrEmpty(url))
			// {
			//     return RedirectToAction("Index", "Home"); //redireccionar a home , creo q esto se resuelve con un error
			// }
			// return Redirect(url);
		}

		// Get: api/accountsController/userNameInUsed
        // BODY: Film (JSON)
		[HttpGet("{userNameInUsed}")]
		[ProducesResponseType(404)]
		public async Task<IActionResult> IsUserNameInUsed([FromBody]string username)
		{
			var user = await userManager.FindByNameAsync(username);
			if (user is null)
			{
				return NotFound();  // 404 Resource not found

			}
			else
			{
				return Json($"Username {username} already in use");
			}
		}
		
		// PUT: api/accountsController/
        // BODY: Film (JSON)
        [HttpPut()]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		 public async Task<IActionResult> ChangePassword([FromBody]ChangePassword password)
        {
			if (! ModelState.IsValid)
			{
				return BadRequest(ModelState); //400 Bad Request
			}

           
            var user = await userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();  // 404 Resource not found

            var result = await userManager.ChangePasswordAsync(user, password.CurrentPassword, password.NewPassword);

            if(!result.Succeeded)
            {
                return GetErrorResult(result); //400 Bad Request

            }

            await signInManager.RefreshSignInAsync(user);
			//return new NoContentResult();   // 204 No Content
            return Json($"Password change correctly");
        }
	}


}