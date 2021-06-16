using System.Collections.Generic;
using System.Threading.Tasks;
using CinePlus.Entities;

namespace CinePlus.Context.Repositories
{
    public interface IDirectorRepository
    {
        Task<Director> CreateAsync(Director director);
        Task<IEnumerable<Director>> RetrieveAllAsync();
        Task<Director> RetrieveAsync(int id);
        Task<Director> UpdateAsync(int id, Director director);
        Task<bool?> DeleteAsync(int id);

    }
}