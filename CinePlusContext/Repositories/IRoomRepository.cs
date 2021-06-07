using System.Collections.Generic;
using System.Threading.Tasks;
using CinePlus.Entities;

namespace CinePlus.Context.Repositories
{
    public interface IRoomRepository
    {
        Task<Room> CreateAsync(Room room);
        Task<IEnumerable<Room>> RetrieveAllAsync();
        Task<Room> RetrieveAsync(int id);
        Task<Room> UpdateAsync(int id, Room room);
        Task<bool?> DeleteAsync(int id);

    }
}