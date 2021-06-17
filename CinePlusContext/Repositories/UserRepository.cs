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
    public class UserRepository : IRepository<User>
    {
        private static ConcurrentDictionary<int, User> userCache;
        private CinePlusDb db;

        public UserRepository(CinePlusDb db)
        {
            this.db = db;

            if (userCache == null)
            {
                userCache = new ConcurrentDictionary<int, User>(
                    db.Users
                    .Include(f => f.NormalPurchases)
                    .ToDictionary(f => f.UserID)
                );
            }
        }
        public async Task<User> CreateAsync(User user)
        {

            EntityEntry<User> added = await db.Users.AddAsync(user);
            int affected = await db.SaveChangesAsync();

            if (affected == 1)
            {
                return userCache.AddOrUpdate(user.UserID, user, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        private User UpdateCache(int id, User user)
        {
            User old;
            if (userCache.TryGetValue(id, out old))
            {
                if (userCache.TryUpdate(id, user, old))
                {
                    return user;
                }
            }
            return null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            User user = await this.db.Users.FindAsync(id);
            this.db.Users.Remove(user);
            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return userCache.TryRemove(user.UserID, out user);
            }
            else
            {
                return null;
            }
        }

        public Task<IEnumerable<User>> RetrieveAllAsync()
        {
            return Task.Run<IEnumerable<User>>(
                () => userCache.Values
            );
        }

        public Task<User> RetrieveAsync(int id)
        {
            return Task.Run(() =>
            {
                userCache.TryGetValue(id, out User user);
                return user;
            });

        }

        public async Task<User> UpdateAsync(int id, User user)
        {
            this.db.Users.Update(user);

            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return UpdateCache(id, user);
            }
            return null;
        }
    }
}