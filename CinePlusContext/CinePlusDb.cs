using System;
using CinePlus.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CinePlus.Context
{
    public class CinePlusDb: DbContext
    {
        public CinePlusDb(){}
        public CinePlusDb(DbContextOptions options):base(options)
        {
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Performer> Performers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Showing> Showings { get; set; }
        public DbSet<NormalPurchase> NormalPurchases { get; set; }
        public DbSet<MemberPurchase> MemberPurchases { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localDB)\MSSQLLocalDB;Database=CinePlusDB;Integrated Security=true;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Configuring Director Entity.
            builder.Entity<Director>().HasKey(
                d => new {
                    d.ArtistID,
                    d.FilmID
                }
            );

            builder.Entity<Director>()
                .HasOne(d => d.Artist)
                .WithMany(a => a.Directors)
                .HasForeignKey(d => d.ArtistID);

            builder.Entity<Director>()
                .HasOne(d => d.Film)
                .WithMany(f => f.Directors)
                .HasForeignKey(d => d.FilmID);

            builder.Entity<Director>().HasData(
            new { ArtistID = 1, FilmID = 1 },
            new { ArtistID = 2, FilmID = 2 },
            new { ArtistID = 3, FilmID = 3 },
            new { ArtistID = 4, FilmID = 4 },
            new { ArtistID = 5, FilmID = 4 },
            new { ArtistID = 6, FilmID = 5 }
            );

            // Configuring Performer entity.
            builder.Entity<Performer>().HasKey(
                p => new {
                    p.ArtistID,
                    p.FilmID
                }
            );

            builder.Entity<Performer>()
                .HasOne(p => p.Artist)
                .WithMany(a => a.Performers)
                .HasForeignKey(p => p.ArtistID);
            
            builder.Entity<Performer>()
                .HasOne(p => p.Film)
                .WithMany(f => f.Performers)
                .HasForeignKey(p => p.FilmID);

            builder.Entity<Performer>().HasData(
                new { ArtistID = 1, FilmID = 1 },
                new { ArtistID = 2, FilmID = 2 },
                new { ArtistID = 3, FilmID = 3 },
                new { ArtistID = 4, FilmID = 4 },
                new { ArtistID = 5, FilmID = 4 },
                new { ArtistID = 6, FilmID = 5 }
                );

            // Configuring Seat Entity
            builder.Entity<Seat>().HasKey(
                s => new {s.SeatID, s.RoomID}
            );

            builder.Entity<Seat>()
                .HasOne(s => s.Room)
                .WithMany(r => r.Seats)
                .HasForeignKey(s => s.RoomID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Seat>().HasData(
                 new { SeatID = 1, RoomID = 1},
                 new { SeatID = 2, RoomID = 1 },
                 new { SeatID = 3, RoomID = 1 },
                 new { SeatID = 4, RoomID = 2 },
                 new { SeatID = 5, RoomID = 3 },
                 new { SeatID = 6, RoomID = 4 }
                );

            // Configuring ShowingSeat Entity.
            builder.Entity<Showing>().HasKey(
                sh => new {sh.ShowingStart, sh.FilmID, sh.RoomID, sh.ShowingEnd}
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
            builder.Entity<NormalPurchase>().HasKey(
                ss => new {ss.UserId, ss.ShowingStart, ss.FilmID, ss.RoomID, ss.SeatID}
            );

            builder.Entity<NormalPurchase>()
                .HasOne(ss => ss.Seat)
                .WithMany(s => s.NormalPurchases)
                .HasForeignKey(ss => new {ss.SeatID, ss.RoomID});

            builder.Entity<NormalPurchase>()
                .HasOne(ss => ss.Showing)
                .WithMany(sh => sh.NormalPurchases)
                .HasForeignKey(ss => new {ss.ShowingStart, ss.FilmID, ss.RoomID});

            builder.Entity<NormalPurchase>()
                .HasOne(sh => sh.User)
                .WithMany(u => u.NormalPurchases)
                .HasForeignKey(sh => sh.UserId);

            builder.Entity<NormalPurchase>().HasData(
                new { UserId = 1, ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00), FilmID = 1, RoomID = 1, ShowingEnd = new DateTime(2021, 05, 28, 12, 00, 00), Price = 10 },
                new { UserId = 2, ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00), FilmID = 1, RoomID = 1, ShowingEnd = new DateTime(2021, 05, 28, 12, 00, 00), Price = 10 },
                new { UserId = 3, ShowingStart = new DateTime(2021, 05, 29, 10, 00, 00), FilmID = 4, RoomID = 1, ShowingEnd = new DateTime(2021, 05, 29, 12, 00, 00), Price = 12 },
                new { UserId = 4, ShowingStart = new DateTime(2021, 05, 29, 10, 00, 00), FilmID = 4, RoomID = 1, ShowingEnd = new DateTime(2021, 05, 29, 12, 00, 00), Price = 12 },
                new { UserId = 5, ShowingStart = new DateTime(2021, 05, 29, 11, 00, 00), FilmID = 5, RoomID = 4, ShowingEnd = new DateTime(2021, 05, 29, 13, 00, 00), Price = 13 },
                new { UserId = 6, ShowingStart = new DateTime(2021, 05, 29, 11, 00, 00), FilmID = 5, RoomID = 4, ShowingEnd = new DateTime(2021, 05, 29, 13, 00, 00), Price = 13 }
                );

            // Configuring MemberPurchase Entity
            builder.Entity<MemberPurchase>().HasKey(
                ss => new {ss.MemberId, ss.ShowingStart, ss.FilmID, ss.RoomID, ss.SeatID}
            );

            builder.Entity<MemberPurchase>()
                .HasOne(ss => ss.Seat)
                .WithMany(s => s.MemberPurchases)
                .HasForeignKey(ss => new {ss.SeatID, ss.RoomID});

            builder.Entity<MemberPurchase>()
                .HasOne(ss => ss.Showing)
                .WithMany(sh => sh.MemberPurchases)
                .HasForeignKey(ss => new {ss.ShowingStart, ss.FilmID, ss.RoomID});

            builder.Entity<MemberPurchase>()
                .HasOne(sh => sh.Member)
                .WithMany(m => m.MemberPurchases)
                .HasForeignKey(sh => sh.MemberId);

            builder.Entity<MemberPurchase>().HasData(
                new {MemberId = 1, 
                    FimlID = 1, 
                    RoomID = 1, 
                    SeatID = 1, 
                    PayWithPoints = true, 
                    Points = 30, 
                    PurchaseCode = "ABCDEFGA", 
                    UsedPoints = 5, 
                    ShowingStarts = new DateTime(2021, 05, 28, 12, 00, 00)},

                new {MemberId = 3,
                     FilmID = 3,
                     RoomID = 3,
                     SeatID = 5,
                     PayWithPoints =  false,
                     Points = 10,
                     PurchaseCode = "DEDFGRHA",
                     UsedPoints = 0,
                     ShowingStart = new DateTime(2021, 05, 28, 10, 00, 00)
                }
                );

            // Configuring Member Entity.
            builder.Entity<Member>().HasKey(
                ss => new {ss.MemberID, ss.UserID, ss.Points}
                );

            builder.Entity<Member>()
                .HasOne(m => m.User)
                .WithOne(u => u.Member);

            builder.Entity<Member>().HasData(
                new { MemberID = 1, UserID = 2, Points = 35 },
                new { MemberID = 2, UserID = 3, Points = 25 },
                new { MemberID = 3, UserID = 5, Points = 10 }
                );


            //Configuring Film Entity
            builder.Entity<Film>().HasKey(
                ss => new { ss.FilmID, ss.Name, ss.Country, ss.Directors, ss.FilmLength, ss.Genre, ss.Performers, ss.Showings, ss.Synopsis }
                );

            builder.Entity<Film>().HasData(
                new {FilmID = 1,
                    Name = "The Notebook", 
                    Country = "Estados Unidos", 
                    Directors = "Nick Cassavetes", 
                    FilmLength = new TimeSpan(2,04,0), 
                    Genre = "Romance/Drama", 
                    Synosis = "En un hogar de retiro un hombre le lee a una mujer, que sufre de Alzheimer, la historia de dos jóvenes de distintas clases sociales que se enamoraron " +
                    "durante la convulsionada década del 40, y de cómo fueron separados por terceros, y por la guerra"},
                new
                {
                    FilmID = 2,
                    Name = "Rain Man",
                    Country = "Estados Unidos",
                    Directors = "Barry Levinson",
                    FilmLength = new TimeSpan(2, 14, 0),
                    Genre = "Drama/Melodrama",
                    Synosis = "Un joven codicioso entabla una rara relación con el hermano autista que nunca conoció, y quien heredó la fortuna de su padre."
                },
                new
                {
                    FilmID = 3,
                    Name = "Scent of a Woman",
                    Country = "Estados Unidos",
                    Directors = "Martin Brest",
                    FilmLength = new TimeSpan(2, 37, 0),
                    Genre = "Drama",
                    Synosis = "Un excoronel invidente lleva a su joven guía a pasar una aventura maravillosa en Nueva York y enseñarle los placeres de la vida."
                },
                new
                {
                    FilmID = 4,
                    Name = "The Shawshank Redemption",
                    Country = "Estados Unidos",
                    Directors = " Frank Darabont",
                    FilmLength = new TimeSpan(2, 22, 0),
                    Genre = "Drama/Crimen",
                    Synosis = "Un hombre inocente es enviado a una corrupta penitenciaria de Maine en 1947 y sentenciado a dos cadenas perpetuas por un doble asesinato."
                },
                new
                {
                    FilmID = 5,
                    Name = "Before Sunrise",
                    Country = "Estados Unidos",
                    Directors = "Richard Linklater",
                    FilmLength = new TimeSpan(1, 45, 0),
                    Genre = "Romance/Independiente",
                    Synosis = "Dos viajeros, un joven estadounidense y una mujer francesa, se conocen en un tren y pasan un día romántico en Viena, Austria."
                },
                new
                {
                    FilmID = 6,
                    Name = "Good Will Hunting",
                    Country = "Estados Unidos",
                    Directors = " Gus Van Sant",
                    FilmLength = new TimeSpan(2, 07, 0),
                    Genre = "Romance/Drama",
                    Synosis = "Un joven, tras descubrir su talento con las matemáticas, " +
                    "deberá decidir si seguir con su vida de siempre o aprovechar sus grandes cualidades intelectuales en alguna universidad."
                },
                new
                {
                    FilmID = 7,
                    Name = "Forrest Gump",
                    Country = "Estados Unidos",
                    Directors = "Robert Zemeckis",
                    FilmLength = new TimeSpan(2, 22, 0),
                    Genre = "Romance/Drama",
                    Synosis = "Forrest Gump, un joven sureño, tenaz e inocente, es protagonista de acontecimientos cruciales en la historia de los Estados Unidos."
                },
                new
                {
                    FilmID = 8,
                    Name = "The Shining",
                    Country = "Estados Unidos",
                    Directors = "Stanley Kubrick",
                    FilmLength = new TimeSpan(2, 26, 0),
                    Genre = "Suspenso/Terror",
                    Synosis = "Jack Torrance se convierte en cuidador de invierno en el Hotel Overlook, en Colorado, con la esperanza de vencer su bloqueo con la escritura. " +
                    "Se instala allí junto con su esposa, Wendy, y su hijo, Danny, que está plagado de premoniciones psíquicas. Mientras la escritura de Jack no fluye " +
                    "y las visiones de Danny se vuelven más preocupantes, " +
                    "Jack descubre oscuros secretos del hotel y comienza a convertirse en un maníaco homicida, empeñado en aterrorizar a su familia."
                },
                new
                {
                    FilmID = 9,
                    Name = "Interstellar",
                    Country = "Estados Unidos",
                    Directors = "Christopher Nolan",
                    FilmLength = new TimeSpan(2, 49, 0),
                    Genre = "Ciencia ficción/Aventura",
                    Synosis = "Gracias a un descubrimiento, un grupo de científicos y exploradores, encabezados por Cooper, " +
                    "se embarcan en un viaje espacial para encontrar un lugar con las condiciones necesarias para reemplazar a la Tierra" +
                    " y comenzar una nueva vida allí."
                },
                new
                {
                    FilmID = 10,
                    Name = "Eternal Sunshine Of The Spotless Mind",
                    Country = "Estados Unidos",
                    Directors = "Michel Gondry",
                    FilmLength = new TimeSpan(1, 48, 0),
                    Genre = "Romance/Ciencia ficción",
                    Synosis = "Parecían la pareja ideal, su primer encuentro fue mágico, pero con el paso del tiempo ella deseó nunca haberlo conocido." +
                    " Su anhelo se hace realidad gracias a un polémico y radical invento. Sin embargo descubrirán que el destino no se puede controlar."
                }
                );

            //Configuring Artist Entity
            builder.Entity<Artist>().HasKey(
                ss => new { ss.ArtistID, ss.Name }
                );

            builder.Entity<Artist>().HasData(
                new { ArtistID = 1, Name = "Ryan Gosling" },
                new { ArtistID = 2, Name = "Rachel MacAdams" },
                new { ArtistID = 3, Name = "Tom Cruise" },
                new { ArtistID = 4, Name = "Dustin Hoffman" },
                new { ArtistID = 5, Name = "Al Pacino" },
                new { ArtistID = 6, Name = "Morgan Freeman" },
                new { ArtistID = 7, Name = "Tim Robbins" },
                new { ArtistID = 8, Name = "Tom Hanks" },
                new { ArtistID = 9, Name = "Ethan Hawke" },
                new { ArtistID = 10, Name = "Julie Delpy" },
                new { ArtistID = 11, Name = "Jim Carrey" },
                new { ArtistID = 12, Name = "Kate Winslet" },
                new { ArtistID = 13, Name = "Anne Hathaway" },
                new { ArtistID = 14, Name = "Matthew McConaughey" },
                new { ArtistID = 15, Name = "Jack Nicholson" },
                new { ArtistID = 16, Name = "Matt Damon" },
                new { ArtistID = 17, Name = "Ben Affleck" }
                );

            //Configuring User Entity
            builder.Entity<User>().HasKey(
                ss => new { ss.UserID, ss.Member, ss.Nick, ss.NormalPurchases}
                );

            builder.Entity<User>().HasData(
                new  {UserID = 1, Nick = 232456 },
                new { UserID = 2, Nick = 893484334353 },
                new { UserID = 3, Nick = 6433334343 },
                new { UserID = 4, Nick = 6576423433 },
                new { UserID = 5, Nick = 945848452433 },
                new { UserID = 6, Nick = 2656565653433 }
                );

            //Configuring Room Entity
            builder.Entity<Room>().HasKey(
                ss => new { ss.RoomID, ss.RoomName, ss.Seats, ss.Showings }
                );

            builder.Entity<Room>().HasData(
                new 
                {
                    RoomID = 1, 
                    RoomName = "A1", 
                    Seats = new List<Seat> { new Seat { SeatID = 1 }, new Seat { SeatID = 2 }, new Seat { SeatID = 3 } } 
                },
                new
                {
                    RoomID = 2,
                    RoomName = "B1",
                    Seats = new List<Seat> { new Seat { SeatID = 4 } }
                },
                new
                {
                    RoomID = 3,
                    RoomName = "C1",
                    Seats = new List<Seat> { new Seat { SeatID = 5 } }
                },
                new
                {
                    RoomID = 4,
                    RoomName = "D1",
                    Seats = new List<Seat> { new Seat { SeatID = 6 } }
                }
                );
        }

    }
}
