using System.Collections.Generic;
using System.Threading.Tasks;
using CinePlus.Entities;

namespace CinePlus.Context.Repositories
{
    public interface INormalPurchaseRepository
    {
        Task<Film> CreateAsync(NormalPurchase normalPurchase);
        Task<IEnumerable<NormalPurchase>> RetrieveAllAsync();
        Task<NormalPurchase> RetrieveAsync(int id);
        Task<NormalPurchase> UpdateAsync(int id, NormalPurchase normalPurchase);
        Task<bool?> DeleteAsync(int id);

    }
}