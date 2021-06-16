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
    public class MemberPurchaseRepository : IMemberPurchaseRepository
    {
        private static ConcurrentDictionary<int, MemberPurchase> memberPurchaseCache;
        private CinePlusDb db;

        public MemberPurchaseRepository(CinePlusDb db)
        {
            this.db = db;
        }
        public async Task<MemberPurchase> CreateAsync(MemberPurchase memberPurchase)
        {

            EntityEntry<MemberPurchase> added = await db.MemberPurchases.AddAsync(memberPurchase);
            int affected = await db.SaveChangesAsync();

            if (affected == 1)
            {
                return memberPurchaseCache.AddOrUpdate(memberPurchase.MemberPurchaseID, memberPurchase, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        private MemberPurchase UpdateCache(int id, MemberPurchase memberPurchase)
        {
            MemberPurchase old;
            if (memberPurchaseCache.TryGetValue(id, out old))
            {
                if (memberPurchaseCache.TryUpdate(id, memberPurchase, old))
                {
                    return memberPurchase;
                }
            }
            return null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            MemberPurchase memberPurchase = await this.db.MemberPurchases.FindAsync(id);
            this.db.MemberPurchases.Remove(memberPurchase);
            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return memberPurchaseCache.TryRemove(memberPurchase.MemberPurchaseID, out memberPurchase);
            }
            else
            {
                return null;
            }
        }

        public Task<IEnumerable<MemberPurchase>> RetrieveAllAsync()
        {
            return Task.Run<IEnumerable<MemberPurchase>>(
                () => memberPurchaseCache.Values
            );
        }

        public Task<MemberPurchase> RetrieveAsync(int id)
        {
            return Task.Run(() =>
            {
                memberPurchaseCache.TryGetValue(id, out MemberPurchase memberPurchase);
                return memberPurchase;
            });

        }

        public async Task<MemberPurchase> UpdateAsync(int id, MemberPurchase memberPurchase)
        {
            this.db.MemberPurchases.Update(memberPurchase);

            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return UpdateCache(id, memberPurchase);
            }
            return null;
        }
    }
}