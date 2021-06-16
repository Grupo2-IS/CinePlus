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
    public class MemberPurchasesController : ControllerBase
    {
        private IMemberPurchaseRepository repository;

        // constructor injects repository registered in startup
        public MemberPurchasesController(IMemberPurchaseRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/films/[id]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(MemberPurchase))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetMemberPurchase(int id)
        {
            MemberPurchase memberPurchase = await this.repository.RetrieveAsync(id);

            if (memberPurchase == null)
            {
                return NotFound(); // 404 resource not found
            }
            else
            {
                return Ok(memberPurchase);
            }
        }

        // POST: api/films
        // BODY: Film (JSON)
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

        // PUT: api/films/[id]
        // BODY: Film (JSON)
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] MemberPurchase memberPurchase)
        {
            if (memberPurchase == null || memberPurchase.MemberPurchaseID != id)
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

            await this.repository.UpdateAsync(id, memberPurchase);

            return new NoContentResult();   // 204 No Content
        }

        // DELETE: api/films/[id]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            MemberPurchase memberPurchase = await this.repository.RetrieveAsync(id);
            if (film == null)
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