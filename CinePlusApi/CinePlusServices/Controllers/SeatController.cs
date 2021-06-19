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
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Seat>))]
        public async Task<IEnumerable<Seat>> GetSeats()
        {
            return await this.repository.RetrieveAllAsync();
        }

        // GET: api/seats/[id]
        [HttpGet("{id:int}")]
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


    }
}