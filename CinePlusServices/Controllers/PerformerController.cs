using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CinePlus.Entities;
using CinePlus.Context.Repositories;

namespace CinePlusServices.Controllers
{
    // base address: api/performers
    [Route("api/[controller]")]
    [ApiController]
    public class PerformersController : ControllerBase
    {
        private IPerformerRepository repository;

        // constructor injects repository registered in startup
        public PerformersController(IPerformerRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/performers/[id]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Performer))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPerformer(int FilmID,int ArtistID)
        {
            Performer performer = await this.repository.RetrieveAsync(FilmID,ArtistID);

            if (performer == null)
            {
                return NotFound(); // 404 resource not found
            }
            else
            {
                return Ok(performer);
            }
        }

        // POST: api/performers
        // BODY: Performer (JSON)
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Performer))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] Performer performer)
        {
            if (performer == null)
            {
                return BadRequest();  // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            Performer added = await repository.CreateAsync(performer);

            return CreatedAtRoute( // 201 Created
                routeName: nameof(this.GetPerformer),
                routeValues: new { id = added.PerformerID },
                value: added
            );
        }

        // PUT: api/performers/[id]
        // BODY: Performer (JSON)
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int FilmID,int ArtistID, [FromBody] Performer performer)
        {
          if (performer == null || performer.FilmID!=FilmID|| performer.ArtistID!=ArtistID)
            {
                return BadRequest(); // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad request
            }

            var existing = await this.repository.RetrieveAsync( FilmID,ArtistID);

            if (existing == null)
            {
                return NotFound();  // 404 Resource not found
            }

            await this.repository.UpdateAsync(FilmID, ArtistID, performer);

            return new NoContentResult();   // 204 No Content
        }

        // DELETE: api/performers/[id]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int FilmID,int ArtistID)
        {
            Performer performer = await this.repository.RetrieveAsync(FilmID,ArtistID);
            if (performer == null)
            {
                return NotFound();  // 404 Resource No Found
            }

            bool? deleted = await this.repository.DeleteAsync(FilmID, ArtistID);
            if (deleted.HasValue && deleted.Value)
            {
                return new NoContentResult();   // 204 No Content
            }
            else
            {
                return BadRequest(  // 400 Bad Request
                    $"Performer with id {id} was found but failed to delete."
                );
            }
        }


    }
}