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
    public class PurchaseControllerTest
    {
        [Fact]
        public async Task GetAllTest()
        {
            
            CinePlusDb db = new CinePlusDb();
            Purchase purchase1 = new Purchase
            {
                PurchaseCode = "npc1111",
                ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00),
                FilmID = 1,
                RoomID = 1,
                SeatID = 1,
            };

            Purchase purchase2 = new Purchase
            {
                PurchaseCode = "npc1112",
                ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00),
                FilmID = 1,
                RoomID = 1,
                SeatID = 2,
            };

            Purchase purchase3 = new Purchase
            {
                PurchaseCode = "npc1113",
                ShowingStart = new DateTime(2021, 05, 29, 10, 00, 00),
                FilmID = 4,
                RoomID = 1,
                SeatID = 3,
            };

            PurchaseRepository repo = new PurchaseRepository(db);
            await repo.CreateAsync(purchase1);
            await repo.CreateAsync(purchase2);
            await repo.CreateAsync(purchase3);

            PurchasesController controller = new PurchasesController(repo);

            var ans1 = controller.GetAll().Result.ToList();

            Assert.Equal(3, ans1.Count);
            db.Purchases.Remove(purchase1);
            db.Purchases.Remove(purchase2);
            db.Purchases.Remove(purchase3);
            await db.SaveChangesAsync();
        }
        [Fact]
        public async Task GetByShowing()
        {
            CinePlusDb db = new CinePlusDb();
            Purchase purchase1 = new Purchase
            {
                PurchaseCode = "npc1111",
                ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00),
                FilmID = 1,
                RoomID = 1,
                SeatID = 1,
                Price = 10,
                PayWithPoints = false
            };

            Purchase purchase2 = new Purchase
            {
                PurchaseCode = "npc1112",
                ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00),
                FilmID = 1,
                RoomID = 1,
                SeatID = 2,
            };

            Purchase purchase3 = new Purchase
            {
                PurchaseCode = "npc1113",
                ShowingStart = new DateTime(2021, 05, 29, 10, 00, 00),
                FilmID = 4,
                RoomID = 1,
                SeatID = 3,
            };

            PurchaseRepository repo = new PurchaseRepository(db);
            await repo.CreateAsync(purchase1);
            await repo.CreateAsync(purchase2);
            await repo.CreateAsync(purchase3);

            PurchasesController controller = new PurchasesController(repo);

            var ans = controller.GetByShowing(1, 1, purchase1.ShowingStart).Result.ToList();

            Assert.Equal(2, ans.Count);
        }
        [Fact]
        public async Task GetPurchaseTest()
        {
            CinePlusDb db = new CinePlusDb();
            Purchase purchase1 = new Purchase
            {
                PurchaseCode = "npc1111",
                ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00),
                FilmID = 1,
                RoomID = 1,
                SeatID = 1,
            };

            PurchaseRepository repo = new PurchaseRepository(db);
            await repo.CreateAsync(purchase1);

            PurchasesController controller = new PurchasesController(repo);

            var ans1 = controller.GetPurchase(4, 5, 2, purchase1.ShowingStart).Result as StatusCodeResult;
            var ans2 = controller.GetPurchase(1, 1, 1, purchase1.ShowingStart).Result as ObjectResult;

            Assert.Equal(400, ans1.StatusCode);
            Assert.Equal(200, ans2.StatusCode);

        }

        [Fact]
        public async Task CreateTest()
        {
            CinePlusDb db = new CinePlusDb();
            Purchase purchase1 = new Purchase
            {
                PurchaseCode = "npc1111",
                ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00),
                FilmID = 1,
                RoomID = 2,
                SeatID = 9,
            };

            PurchaseRepository repo = new PurchaseRepository(db);
            PurchasesController controller = new PurchasesController(repo);

            var ans1 = controller.Create(null).Result as StatusCodeResult;
            var ans2 = controller.Create(purchase1).Result as StatusCodeResult;

            Assert.Equal(400, ans1.StatusCode);
            Assert.Equal(201, ans2.StatusCode);
        }

        [Fact]
        public async Task UpdateTest()
        {
            CinePlusDb db = new CinePlusDb();
            Purchase purchase1 = new Purchase
            {
                PurchaseCode = "npc1111",
                ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00),
                FilmID = 1,
                RoomID = 2,
                SeatID = 9,
            };

            Purchase purchase2 = new Purchase
            {
                PurchaseCode = "npc0000",
                ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00),
                FilmID = 1,
                RoomID = 2,
                SeatID = 9,
            };

            Purchase purchase3 = new Purchase
            {
                PurchaseCode = "npc0000",
                ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00),
                FilmID = 2,
                RoomID = 2,
                SeatID = 2,
            };

            PurchaseRepository repo = new PurchaseRepository(db);
            await repo.CreateAsync(purchase1);

            PurchasesController controller = new PurchasesController(repo);

            var ans1 = controller.Update(9, 1, 2, purchase1.ShowingStart, null).Result as StatusCodeResult;
            var ans2 = controller.Update(9, 1, 2, purchase1.ShowingStart, purchase1).Result as StatusCodeResult;
            var ans3 = controller.Update(2, 2, 2, purchase3.ShowingStart, purchase3).Result as StatusCodeResult;
            var ans4 = controller.Update(9, 1, 2, purchase2.ShowingStart, purchase2).Result as StatusCodeResult;

            Assert.Equal(400, ans1.StatusCode);
            Assert.Equal(400, ans2.StatusCode);
            Assert.Equal(404, ans3.StatusCode);
            Assert.Equal(204, ans4.StatusCode);
        }

        [Fact]
        public async Task Delete()
        {
            CinePlusDb db = new CinePlusDb();
            Purchase purchase1 = new Purchase
            {
                PurchaseCode = "npc1111",
                ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00),
                FilmID = 1,
                RoomID = 2,
                SeatID = 9,
            };

            PurchaseRepository repo = new PurchaseRepository(db);
            await repo.CreateAsync(purchase1);

            PurchasesController controller = new PurchasesController(repo);

            var ans1 = controller.Delete(8, 10, 2, purchase1.ShowingStart).Result as StatusCodeResult;
            var ans2 = controller.Delete(9, 1, 2, purchase1.ShowingStart).Result as StatusCodeResult;

            Assert.Equal(404, ans1.StatusCode);
            Assert.Equal(204, ans2.StatusCode);
        }
    }
}
