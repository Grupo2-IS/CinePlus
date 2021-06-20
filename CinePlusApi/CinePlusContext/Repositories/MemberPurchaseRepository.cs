using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using CinePlus.Entities;
using CinePlus.Context;
using System;

namespace CinePlus.Context.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private static ConcurrentDictionary<(int, int, int, DateTime), Purchase> purchaseCache;
        private CinePlusDb db;

        public PurchaseRepository(CinePlusDb db)
        {
            this.db = db;
            if (purchaseCache == null)
            {
                purchaseCache = new ConcurrentDictionary<(int, int, int, DateTime), Purchase>(
                    this.db.Purchases
                    .ToDictionary(d => (d.SeatID, d.FilmID, d.RoomID, d.ShowingStart))
                );
            }
        }
        public async Task<Purchase> CreateAsync(Purchase purchase)
        {

            EntityEntry<Purchase> added = await db.Purchases.AddAsync(purchase);
            int affected = await db.SaveChangesAsync();

            if (affected == 1)
            {
                return purchaseCache.AddOrUpdate((purchase.SeatID, purchase.FilmID, purchase.RoomID, purchase.ShowingStart), purchase, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        private Purchase UpdateCache((int, int, int, DateTime) clave, Purchase purchase)
        {
            Purchase old;
            if (purchaseCache.TryGetValue(clave, out old))
            {
                if (purchaseCache.TryUpdate(clave, purchase, old))
                {
                    return purchase;
                }
            }
            return null;
        }

        public async Task<bool?> DeleteAsync(int SeatID, int FilmID, int RoomID, DateTime ShowingStart)
        {
            var key = (SeatID, FilmID, RoomID, ShowingStart);
            Purchase memberPurchase = await this.db.Purchases.FindAsync(SeatID, FilmID, RoomID, ShowingStart);
            this.db.Purchases.Remove(memberPurchase);
            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return purchaseCache.TryRemove(key, out memberPurchase);
            }
            else
            {
                return null;
            }
        }

        public Task<IEnumerable<Purchase>> RetrieveAllAsync()
        {
            return Task.Run<IEnumerable<Purchase>>(
                () => purchaseCache.Values
            );
        }

        public Task<Purchase> RetrieveAsync(int SeatID, int FilmID, int RoomID, DateTime ShowingStart)
        {
            var clave = (SeatID, FilmID, RoomID, ShowingStart);
            return Task.Run(() =>
            {
                purchaseCache.TryGetValue(clave, out Purchase memberPurchase);
                return memberPurchase;
            });

        }

        public async Task<Purchase> UpdateAsync(int SeatID, int FilmID, int RoomID, DateTime ShowingStart, Purchase memberPurchase)
        {
            var key = (SeatID, FilmID, RoomID, ShowingStart);
            this.db.Purchases.Update(memberPurchase);

            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return UpdateCache(key, memberPurchase);
            }
            return null;
        }
    }
}