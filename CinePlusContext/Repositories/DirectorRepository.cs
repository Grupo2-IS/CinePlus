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
    public class DirectorRepository : IDirectorRepository
    {
        
        private static ConcurrentDictionary<int, Director> directorCache;
        private CinePlusDb db;

        public DirectorRepository(CinePlusDb db)
        {
            this.db = db;

        }
        public async Task<Director> CreateAsync(Director director)
        {

            EntityEntry<Director> added = await db.Directors.AddAsync(director);
            int affected = await db.SaveChangesAsync();

            if (affected == 1)
            {
                return directorCache.AddOrUpdate(director.IDDirector, director, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        private Director UpdateCache(int id, Director director)
        {
            Director old;
            if (directorCache.TryGetValue(id, out old))
            {
                if (directorCache.TryUpdate(id, director, old))
                {
                    return director;
                }
            }
            return null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Director director = await this.db.Directors.FindAsync(id);
            this.db.Directors.Remove(director);
            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return directorCache.TryRemove(director.IDDirector, out director);
            }
            else
            {
                return null;
            }
        }

        public Task<IEnumerable<Director>> RetrieveAllAsync()
        {
            return Task.Run<IEnumerable<Director>>(
                () => directorCache.Values
            );
        }

        public Task<Director> RetrieveAsync(int id)
        {
            return Task.Run(() =>
            {
                directorCache.TryGetValue(id, out Director director);
                return director;
            });

        }

        public async Task<Director> UpdateAsync(int id, Director director)
        {
            this.db.Directors.Update(director);

            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return UpdateCache(id, director);
            }
            return null;
        }
    }
}