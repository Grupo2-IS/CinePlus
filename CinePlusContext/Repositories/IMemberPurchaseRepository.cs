using System.Collections.Generic;
using System.Threading.Tasks;
using CinePlus.Entities;

namespace CinePlus.Context.Repositories
{
    public interface IMemberPurchaseRepository
    {
        Task<MemberPurchase> CreateAsync(MemberPurchase memberPurchase);
        Task<IEnumerable<MemberPurchase>> RetrieveAllAsync();
        Task<MemberPurchase> RetrieveAsync(int id);
        Task<MemberPurchase> UpdateAsync(int id, MemberPurchase memberPurchase);
        Task<bool?> DeleteAsync(int id);

    }
}