using System.Collections.Generic;
using System.Threading.Tasks;
using CinePlus.Entities;
using System;

namespace CinePlus.Context.Repositories
{
    public interface IMemberPurchaseRepository
    {
        Task<MemberPurchase> CreateAsync(MemberPurchase memberPurchase);
        Task<IEnumerable<MemberPurchase>> RetrieveAllAsync();
        Task<MemberPurchase> RetrieveAsync(int MemberID , int SeatID,int FilmID,int RoomID,DateTime ShowingStart);
        Task<MemberPurchase> UpdateAsync(int MemberID , int SeatID,int FilmID,int RoomID,DateTime ShowingStart, MemberPurchase memberPurchase);
        Task<bool?> DeleteAsync(int MemberID , int SeatID,int FilmID,int RoomID,DateTime ShowingStart);

    }
}