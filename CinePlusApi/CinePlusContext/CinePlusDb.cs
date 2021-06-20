using System;
using CinePlus.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft;

namespace CinePlus.Context
{
    public class CinePlusDb : DbContext
    {
        public CinePlusDb() { }
        public CinePlusDb(DbContextOptions<CinePlusDb> options) : base(options)
        {
        }

        public DbSet<Film> Films { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Showing> Showings { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(@"Server=(localDB)\MSSQLLocalDB;Database=CinePlusDB;Integrated Security=true;");

            // in memory database used for simplicity, change to a real db for production applications
            optionsBuilder.UseInMemoryDatabase("TestDb");

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Configuring Seat Entity
            builder.Entity<Seat>().HasKey(
                s => new { s.SeatID, s.RoomID }
            );

            builder.Entity<Seat>()
                .HasOne(s => s.Room)
                .WithMany(r => r.Seats)
                .HasForeignKey(s => s.RoomID)
                .OnDelete(DeleteBehavior.NoAction);

            this.SeedSeats(builder);

            // Configuring ShowingSeat Entity.
            builder.Entity<Showing>().HasKey(
                sh => new { sh.ShowingStart, sh.FilmID, sh.RoomID }
            );

            builder.Entity<Showing>()
                .HasOne(sh => sh.Room)
                .WithMany(r => r.Showings)
                .HasForeignKey(sh => sh.RoomID);

            builder.Entity<Showing>()
                .HasOne(sh => sh.Film)
                .WithMany(f => f.Showings)
                .HasForeignKey(sh => sh.FilmID);

            builder.Entity<Showing>().HasData(
                new { ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00), FilmID = 1, RoomID = 1, ShowingEnd = new DateTime(2021, 05, 28, 12, 00, 00) },
                new { ShowingStart = new DateTime(2021, 05, 28, 11, 00, 00), FilmID = 2, RoomID = 2, ShowingEnd = new DateTime(2021, 05, 28, 13, 00, 00) },
                new { ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00), FilmID = 3, RoomID = 3, ShowingEnd = new DateTime(2021, 05, 28, 12, 00, 00) },
                new { ShowingStart = new DateTime(2021, 05, 29, 10, 00, 00), FilmID = 4, RoomID = 1, ShowingEnd = new DateTime(2021, 05, 29, 12, 00, 00) },
                new { ShowingStart = new DateTime(2021, 05, 29, 11, 00, 00), FilmID = 5, RoomID = 4, ShowingEnd = new DateTime(2021, 05, 29, 13, 00, 00) }

                );

            // Configuring NormalPurchase Entity
            builder.Entity<Purchase>().HasKey(
                np => new { np.ShowingStart, np.FilmID, np.RoomID, np.SeatID }
            );

            builder.Entity<Purchase>()
                .HasOne(np => np.Seat)
                .WithMany(s => s.Purchases)
                .HasForeignKey(np => new { np.SeatID, np.RoomID });

            builder.Entity<Purchase>()
                .HasOne(np => np.Showing)
                .WithMany(sh => sh.Purchases)
                .HasForeignKey(np => new { np.ShowingStart, np.FilmID, np.RoomID });

            builder.Entity<Purchase>()
                .HasOne(np => np.User)
                .WithMany(u => u.Purchases)
                .HasForeignKey(np => np.UserID);

            this.SeedPurchases(builder);

            // Configuring MemberPurchase Entity
            #region 
            // builder.Entity<MemberPurchase>().HasKey(
            //     mp => new { mp.MemberId, mp.ShowingStart, mp.FilmID, mp.RoomID, mp.SeatID }
            // );

            // builder.Entity<MemberPurchase>()
            //     .HasOne(mp => mp.Seat)
            //     .WithMany(s => s.MemberPurchases)
            //     .HasForeignKey(mp => new { mp.SeatID, mp.RoomID });

            // builder.Entity<MemberPurchase>()
            //     .HasOne(mp => mp.Showing)
            //     .WithMany(sh => sh.MemberPurchases)
            //     .HasForeignKey(mp => new { mp.ShowingStart, mp.FilmID, mp.RoomID });

            // builder.Entity<MemberPurchase>()
            //     .HasOne(mp => mp.Member)
            //     .WithMany(m => m.MemberPurchases)
            //     .HasForeignKey(mp => mp.MemberId);


            // builder.Entity<MemberPurchase>().HasData(
            //     new
            //     {
            //         MemberId = 1,
            //         FilmID = 1,
            //         RoomID = 1,
            //         SeatID = 10,
            //         PayWithPoints = true,
            //         Price = 30,
            //         PurchaseCode = "ABCDEFGA",
            //         UsedPoints = 5,
            //         ShowingStart = new DateTime(2021, 05, 28, 12, 00, 00)
            //     },

            //     new
            //     {
            //         MemberId = 3,
            //         FilmID = 3,
            //         RoomID = 3,
            //         SeatID = 15,
            //         PayWithPoints = false,
            //         Price = 10,
            //         PurchaseCode = "DEDFGRHA",
            //         UsedPoints = 0,
            //         ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00)
            //     }
            //     );
            #endregion

            // Configuring Member Entity.

            builder.Entity<Member>()
                .HasOne(m => m.User)
                .WithOne(u => u.Member);

            builder.Entity<Member>().HasData(
                new { MemberID = 1, UserID = 2, Points = 35, Email = "juan@test.com" },
                new { MemberID = 2, UserID = 3, Points = 25, Email = "peny@test.com" },
                new { MemberID = 3, UserID = 5, Points = 10, Email = "luis@test.com" }
                );


            //Configuring Film Entity
            this.SeedFilms(builder);


            //Configuring Artist Entity


            //Configuring User Entity
            builder.Entity<User>().HasData(
                new { UserID = 1, Nick = "pablito", Name = "Pablo", PasswordHash = BCrypt.Net.BCrypt.HashPassword("test"), Role = Role.User },
                new { UserID = 2, Nick = "juanitin", Name = "Juan", PasswordHash = BCrypt.Net.BCrypt.HashPassword("test"), Role = Role.Member },
                new { UserID = 3, Nick = "penelope", Name = "Peny", PasswordHash = BCrypt.Net.BCrypt.HashPassword("test"), Role = Role.Member },
                new { UserID = 4, Nick = "anacleta", Name = "Ana", PasswordHash = BCrypt.Net.BCrypt.HashPassword("test"), Role = Role.User },
                new { UserID = 5, Nick = "el ruso", Name = "Luis", PasswordHash = BCrypt.Net.BCrypt.HashPassword("test"), Role = Role.Member },
                new { UserID = 6, Nick = "el sueco", Name = "Jose", PasswordHash = BCrypt.Net.BCrypt.HashPassword("test"), Role = Role.User }
                );

            //Configuring Room Entity
            builder.Entity<Room>().HasData(
                new
                {
                    RoomID = 1,
                    RoomName = "A1"
                },
                new
                {
                    RoomID = 2,
                    RoomName = "B1"
                },
                new
                {
                    RoomID = 3,
                    RoomName = "C1"
                }
                );
        }


        private void SeedSeats(ModelBuilder builder)
        {
            builder.Entity<Seat>().HasData(
                // Room A1
                new { SeatID = 1, RoomID = 1, Row = 1, Column = 1 },
                new { SeatID = 2, RoomID = 1, Row = 1, Column = 2 },
                new { SeatID = 3, RoomID = 1, Row = 1, Column = 3 },
                new { SeatID = 4, RoomID = 1, Row = 1, Column = 4 },
                new { SeatID = 5, RoomID = 1, Row = 1, Column = 5 },
                new { SeatID = 6, RoomID = 1, Row = 1, Column = 6 },
                new { SeatID = 7, RoomID = 1, Row = 1, Column = 7 },
                new { SeatID = 8, RoomID = 1, Row = 1, Column = 8 },

                new { SeatID = 9, RoomID = 1, Row = 2, Column = 1 },
                new { SeatID = 10, RoomID = 1, Row = 2, Column = 2 },
                new { SeatID = 11, RoomID = 1, Row = 2, Column = 3 },
                new { SeatID = 12, RoomID = 1, Row = 2, Column = 4 },
                new { SeatID = 13, RoomID = 1, Row = 2, Column = 5 },
                new { SeatID = 14, RoomID = 1, Row = 2, Column = 6 },
                new { SeatID = 15, RoomID = 1, Row = 2, Column = 7 },
                new { SeatID = 16, RoomID = 1, Row = 2, Column = 8 },

                new { SeatID = 17, RoomID = 1, Row = 3, Column = 1 },
                new { SeatID = 18, RoomID = 1, Row = 3, Column = 2 },
                new { SeatID = 19, RoomID = 1, Row = 3, Column = 3 },
                new { SeatID = 20, RoomID = 1, Row = 3, Column = 4 },
                new { SeatID = 21, RoomID = 1, Row = 3, Column = 5 },
                new { SeatID = 22, RoomID = 1, Row = 3, Column = 6 },
                new { SeatID = 23, RoomID = 1, Row = 3, Column = 7 },
                new { SeatID = 24, RoomID = 1, Row = 3, Column = 8 },

                new { SeatID = 25, RoomID = 1, Row = 4, Column = 1 },
                new { SeatID = 26, RoomID = 1, Row = 4, Column = 2 },
                new { SeatID = 27, RoomID = 1, Row = 4, Column = 3 },
                new { SeatID = 28, RoomID = 1, Row = 4, Column = 4 },
                new { SeatID = 29, RoomID = 1, Row = 4, Column = 5 },
                new { SeatID = 30, RoomID = 1, Row = 4, Column = 6 },
                new { SeatID = 31, RoomID = 1, Row = 4, Column = 7 },
                new { SeatID = 32, RoomID = 1, Row = 4, Column = 8 },

                new { SeatID = 33, RoomID = 1, Row = 5, Column = 1 },
                new { SeatID = 34, RoomID = 1, Row = 5, Column = 2 },
                new { SeatID = 35, RoomID = 1, Row = 5, Column = 3 },
                new { SeatID = 36, RoomID = 1, Row = 5, Column = 4 },
                new { SeatID = 37, RoomID = 1, Row = 5, Column = 5 },
                new { SeatID = 38, RoomID = 1, Row = 5, Column = 6 },
                new { SeatID = 39, RoomID = 1, Row = 5, Column = 7 },
                new { SeatID = 40, RoomID = 1, Row = 5, Column = 8 },

                new { SeatID = 41, RoomID = 1, Row = 6, Column = 1 },
                new { SeatID = 42, RoomID = 1, Row = 6, Column = 2 },
                new { SeatID = 43, RoomID = 1, Row = 6, Column = 3 },
                new { SeatID = 44, RoomID = 1, Row = 6, Column = 4 },
                new { SeatID = 45, RoomID = 1, Row = 6, Column = 5 },
                new { SeatID = 46, RoomID = 1, Row = 6, Column = 6 },
                new { SeatID = 47, RoomID = 1, Row = 6, Column = 7 },
                new { SeatID = 48, RoomID = 1, Row = 6, Column = 8 },

                // Room B1
                new { SeatID = 1, RoomID = 2, Row = 1, Column = 1 },
                new { SeatID = 2, RoomID = 2, Row = 1, Column = 2 },
                new { SeatID = 3, RoomID = 2, Row = 1, Column = 3 },
                new { SeatID = 4, RoomID = 2, Row = 1, Column = 4 },
                new { SeatID = 5, RoomID = 2, Row = 1, Column = 5 },
                new { SeatID = 6, RoomID = 2, Row = 1, Column = 6 },
                new { SeatID = 7, RoomID = 2, Row = 1, Column = 7 },
                new { SeatID = 8, RoomID = 2, Row = 1, Column = 8 },

                new { SeatID = 9, RoomID = 2, Row = 2, Column = 1 },
                new { SeatID = 10, RoomID = 2, Row = 2, Column = 2 },
                new { SeatID = 11, RoomID = 2, Row = 2, Column = 3 },
                new { SeatID = 12, RoomID = 2, Row = 2, Column = 4 },
                new { SeatID = 13, RoomID = 2, Row = 2, Column = 5 },
                new { SeatID = 14, RoomID = 2, Row = 2, Column = 6 },
                new { SeatID = 15, RoomID = 2, Row = 2, Column = 7 },
                new { SeatID = 16, RoomID = 2, Row = 2, Column = 8 },

                new { SeatID = 17, RoomID = 2, Row = 3, Column = 1 },
                new { SeatID = 18, RoomID = 2, Row = 3, Column = 2 },
                new { SeatID = 19, RoomID = 2, Row = 3, Column = 3 },
                new { SeatID = 20, RoomID = 2, Row = 3, Column = 4 },
                new { SeatID = 21, RoomID = 2, Row = 3, Column = 5 },
                new { SeatID = 22, RoomID = 2, Row = 3, Column = 6 },
                new { SeatID = 23, RoomID = 2, Row = 3, Column = 7 },
                new { SeatID = 24, RoomID = 2, Row = 3, Column = 8 },

                new { SeatID = 25, RoomID = 2, Row = 4, Column = 1 },
                new { SeatID = 26, RoomID = 2, Row = 4, Column = 2 },
                new { SeatID = 27, RoomID = 2, Row = 4, Column = 3 },
                new { SeatID = 28, RoomID = 2, Row = 4, Column = 4 },
                new { SeatID = 29, RoomID = 2, Row = 4, Column = 5 },
                new { SeatID = 30, RoomID = 2, Row = 4, Column = 6 },
                new { SeatID = 31, RoomID = 2, Row = 4, Column = 7 },
                new { SeatID = 32, RoomID = 2, Row = 4, Column = 8 },

                new { SeatID = 33, RoomID = 2, Row = 5, Column = 1 },
                new { SeatID = 34, RoomID = 2, Row = 5, Column = 2 },
                new { SeatID = 35, RoomID = 2, Row = 5, Column = 3 },
                new { SeatID = 36, RoomID = 2, Row = 5, Column = 4 },
                new { SeatID = 37, RoomID = 2, Row = 5, Column = 5 },
                new { SeatID = 38, RoomID = 2, Row = 5, Column = 6 },
                new { SeatID = 39, RoomID = 2, Row = 5, Column = 7 },
                new { SeatID = 40, RoomID = 2, Row = 5, Column = 8 },

                new { SeatID = 41, RoomID = 2, Row = 6, Column = 1 },
                new { SeatID = 42, RoomID = 2, Row = 6, Column = 2 },
                new { SeatID = 43, RoomID = 2, Row = 6, Column = 3 },
                new { SeatID = 44, RoomID = 2, Row = 6, Column = 4 },
                new { SeatID = 45, RoomID = 2, Row = 6, Column = 5 },
                new { SeatID = 46, RoomID = 2, Row = 6, Column = 6 },
                new { SeatID = 47, RoomID = 2, Row = 6, Column = 7 },
                new { SeatID = 48, RoomID = 2, Row = 6, Column = 8 },

                // Room C1
                new { SeatID = 1, RoomID = 3, Row = 1, Column = 1 },
                new { SeatID = 2, RoomID = 3, Row = 1, Column = 2 },
                new { SeatID = 3, RoomID = 3, Row = 1, Column = 3 },
                new { SeatID = 4, RoomID = 3, Row = 1, Column = 4 },
                new { SeatID = 5, RoomID = 3, Row = 1, Column = 5 },
                new { SeatID = 6, RoomID = 3, Row = 1, Column = 6 },
                new { SeatID = 7, RoomID = 3, Row = 1, Column = 7 },
                new { SeatID = 8, RoomID = 3, Row = 1, Column = 8 },

                new { SeatID = 9, RoomID = 3, Row = 2, Column = 1 },
                new { SeatID = 10, RoomID = 3, Row = 2, Column = 2 },
                new { SeatID = 11, RoomID = 3, Row = 2, Column = 3 },
                new { SeatID = 12, RoomID = 3, Row = 2, Column = 4 },
                new { SeatID = 13, RoomID = 3, Row = 2, Column = 5 },
                new { SeatID = 14, RoomID = 3, Row = 2, Column = 6 },
                new { SeatID = 15, RoomID = 3, Row = 2, Column = 7 },
                new { SeatID = 16, RoomID = 3, Row = 2, Column = 8 },

                new { SeatID = 17, RoomID = 3, Row = 3, Column = 1 },
                new { SeatID = 18, RoomID = 3, Row = 3, Column = 2 },
                new { SeatID = 19, RoomID = 3, Row = 3, Column = 3 },
                new { SeatID = 20, RoomID = 3, Row = 3, Column = 4 },
                new { SeatID = 21, RoomID = 3, Row = 3, Column = 5 },
                new { SeatID = 22, RoomID = 3, Row = 3, Column = 6 },
                new { SeatID = 23, RoomID = 3, Row = 3, Column = 7 },
                new { SeatID = 24, RoomID = 3, Row = 3, Column = 8 },

                new { SeatID = 25, RoomID = 3, Row = 4, Column = 1 },
                new { SeatID = 26, RoomID = 3, Row = 4, Column = 2 },
                new { SeatID = 27, RoomID = 3, Row = 4, Column = 3 },
                new { SeatID = 28, RoomID = 3, Row = 4, Column = 4 },
                new { SeatID = 29, RoomID = 3, Row = 4, Column = 5 },
                new { SeatID = 30, RoomID = 3, Row = 4, Column = 6 },
                new { SeatID = 31, RoomID = 3, Row = 4, Column = 7 },
                new { SeatID = 32, RoomID = 3, Row = 4, Column = 8 },

                new { SeatID = 33, RoomID = 3, Row = 5, Column = 1 },
                new { SeatID = 34, RoomID = 3, Row = 5, Column = 2 },
                new { SeatID = 35, RoomID = 3, Row = 5, Column = 3 },
                new { SeatID = 36, RoomID = 3, Row = 5, Column = 4 },
                new { SeatID = 37, RoomID = 3, Row = 5, Column = 5 },
                new { SeatID = 38, RoomID = 3, Row = 5, Column = 6 },
                new { SeatID = 39, RoomID = 3, Row = 5, Column = 7 },
                new { SeatID = 40, RoomID = 3, Row = 5, Column = 8 },

                new { SeatID = 41, RoomID = 3, Row = 6, Column = 1 },
                new { SeatID = 42, RoomID = 3, Row = 6, Column = 2 },
                new { SeatID = 43, RoomID = 3, Row = 6, Column = 3 },
                new { SeatID = 44, RoomID = 3, Row = 6, Column = 4 },
                new { SeatID = 45, RoomID = 3, Row = 6, Column = 5 },
                new { SeatID = 46, RoomID = 3, Row = 6, Column = 6 },
                new { SeatID = 47, RoomID = 3, Row = 6, Column = 7 },
                new { SeatID = 48, RoomID = 3, Row = 6, Column = 8 }
                );
        }

        private void SeedFilms(ModelBuilder builder)
        {
            builder.Entity<Film>().HasData(
                new
                {
                    FilmID = 1,
                    Name = "The Notebook",
                    Country = "Estados Unidos",
                    FilmLength = new TimeSpan(2, 04, 0).TotalMinutes,
                    Genre = "Romance/Drama",
                    Synopsis = "En un hogar de retiro un hombre le lee a una mujer, que sufre de Alzheimer, la historia de dos jóvenes de distintas clases sociales que se enamoraron " +
                    "durante la convulsionada década del 40, y de cómo fueron separados por terceros, y por la guerra",
                    Rating = 6
                },
                new
                {
                    FilmID = 2,
                    Name = "Rain Man",
                    Country = "Estados Unidos",
                    FilmLength = new TimeSpan(2, 14, 0).TotalMinutes,
                    Genre = "Drama/Melodrama",
                    Synopsis = "Un joven codicioso entabla una rara relación con el hermano autista que nunca conoció, y quien heredó la fortuna de su padre.",
                    Rating = 4
                },
                new
                {
                    FilmID = 3,
                    Name = "Scent of a Woman",
                    Country = "Estados Unidos",
                    FilmLength = new TimeSpan(2, 37, 0).TotalMinutes,
                    Genre = "Drama",
                    Synopsis = "Un excoronel invidente lleva a su joven guía a pasar una aventura maravillosa en Nueva York y enseñarle los placeres de la vida.",
                    Rating = 7

                },
                new
                {
                    FilmID = 4,
                    Name = "The Shawshank Redemption",
                    Country = "Estados Unidos",
                    FilmLength = new TimeSpan(2, 22, 0).TotalMinutes,
                    Genre = "Drama/Crimen",
                    Synopsis = "Un hombre inocente es enviado a una corrupta penitenciaria de Maine en 1947 y sentenciado a dos cadenas perpetuas por un doble asesinato.",
                    Rating = 8
                },
                new
                {
                    FilmID = 5,
                    Name = "Before Sunrise",
                    Country = "Estados Unidos",
                    FilmLength = new TimeSpan(1, 45, 0).TotalMinutes,
                    Genre = "Romance/Independiente",
                    Synopsis = "Dos viajeros, un joven estadounidense y una mujer francesa, se conocen en un tren y pasan un día romántico en Viena, Austria.",
                    Rating = 4
                },
                new
                {
                    FilmID = 6,
                    Name = "Good Will Hunting",
                    Country = "Estados Unidos",
                    FilmLength = new TimeSpan(2, 07, 0).TotalMinutes,
                    Genre = "Romance/Drama",
                    Synopsis = "Un joven, tras descubrir su talento con las matemáticas, " +
                    "deberá decidir si seguir con su vida de siempre o aprovechar sus grandes cualidades intelectuales en alguna universidad.",
                    Rating = 6
                },
                new
                {
                    FilmID = 7,
                    Name = "Forrest Gump",
                    Country = "Estados Unidos",
                    FilmLength = new TimeSpan(2, 22, 0).TotalMinutes,
                    Genre = "Romance/Drama",
                    Synopsis = "Forrest Gump, un joven sureño, tenaz e inocente, es protagonista de acontecimientos cruciales en la historia de los Estados Unidos.",
                    Rating = 8
                },
                new
                {
                    FilmID = 8,
                    Name = "The Shining",
                    Country = "Estados Unidos",
                    FilmLength = new TimeSpan(2, 26, 0).TotalMinutes,
                    Genre = "Suspenso/Terror",
                    Synopsis = "Jack Torrance se convierte en cuidador de invierno en el Hotel Overlook, en Colorado, con la esperanza de vencer su bloqueo con la escritura. " +
                    "Se instala allí junto con su esposa, Wendy, y su hijo, Danny, que está plagado de premoniciones psíquicas. Mientras la escritura de Jack no fluye " +
                    "y las visiones de Danny se vuelven más preocupantes, " +
                    "Jack descubre oscuros secretos del hotel y comienza a convertirse en un maníaco homicida, empeñado en aterrorizar a su familia.",
                    Rating = 9
                },
                new
                {
                    FilmID = 9,
                    Name = "Interstellar",
                    Country = "Estados Unidos",
                    FilmLength = new TimeSpan(2, 49, 0).TotalMinutes,
                    Genre = "Ciencia ficción/Aventura",
                    Synopsis = "Gracias a un descubrimiento, un grupo de científicos y exploradores, encabezados por Cooper, " +
                    "se embarcan en un viaje espacial para encontrar un lugar con las condiciones necesarias para reemplazar a la Tierra" +
                    " y comenzar una nueva vida allí.",
                    Rating = 8
                },
                new
                {
                    FilmID = 10,
                    Name = "Eternal Sunshine Of The Spotless Mind",
                    Country = "Estados Unidos",
                    FilmLength = new TimeSpan(1, 48, 0).TotalMinutes,
                    Genre = "Romance/Ciencia ficción",
                    Synopsis = "Parecían la pareja ideal, su primer encuentro fue mágico, pero con el paso del tiempo ella deseó nunca haberlo conocido." +
                    " Su anhelo se hace realidad gracias a un polémico y radical invento. Sin embargo descubrirán que el destino no se puede controlar.",
                    Rating = 5
                }
                );
        }

        private void SeedPurchases(ModelBuilder builder)
        {
            builder.Entity<Purchase>().HasData(
                new { UserID = 1, PurchaseCode = "npc1111", ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00), FilmID = 1, RoomID = 1, SeatID = 1, PayWithPoints = false, ShowingEnd = new DateTime(2021, 05, 28, 12, 00, 00), Price = 10, UsedPoints = 0 },
                new { UserID = 2, PurchaseCode = "npc1112", ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00), FilmID = 1, RoomID = 1, SeatID = 2, PayWithPoints = false, ShowingEnd = new DateTime(2021, 05, 28, 12, 00, 00), Price = 10, UsedPoints = 0 },
                new { UserID = 3, PurchaseCode = "npc1113", ShowingStart = new DateTime(2021, 05, 29, 10, 00, 00), FilmID = 4, RoomID = 1, SeatID = 3, PayWithPoints = false, ShowingEnd = new DateTime(2021, 05, 29, 12, 00, 00), Price = 12, UsedPoints = 0 },
                new { UserID = 4, PurchaseCode = "npc1114", ShowingStart = new DateTime(2021, 05, 29, 10, 00, 00), FilmID = 4, RoomID = 1, SeatID = 4, PayWithPoints = false, ShowingEnd = new DateTime(2021, 05, 29, 12, 00, 00), Price = 12, UsedPoints = 0 },
                new { UserID = 5, PurchaseCode = "npc1115", ShowingStart = new DateTime(2021, 05, 29, 11, 00, 00), FilmID = 5, RoomID = 4, SeatID = 4, PayWithPoints = false, ShowingEnd = new DateTime(2021, 05, 29, 13, 00, 00), Price = 13, UsedPoints = 0 },
                new { UserID = 6, PurchaseCode = "npc1116", ShowingStart = new DateTime(2021, 05, 29, 11, 00, 00), FilmID = 5, RoomID = 4, SeatID = 5, PayWithPoints = false, ShowingEnd = new DateTime(2021, 05, 29, 13, 00, 00), Price = 13, UsedPoints = 0 },
                new { UserID = 1, PurchaseCode = "ABCDEFGA", ShowingStart = new DateTime(2021, 05, 28, 12, 00, 00), FilmID = 1, RoomID = 1, SeatID = 10, PayWithPoints = true, Price = 30, UsedPoints = 5 },
                new { UserID = 3, PurchaseCode = "DEDFGRHA", ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00), FilmID = 3, RoomID = 3, SeatID = 15, PayWithPoints = false, Price = 10, UsedPoints = 0 }
                );

        }
    }
}
