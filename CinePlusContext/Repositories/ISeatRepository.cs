using System.Collections.Generic;
using System.Threading.Tasks;
using CinePlus.Entities;

namespace CinePlus.Context.Repositories
{
    public interface ISeatRepository
    {
        Task<Seat> CreateAsync(Seat seat);
        Task<IEnumerable<Seat>> RetrieveAllAsync();
        Task<Seat> RetrieveAsync(int id);
        Task<bool?> DeleteAsync(int id);

    }
}