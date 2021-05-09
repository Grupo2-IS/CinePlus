using System;
using CinePlus.Entities;
using Microsoft.EntityFrameworkCore;

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


            // Configuring Seat Entity
            builder.Entity<Seat>().HasKey(
                s => new {s.SeatID, s.RoomID}
            );

            builder.Entity<Seat>()
                .HasOne(s => s.Room)
                .WithMany(r => r.Seats)
                .HasForeignKey(s => s.RoomID)
                .OnDelete(DeleteBehavior.NoAction);

            // Configuring ShowingSeat Entity.
            builder.Entity<Showing>().HasKey(
                sh => new {sh.ShowingStart, sh.FilmID, sh.RoomID}
            );

            builder.Entity<Showing>()
                .HasOne(sh => sh.Room)
                .WithMany(r => r.Showings)
                .HasForeignKey(sh => sh.RoomID);
            
            builder.Entity<Showing>()
                .HasOne(sh => sh.Film)
                .WithMany(f => f.Showings)
                .HasForeignKey(sh => sh.FilmID);
            

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
            
            // Configuring Member Entity.
            builder.Entity<Member>()
                .HasOne(m => m.User)
                .WithOne(u => u.Member);
        }

    }
}
