using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CinePlus.Entities;
using CinePlus.Context.Repositories;

namespace CinePlusServices.Controllers
{
    // base address: api/rooms
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private IRepository<Room> repository;

        // constructor injects repository registered in startup
        public RoomsController(IRepository<Room> repository)
        {
            this.repository = repository;
        }

        // GET: api/rooms
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Room>))]
        public async Task<IEnumerable<Room>> GetAll()
        {
            return await this.repository.RetrieveAllAsync();
        }

        // GET: api/rooms/[id]
        [HttpGet("{id:int}")]
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


    }
}