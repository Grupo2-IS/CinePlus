using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CinePlus.Entities;
using CinePlus.Context.Repositories;

namespace CinePlusServices.Controllers
{
    public class AccountsCrontroller :ControllerBase
    {
    
        [Route("users")]
	    public IHttpActionResult GetUsers()
	    {
		    return Ok(this.AppUserManager.Users.ToList().Select(u => this.TheModelFactory.Create(u)));
	    }
 
	    [Route("user/{id:guid}", Name = "GetUserById")]
	    public async Task<IHttpActionResult> GetUser(string Id)
	    {
		    var user = await this.AppUserManager.FindByIdAsync(Id);
 
		    if (user != null)
		    {
			    return Ok(this.TheModelFactory.Create(user));
		    }
 
		    return NotFound();
 
	    }
 
	[Route("user/{username}")]
	public async Task<IHttpActionResult> GetUserByName(string username)
	{
		var user = await this.AppUserManager.FindByNameAsync(username);
 
		if (user != null)
		{
			return Ok(this.TheModelFactory.Create(user));
		}
 
		return NotFound();
 
	}

    }
}