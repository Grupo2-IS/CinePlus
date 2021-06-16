using System.Collections.Generic;
using System.Threading.Tasks;
using CinePlus.Entities;

namespace CinePlus.Context.Repositories
{
    public interface IShowingRepository
    {
        Task<Showing> CreateAsync(Showing showing);
        Task<IEnumerable<Showing>> RetrieveAllAsync();
        Task<Showing> RetrieveAsync(int id);
        Task<Showing> UpdateAsync(int id, Showing showing);
        Task<bool?> DeleteAsync(int id);

    }
}