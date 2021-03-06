using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CinePlus.Entities;
using CinePlus.Context.Repositories;
using CinePlus.Authorization;


namespace CinePlusServices.Controllers
{
    // base address: api/films
    // [Authorize("admin,member,user")]
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private IRepository<Film> repository;

        // constructor injects repository registered in startup
        public FilmsController(IRepository<Film> repository)
        {
            this.repository = repository;
        }

        // GET: api/films
        // GET: api/films/?genre=[genre]
        // This will return a list offilms that may be empty.
        [HttpGet("{genre}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Film>))]
        public async Task<IEnumerable<Film>> GetFilms(string genre)
        {
            if (string.IsNullOrEmpty(genre))
            {
                return await this.repository.RetrieveAllAsync();
            }
            else
            {
                return (await this.repository.RetrieveAllAsync())
                        .Where(f => f.Genre == genre);
            }
        }

        // GET: api/film
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Film>))]
        public async Task<IEnumerable<Film>> GetAll()
        {
            return await this.repository.RetrieveAllAsync();
        }


        // GET: api/films/[id]
        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Type = typeof(Film))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetFilm(int id)
        {
            Film film = await this.repository.RetrieveAsync(id);

            if (film == null)
            {
                return NotFound(); // 404 resource not found
            }
            else
            {
                return Ok(film);
            }
        }

        // POST: api/films
        // BODY: Film (JSON)
        [HttpPost]
        // [Authorize("admin")]
        [ProducesResponseType(201, Type = typeof(Film))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] Film film)
        {
            if (film == null)
            {
                return BadRequest();  // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            Film added = await repository.CreateAsync(film);

            // return CreatedAtRoute( // 201 Created
            //     routeName: nameof(this.GetFilm),
            //     routeValues: new { id = added.FilmID },
            //     value: added
            // );
            return StatusCode(201);
        }

        // PUT: api/films/[id]
        // BODY: Film (JSON)
        // [Authorize("admin")]
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] Film film)
        {
            if (film == null || film.FilmID != id)
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

            await this.repository.UpdateAsync(id, film);

            return new NoContentResult();   // 204 No Content
        }

        // DELETE: api/films/[id]
        // [Authorize("admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            Film film = await this.repository.RetrieveAsync(id);
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