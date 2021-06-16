using System.Collections.Generic;
using System.Threading.Tasks;
using CinePlus.Entities;

namespace CinePlus.Context.Repositories
{
    public interface IPerformerRepository
    {
        Task<Performer> CreateAsync(Performer performer);
        Task<IEnumerable<Performer>> RetrieveAllAsync();
        Task<Performer> RetrieveAsync(int id);
        Task<Performer> UpdateAsync(int id, Performer performer);
        Task<bool?> DeleteAsync(int id);

    }
}