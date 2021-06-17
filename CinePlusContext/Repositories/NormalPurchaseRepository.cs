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
    public class NormalPurchaseRepository : INormalPurchaseRepository
    {
        private static ConcurrentDictionary<(int,int,int,int,DateTime), NormalPurchase> normalPurchaseCache;
        private CinePlusDb db;

        public NormalPurchaseRepository(CinePlusDb db)
        {
            this.db = db;
            if (normalPurchaseCache == null)
            {
                normalPurchaseCache = new ConcurrentDictionary<(int,int,int,int,DateTime), NormalPurchase>(
                    db.NormalPurchases
                    .Include(n => n.Showing)
                    .ToDictionary(n => (n.UserId, n.SeatID, n.FilmID, n.RoomID, n.ShowingStart ))
                );
            }

        }
        public async Task<NormalPurchase> CreateAsync(NormalPurchase normalPurchase)
        {

            EntityEntry<NormalPurchase> added = await db.NormalPurchases.AddAsync(normalPurchase);
            int affected = await db.SaveChangesAsync();

            if (affected == 1)
            {
                return normalPurchaseCache.AddOrUpdate((normalPurchase.UserId,normalPurchase.SeatID , normalPurchase.FilmID, normalPurchase.RoomID,normalPurchase.ShowingStart ), normalPurchase, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        private NormalPurchase UpdateCache((int,int,int,int,DateTime) clave, NormalPurchase normalPurchase)
        {
          
            NormalPurchase old;
            if (normalPurchaseCache.TryGetValue(clave, out old))
            {
                if (normalPurchaseCache.TryUpdate(clave, normalPurchase, old))
                {
                    return normalPurchase;
                }
            }
            return null;
        }

        public async Task<bool?> DeleteAsync(int UserId,int SeatID,int FilmID,int RoomID,DateTime ShowingStart)
        {
            var clave=( UserId,SeatID,FilmID,RoomID, ShowingStart);
            NormalPurchase normalPurchase = await this.db.NormalPurchases.FindAsync( UserId,SeatID , FilmID, RoomID,ShowingStart );
            this.db.NormalPurchases.Remove(normalPurchase);
            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return normalPurchaseCache.TryRemove(clave, out normalPurchase);
            }
            else
            {
                return null;
            }
        }

        public Task<IEnumerable<NormalPurchase>> RetrieveAllAsync()
        {
            return Task.Run<IEnumerable<NormalPurchase>>(
                () => normalPurchaseCache.Values
            );
        }

        public Task<NormalPurchase> RetrieveAsync(int UserId,int SeatID,int FilmID,int RoomID,DateTime ShowingStart)
        {
            var clave=( UserId,SeatID,FilmID, RoomID, ShowingStart);
            return Task.Run(() =>
            {
                normalPurchaseCache.TryGetValue(clave, out NormalPurchase normalPurchase);
                return normalPurchase;
            });

        }

        public async Task<NormalPurchase> UpdateAsync( int UserId, int SeatID, int FilmID, int RoomID, DateTime ShowingStart, NormalPurchase normalPurchase)
        {
            var clave=( UserId,SeatID,FilmID, RoomID, ShowingStart);
            this.db.NormalPurchases.Update(normalPurchase);

            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return UpdateCache(clave, normalPurchase);
            }
            return null;
        }
    }
}