using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CinePlus.Entities;
using CinePlus.Context.Repositories;
using System;

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
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<NormalPurchase>))]
        public async Task<IEnumerable<NormalPurchase>> GetAll()
        {
            return await this.repository.RetrieveAllAsync();
        }

        // GET: api/normalpurchases
        // This will return a list offilms that may be empty.

        // GET: api/normalpurchases/[UserId]/[ShowingStart]/[FilmID]/[RoomID]/[SeatID]
        [HttpGet( "{UserId:int}/{SeatID:int}/{FilmID:int}/{RoomID:int}/{ShowingStart:DateTime}") ]
        [ProducesResponseType(200, Type = typeof(NormalPurchase))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetNormalPurchase(int UserId,int SeatID,int FilmID,int RoomID,DateTime ShowingStart)
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

        // PUT: api/normalpurchases/[UserId]/[ShowingStart]/[FilmID]/[RoomID]/[SeatID]
        // BODY: Normalpurchases (JSON)
        [HttpPut("{UserId:int}/{SeatID:int}/{FilmID:int}/{RoomID:int}/{ShowingStart:DateTime}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int UserId,int SeatID,int FilmID,int RoomID,DateTime ShowingStart, [FromBody] NormalPurchase normalPurchase)
        {
             if (normalPurchase == null || normalPurchase.UserId != UserId || normalPurchase.SeatID !=SeatID|| normalPurchase.FilmID!=FilmID|| normalPurchase.RoomID!=RoomID||normalPurchase.ShowingStart!=ShowingStart)
            {
                return BadRequest(); // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad request
            }

            var existing = await this.repository.RetrieveAsync(UserId,SeatID,FilmID,RoomID,ShowingStart);

            if (existing == null)
            {
                return NotFound();  // 404 Resource not found
            }

            await this.repository.UpdateAsync(UserId,SeatID,FilmID,RoomID,ShowingStart, normalPurchase);

            return new NoContentResult();   // 204 No Content
        }

        // DELETE: api/normalpurchases/[UserId]/[ShowingStart]/[FilmID]/[RoomID]/[SeatID]
        [HttpDelete("{UserId:int}/{SeatID:int}/{FilmID:int}/{RoomID:int}/{ShowingStart:DateTime}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int UserId,int SeatID,int FilmID,int RoomID,DateTime ShowingStart)
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
                    $"NormalPurchase with id {id} was found but failed to delete."
                );
            }
        }


    }
}