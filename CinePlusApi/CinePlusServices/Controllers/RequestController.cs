using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CinePlus.Context.Repositories;
using CinePlus.Entities;

namespace CinePlusServices.Controllers
{
    // direccion base : api /

    [ApiController]
    [Route("api/[controller]")]
    public class RequestController : ControllerBase
    {
        private IRequestRepository repo;

        public RequestController(IRequestRepository repo)
        {

            this.repo = repo;
        }

        [HttpGet("sellsByDay")]
        [ProducesResponseType(200, Type = typeof(int))]
        public async Task<int> GetEntradasPorDia(int dia, int mes, int año)
        {
            return await this.repo.GetEntradasPorDia(dia, mes, año);
        }

        [HttpGet("sellsByMoth")]
        [ProducesResponseType(200, Type = typeof(int))]
        public async Task<int> GetEntradasPorMes(int mes, int año)
        {
            return await this.repo.GetEntradasPorMes(mes, año);
        }

        [HttpGet("sellsByYear")]
        [ProducesResponseType(200, Type = typeof(int))]
        public async Task<int> GetEntradasPoraño(int year)
        {
            return await this.repo.GetEntradasPoraño(year);
        }

        [HttpGet("sellsByFilm")]
        [ProducesResponseType(200, Type = typeof(int))]
        public async Task<int> GetEntradasPorPelicula(int idfilm)
        {
            return await this.repo.GetEntradasPorPelicula(idfilm);
        }

        [HttpGet("filmsByRating")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Film>))]
        public async Task<IEnumerable<Film>> GetPeliculasMasGustadas()
        {
            return await this.repo.GetFilmsRating();
        }

    }
}