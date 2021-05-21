using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinePlus.Context.Repositories
{
    public interface IRepository<T>
    {
        Task<T> CreateAsync(T film);
        Task<IEnumerable<T>> RetrieveAllAsync();
        Task<T> RetrieveAsync(int id);
        Task<T> UpdateAsync(int id, T film);
        Task<bool?> DeleteAsync(int id);
    }
}