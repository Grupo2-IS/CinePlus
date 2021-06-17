using System.Collections.Generic;
using System.Threading.Tasks;
using CinePlus.Entities;

namespace CinePlus.Context.Repositories
{
    public interface IDirectorRepository
    {
        Task<Director> CreateAsync(Director director);
        Task<IEnumerable<Director>> RetrieveAllAsync();
        Task<Director> RetrieveAsync(int FilmID,int ArtistID);
        Task<Director> UpdateAsync(int FilmID,int ArtistID, Director director);
        Task<bool?> DeleteAsync(int FilmID,int ArtistID);

    }
}