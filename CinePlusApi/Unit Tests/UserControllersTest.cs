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
    public class UserControllersTest
    {
        [Fact]
        public async Task GetAllTest()
        {
            CinePlusDb db = new CinePlusDb();
            User user1 = new User { Name = "name1", UserID = 1 };
            User user2 = new User { Name = "name2", UserID = 2 };
            User user3 = new User { Name = "name3", UserID = 3 };

            UserRepository repo = new UserRepository(db);
            await repo.CreateAsync(user1);
            await repo.CreateAsync(user2);
            await repo.CreateAsync(user3);

            UsersController controller = new UsersController(repo);

            var ans = controller.GetAll().Result.ToList();

            Assert.Equal(3, ans.Count);

        }

        [Fact]
        public async Task GetUserTest()
        {
            CinePlusDb db = new CinePlusDb();
            User user1 = new User { Name = "name1", UserID = 1 };
            User user2 = new User { Name = "name2", UserID = 2 };
            User user3 = new User { Name = "name3", UserID = 3 };

            UserRepository repo = new UserRepository(db);
            await repo.CreateAsync(user1);
            await repo.CreateAsync(user2);
            await repo.CreateAsync(user3);

            UsersController controller = new UsersController(repo);

            var ans1 = controller.GetUser(10).Result as StatusCodeResult;
            var ans2 = controller.GetUser(2).Result as ObjectResult;

            Assert.Equal(404, ans1.StatusCode);
            Assert.Equal(200, ans2.StatusCode);

        }

        [Fact]
        public async Task CreateTest()
        {
            CinePlusDb db = new CinePlusDb();
            User user1 = new User { Name = "name1", UserID = 1 };

            UserRepository repo = new UserRepository(db);
            UsersController controller = new UsersController(repo);

            var ans1 = controller.Create(null).Result as StatusCodeResult;
            var ans2 = controller.Create(user1).Result as StatusCodeResult;

            Assert.Equal(400, ans1.StatusCode);
            Assert.Equal(201, ans2.StatusCode);


        }

        [Fact]
        public async Task UpdateTest()
        {
            CinePlusDb db = new CinePlusDb();
            User user1 = new User { Name = "name1", UserID = 1, Nick = "nick1", Purchases = new List<Purchase> { new Purchase { PurchaseCode = "aaannnn", FilmID = 1, RoomID = 1 } } };
            User newUser = new User { Name = "newName", UserID = 1, Nick = "n", Purchases = new List<Purchase> { new Purchase { PurchaseCode = "aaannnn", FilmID = 1, RoomID = 1} } };
            User user2 = new User { Name = "name1", UserID = 10, Nick = "nick", Purchases = new List<Purchase> { new Purchase { PurchaseCode = "nn", FilmID = 10, RoomID = 1} } };

            UserRepository repo = new UserRepository(db);
            await repo.CreateAsync(user1);

            UsersController controller = new UsersController(repo);

            var ans1 = controller.Update(1, null).Result as StatusCodeResult;
            var ans2 = controller.Update(3, user1).Result as StatusCodeResult;
            var ans3 = controller.Update(10, user2).Result as StatusCodeResult;
            var ans4 = controller.Update(1, newUser).Result as StatusCodeResult;

            Assert.Equal(400, ans1.StatusCode);
            Assert.Equal(400, ans2.StatusCode);
            Assert.Equal(404, ans3.StatusCode);
            Assert.Equal(204, ans4.StatusCode);


        }
        [Fact]
        public async Task Delete()
        {
            CinePlusDb db = new CinePlusDb();
            User user1 = new User { Name = "name1", UserID = 1 };
            User user2 = new User { Name = "name2", UserID = 2 };
            User user3 = new User { Name = "name3", UserID = 3 };

            UserRepository repo = new UserRepository(db);
            await repo.CreateAsync(user1);
            await repo.CreateAsync(user2);
            await repo.CreateAsync(user3);

            UsersController controller = new UsersController(repo);

            var ans1 = controller.Delete(10).Result as StatusCodeResult;
            var ans2 = controller.Delete(2).Result as StatusCodeResult;

            Assert.Equal(404, ans1.StatusCode);
            Assert.Equal(204, ans2.StatusCode);
        }
    
    }
}
