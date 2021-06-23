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
    public class RequestControllerTest
    {
        [Fact]
        public async Task GetEntradasPorDiaTest()
        {
            CinePlusDb db = new CinePlusDb();
            Purchase purchase1 = new Purchase
            {
                PurchaseCode = "npc1111",
                ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00),
                FilmID = 1,
                RoomID = 1,
                SeatID = 1,
                UserID = 1,
                User = new User { Name="C"},
                Showing = new Showing { Film = new Film { Name = "The Notebook"} },
                Price = 20,
                PayWithPoints = false,
                Seat = new Seat { Row = 1, Column = 1}
            };

            Purchase purchase2 = new Purchase
            {
                
                PurchaseCode = "npc1112",
                ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00),
                FilmID = 1,
                RoomID = 1,
                SeatID = 2,
                UserID = 2,
                User = new User { Name = "B" },
                Showing = new Showing { Film = new Film { Name = "The Shining" } },
                Price = 20,
                PayWithPoints = false,
                Seat = new Seat { Row = 2, Column = 2 }
            };

            Purchase purchase3 = new Purchase
            {
                PurchaseCode = "npc1113",
                ShowingStart = new DateTime(2021, 05, 29, 10, 00, 00),
                FilmID = 4,
                RoomID = 1,
                SeatID = 3,
                UserID = 3,
                User = new User { Name = "A" },
                Showing = new Showing { Film = new Film { Name = "Before" } },
                Price = 20,
                PayWithPoints = false,
                Seat = new Seat { Row = 3, Column = 3 }
            };

            PurchaseRepository repoPurchase = new PurchaseRepository(db);
            await repoPurchase.CreateAsync(purchase1);
            await repoPurchase.CreateAsync(purchase2);
            await repoPurchase.CreateAsync(purchase3);

            RequestRepository repoRequest = new RequestRepository(db);
            RequestController controller = new RequestController(repoRequest);

            var ans = controller.GetEntradasPorDia(28, 05, 2021).Result;

            Assert.Equal(2, ans);

        }

        [Fact]
        public async Task GetEntradasPorMes()
        {
            CinePlusDb db = new CinePlusDb();
            Purchase purchase1 = new Purchase
            {
                PurchaseCode = "npc1111",
                ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00),
                FilmID = 1,
                RoomID = 1,
                SeatID = 1,
                UserID = 1,
                User = new User { Name = "C" },
                Showing = new Showing { Film = new Film { Name = "The Notebook" } },
                Price = 20,
                PayWithPoints = false,
                Seat = new Seat { Row = 1, Column = 1 }
            };

            Purchase purchase2 = new Purchase
            {

                PurchaseCode = "npc1112",
                ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00),
                FilmID = 1,
                RoomID = 1,
                SeatID = 2,
                UserID = 2,
                User = new User { Name = "B" },
                Showing = new Showing { Film = new Film { Name = "The Shining" } },
                Price = 20,
                PayWithPoints = false,
                Seat = new Seat { Row = 2, Column = 2 }
            };

            Purchase purchase3 = new Purchase
            {
                PurchaseCode = "npc1113",
                ShowingStart = new DateTime(2021, 05, 29, 10, 00, 00),
                FilmID = 4,
                RoomID = 1,
                SeatID = 3,
                UserID = 3,
                User = new User { Name = "A" },
                Showing = new Showing { Film = new Film { Name = "Before" } },
                Price = 20,
                PayWithPoints = false,
                Seat = new Seat { Row = 3, Column = 3 }
            };

            PurchaseRepository repoPurchase = new PurchaseRepository(db);
            await repoPurchase.CreateAsync(purchase1);
            await repoPurchase.CreateAsync(purchase2);
            await repoPurchase.CreateAsync(purchase3);

            RequestRepository repoRequest = new RequestRepository(db);
            RequestController controller = new RequestController(repoRequest);

            var ans = controller.GetEntradasPorMes(05, 2021).Result;

            Assert.Equal(3, ans);
        }

        [Fact]
        public async Task GetEntradasPorAñoTest()
        {

            CinePlusDb db = new CinePlusDb();
            Purchase purchase1 = new Purchase
            {
                PurchaseCode = "npc1111",
                ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00),
                FilmID = 1,
                RoomID = 1,
                SeatID = 1,
                UserID = 1,
                User = new User { Name = "C" },
                Showing = new Showing { Film = new Film { Name = "The Notebook" } },
                Price = 20,
                PayWithPoints = false,
                Seat = new Seat { Row = 1, Column = 1 }
            };

            Purchase purchase2 = new Purchase
            {

                PurchaseCode = "npc1112",
                ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00),
                FilmID = 1,
                RoomID = 1,
                SeatID = 2,
                UserID = 2,
                User = new User { Name = "B" },
                Showing = new Showing { Film = new Film { Name = "The Shining" } },
                Price = 20,
                PayWithPoints = false,
                Seat = new Seat { Row = 2, Column = 2 }
            };

            Purchase purchase3 = new Purchase
            {
                PurchaseCode = "npc1113",
                ShowingStart = new DateTime(2021, 05, 29, 10, 00, 00),
                FilmID = 4,
                RoomID = 1,
                SeatID = 3,
                UserID = 3,
                User = new User { Name = "A" },
                Showing = new Showing { Film = new Film { Name = "Before" } },
                Price = 20,
                PayWithPoints = false,
                Seat = new Seat { Row = 3, Column = 3 }
            };

            PurchaseRepository repoPurchase = new PurchaseRepository(db);
            await repoPurchase.CreateAsync(purchase1);
            await repoPurchase.CreateAsync(purchase2);
            await repoPurchase.CreateAsync(purchase3);

            RequestRepository repoRequest = new RequestRepository(db);
            RequestController controller = new RequestController(repoRequest);

            var ans = controller.GetEntradasPoraño(2021).Result;

            Assert.Equal(3, ans);
        }

        [Fact]
        public async Task GetEntradasPorPelicula()
        {

            CinePlusDb db = new CinePlusDb();
            Purchase purchase1 = new Purchase
            {
                PurchaseCode = "npc1111",
                ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00),
                FilmID = 1,
                RoomID = 1,
                SeatID = 1,
                UserID = 1,
                User = new User { Name = "C" },
                Showing = new Showing { Film = new Film { Name = "The Notebook" } },
                Price = 20,
                PayWithPoints = false,
                Seat = new Seat { Row = 1, Column = 1 }
            };

            Purchase purchase2 = new Purchase
            {

                PurchaseCode = "npc1112",
                ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00),
                FilmID = 1,
                RoomID = 1,
                SeatID = 2,
                UserID = 2,
                User = new User { Name = "B" },
                Showing = new Showing { Film = new Film { Name = "The Shining" } },
                Price = 20,
                PayWithPoints = false,
                Seat = new Seat { Row = 2, Column = 2 }
            };

            Purchase purchase3 = new Purchase
            {
                PurchaseCode = "npc1113",
                ShowingStart = new DateTime(2021, 05, 29, 10, 00, 00),
                FilmID = 4,
                RoomID = 1,
                SeatID = 3,
                UserID = 3,
                User = new User { Name = "A" },
                Showing = new Showing { Film = new Film { Name = "Before" } },
                Price = 20,
                PayWithPoints = false,
                Seat = new Seat { Row = 3, Column = 3 }
            };

            PurchaseRepository repoPurchase = new PurchaseRepository(db);
            await repoPurchase.CreateAsync(purchase1);
            await repoPurchase.CreateAsync(purchase2);
            await repoPurchase.CreateAsync(purchase3);

            RequestRepository repoRequest = new RequestRepository(db);
            RequestController controller = new RequestController(repoRequest);

            var ans = controller.GetEntradasPorPelicula(1).Result;

            Assert.Equal(2, ans);
        }

        [Fact]
        public async Task GetPeliculasMasGustadasTest()
        {

            CinePlusDb db = new CinePlusDb();
            Film film1 = new Film { Name = "The Notebook", Rating = 10 };
            Film film2 = new Film { Name = "Saw", Rating = 5 };
            Film film3 = new Film { Name = "Before Sunrise", Rating = 8 };

            FilmRepository repoFilm = new FilmRepository(db);
            await repoFilm.CreateAsync(film1);
            await repoFilm.CreateAsync(film2);
            await repoFilm.CreateAsync(film3);

            RequestRepository repoRequest = new RequestRepository(db);
            RequestController controller = new RequestController(repoRequest);

            var ans = controller.GetPeliculasMasGustadas().Result.ToList();

            Assert.Equal(10, ans[0].Rating);
        }
    }
}
