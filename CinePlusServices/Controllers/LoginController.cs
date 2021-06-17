
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CinePlus.Entities;
using CinePlus.Context.Repositories;
using CinePlus.Context.AuxiliarClass;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;



namespace CinePlusServices.Controllers
{
    [Route("api/[controller]")]
  public class LoginController : ControllerBase
  {
    private readonly UserManager<User> _userManager;
    private readonly IJwtFactory _jwtFactory;
    private readonly JwtOptions _jwtOptions;

    public LoginController(UserManager<User> userManager, IJwtFactory jwtFactory, IOptions<JwtOptions> jwtOptions)
    {
      _userManager = userManager;
      _jwtFactory = jwtFactory;
      _jwtOptions = jwtOptions.Value;

    }

    [HttpPost("login")]
    public async Task<IActionResult> Post([FromBody]Login login)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }

      var identity = await GetClaimsIdentity(login.UserName, login.Password);

      if (identity == null)
      {
        return BadRequest();
      }

      var jwt = await TokenGenerator.GenerateJwt(
        identity,
        _jwtFactory,
        credentials.UserName,
        _jwtOptions,
        new JsonSerializerSettings { Formatting = Formatting.Indented });

      return new OkObjectResult(jwt);

    }

    private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
    {
      if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        return await Task.FromResult<ClaimsIdentity>(null);

      // get the user to verifty
      var userToVerify = await _userManager.FindByNameAsync(userName);

      if (userToVerify == null) 
        return await Task.FromResult<ClaimsIdentity>(null);

      // check the credentials
      if (await _userManager.CheckPasswordAsync(userToVerify, password))
      {
        return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
      }

      // Credentials are invalid, or account doesn't exist
      return await Task.FromResult<ClaimsIdentity>(null);

    }

  }
}