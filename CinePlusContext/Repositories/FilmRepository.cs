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
    public class FilmRepository : IFilmRepository
    {
        private static ConcurrentDictionary<int, Film> filmCache;
        private CinePlusDb db;

        public FilmRepository(CinePlusDb db)
        {
            this.db = db;

            if (filmCache == null)
            {
                filmCache = new ConcurrentDictionary<int, Film>(
                    db.Films
                    .Include(f => f.Directors)
                    .Include(f => f.Performers)
                    .ToDictionary(f => f.FilmID)
                );
            }
        }
        public async Task<Film> CreateAsync(Film film)
        {

            EntityEntry<Film> added = await db.Films.AddAsync(film);
            int affected = await db.SaveChangesAsync();

            if (affected == 1)
            {
                return filmCache.AddOrUpdate(film.FilmID, film, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        private Film UpdateCache(int id, Film film)
        {
            Film old;
            if (filmCache.TryGetValue(id, out old))
            {
                if (filmCache.TryUpdate(id, film, old))
                {
                    return film;
                }
            }
            return null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Film film = await this.db.Films.FindAsync(id);
            this.db.Films.Remove(film);
            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return filmCache.TryRemove(film.FilmID, out film);
            }
            else
            {
                return null;
            }
        }

        public Task<IEnumerable<Film>> RetrieveAllAsync()
        {
            return Task.Run<IEnumerable<Film>>(
                () => filmCache.Values
            );
        }

        public Task<Film> RetrieveAsync(int id)
        {
            return Task.Run(() =>
            {
                filmCache.TryGetValue(id, out Film film);
                return film;
            });

        }

        public async Task<Film> UpdateAsync(int id, Film film)
        {
            this.db.Films.Update(film);

            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return UpdateCache(id, film);
            }
            return null;
        }
    }
}
