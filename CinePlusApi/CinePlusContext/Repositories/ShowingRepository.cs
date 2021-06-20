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
        private static ConcurrentDictionary<(int, int, DateTime, DateTime), Showing> showingCache;
        private CinePlusDb db;

        public ShowingRepository(CinePlusDb db)
        {
            this.db = db;
            if (showingCache == null)
            {
                showingCache = new ConcurrentDictionary<(int, int, DateTime, DateTime), Showing>(
                    this.db.Showings
                    .Include(sh => sh.Film)
                    .Include(sh => sh.Room)
                    .ToDictionary(d => (d.FilmID, d.RoomID, d.ShowingStart, d.ShowingEnd))
                );
            }
        }

        public async Task<ShowingWrapper> CreateAsync(Showing showing)
        {

            EntityEntry<Showing> added = await db.Showings.AddAsync(showing);
            int affected = await db.SaveChangesAsync();

            if (affected == 1)
            {
                var sh = showingCache.AddOrUpdate((showing.FilmID, showing.RoomID, showing.ShowingStart, showing.ShowingEnd), showing, UpdateCache);
                return new ShowingWrapper(sh.Film.Name, sh.Room.RoomName, sh.ShowingStart, sh.Film.FilmLength,
                        sh.RoomID, sh.FilmID);
            }
            else
            {
                return null;
            }
        }

        private Showing UpdateCache((int, int, DateTime, DateTime) clave, Showing showing)
        {
            Showing old;
            if (showingCache.TryGetValue(clave, out old))
            {
                if (showingCache.TryUpdate(clave, showing, old))
                {
                    return showing;
                }
            }
            return null;
        }

        public async Task<IEnumerable<ShowingWrapper>> GetActiveShowings()
        {
            return await Task.Run<IEnumerable<ShowingWrapper>>(
                () => showingCache.Values.Select(
                    sh => new ShowingWrapper(sh.Film.Name, sh.Room.RoomName, sh.ShowingStart, sh.Film.FilmLength,
                        sh.RoomID, sh.FilmID))
                    .Where(shw => shw.StartDate >= DateTime.Now)
            );
        }
        public async Task<bool?> DeleteAsync(int FilmId, int RoomID, DateTime ShowingStart, DateTime ShowingEnd)
        {
            var clave = (FilmId, RoomID, ShowingStart, ShowingEnd);
            Showing showing = await this.db.Showings.FindAsync(FilmId, RoomID, ShowingStart, ShowingEnd);
            this.db.Showings.Remove(showing);
            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return showingCache.TryRemove(clave, out showing);
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<ShowingWrapper>> RetrieveAllAsync()
        {
            return await Task.Run<IEnumerable<ShowingWrapper>>(
                () => showingCache.Values.Select(
                    sh => new ShowingWrapper(sh.Film.Name, sh.Room.RoomName, sh.ShowingStart, sh.Film.FilmLength,
                        sh.RoomID, sh.FilmID)
                )
            );
        }

        public Task<ShowingWrapper> RetrieveAsync(int FilmId, int RoomID, DateTime ShowingStart, DateTime ShowingEnd)
        {
            var clave = (FilmId, RoomID, ShowingStart, ShowingEnd);
            return Task.Run(() =>
            {
                showingCache.TryGetValue(clave, out Showing sh);
                return new ShowingWrapper(sh.Film.Name, sh.Room.RoomName, sh.ShowingStart, sh.Film.FilmLength,
                        sh.RoomID, sh.FilmID);
            });

        }

        public async Task<Showing> UpdateAsync(int FilmID, int RoomID, DateTime ShowingStart, DateTime ShowingEnd, Showing showing)
        {
            var clave = (FilmID, RoomID, ShowingStart, ShowingEnd);
            this.db.Showings.Update(showing);

            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return UpdateCache(clave, showing);
            }
            return null;
        }
    }
}