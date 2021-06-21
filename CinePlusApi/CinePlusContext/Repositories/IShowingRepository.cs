using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CinePlus.Entities;


namespace CinePlus.Context.Repositories
{
    public interface IShowingRepository
    {
        Task<ShowingWrapper> CreateAsync(Showing showing);
        Task<IEnumerable<ShowingWrapper>> GetActiveShowings();
        Task<IEnumerable<ShowingWrapper>> RetrieveAllAsync();
        Task<ShowingWrapper> RetrieveAsync(int FilmId, int RoomID, DateTime ShowingStart, DateTime ShowingEnd);
        Task<Showing> UpdateAsync(int FilmId, int RoomID, DateTime ShowingStart, DateTime ShowingEnd, Showing showing);
        Task<bool?> DeleteAsync(int FilmId, int RoomID, DateTime ShowingStart, DateTime ShowingEnd);

    }
}