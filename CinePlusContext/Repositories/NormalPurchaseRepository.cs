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
        private static ConcurrentDictionary<int, NormalPurchase> normalPurchaseCache;
        private CinePlusDb db;

        public NormalPurchaseRepository(CinePlusDb db)
        {
            this.db = db;
        }
        public async Task<NormalPurchase> CreateAsync(NormalPurchase normalPurchase)
        {

            EntityEntry<NormalPurchase> added = await db.NormalPurchases.AddAsync(normalPurchase);
            int affected = await db.SaveChangesAsync();

            if (affected == 1)
            {
                return normalPurchaseCache.AddOrUpdate(normalPurchase.NormalPurchaseID, normalPurchase, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        private NormalPurchase UpdateCache(int id, NormalPurchase normalPurchase)
        {
            NormalPurchase old;
            if (normalPurchaseCache.TryGetValue(id, out old))
            {
                if (normalPurchaseCache.TryUpdate(id, normalPurchase, old))
                {
                    return normalPurchase;
                }
            }
            return null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            NormalPurchase normalPurchase = await this.db.NormalPurchases.FindAsync(id);
            this.db.NormalPurchases.Remove(normalPurchase);
            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return normalPurchaseCache.TryRemove(normalPurchase.NormalPurchaseID, out normalPurchase);
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

        public Task<NormalPurchase> RetrieveAsync(int id)
        {
            return Task.Run(() =>
            {
                normalPurchaseCache.TryGetValue(id, out NormalPurchase normalPurchase);
                return normalPurchase;
            });

        }

        public async Task<NormalPurchase> UpdateAsync(int id, NormalPurchase normalPurchase)
        {
            this.db.NormalPurchases.Update(normalPurchase);

            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return UpdateCache(id, normalPurchase);
            }
            return null;
        }
    }
}