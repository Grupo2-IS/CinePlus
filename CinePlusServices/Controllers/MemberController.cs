using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CinePlus.Entities;
using CinePlus.Context.Repositories;

namespace CinePlusServices.Controllers
{
    // base address: api/members
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private IRepository<Member> repository;

        // constructor injects repository registered in startup
        public MemberController(IRepository<Member> repository)
        {
            this.repository = repository;
        }

        // GET: api/members/[id]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Member))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetMember(int id)
        {
            Member member = await this.repository.RetrieveAsync(id);

            if (member== null)
            {
                return NotFound(); // 404 resource not found
            }
            else
            {
                return Ok(member);
            }
        }

        // POST: api/members
        // BODY: Member (JSON)
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Member))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody]Member member)
        {
            if (member == null)
            {
                return BadRequest();  // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            Member added = await repository.CreateAsync(member);

            return CreatedAtRoute( // 201 Created
                routeName: nameof(this.GetMember),
                routeValues: new { id = added.MemberID },
                value: added
            );
        }

        // PUT: api/members/[id]
        // BODY: Member (JSON)
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] Member member)
        {
            if (member == null || member.MemberID != id)
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

            await this.repository.UpdateAsync(id, member);

            return new NoContentResult();   // 204 No Content
        }

        // DELETE: api/members/[id]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            Member member = await this.repository.RetrieveAsync(id);
            if (member == null)
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
                    $"Member with id {id} was found but failed to delete."
                );
            }
        }


    }
}