using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CinePlus.Entities;
using CinePlus.Context.Repositories;

namespace CinePlusServices.Controllers
{
    // base address: api/directors
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

        public async Task<IActionResult> GetDirector(int FilmID,int ArtistID)
        {
            Director director = await this.repository.RetrieveAsync(FilmID,ArtistID);

            if (director == null)
            {
                return NotFound(); // 404 resource not found
            }
            else
            {
                return Ok(director);
            }
        }

        // POST: api/directors
        // BODY: Director (JSON)
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Director))]
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

        // PUT: api/directors/[id]
        // BODY: Director (JSON)
        [HttpPut("{FilmID,ArtistID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int FilmID,int ArtistID, [FromBody]  Director director)
        {
            if (director == null || director.FilmID!=FilmID ||director.ArtistID != ArtistID)
            {
                return BadRequest(); // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad request
            }

            var existing = await this.repository.RetrieveAsync(FilmID, ArtistID);

            if (existing == null)
            {
                return NotFound();  // 404 Resource not found
            }

            await this.repository.UpdateAsync(FilmID,ArtistID, director);

            return new NoContentResult();   // 204 No Content
        }

        // DELETE: api/directors/[id]
        [HttpDelete("{FilmID,ArtistID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int FilmID,int ArtistID)
        {
             Director director = await this.repository.RetrieveAsync(FilmID,ArtistID);
            if (director == null)
            {
                return NotFound();  // 404 Resource No Found
            }

            bool? deleted = await this.repository.DeleteAsync(FilmID,ArtistID);
            if (deleted.HasValue && deleted.Value)
            {
                return new NoContentResult();   // 204 No Content
            }
            else
            {
                return BadRequest(  // 400 Bad Request
                    $"Director with id {id} was found but failed to delete."
                );
            }
        }


    }
}