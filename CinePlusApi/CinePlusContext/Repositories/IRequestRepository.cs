using System.Collections.Generic;
using System.Threading.Tasks;
using CinePlus.Entities;
using System;

namespace CinePlus.Context.Repositories
{
    public interface IRequestRepository
    {
         Task<int> GetEntradasPorDia(int dia, int mes , int año);
         Task<int>  GetEntradasPorMes(int mes,int año);
         Task<int> GetEntradasPoraño(int year );
         Task<int> GetEntradasPorPelicula(int idfilm);
         Task<IEnumerable<Film>> GetFilmsRating();

    }
}