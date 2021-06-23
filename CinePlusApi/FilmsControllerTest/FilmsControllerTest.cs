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
namespace FilmsControllerTest
{
    public class FilmsControllerTest
    {
        [Fact]
        public async Task GetFilmsNotNullTest()
        {
            CinePlusDb cinePlusDb = new CinePlusDb();
            Film film1 = new Film { Name = "Before Sunshine", Genre = "Romance/Independiente", FilmID = 1 };
            Film film2 = new Film { Name = "The Shining", Genre = "Suspenso/Terror", FilmID = 2 };
            Film film3 = new Film { Name = "The Notebook", Genre = "Romance/Drama", FilmID = 3 };


            FilmRepository repo = new FilmRepository(cinePlusDb);
            await repo.CreateAsync(film1);
            await repo.CreateAsync(film2);
            await repo.CreateAsync(film3);

            FilmsController controller = new FilmsController(repo);
            List<Film> ansNotNull = controller.GetFilms("Romance/Drama").Result.ToList();
            List<Film> ansNull = controller.GetFilms(null).Result.ToList();

            Assert.True(ansNotNull.Count == 1);
            Assert.Equal(3, ansNull.Count);

            // Cleaning the film cache
            cinePlusDb.Films.Remove(film1);
            cinePlusDb.Films.Remove(film2);
            cinePlusDb.Films.Remove(film3);
            await cinePlusDb.SaveChangesAsync();
        }


        [Fact]
        public async Task GetAllTest()
        {
            CinePlusDb cinePlusDb = new CinePlusDb();
            Film film1 = new Film { Name = "Before Sunshine", Genre = "Romance/Independiente", FilmID = 1 };
            Film film2 = new Film { Name = "The Shining", Genre = "Suspenso/Terror", FilmID = 2 };
            Film film3 = new Film { Name = "The Notebook", Genre = "Romance/Drama", FilmID = 3 };

            FilmRepository repo = new FilmRepository(cinePlusDb);
            await repo.CreateAsync(film1);
            await repo.CreateAsync(film2);
            await repo.CreateAsync(film3);


            FilmsController controller = new FilmsController(repo);
            List<Film> ans = controller.GetAll().Result.ToList();

            Assert.Equal(3, ans.Count);

            // Cleaning the film cache
            cinePlusDb.Films.Remove(film1);
            cinePlusDb.Films.Remove(film2);
            cinePlusDb.Films.Remove(film3);
            await cinePlusDb.SaveChangesAsync();
        }

        [Fact]
        public async Task GetFilmExistingTest()
        {
            CinePlusDb db = new CinePlusDb();
            Film film1 = new Film { Name = "Before Sunshine", Genre = "Romance/Independiente", FilmID = 1 };

            FilmRepository repo = new FilmRepository(db);
            await repo.CreateAsync(film1);


            FilmsController controller = new FilmsController(repo);
            ObjectResult ans = controller.GetFilm(1).Result as ObjectResult;
            Film film = ans.Value as Film;

            Assert.Equal(1, film.FilmID);
            Assert.Equal(200, ans.StatusCode);

            db.Films.Remove(film1);
            await db.SaveChangesAsync();
        }

        [Fact]
        public async Task GetFilmNotExisting()
        {
            CinePlusDb db = new CinePlusDb();
            FilmRepository repo = new FilmRepository(db);
           
            FilmsController controller = new FilmsController(repo);
            var ans = controller.GetFilm(1).Result as StatusCodeResult;

            Assert.Equal(404, ans.StatusCode);

        }

        [Fact]
        public async Task CreateFilmTest()
        {
            CinePlusDb db = new CinePlusDb();
            FilmRepository repo = new FilmRepository(db);

            Film film = new Film { Name = "After", FilmID = 1, Genre = "Romance" };
            FilmsController controller = new FilmsController(repo);

            var ans1 = controller.Create(null).Result as StatusCodeResult;
            CreatedAtRouteResult ans = controller.Create(film).Result as CreatedAtRouteResult;
            var res = ans.Value;

            Assert.Equal(400, ans1.StatusCode);
            Assert.NotNull(res);

            var count = db.Films.Count();
            Assert.Equal(1, count);

            db.Films.Remove(film);
            await db.SaveChangesAsync();
        }


        [Fact]
        public async Task UpdateTest()
        {
            CinePlusDb db = new CinePlusDb();
            Film film = new Film { Name = "Before Sunshine", Genre = "Romance/Independiente", FilmID = 1 };
           
            FilmRepository repo = new FilmRepository(db);
            await repo.CreateAsync(film);


            Film film1 = new Film { Name = "Before Midnight", Genre = "Romance", FilmID = 1 };
            Film film2 = new Film { Name = "Forever 21", FilmID = 6, Genre = "Romance" };
            FilmsController controller = new FilmsController(repo);

            var ans1 = controller.Update(1, null).Result as StatusCodeResult;  // film is null
            var ans2 = controller.Update(2, film1).Result as StatusCodeResult; // film != id
            var ans3 = controller.Update(6, film2).Result as StatusCodeResult; // Not exists a film with that ID in the db 
            var ans4 = controller.Update(1, film1).Result as StatusCodeResult; // Exits a film with that ID in the db

            Assert.Equal(400, ans1.StatusCode);
            Assert.Equal(400, ans2.StatusCode);
            Assert.Equal(404, ans3.StatusCode);
            Assert.Equal(204, ans4.StatusCode);
            Assert.NotEqual("Before Sunshine", db.Films.Find(1).Name);

            db.Films.Remove(film);
            await db.SaveChangesAsync();
        }

        [Fact]
        public async Task DeleteTest()
        {
            CinePlusDb db = new CinePlusDb();
            Film film = new Film { Name = "Before Sunshine", Genre = "Romance/Independiente", FilmID = 1 };
 
            FilmRepository repo = new FilmRepository(db);
            await repo.CreateAsync(film);

            FilmsController controller = new FilmsController(repo);

            var ans1 = controller.Delete(6).Result as StatusCodeResult; // Not exists a film with that ID in the db
            var ans2 = controller.Delete(1).Result as StatusCodeResult; // Exits a film with that ID in the db

            Assert.Equal(404, ans1.StatusCode);
            Assert.Equal(204, ans2.StatusCode);

           
        }

      
    }
}
