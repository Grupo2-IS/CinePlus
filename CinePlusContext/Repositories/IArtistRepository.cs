using System.Collections.Generic;
using System.Threading.Tasks;
using CinePlus.Entities;

namespace CinePlus.Context.Repositories
{
    public interface IArtistRepository
    {
        Task<Artist> CreateAsync(Artist artist);
        Task<IEnumerable<Artist>> RetrieveAllAsync();
        Task<Artist> RetrieveAsync(int id);
        Task<Artist> UpdateAsync(int id, Artist artist);
        Task<bool?> DeleteAsync(int id);

    }
}