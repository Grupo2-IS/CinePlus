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
     [Authorize(Roles = "Admin")]

    public class ArtistsController : ControllerBase
    {
        private IArtistRepository repository;

        // constructor injects repository registered in startup
        public ArtistsController(IArtistRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/films/[id]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Artist))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetArtist(int id)
        {
            Artist artist = await this.repository.RetrieveAsync(id);

            if (artist== null)
            {
                return NotFound(); // 404 resource not found
            }
            else
            {
                return Ok(artist);
            }
        }

        // POST: api/films
        // BODY: Film (JSON)
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Artist))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody]Artist artist)
        {
            if (artist == null)
            {
                return BadRequest();  // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            Artist added = await repository.CreateAsync(artist);

            return CreatedAtRoute( // 201 Created
                routeName: nameof(this.GetArtist),
                routeValues: new { id = added.ArtistID },
                value: added
            );
        }

        // PUT: api/films/[id]
        // BODY: Film (JSON)
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] Artist artist)
        {
            if (artist == null || artist.ArtistID != id)
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

            await this.repository.UpdateAsync(id, artist);

            return new NoContentResult();   // 204 No Content
        }

        // DELETE: api/films/[id]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            Artist artist = await this.repository.RetrieveAsync(id);
            if (artist == null)
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