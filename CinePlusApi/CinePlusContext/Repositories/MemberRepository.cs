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
    public class MemberRepository : IRepository<Member>
    {
        private static ConcurrentDictionary<int, Member> memberCache;
        private CinePlusDb db;

        public MemberRepository(CinePlusDb db)
        {
            this.db = db;

            if (memberCache == null)
            {
                memberCache = new ConcurrentDictionary<int, Member>(
                    db.Members
                    .Include(f => f.MemberPurchases)
                    .AsSplitQuery()
                    .ToDictionary(f => f.MemberID)
                );
            }
        }
        public async Task<Member> CreateAsync(Member member)
        {

            EntityEntry<Member> added = await db.Members.AddAsync(member);
            int affected = await db.SaveChangesAsync();

            if (affected == 1)
            {
                return memberCache.AddOrUpdate(member.MemberID, member, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        private Member UpdateCache(int id, Member member)
        {
            Member old;
            if (memberCache.TryGetValue(id, out old))
            {
                if (memberCache.TryUpdate(id, member, old))
                {
                    return member;
                }
            }
            return null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Member member = await this.db.Members.FindAsync(id);
            this.db.Members.Remove(member);
            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return memberCache.TryRemove(member.MemberID, out member);
            }
            else
            {
                return null;
            }
        }

        public Task<IEnumerable<Member>> RetrieveAllAsync()
        {
            return Task.Run<IEnumerable<Member>>(
                () => memberCache.Values
            );
        }

        public Task<Member> RetrieveAsync(int id)
        {
            return Task.Run(() =>
            {
                memberCache.TryGetValue(id, out Member member);
                return member;
            });

        }

        public async Task<Member> UpdateAsync(int id, Member member)
        {
            this.db.Members.Update(member);

            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return UpdateCache(id, member);
            }
            return null;
        }
    }
}