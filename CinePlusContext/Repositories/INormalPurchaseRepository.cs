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
        Task<NormalPurchase> RetrieveAsync(int UserId, DateTime ShowingStart, int FilmID, int RoomID, int SeatID);
        Task<NormalPurchase> UpdateAsync(int UserId, DateTime ShowingStart, int FilmID, int RoomID, int SeatID, NormalPurchase normalPurchase);
        Task<bool?> DeleteAsync(int UserId, DateTime ShowingStart, int FilmID, int RoomID, int SeatID);

    }
}