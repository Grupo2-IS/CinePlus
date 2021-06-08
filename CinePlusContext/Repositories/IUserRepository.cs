using System.Collections.Generic;
using System.Threading.Tasks;
using CinePlus.Entities;

namespace CinePlus.Context.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<IEnumerable<User>> RetrieveAllAsync();
        Task<User> RetrieveAsync(int id);
        Task<User> UpdateAsync(int id, User user);
        Task<bool?> DeleteAsync(int id);

    }
}