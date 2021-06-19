using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CinePlus.Entities;
using CinePlus.Context.Repositories;
using System;

namespace CinePlusServices.Controllers
{
    // base address: api/showings
    [Route("api/[controller]")]
    [ApiController]
    public class ShowingsController : ControllerBase
    {
        private IShowingRepository repository;

        // constructor injects repository registered in startup
        public ShowingsController(IShowingRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/showings
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Showing>))]
        public async Task<IEnumerable<Showing>> GetAll()
        {
            return await this.repository.RetrieveAllAsync();
        }

        // GET: api/showings/[FilmID]/[RoomID]/[ShowingStar]/[ShowingEnd]
        [HttpGet("{FilmId:int}/{RoomID:int}/{ShowingStart:DateTime}/{ShowingEnd:DateTime}")]
        [ProducesResponseType(200, Type = typeof(Showing))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetShowing(int FilmId, int RoomID, DateTime ShowingStart, DateTime ShowingEnd)
        {
            Showing showing = await this.repository.RetrieveAsync(FilmId, RoomID, ShowingStart, ShowingEnd);

            if (showing == null)
            {
                return NotFound(); // 404 resource not found
            }
            else
            {
                return Ok(showing);
            }
        }

        // POST: api/showings
        // BODY: Showings (JSON)
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Showing))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] Showing showing)
        {
            if (showing == null)
            {
                return BadRequest();  // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            Showing added = await repository.CreateAsync(showing);

            return StatusCode(201);
        }

        // PUT: api/Showings/[FilmID]/[RoomID]/[ShowingStar]/[ShowingEnd]
        // BODY: Showing (JSON)
        [HttpPut("{FilmId:int}/{RoomID:int}/{ShowingStart:DateTime}/{ShowingEnd:DateTime}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int FilmID, int RoomID, DateTime ShowingStart, DateTime ShowingEnd, [FromBody] Showing showing)
        {
            if (showing == null || showing.FilmID != FilmID || showing.RoomID != RoomID || showing.ShowingStart != ShowingStart || showing.ShowingEnd != ShowingEnd)
            {
                return BadRequest(); // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad request
            }

            var existing = await this.repository.RetrieveAsync(FilmID, RoomID, ShowingStart, ShowingEnd);

            if (existing == null)
            {
                return NotFound();  // 404 Resource not found
            }

            await this.repository.UpdateAsync(FilmID, RoomID, ShowingStart, ShowingEnd, showing);

            return new NoContentResult();   // 204 No Content
        }

        // DELETE: api/showings/[FilmID]/[RoomID]/[ShowingStar]/[ShowingEnd]
        [HttpDelete("{FilmId:int}/{RoomID:int}/{ShowingStart:DateTime}/{ShowingEnd:DateTime}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int FilmID, int RoomID, DateTime ShowingStart, DateTime ShowingEnd)
        {
            Showing showing = await this.repository.RetrieveAsync(FilmID, RoomID, ShowingStart, ShowingEnd);
            if (showing == null)
            {
                return NotFound();  // 404 Resource No Found
            }

            bool? deleted = await this.repository.DeleteAsync(FilmID, RoomID, ShowingStart, ShowingEnd);
            if (deleted.HasValue && deleted.Value)
            {
                return new NoContentResult();   // 204 No Content
            }
            else
            {
                return BadRequest(  // 400 Bad Request
                    $"Showing was found but failed to delete."
                );
            }
        }


    }
}