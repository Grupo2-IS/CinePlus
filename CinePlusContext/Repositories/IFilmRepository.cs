using System.Collections.Generic;
using System.Threading.Tasks;
using CinePlus.Entities;

namespace CinePlus.Context.Repositories
{
    public interface IFilmRepository
    {
        Task<Film> CreateAsync(Film film);
        Task<IEnumerable<Film>> RetrieveAllAsync();
        Task<Film> RetrieveAsync(int id);
        Task<Film> UpdateAsync(int id, Film film);
        Task<bool?> DeleteAsync(int id);

    }
}