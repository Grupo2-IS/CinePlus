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
    public class RoomsController : ControllerBase
    {
        private IRoomRepository repository;

        // constructor injects repository registered in startup
        public RoomsController(IRoomRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/films
        // GET: api/films/?genre=[genre]
        // This will return a list offilms that may be empty.
       

        // GET: api/films/[id]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Room))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetRoom(int id)
        {
            Room room = await this.repository.RetrieveAsync(id);

            if (room == null)
            {
                return NotFound(); // 404 resource not found
            }
            else
            {
                return Ok(room);
            }
        }

        // POST: api/films
        // BODY: Film (JSON)
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Room))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] Room room)
        {
            if (room == null)
            {
                return BadRequest();  // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            Room added = await repository.CreateAsync(room);

            return CreatedAtRoute( // 201 Created
                routeName: nameof(this.GetRoom),
                routeValues: new { id = added.RoomID },
                value: added
            );
        }

        // PUT: api/films/[id]
        // BODY: Film (JSON)
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] Room room)
        {
            if ( room == null ||room.RoomID != id)
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

            await this.repository.UpdateAsync(id, room);

            return new NoContentResult();   // 204 No Content
        }

        // DELETE: api/films/[id]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            Room room = await this.repository.RetrieveAsync(id);
            if (room == null)
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