using System.Threading.Tasks;
using System.Collections.Generic;
using CinePlus.Entities;

namespace CinePlus.Context.Repositories
{
    public interface ISeatRepository
    {
        Task<Seat> CreateAsync(Seat item);
        Task<IEnumerable<Seat>> RetrieveAllAsync();
        Task<Seat> RetrieveAsync(int SeatID, int roomID);
        Task<Seat> UpdateAsync(int SeatID, int roomID, Seat item);
        Task<bool?> DeleteAsync(int SeatID, int roomID);
    }
}