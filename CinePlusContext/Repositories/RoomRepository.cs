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
    public class RoomRepository : IRoomRepository
    {
        private static ConcurrentDictionary<int, Room> roomCache;
        private CinePlusDb db;

        public RoomRepository(CinePlusDb db)
        {
            this.db = db;
            if (roomCache == null)
            {
                roomCache = new ConcurrentDictionary<int, Room>(
                    db.Rooms
                    .Include(f => f.Seats)
                    .Include(f => f.Showings)
                    .ToDictionary(f => f.RoomID)
                );
            }
        }
        public async Task<Room> CreateAsync(Room room)
        {

            EntityEntry<Room> added = await db.Rooms.AddAsync(room);
            int affected = await db.SaveChangesAsync();

            if (affected == 1)
            {
                return roomCache.AddOrUpdate(room.RoomID,room,UpdateCache);
            }
            else
            {
                return null;
            }
        }

        private Room UpdateCache(int id, Room room)
        {
            Room  old;
            if (roomCache.TryGetValue(id, out old))
            {
                if (roomCache.TryUpdate(id, room, old))
                {
                    return room;
                }
            }
            return null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Room room = await this.db.Rooms.FindAsync(id);
            this.db.Rooms.Remove(room);
            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return roomCache.TryRemove(room.RoomID, out room);
            }
            else
            {
                return null;
            }
        }

        public Task<IEnumerable<Room>> RetrieveAllAsync()
        {
            return Task.Run<IEnumerable<Room>>(
                () => roomCache.Values
            );
        }

        public Task<Room> RetrieveAsync(int id)
        {
            return Task.Run(() =>
            {
                roomCache.TryGetValue(id, out Room room);
                return room;
            });

        }

        public async Task<Room> UpdateAsync(int id, Room room)
        {
            this.db.Rooms.Update(room);

            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return UpdateCache(id, room);
            }
            return null;
        }
    }
}