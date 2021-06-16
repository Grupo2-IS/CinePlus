using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CinePlus.Entities;
using CinePlus.Context.Repositories;

namespace CinePlusServices.Controllers
{
    // base address: api/films
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private IDirectorRepository repository;

        // constructor injects repository registered in startup
        public DirectorsController(IDirectorRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> GetDirector(int id)
        {
            Director director = await this.repository.RetrieveAsync(id);

            if (director == null)
            {
                return NotFound(); // 404 resource not found
            }
            else
            {
                return Ok(director);
            }
        }

        // POST: api/films
        // BODY: Film (JSON)
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Film))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] Director director)
        {
            if (director == null)
            {
                return BadRequest();  // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            Director added = await repository.CreateAsync(director);

            return CreatedAtRoute( // 201 Created
                routeName: nameof(this.GetDirector),
                routeValues: new { id = added.IDDirector },
                value: added
            );
        }

        // PUT: api/films/[id]
        // BODY: Film (JSON)
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody]  Director director)
        {
            if (director == null || director.IDDirector != id)
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

            await this.repository.UpdateAsync(id, director);

            return new NoContentResult();   // 204 No Content
        }

        // DELETE: api/films/[id]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
             Director director = await this.repository.RetrieveAsync(id);
            if (director == null)
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