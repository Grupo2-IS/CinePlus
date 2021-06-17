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
        private static ConcurrentDictionary<(int,int,int,int,DateTime), MemberPurchase> memberPurchaseCache;
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
                return memberPurchaseCache.AddOrUpdate((memberPurchase.MemberId , memberPurchase.SeatID, memberPurchase.FilmID,memberPurchase.RoomID,memberPurchase.ShowingStart), memberPurchase, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        private MemberPurchase UpdateCache((int,int,int,int,DateTime) clave, MemberPurchase memberPurchase)
        {
            MemberPurchase old;
            if (memberPurchaseCache.TryGetValue(clave , out old))
            {
                if (memberPurchaseCache.TryUpdate(clave, memberPurchase, old))
                {
                    return memberPurchase;
                }
            }
            return null;
        }

        public async Task<bool?> DeleteAsync(int MemberID , int SeatID,int FilmID,int RoomID,DateTime ShowingStart)
        {
            var key=(MemberID,SeatID,FilmID,RoomID,ShowingStart);
            MemberPurchase memberPurchase = await this.db.MemberPurchases.FindAsync(MemberID , SeatID, FilmID, RoomID,ShowingStart);
            this.db.MemberPurchases.Remove(memberPurchase);
            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return memberPurchaseCache.TryRemove(key, out memberPurchase);
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

        public Task<MemberPurchase> RetrieveAsync(int MemberID , int SeatID,int FilmID,int RoomID,DateTime ShowingStart)
        {
            var clave=(MemberID,SeatID,FilmID,RoomID,ShowingStart);
            return Task.Run(() =>
            {
                memberPurchaseCache.TryGetValue(clave, out MemberPurchase memberPurchase);
                return memberPurchase;
            });

        }

        public async Task<MemberPurchase> UpdateAsync(int MemberID , int SeatID,int FilmID,int RoomID,DateTime ShowingStart, MemberPurchase memberPurchase)
        {
            var key =(MemberID,SeatID,FilmID,RoomID,ShowingStart);
            this.db.MemberPurchases.Update(memberPurchase);

            int affected = await this.db.SaveChangesAsync();
            if (affected == 1)
            {
                return UpdateCache(key, memberPurchase);
            }
            return null;
        }
    }
}