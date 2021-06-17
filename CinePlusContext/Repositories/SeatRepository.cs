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
    public class SeatRepository : IRepository<Seat>
    {
        private static ConcurrentDictionary<int, Seat> seatCache;
        private CinePlusDb db;

        public SeatRepository(CinePlusDb db)
        {
            this.db = db;

            if (seatCache == null)
            {
                seatCache = new ConcurrentDictionary<int, Seat>(
                    db.Seats
                    .Include(f => f.NormalPurchases)
                    .Include(f => f.MemberPurchases)
                    .ToDictionary(f => f.SeatID)
                );
            }
        }

        public async Task<Seat> CreateAsync(Seat seat)
        {

            EntityEntry<Seat> added = await db.Seats.AddAsync(seat);
            int affected = await db.SaveChangesAsync();

            if (affected == 1)
            {
                return seatCache.AddOrUpdate(seat.SeatID, seat, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        private Seat UpdateCache(int id, Seat seat)
        {
            Seat old;
            if (seatCache.TryGetValue(id, out old))
            {
                if (seatCache.TryUpdate(id, seat, old))
                {
                    return seat;
                }
            }
            return null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Seat seat = await this.db.Seats.FindAsync(id);
            this.db.Seats.Remove(Seat);
            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return seatCache.TryRemove(seat.SeatID, out seat);
            }
            else
            {
                return null;
            }
        }

        public Task<IEnumerable<Seat>> RetrieveAllAsync()
        {
            return Task.Run<IEnumerable<Seat>>(
                () => seatCache.Values
            );
        }

        public Task<Seat> RetrieveAsync(int id)
        {
            return Task.Run(() =>
            {
                seatCache.TryGetValue(id, out Seat seat);
                return seat;
            });

        }

 public async Task<Seat> UpdateAsync(int id, Seat seat)
        {
            this.db.Seats.Update(seat);

            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return UpdateCache(id, seat);
            }
            return null;
        }

       
    }
}