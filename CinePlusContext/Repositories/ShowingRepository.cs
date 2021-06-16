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
    public class ShowingRepository : IShowingRepository
    {
        private static ConcurrentDictionary<int, Showing> showingCache;
        private CinePlusDb db;

        public ShowingRepository(CinePlusDb db)
        {
            this.db = db;
        }

        public async Task<Showing> CreateAsync(Showing showing)
        {

            EntityEntry<Showing> added = await db.Showings.AddAsync(showing);
            int affected = await db.SaveChangesAsync();

            if (affected == 1)
            {
                return showingCache.AddOrUpdate(showing.ShowingID, showing, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        private Showing UpdateCache(int id, Showing showing)
        {
            Showing old;
            if (showingCache.TryGetValue(id, out old))
            {
                if (showingCache.TryUpdate(id, showing, old))
                {
                    return showing;
                }
            }
            return null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Showing showing = await this.db.Showings.FindAsync(id);
            this.db.Showings.Remove(showing);
            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return showingCache.TryRemove(showing.ShowingID, out showing);
            }
            else
            {
                return null;
            }
        }

        public Task<IEnumerable<Showing>> RetrieveAllAsync()
        {
            return Task.Run<IEnumerable<Showing>>(
                () => showingCache.Values
            );
        }

        public Task<Showing> RetrieveAsync(int id)
        {
            return Task.Run(() =>
            {
                showingCache.TryGetValue(id, out Showing showing);
                return showing;
            });

        }

        public async Task<Showing> UpdateAsync(int id, Showing showing)
        {
            this.db.Showings.Update(showing);

            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return UpdateCache(id, showing);
            }
            return null;
        }
    }
}