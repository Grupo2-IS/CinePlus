using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CinePlus.Entities;
using System;

namespace CinePlus.Context.Repositories
{
    public interface IShowingRepository
    {
        Task<Showing> CreateAsync(Showing showing);
        Task<IEnumerable<Showing>> RetrieveAllAsync();
        Task<Showing> RetrieveAsync(int FilmId, int RoomID, DateTime ShowingStart, DateTime ShowingEnd);
        Task<Showing> UpdateAsync(int FilmId, int RoomID, DateTime ShowingStart, DateTime ShowingEnd, Showing showing);
        Task<bool?> DeleteAsync(int FilmId, int RoomID, DateTime ShowingStart, DateTime ShowingEnd);

    }
}