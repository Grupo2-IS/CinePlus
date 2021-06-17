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
    public class ArtistRepository : IRepository<Artist>
    {
        private static ConcurrentDictionary<int, Artist> artistCache;
        private CinePlusDb db;

        public ArtistRepository(CinePlusDb db)
        {
            this.db = db;

            if (artistCache == null)
            {
                artistCache = new ConcurrentDictionary<int, Artist>(
                    db.Artists
                    .Include(f => f.Directors)
                    .Include(f => f.Performers)
                    .ToDictionary(f => f.ArtistID)
                );
            }
        }
        public async Task<Artist> CreateAsync(Artist artist)
        {

            EntityEntry<Artist> added = await db.Artists.AddAsync(artist);
            int affected = await db.SaveChangesAsync();

            if (affected == 1)
            {
                return artistCache.AddOrUpdate(artist.ArtistID, artist, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        private Artist UpdateCache(int id, Artist artist)
        {
            Artist old;
            if (artistCache.TryGetValue(id, out old))
            {
                if (artistCache.TryUpdate(id, artist, old))
                {
                    return artist;
                }
            }
            return null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Artist artist = await this.db.Artists.FindAsync(id);
            this.db.Artists.Remove(artist);
            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return artistCache.TryRemove(artist.ArtistID, out artist);
            }
            else
            {
                return null;
            }
        }

        public Task<IEnumerable<Artist>> RetrieveAllAsync()
        {
            return Task.Run<IEnumerable<Artist>>(
                () => artistCache.Values
            );
        }

        public Task<Artist> RetrieveAsync(int id)
        {
            return Task.Run(() =>
            {
                artistCache.TryGetValue(id, out Artist artist);
                return artist;
            });

        }

        public async Task<Artist> UpdateAsync(int id, Artist artist)
        {
            this.db.Artists.Update(artist);

            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return UpdateCache(id, artist);
            }
            return null;
        }
    }
}