using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CinePlus.Entities;
using CinePlus.Authorization;
using CinePlus.Context.Repositories;
using System;

namespace CinePlusServices.Controllers
{
    // base address: api/purchases
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private IPurchaseRepository repository;

        // constructor injects repository registered in startup
        public PurchasesController(IPurchaseRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/purchases
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PurchaseWrapper>))]
        public async Task<IEnumerable<PurchaseWrapper>> GetAll()
        {
            return await this.repository.RetrieveAllAsync();
        }

        [HttpGet("byShowing/{FilmID:int}/{RoomID:int}/{ShowingStart:DateTime}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PurchaseWrapper>))]
        public async Task<IEnumerable<PurchaseWrapper>> GetByShowing(int FilmID, int RoomID, DateTime StartDate)
        {
            return await this.repository.RetrieveByShowingAsync(FilmID, RoomID, StartDate);
        }

        // GET: api/purchases/[UserId]/[ShowingStart]/[FilmID]/[RoomID]/[SeatID]
        [HttpGet("{SeatID:int}/{FilmID:int}/{RoomID:int}/{ShowingStart:DateTime}")]
        [ProducesResponseType(200, Type = typeof(PurchaseWrapper))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPurchase(int SeatID, int FilmID, int RoomID, DateTime ShowingStart)
        {
            PurchaseWrapper memberPurchase = await this.repository.RetrieveAsync(SeatID, FilmID, RoomID, ShowingStart);

            if (memberPurchase == null)
            {
                return NotFound(); // 404 resource not found
            }
            else
            {
                return Ok(memberPurchase);
            }
        }

        // POST: api/purchases
        // BODY: Purchase (JSON)
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Purchase))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] Purchase memberPurchase)
        {
            if (memberPurchase == null)
            {
                return BadRequest();  // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            Purchase added = await repository.CreateAsync(memberPurchase);

            return StatusCode(201);
        }

        // PUT: api/purchases/[UserId]/[ShowingStart]/[FilmID]/[RoomID]/[SeatID]
        // BODY: Purchase (JSON)
        [HttpPut("{SeatID:int}/{FilmID:int}/{RoomID:int}/{ShowingStart:DateTime}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int SeatID, int FilmID, int RoomID, DateTime ShowingStart, [FromBody] Purchase memberPurchase)
        {
            if (memberPurchase == null || memberPurchase.SeatID != SeatID || memberPurchase.FilmID != FilmID || memberPurchase.RoomID != RoomID || memberPurchase.ShowingStart != ShowingStart)
            {
                return BadRequest(); // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad request
            }

            var existing = await this.repository.RetrieveAsync(SeatID, FilmID, RoomID, ShowingStart);

            if (existing == null)
            {
                return NotFound();  // 404 Resource not found
            }

            await this.repository.UpdateAsync(SeatID, FilmID, RoomID, ShowingStart, memberPurchase);

            return new NoContentResult();   // 204 No Content
        }

        // DELETE: api/purchases/[UserId]/[ShowingStart]/[FilmID]/[RoomID]/[SeatID]
        [HttpDelete("{SeatID:int}/{FilmID:int}/{RoomID:int}/{ShowingStart:DateTime}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int SeatID, int FilmID, int RoomID, DateTime ShowingStart)
        {
            PurchaseWrapper memberPurchase = await this.repository.RetrieveAsync(SeatID, FilmID, RoomID, ShowingStart);
            if (memberPurchase == null)
            {
                return NotFound();  // 404 Resource No Found
            }

            bool? deleted = await this.repository.DeleteAsync(SeatID, FilmID, RoomID, ShowingStart);
            if (deleted.HasValue && deleted.Value)
            {
                return new NoContentResult();   // 204 No Content
            }
            else
            {
                return BadRequest(  // 400 Bad Request
                    $"Purchase was found but failed to delete."
                );
            }
        }


    }
}