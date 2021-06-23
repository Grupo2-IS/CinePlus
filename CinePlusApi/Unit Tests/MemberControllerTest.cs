using System;
using Xunit;
using CinePlus.Context.Repositories;
using CinePlus.Entities;
using CinePlus.Context;
using System.Collections.Generic;
using CinePlusServices.Controllers;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Unit_Tests
{
    public class MemberControllerTest
    {
        [Fact]
        public async Task GetAllTest()
        {
            CinePlusDb db = new CinePlusDb();
            Member member1 = new Member { MemberID = 1, UserID = 1 };
            Member member2 = new Member { MemberID = 2, UserID = 2 };
            Member member3 = new Member { MemberID = 3, UserID = 3 };

            MemberRepository repo1 = new MemberRepository(db);
            await repo1.CreateAsync(member1);
            await repo1.CreateAsync(member2);
            await repo1.CreateAsync(member3);

            MemberController controller = new MemberController(repo1);

            var ans = controller.GetAll().Result.ToList();

            Assert.Equal(3, ans.Count);

            db.Members.Remove(member1);
            db.Members.Remove(member2);
            db.Members.Remove(member3);
            await db.SaveChangesAsync();

        }

        [Fact]
        public async Task GetMemberTest()
        {
            CinePlusDb db = new CinePlusDb();
            Member member1 = new Member { MemberID = 1, UserID = 1 };
            Member member2 = new Member { MemberID = 2, UserID = 2 };
            Member member3 = new Member { MemberID = 3, UserID = 3 };

            MemberRepository repo2 = new MemberRepository(db);
            await repo2.CreateAsync(member1);
            await repo2.CreateAsync(member2);
            await repo2.CreateAsync(member3);

            MemberController controller = new MemberController(repo2);

            var ans1 = controller.GetMember(9).Result as StatusCodeResult;
            var ans2 = controller.GetMember(3).Result as ObjectResult;

            Assert.Equal(404, ans1.StatusCode);
            Assert.Equal(200, ans2.StatusCode);

            db.Members.Remove(member1);
            db.Members.Remove(member2);
            db.Members.Remove(member3);
            await db.SaveChangesAsync();

        }
        
        [Fact]
        public async Task CreateMemberTest()
        {
            CinePlusDb db = new CinePlusDb();
            Member member = new Member { MemberID = 1, UserID = 1 };

            MemberRepository repo3 = new MemberRepository(db);

            MemberController controller = new MemberController(repo3);

            var ans1 = controller.Create(null).Result as StatusCodeResult;
            var ans2 = controller.Create(member).Result as CreatedAtRouteResult;

            Assert.Equal(400, ans1.StatusCode);
            Assert.NotNull(ans2);
            Assert.Equal(1, db.Members.Count());

            db.Members.Remove(member);
            await db.SaveChangesAsync();

        }

        [Fact]
        public async Task UpdateTest()
        {
            CinePlusDb db = new CinePlusDb();
            Member member1 = new Member { MemberID = 1, UserID = 1 , Points = 20};
            Member member2 = new Member { MemberID = 1, UserID = 1, Points = 40 };
            Member member3 = new Member { MemberID = 3, UserID = 5, Points = 10 };

            MemberRepository repo4 = new MemberRepository(db);
            await repo4.CreateAsync(member1);

            MemberController controller = new MemberController(repo4);

            var ans1 = controller.Update(1, null).Result as StatusCodeResult;
            var ans2 = controller.Update(2, member2).Result as StatusCodeResult;
            var ans3 = controller.Update(3, member3).Result as StatusCodeResult;
            var ans4 = controller.Update(1, member2).Result as StatusCodeResult;

            Assert.Equal(400, ans1.StatusCode);
            Assert.Equal(400, ans2.StatusCode);
            Assert.Equal(404, ans3.StatusCode);
            Assert.Equal(204, ans4.StatusCode);
            Assert.NotEqual(20, member1.Points);

            await controller.Delete(1);

        }

        [Fact]
        public async Task DeleteTest()
        {
            CinePlusDb db = new CinePlusDb();
            Member member1 = new Member { MemberID = 10, UserID = 10, Points = 20 };

            MemberRepository repo = new MemberRepository(db);
            await repo.CreateAsync(member1);

            MemberController controller = new MemberController(repo);

            var ans1 = controller.Delete(2).Result as StatusCodeResult;
            var ans2 = controller.Delete(10).Result as StatusCodeResult;

            Assert.Equal(404, ans1.StatusCode);
            Assert.Equal(204, ans2.StatusCode);

            

        }
    }
}
    

