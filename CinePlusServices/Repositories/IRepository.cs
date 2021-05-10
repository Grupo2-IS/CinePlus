using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinePlus.Services.Repositories
{
    public interface IFilmRepository<T>
    {
        Task<T> CreateAsync(T t);
        Task<IEnumerable<T>> RetrieveAllAsync();

    }
}