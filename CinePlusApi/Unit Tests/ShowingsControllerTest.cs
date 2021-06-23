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
    public class ShowingsControllerTest
    {
        [Fact]
        public async Task GetAllTest()
        {
            CinePlusDb db = new CinePlusDb();
            Film film1 = new Film
            {
                Name = "film1",
                FilmID = 1,
                FilmLength = 120,
                Synopsis = "aaaa",
                Genre = "Genero1",
                Country = "Country1"
            };
            Film film2 = new Film
            {
                Name = "film2",
                FilmID = 2,
                FilmLength = 120,
                Synopsis = "bbbb",
                Genre = "Genero2",
                Country = "Country2"
            };
            Film film3 = new Film
            {
                Name = "film3",
                FilmID = 1,
                FilmLength = 180,
                Synopsis = "cccc",
                Genre = "Genero1",
                Country = "Country2"
            };
            Room room1 = new Room { RoomID = 1, RoomName = "A" };
            Room room2 = new Room { RoomID = 2, RoomName = "B" };
            Room room3 = new Room { RoomID = 1, RoomName = "C" };

            Showing s1 = new Showing 
            { 
                FilmID = 1, 
                RoomID = 1, 
                ShowingStart = new DateTime(2021, 06, 22, 10, 00, 00),
                ShowingEnd = new DateTime(2021, 06, 22, 12,00,00,00),
                Film = film1,
                Room = room1,
                Price = 20
            };
            Showing s2 = new Showing 
            { 
                FilmID = 2, 
                RoomID = 2,
                ShowingStart = new DateTime(2021, 06, 22, 11, 00, 00),
                ShowingEnd = new DateTime(2021, 06, 22, 13, 00, 00),
                Film = film2,
                Room =  room2,
                Price = 10
            };
            Showing s3 = new Showing 
            { 
                FilmID = 1, 
                RoomID = 1, 
                ShowingStart = new DateTime(2021, 06, 23, 10, 00, 00),
                ShowingEnd = new DateTime(2021, 06, 23, 13, 00, 00),
                Film = film3,
                Room = room3,
                Price = 20
            };

            ShowingRepository repo = new ShowingRepository(db);
            FilmRepository repoFilm = new FilmRepository(db);
            RoomRepository repoRoom = new RoomRepository(db);

            await repoFilm.CreateAsync(film1);
            await repoFilm.CreateAsync(film2);
            await repoFilm.CreateAsync(film3);
            await repoRoom.CreateAsync(room1);
            await repoRoom.CreateAsync(room2);
            await repoRoom.CreateAsync(room3);
            await repo.CreateAsync(s1);
            await repo.CreateAsync(s2);
            await repo.CreateAsync(s3);

            ShowingsController controller = new ShowingsController(repo);

            var ans1 = controller.GetAll().Result.ToList();

            Assert.Equal(3, ans1.Count);
        }

        [Fact]
        public async Task GetActiveTest()
        {
            CinePlusDb db = new CinePlusDb();
            Film film1 = new Film
            {
                Name = "film1",
                FilmID = 1,
                FilmLength = 120,
                Synopsis = "aaaa",
                Genre = "Genero1",
                Country = "Country1"
            };
            Film film2 = new Film
            {
                Name = "film2",
                FilmID = 2,
                FilmLength = 120,
                Synopsis = "bbbb",
                Genre = "Genero2",
                Country = "Country2"
            };
            Film film3 = new Film
            {
                Name = "film3",
                FilmID = 1,
                FilmLength = 180,
                Synopsis = "cccc",
                Genre = "Genero1",
                Country = "Country2"
            };
            Room room1 = new Room { RoomID = 1, RoomName = "A" };
            Room room2 = new Room { RoomID = 2, RoomName = "B" };
            Room room3 = new Room { RoomID = 1, RoomName = "C" };

            Showing s1 = new Showing
            {
                FilmID = 1,
                RoomID = 1,
                ShowingStart = new DateTime(2021, 06, 22, 10, 00, 00),
                ShowingEnd = new DateTime(2021, 06, 22, 12, 00, 00, 00),
                Film = film1,
                Room = room1,
                Price = 20
            };
            Showing s2 = new Showing
            {
                FilmID = 2,
                RoomID = 2,
                ShowingStart = new DateTime(2021, 06, 26, 11, 00, 00),
                ShowingEnd = new DateTime(2021, 06, 26, 13, 00, 00),
                Film = film2,
                Room = room2,
                Price = 10
            };
            Showing s3 = new Showing
            {
                FilmID = 1,
                RoomID = 1,
                ShowingStart = new DateTime(2021, 06, 26, 10, 00, 00),
                ShowingEnd = new DateTime(2021, 06, 26, 13, 00, 00),
                Film = film3,
                Room = room3,
                Price = 20
            };

            ShowingRepository repo = new ShowingRepository(db);
            FilmRepository repoFilm = new FilmRepository(db);
            RoomRepository repoRoom = new RoomRepository(db);

            await repoFilm.CreateAsync(film1);
            await repoFilm.CreateAsync(film2);
            await repoFilm.CreateAsync(film3);
            await repoRoom.CreateAsync(room1);
            await repoRoom.CreateAsync(room2);
            await repoRoom.CreateAsync(room3);
            await repo.CreateAsync(s1);
            await repo.CreateAsync(s2);
            await repo.CreateAsync(s3);

            ShowingsController controller = new ShowingsController(repo);
            var ans = controller.GetActive().Result.ToList();

            Assert.Equal(2, ans.Count);
        }
        [Fact]
        public async Task GetShowingTest()
        {
            CinePlusDb db = new CinePlusDb();
            Film film1 = new Film
            {
                Name = "film1",
                FilmID = 1,
                FilmLength = 120,
                Synopsis = "aaaa",
                Genre = "Genero1",
                Country = "Country1"
            };
            Film film2 = new Film
            {
                Name = "film2",
                FilmID = 2,
                FilmLength = 120,
                Synopsis = "bbbb",
                Genre = "Genero2",
                Country = "Country2"
            };
            Film film3 = new Film
            {
                Name = "film3",
                FilmID = 1,
                FilmLength = 180,
                Synopsis = "cccc",
                Genre = "Genero1",
                Country = "Country2"
            };
            Room room1 = new Room { RoomID = 1, RoomName = "A" };
            Room room2 = new Room { RoomID = 2, RoomName = "B" };
            Room room3 = new Room { RoomID = 1, RoomName = "C" };

            Showing s1 = new Showing
            {
                FilmID = 1,
                RoomID = 1,
                ShowingStart = new DateTime(2021, 06, 22, 10, 00, 00),
                ShowingEnd = new DateTime(2021, 06, 22, 12, 00, 00, 00),
                Film = film1,
                Room = room1,
                Price = 20
            };
            Showing s2 = new Showing
            {
                FilmID = 2,
                RoomID = 2,
                ShowingStart = new DateTime(2021, 06, 22, 11, 00, 00),
                ShowingEnd = new DateTime(2021, 06, 22, 13, 00, 00),
                Film = film2,
                Room = room2,
                Price = 10
            };
            Showing s3 = new Showing
            {
                FilmID = 1,
                RoomID = 1,
                ShowingStart = new DateTime(2021, 06, 23, 10, 00, 00),
                ShowingEnd = new DateTime(2021, 06, 23, 13, 00, 00),
                Film = film3,
                Room = room3,
                Price = 20
            };

            ShowingRepository repo = new ShowingRepository(db);
            FilmRepository repoFilm = new FilmRepository(db);
            RoomRepository repoRoom = new RoomRepository(db);

            await repoFilm.CreateAsync(film1);
            await repoFilm.CreateAsync(film2);
            await repoFilm.CreateAsync(film3);
            await repoRoom.CreateAsync(room1);
            await repoRoom.CreateAsync(room2);
            await repoRoom.CreateAsync(room3);
            await repo.CreateAsync(s1);
            await repo.CreateAsync(s2);
            await repo.CreateAsync(s3);

            ShowingsController controller = new ShowingsController(repo);

            var ans1 = controller.GetShowing(1, 1, s3.ShowingStart, s3.ShowingEnd).Result as ObjectResult;
            var ans2 = controller.GetShowing(2, 3, s2.ShowingStart, s1.ShowingEnd).Result as StatusCodeResult;

            Assert.Equal(200, ans1.StatusCode);
            Assert.Equal(404, ans2.StatusCode);

        }

        [Fact]
        public async Task CreateTest()
        {
            CinePlusDb db = new CinePlusDb();
            Film film3 = new Film
            {
                Name = "film3",
                FilmID = 1,
                FilmLength = 180,
                Synopsis = "cccc",
                Genre = "Genero1",
                Country = "Country2"
            };
            Room room3 = new Room { RoomID = 1, RoomName = "C" };
            Showing s = new Showing
            {
                FilmID = 1,
                RoomID = 1,
                ShowingStart = new DateTime(2021, 06, 23, 10, 00, 00),
                ShowingEnd = new DateTime(2021, 06, 23, 13, 00, 00),
                Film = film3,
                Room = room3,
                Price = 20
            };

            ShowingRepository repo = new ShowingRepository(db);
            ShowingsController controller = new ShowingsController(repo);

            var ans1 = controller.Create(null).Result as StatusCodeResult;
            var ans2 = controller.Create(s).Result as StatusCodeResult;

            Assert.Equal(400, ans1.StatusCode);
            Assert.Equal(201, ans2.StatusCode);
        }

        [Fact]
        public async Task UpdateTest()
        {
            CinePlusDb db = new CinePlusDb();
            Film film1 = new Film
            {
                Name = "film1",
                FilmID = 1,
                FilmLength = 120,
                Synopsis = "aaaa",
                Genre = "Genero1",
                Country = "Country1"
            };
            Film film3 = new Film
            {
                Name = "film3",
                FilmID = 1,
                FilmLength = 180,
                Synopsis = "cccc",
                Genre = "Genero1",
                Country = "Country2"
            };
            Room room1 = new Room { RoomID = 1, RoomName = "A" };
            Room room3 = new Room { RoomID = 1, RoomName = "C" };
            Showing s1 = new Showing
            {
                FilmID = 1,
                RoomID = 1,
                ShowingStart = new DateTime(2021, 06, 23, 10, 00, 00),
                ShowingEnd = new DateTime(2021, 06, 23, 13, 00, 00),
                Film = film3,
                Room = room3,
                Price = 20
            };
            Showing s2 = new Showing
            {
                FilmID = 1,
                RoomID = 1,
                ShowingStart = new DateTime(2021, 06, 22, 10, 00, 00),
                ShowingEnd = new DateTime(2021, 06, 22, 12, 00, 00, 00),
                Film = film1,
                Room = room1,
                Price = 20
            };

            ShowingRepository repo = new ShowingRepository(db);
            await repo.CreateAsync(s1);
                
            ShowingsController controller = new ShowingsController(repo);

            var ans1 = controller.Update(1, 1, s1.ShowingStart, s1.ShowingEnd, null).Result as StatusCodeResult;
            var ans2 = controller.Update(2, 2, s1.ShowingStart, s1.ShowingEnd, s1).Result as StatusCodeResult;
            var ans3 = controller.Update(1, 1, s1.ShowingStart, s2.ShowingEnd, s1).Result as StatusCodeResult;
            var ans4 = controller.Update(1, 1, s2.ShowingStart, s2.ShowingEnd, s2).Result as StatusCodeResult;
            var ans5 = controller.Update(1, 1, s1.ShowingStart, s1.ShowingEnd, s2).Result as StatusCodeResult;
            

            Assert.Equal(400, ans1.StatusCode);
            Assert.Equal(400, ans2.StatusCode);
            Assert.Equal(400, ans3.StatusCode);
            Assert.Equal(404, ans4.StatusCode);
            Assert.Equal(204, ans5.StatusCode);
        }
    }
}
