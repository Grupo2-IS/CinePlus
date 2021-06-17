using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CinePlus.Entities;
using CinePlus.Context.Repositories;

namespace CinePlusServices.Controllers
{
    // base address: api/users
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository repository;

        // constructor injects repository registered in startup
        public UsersController(IUserRepository repository)
        {
            this.repository = repository;
        }
        // GET: api/users/[id]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUser(int id)
        {
            User user = await this.repository.RetrieveAsync(id);

            if (user == null)
            {
                return NotFound(); // 404 resource not found
            }
            else
            {
                return Ok(user);
            }
        }

        // POST: api/users
        // BODY: User (JSON)
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(User))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();  // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            User added = await repository.CreateAsync(user);

            return CreatedAtRoute( // 201 Created
                routeName: nameof(this.GetUser),
                routeValues: new { id = added.UserID },
                value: added
            );
        }

        // PUT: api/users/[id]
        // BODY: User (JSON)
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            if (user == null || user.UserID != id)
            {
                return BadRequest(); // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad request
            }

            var existing = await this.repository.RetrieveAsync(id);

            if (existing == null)
            {
                return NotFound();  // 404 Resource not found
            }

            await this.repository.UpdateAsync(id, user);

            return new NoContentResult();   // 204 No Content
        }

        // DELETE: api/users/[id]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            User user = await this.repository.RetrieveAsync(id);
            if (user == null)
            {
                return NotFound();  // 404 Resource No Found
            }

            bool? deleted = await this.repository.DeleteAsync(id);
            if (deleted.HasValue && deleted.Value)
            {
                return new NoContentResult();   // 204 No Content
            }
            else
            {
                return BadRequest(  // 400 Bad Request
                    $"Film with id {id} was found but failed to delete."
                );
            }
        }


    }
}