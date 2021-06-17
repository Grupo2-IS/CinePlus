using System.Collections.Generic;
using System.Link;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CinePlusServices.Controllers
{
      // direccion base : api /
      [Ruta ("api / [controlador]")]
    [ApiController]

    public class RequestController:ControllerBase
    {
         private CinePlusDb.Repositories.IRequestRepository repo;

         public RequestController(CinePlusDb.Repositories.IRequestRepository repo)
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
        public Task<int> GetEntradasPorGenero(string genero)
         {
             return await this.repo.GetEntradasPorGenero(genero);
        }

    }
}