using System.Collections.Generic;
using System.Threading.Tasks;
using CinePlus.Entities;

namespace CinePlus.Context.Repositories
{
    public interface IMemberRepository
    {
        Task<Member> CreateAsync(Member member);
        Task<IEnumerable<Member>> RetrieveAllAsync();
        Task<Member> RetrieveAsync(int id);
        Task<Member> UpdateAsync(int id, Member member);
        Task<bool?> DeleteAsync(int id);

    }
}