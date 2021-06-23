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


            FilmRepository repo1 = new FilmRepository(cinePlusDb);
            await repo1.CreateAsync(film1);
            await repo1.CreateAsync(film2);
            await repo1.CreateAsync(film3);

            FilmsController controller = new FilmsController(repo1);
            List<Film> ansNotNull = controller.GetFilms("Romance/Drama").Result.ToList();
            List<Film> ansNull = controller.GetFilms(null).Result.ToList();

            Assert.True(ansNotNull.Count == 1);
            Assert.Equal(3, ansNull.Count);

            // Cleaning the film cache
            /* cinePlusDb.Films.Remove(film1);
             cinePlusDb.Films.Remove(film2);
             cinePlusDb.Films.Remove(film3);
             await cinePlusDb.SaveChangesAsync();
           */
            await repo1.DeleteAsync(1);
            await repo1.DeleteAsync(2);
            await repo1.DeleteAsync(3);
            
        }


        [Fact]
        public async Task GetAllTest()
        {
            CinePlusDb cinePlusDb = new CinePlusDb();
            Film film1 = new Film { Name = "Before Sunshine", Genre = "Romance/Independiente", FilmID = 1 };
            Film film2 = new Film { Name = "The Shining", Genre = "Suspenso/Terror", FilmID = 2 };
            Film film3 = new Film { Name = "The Notebook", Genre = "Romance/Drama", FilmID = 3 };

            FilmRepository repo2 = new FilmRepository(cinePlusDb);
            await repo2.CreateAsync(film1);
            await repo2.CreateAsync(film2);
            await repo2.CreateAsync(film3);


            FilmsController controller = new FilmsController(repo2);
            List<Film> ans = controller.GetAll().Result.ToList();

            Assert.Equal(3, ans.Count);

            // Cleaning the film cache
            /*  cinePlusDb.Films.Remove(film1);
              cinePlusDb.Films.Remove(film2);
              cinePlusDb.Films.Remove(film3);
              await cinePlusDb.SaveChangesAsync();
            */
            await repo2.DeleteAsync(1);
            await repo2.DeleteAsync(2);
            await repo2.DeleteAsync(3);
            
        }

        [Fact]
        public async Task GetFilmExistingTest()
        {
            CinePlusDb db = new CinePlusDb();
            Film film1 = new Film { Name = "Before Sunshine", Genre = "Romance/Independiente", FilmID = 1 };

            FilmRepository repo3 = new FilmRepository(db);
            await repo3.CreateAsync(film1);


            FilmsController controller = new FilmsController(repo3);
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
            FilmRepository repo4 = new FilmRepository(db);

            FilmsController controller = new FilmsController(repo4);
            var ans = controller.GetFilm(1).Result as StatusCodeResult;

            Assert.Equal(404, ans.StatusCode);

        }

        [Fact]
        public async Task CreateFilmTest()
        {
            CinePlusDb db = new CinePlusDb();
            FilmRepository repo5 = new FilmRepository(db);

            Film film = new Film { Name = "After", FilmID = 1, Genre = "Romance" };
            FilmsController controller = new FilmsController(repo5);

            var ans1 = controller.Create(null).Result as StatusCodeResult;
            var ans = controller.Create(film).Result as StatusCodeResult;
           
            Assert.Equal(400, ans1.StatusCode);
            Assert.Equal(201, ans.StatusCode);
            Assert.NotNull(ans);

            var count = db.Films.Count();
            Assert.Equal(1, count);

            /* db.Films.Remove(film);
             await db.SaveChangesAsync();
            */
            await repo5.DeleteAsync(1);
            
        }


        [Fact]
        public async Task UpdateTest()
        {
            CinePlusDb db = new CinePlusDb();
            Film film = new Film { Name = "Before Sunshine", Genre = "Romance/Independiente", FilmID = 1, Synopsis = " aaaa", Country = "ddd" };
            //await db.AddAsync(film);
            //await db.SaveChangesAsync();

            FilmRepository repo6 = new FilmRepository(db);
            await repo6.CreateAsync(film);

            Film film1 = new Film { Name = "Before Midnight", Genre = "Romance", FilmID = 1, Synopsis = " cccc", Country = "jjjj"};
            Film film2 = new Film { Name = "Forever 21", FilmID = 6, Genre = "Romance", Synopsis = "ddddd"};
            FilmsController controller = new FilmsController(repo6);

            var ans1 = controller.Update(1, null).Result as StatusCodeResult;  // film is null
            var ans2 = controller.Update(2, film1).Result as StatusCodeResult; // film != id
            var ans3 = controller.Update(6, film2).Result as StatusCodeResult; // Not exists a film with that ID in the db 
            var ans4 = controller.Update(1, film1).Result as StatusCodeResult; // Exits a film with that ID in the db

            Assert.Equal(400, ans1.StatusCode);
            Assert.Equal(400, ans2.StatusCode);
            Assert.Equal(404, ans3.StatusCode);
            Assert.Equal(204, ans4.StatusCode);
            //Assert.NotEqual("Before Sunshine", db.Films.Find(1).Name);

            /*  db.Films.Remove(film);
              await db.SaveChangesAsync();
            */
            await repo6.DeleteAsync(1);
           
        }

        [Fact]
        public async Task DeleteTest()
        {
            CinePlusDb db = new CinePlusDb();
            Film film = new Film { Name = "Before Sunshine", Genre = "Romance/Independiente", FilmID = 1 };

            FilmRepository repo7 = new FilmRepository(db);
            await repo7.CreateAsync(film);

            FilmsController controller = new FilmsController(repo7);

            var ans1 = controller.Delete(6).Result as StatusCodeResult; // Not exists a film with that ID in the db
            var ans2 = controller.Delete(1).Result as StatusCodeResult; // Exits a film with that ID in the db

            Assert.Equal(404, ans1.StatusCode);
            Assert.Equal(204, ans2.StatusCode);


        }


    }
}
