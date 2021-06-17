using System.Collections.Generic;
using System.Threading.Tasks;
using CinePlus.Entities;

namespace CinePlus.Context.Repositories
{
    public interface IPerformerRepository
    {
        Task<Performer> CreateAsync(Performer performer);
        Task<IEnumerable<Performer>> RetrieveAllAsync();
        Task<Performer> RetrieveAsync(int Film,int Artist);
        Task<Performer> UpdateAsync(int Film,int Artist, Performer performer);
        Task<bool?> DeleteAsync(int Film,int Artist);

    }
}