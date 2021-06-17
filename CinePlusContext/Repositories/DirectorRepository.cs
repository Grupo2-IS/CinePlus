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
        
        private static ConcurrentDictionary<(int,int), Director> directorCache;
        private CinePlusDb db;

        public DirectorRepository(CinePlusDb db)
        {
            this.db = db;
            if (directorCache == null)
            {
                directorCache = new ConcurrentDictionary<(int,int), Director>(
                    this.db.Directors
                    .Include(d => d.Film)
                    .ToDictionary(d => d.ArtistaID)
                );
            }

        }
        public async Task<Director> CreateAsync(Director director)
        {

            EntityEntry<Director> added = await db.Directors.AddAsync(director);
            int affected = await db.SaveChangesAsync();

            if (affected == 1)
            {
                return directorCache.AddOrUpdate((director.FilmID,director.ArtistID), director, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        private Director UpdateCache((int,int) clave, Director director)
        {
            Director old;
            if (directorCache.TryGetValue(clave, out old))
            {
                if (directorCache.TryUpdate(clave, director, old))
                {
                    return director;
                }
            }
            return null;
        }

        public async Task<bool?> DeleteAsync(int FilmID,int ArtistID)
        {
            var clave =(FilmID,ArtistID);
            Director director = await this.db.Directors.FindAsync(FilmID,ArtistID);
            this.db.Directors.Remove(director);
            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return directorCache.TryRemove(clave, out director);
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

        public Task<Director> RetrieveAsync(int FilmID,int ArtistID)
        {
            var clave=(FilmID,ArtistID);
            return Task.Run(() =>
            {
                directorCache.TryGetValue(clave, out Director director);
                return director;
            });

        }

        public async Task<Director> UpdateAsync(int FilmID,int ArtistID, Director director)
        {
            var key=(FilmID,ArtistID);
            this.db.Directors.Update(director);

            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return UpdateCache(key, director);
            }
            return null;
        }
    }
}