using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CinePlus.Entities;
using CinePlus.Context.Repositories;

namespace CinePlusServices.Controllers
{
    // base address: api/normalpurchases
    [Route("api/[controller]")]
    [ApiController]
    public class NormalPurchasesController : ControllerBase
    {
        private INormalPurchaseRepository repository;

        // constructor injects repository registered in startup
        public NormalPurchasesController(INormalPurchaseRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/normalpurchases
        // This will return a list offilms that may be empty.

        // GET: api/normalpurchases/[id]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(NormalPurchase))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetNormalPurchase(int UserId,DateTime ShowingStart,int FilmID,int RoomID,int SeatID)
        {
            NormalPurchase normalPurchase = await this.repository.RetrieveAsync(UserId, ShowingStart,FilmID, RoomID,SeatID);

            if (normalPurchase == null)
            {
                return NotFound(); // 404 resource not found
            }
            else
            {
                return Ok(normalPurchase);
            }
        }

        // POST: api/normalpurchases
        // BODY: Normalpurchases (JSON)
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(NormalPurchase))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] NormalPurchase normalPurchase)
        {
            if (normalPurchase == null)
            {
                return BadRequest();  // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            NormalPurchase added = await repository.CreateAsync(normalPurchase);

            return CreatedAtRoute( // 201 Created
                routeName: nameof(this.GetNormalPurchase),
                routeValues: new { id = added.NormalPurchaseID },
                value: added
            );
        }

        // PUT: api/normalpurchases/[id]
        // BODY: Normalpurchases (JSON)
        [HttpPut("{ UserId, ShowingStart, FilmID, RoomID, SeatID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int UserId,DateTime ShowingStart,int FilmID,int RoomID,int SeatID, [FromBody] NormalPurchase normalPurchase)
        {
            if (normalPurchase == null || normalPurchase.(UserId,ShowingStart,FilmID,RoomID,SeatID) != {UserId,ShowingStart,FilmID, RoomID,SeatID})
            {
                return BadRequest(); // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad request
            }

            var existing = await this.repository.RetrieveAsync(UserId,ShowingStart, FilmID, RoomID, SeatID);

            if (existing == null)
            {
                return NotFound();  // 404 Resource not found
            }

            await this.repository.UpdateAsync(UserId,ShowingStart,FilmID, RoomID, SeatID, normalPurchase);

            return new NoContentResult();   // 204 No Content
        }

        // DELETE: api/normalpurchases/[id]
        [HttpDelete("{ UserId, ShowingStart, FilmID, RoomID, SeatID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int UserId,DateTime ShowingStart,int FilmID,int RoomID,int SeatID)
        {
            NormalPurchase normalPurchase = await this.repository.RetrieveAsync(UserId, ShowingStart, FilmID, RoomID, SeatID);
            if (normalPurchase == null)
            {
                return NotFound();  // 404 Resource No Found
            }

            bool? deleted = await this.repository.DeleteAsync( UserId, ShowingStart, FilmID, RoomID, SeatID);
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