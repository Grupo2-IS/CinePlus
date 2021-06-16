using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using CinePlus.Entities;
using System;

namespace CinePlus.Context.Repositories
{
    public class PerformerRepository : IPerformerRepository
    {
        private static ConcurrentDictionary<int, Performer> performerCache;
        private CinePlusDb db;

        public PerformerRepository(CinePlusDb db)
        {
            this.db = db;

            
        }
        public async Task<Performer> CreateAsync(Performer performer)
        {

            EntityEntry<Performer> added = await db.Performers.AddAsync(performer);
            int affected = await db.SaveChangesAsync();

            if (affected == 1)
            {
                return performerCache.AddOrUpdate(performer.PerformerID, performer, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        private Performer UpdateCache(int id, Performer performer)
        {
            Performer old;
            if (performerCache.TryGetValue(id, out old))
            {
                if (performerCache.TryUpdate(id, performer, old))
                {
                    return performer;
                }
            }
            return null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Performer performer = await this.db.Performers.FindAsync(id);
            this.db.Performers.Remove(performer);
            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return performerCache.TryRemove(performer.PerformerID, out performer);
            }
            else
            {
                return null;
            }
        }

        public Task<IEnumerable<Performer>> RetrieveAllAsync()
        {
            return Task.Run<IEnumerable<Performer>>(
                () => performerCache.Values
            );
        }

        public Task<Performer> RetrieveAsync(int id)
        {
            return Task.Run(() =>
            {
                performerCache.TryGetValue(id, out Performer performer);
                return performer;
            });

        }

        public async Task<Performer> UpdateAsync(int id, Performer performer)
        {
            this.db.Performers.Update(performer);

            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return UpdateCache(id, performer);
            }
            return null;
        }
    }
}