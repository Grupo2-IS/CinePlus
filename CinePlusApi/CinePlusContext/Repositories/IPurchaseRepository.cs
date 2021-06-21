using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CinePlus.Entities;


namespace CinePlus.Context.Repositories
{
    public interface IPurchaseRepository
    {
        Task<Purchase> CreateAsync(Purchase Purchase);
        Task<IEnumerable<PurchaseWrapper>> RetrieveAllAsync();
        Task<IEnumerable<PurchaseWrapper>> RetrieveByShowingAsync(int FilmID, int RoomID, DateTime StartDate);
        Task<PurchaseWrapper> RetrieveAsync(int SeatID, int FilmID, int RoomID, DateTime ShowingStart);
        Task<Purchase> UpdateAsync(int SeatID, int FilmID, int RoomID, DateTime ShowingStart, Purchase Purchase);
        Task<bool?> DeleteAsync(int SeatID, int FilmID, int RoomID, DateTime ShowingStart);

    }
}