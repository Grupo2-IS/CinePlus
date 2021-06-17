using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CinePlus.Entities;
using CinePlus.Context.Repositories;

namespace CinePlusServices.Controllers
{
    // base address: api/seats
    [Route("api/[controller]")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private IRepository<Seat> repository;

        // constructor injects repository registered in startup
        public SeatsController(IRepository<Seat> repository)
        {
            this.repository = repository;
        }

        // GET: api/seats
        // This will return a list offilms that may be empty.
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Seat>))]
        public async Task<IEnumerable<Seat>> GetSeats(string row)
        {
            if (string.IsNullOrEmpty(row))
            {
                return await this.repository.RetrieveAllAsync();
            }
            else
            {
                return (await this.repository.RetrieveAllAsync())
                        .Where(f => f.Row == row);
            }
        }

        // GET: api/seats/[id]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Seat))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetSeat(int id)
        {
            Seat seat = await this.repository.RetrieveAsync(id);

            if (seat == null)
            {
                return NotFound(); // 404 resource not found
            }
            else
            {
                return Ok(seat);
            }
        }

        // POST: api/seats
        // BODY: Seat (JSON)
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Seat))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] Seat seat)
        {
            if (seat == null)
            {
                return BadRequest();  // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            Seat added = await repository.CreateAsync(seat);

            return CreatedAtRoute( // 201 Created
                routeName: nameof(this.GetSeat),
                routeValues: new { id = added.SeatID },
                value: added
            );
        }

        

        // DELETE: api/Seats/[id]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            Seat seat = await this.repository.RetrieveAsync(id);
            if (seat == null)
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