using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CinePlus.Entities;


namespace CinePlus.Context.Repositories
{
    public interface INormalPurchaseRepository
    {
        Task<NormalPurchase> CreateAsync(NormalPurchase normalPurchase);
        Task<IEnumerable<NormalPurchase>> RetrieveAllAsync();
        Task<NormalPurchase> RetrieveAsync(int UserId,int SeatID,int FilmID,int RoomID,DateTime ShowingStart);
        Task<NormalPurchase> UpdateAsync(int UserId,int SeatID,int FilmID,int RoomID,DateTime ShowingStart, NormalPurchase normalPurchase);
        Task<bool?> DeleteAsync(int UserId,int SeatID,int FilmID,int RoomID,DateTime ShowingStart);

    }
}