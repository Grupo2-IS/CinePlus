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

    public class RequestController:ControllerBase
    {
         private IRequestRepository repo;

         public RequestController(IRequestRepository repo)
         {

            this.repo=repo; 
         }

        public async Task<int> GetEntradasPorDia(int dia, int mes , int año)
        {
             return await this.repo.GetEntradasPorDia(dia,mes,año);
        }
        public Task<int>  GetEntradasPorMes(int mes,int año)
         {
             return await this.repo.GetEntradasPorMes(mes,año);
        }
        public Task<int> GetEntradasPoraño(int year )
         {
             return await this.repo.GetEntradasPoraño(year);
        }
        public Task<int> GetEntradasPorPelicula(int idfilm)
         {
             return await this.repo.GetEntradasPorPelicula(idfilm);
        }
      

         public Task<IEnumerable<Film>>GetPeliculasMasGustadas()
         {
             return await this.repo.GetFilmsRating();
        }

    }
}