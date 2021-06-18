using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CinePlus.Entities;
using CinePlus.Context.Repositories;
using System;

namespace CinePlusServices.Controllers
{
    // base address: api/memberpurchases
    [Route("api/[controller]")]
    [ApiController]
    public class MemberPurchasesController : ControllerBase
    {
        private IMemberPurchaseRepository repository;

        // constructor injects repository registered in startup
        public MemberPurchasesController(IMemberPurchaseRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/memberpurchases
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MemberPurchase>))]
        public async Task<IEnumerable<MemberPurchase>> GetAll()
        {
            return await this.repository.RetrieveAllAsync();
        }
         // GET: api/memberpurchases
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MemberPurchase>))]
        public async Task<IEnumerable<MemberPurchase>> GetAll()
        {
            return await this.repository.RetrieveAllAsync();
        }

        // GET: api/memberpurchases/[UserId]/[ShowingStart]/[FilmID]/[RoomID]/[SeatID]
        [HttpGet("{UserId:int}/{SeatID:int}/{FilmID:int}/{RoomID:int}/{ShowingStart:DateTime}")]
        [ProducesResponseType(200, Type = typeof(MemberPurchase))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetMemberPurchase(int MemberID, int SeatID, int FilmID, int RoomID, DateTime ShowingStart)
        {
            MemberPurchase memberPurchase = await this.repository.RetrieveAsync(MemberID, SeatID, FilmID, RoomID, ShowingStart);

            if (memberPurchase == null)
            {
                return NotFound(); // 404 resource not found
            }
            else
            {
                return Ok(memberPurchase);
            }
        }

        // POST: api/memberpurchases
        // BODY: MemberPurchase (JSON)
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(MemberPurchase))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] MemberPurchase memberPurchase)
        {
            if (memberPurchase == null)
            {
                return BadRequest();  // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            MemberPurchase added = await repository.CreateAsync(memberPurchase);

            return StatusCode(201);
        }

        // PUT: api/memberpurchases/[UserId]/[ShowingStart]/[FilmID]/[RoomID]/[SeatID]
        // BODY: MemberPurchase (JSON)
        [HttpPut("{UserId:int}/{SeatID:int}/{FilmID:int}/{RoomID:int}/{ShowingStart:DateTime}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int MemberID, int SeatID, int FilmID, int RoomID, DateTime ShowingStart, [FromBody] MemberPurchase memberPurchase)
        {
            if (memberPurchase == null || memberPurchase.MemberId != MemberID || memberPurchase.SeatID != SeatID || memberPurchase.FilmID != FilmID || memberPurchase.RoomID != RoomID || memberPurchase.ShowingStart != ShowingStart)
            {
                return BadRequest(); // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad request
            }

            var existing = await this.repository.RetrieveAsync(MemberID, SeatID, FilmID, RoomID, ShowingStart);

            if (existing == null)
            {
                return NotFound();  // 404 Resource not found
            }

            await this.repository.UpdateAsync(MemberID, SeatID, FilmID, RoomID, ShowingStart, memberPurchase);

            return new NoContentResult();   // 204 No Content
        }

        // DELETE: api/memberpurchases/[UserId]/[ShowingStart]/[FilmID]/[RoomID]/[SeatID]
        [HttpDelete("{UserId:int}/{SeatID:int}/{FilmID:int}/{RoomID:int}/{ShowingStart:DateTime}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int MemberID, int SeatID, int FilmID, int RoomID, DateTime ShowingStart)
        {
            MemberPurchase memberPurchase = await this.repository.RetrieveAsync(MemberID, SeatID, FilmID, RoomID, ShowingStart);
            if (film == null)
            {
                return NotFound();  // 404 Resource No Found
            }

            bool? deleted = await this.repository.DeleteAsync(MemberID, SeatID, FilmID, RoomID, ShowingStart);
            if (deleted.HasValue && deleted.Value)
            {
                return new NoContentResult();   // 204 No Content
            }
            else
            {
                return BadRequest(  // 400 Bad Request
                    $"MemberPurchase with id {id} was found but failed to delete."
                );
            }
        }


    }
}