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

        // GET: api/memberpurchases/[id]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(MemberPurchase))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetMemberPurchase(int MemberID , int SeatID,int FilmID,int RoomID,DateTime ShowingStart)
        {
            MemberPurchase memberPurchase = await this.repository.RetrieveAsync(MemberID , SeatID, FilmID, RoomID,ShowingStart);

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

            return CreatedAtRoute( // 201 Created
                routeName: nameof(this.GetMemberPurchase),
                routeValues: new { id = added.MemberPurchaseID },
                value: added
            );
        }

        // PUT: api/memberpurchases/[id]
        // BODY: MemberPurchase (JSON)
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int MemberID , int SeatID,int FilmID,int RoomID,DateTime ShowingStart, [FromBody] MemberPurchase memberPurchase)
        {
            if (memberPurchase == null || memberPurchase.MemberId != MemberID || memberPurchase.SeatID !=SeatID|| memberPurchase.FilmID!=FilmID|| memberPurchase.RoomID!=RoomID||memberPurchase.ShowingStart!=ShowingStart)
            {
                return BadRequest(); // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad request
            }

            var existing = await this.repository.RetrieveAsync( MemberID ,  SeatID, FilmID, RoomID,ShowingStart);

            if (existing == null)
            {
                return NotFound();  // 404 Resource not found
            }

            await this.repository.UpdateAsync( MemberID ,SeatID, FilmID, RoomID,ShowingStart, memberPurchase);

            return new NoContentResult();   // 204 No Content
        }

        // DELETE: api/memberpurchases/[id]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int MemberID , int SeatID,int FilmID,int RoomID,DateTime ShowingStart)
        {
            MemberPurchase memberPurchase = await this.repository.RetrieveAsync( MemberID ,SeatID,FilmID,RoomID,ShowingStart);
            if (film == null)
            {
                return NotFound();  // 404 Resource No Found
            }

            bool? deleted = await this.repository.DeleteAsync( MemberID ,  SeatID, FilmID, RoomID,ShowingStart);
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