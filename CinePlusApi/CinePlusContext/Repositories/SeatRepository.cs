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
    public class SeatRepository : ISeatRepository
    {
        private static ConcurrentDictionary<(int, int), Seat> seatCache;
        private CinePlusDb db;

        public SeatRepository(CinePlusDb db)
        {
            this.db = db;

            if (seatCache == null)
            {
                seatCache = new ConcurrentDictionary<(int, int), Seat>(
                    db.Seats
                    .Include(s => s.Purchases)
                    .ToDictionary(s => (s.SeatID, s.RoomID))
                );
            }
        }

        public async Task<Seat> CreateAsync(Seat seat)
        {

            EntityEntry<Seat> added = await db.Seats.AddAsync(seat);
            int affected = await db.SaveChangesAsync();

            if (affected == 1)
            {
                return seatCache.AddOrUpdate((seat.SeatID, seat.RoomID), seat, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        private Seat UpdateCache((int, int) key, Seat seat)
        {
            Seat old;
            if (seatCache.TryGetValue(key, out old))
            {
                if (seatCache.TryUpdate(key, seat, old))
                {
                    return seat;
                }
            }
            return null;
        }

        public async Task<bool?> DeleteAsync(int SeatID, int RoomID)
        {
            Seat seat = await this.db.Seats.FindAsync(SeatID, RoomID);
            this.db.Seats.Remove(seat);
            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return seatCache.TryRemove((seat.SeatID, seat.RoomID), out seat);
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

        public Task<Seat> RetrieveAsync(int SeatID, int RoomID)
        {
            return Task.Run(() =>
            {
                seatCache.TryGetValue((SeatID, RoomID), out Seat seat);
                return seat;
            });

        }

        public async Task<Seat> UpdateAsync(int SeatID, int RoomID, Seat seat)
        {
            this.db.Seats.Update(seat);

            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return UpdateCache((SeatID, RoomID), seat);
            }
            return null;
        }


    }
}