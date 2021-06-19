using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinePlus.Context.Repositories
{
    public interface IRepository<T>
    {
        Task<T> CreateAsync(T item);
        Task<IEnumerable<T>> RetrieveAllAsync();
        Task<T> RetrieveAsync(int id);
        Task<T> UpdateAsync(int id, T item);
        Task<bool?> DeleteAsync(int id);
    }
}