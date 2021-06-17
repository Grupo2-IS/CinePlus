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
        private static ConcurrentDictionary<(int,int), Performer> performerCache;
        private CinePlusDb db;

        public PerformerRepository(CinePlusDb db)
        {
            this.db = db;
            if (performerCache == null)
            {
                performerCache = new ConcurrentDictionary<(int,int), Performer>(
                    this.db.Performers
                    .ToDictionary(d => (d.ArtistID, d.FilmID))
                );
            }

            
        }
        public async Task<Performer> CreateAsync(Performer performer)
        {

            EntityEntry<Performer> added = await db.Performers.AddAsync(performer);
            int affected = await db.SaveChangesAsync();

            if (affected == 1)
            {
                return performerCache.AddOrUpdate((performer.FilmID, performer.ArtistID), performer, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        private Performer UpdateCache((int,int) clave , Performer performer)
        {

            Performer old;
            if (performerCache.TryGetValue(clave, out old))
            {
                if (performerCache.TryUpdate(clave, performer, old))
                {
                    return performer;
                }
            }
            return null;
        }

        public async Task<bool?> DeleteAsync(int FilmID,int ArtistID)
        {
            var clave=(FilmID,ArtistID);
            Performer performer = await this.db.Performers.FindAsync(FilmID,ArtistID);
            this.db.Performers.Remove(performer);
            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return performerCache.TryRemove(clave, out performer);
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

        public Task<Performer> RetrieveAsync(int FilmID,int ArtistID)
        {
            var clave=(FilmID,ArtistID);
            return Task.Run(() =>
            {
                performerCache.TryGetValue(clave, out Performer performer);
                return performer;
            });

        }

        public async Task<Performer> UpdateAsync(int FilmID,int ArtistID, Performer performer)
        {
            var clave=(FilmID,ArtistID);
            this.db.Performers.Update(performer);

            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return UpdateCache(clave, performer);
            }
            return null;
        }
    }
}