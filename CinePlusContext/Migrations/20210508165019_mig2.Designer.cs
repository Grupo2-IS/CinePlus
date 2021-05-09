﻿// <auto-generated />
using System;
using CinePlus.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CinePlusContext.Migrations
{
    [DbContext(typeof(CinePlusDb))]
    [Migration("20210508165019_mig2")]
    partial class mig2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CinePlus.Entities.Artist", b =>
                {
                    b.Property<int>("ArtistID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("ArtistID");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("CinePlus.Entities.Director", b =>
                {
                    b.Property<int>("ArtistID")
                        .HasColumnType("int");

                    b.Property<int>("FilmID")
                        .HasColumnType("int");

                    b.HasKey("ArtistID", "FilmID");

                    b.HasIndex("FilmID");

                    b.ToTable("Directors");
                });

            modelBuilder.Entity("CinePlus.Entities.Film", b =>
                {
                    b.Property<int>("FilmID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<TimeSpan>("FilmLength")
                        .HasColumnType("time");

                    b.Property<string>("Genre")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("FilmID");

                    b.ToTable("Films");
                });

            modelBuilder.Entity("CinePlus.Entities.MemberPurchase", b =>
                {
                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ShowingStart")
                        .HasColumnType("datetime2");

                    b.Property<int>("FilmID")
                        .HasColumnType("int");

                    b.Property<int>("RoomID")
                        .HasColumnType("int");

                    b.Property<int>("SeatID")
                        .HasColumnType("int");

                    b.Property<bool>("PayWithPoints")
                        .HasColumnType("bit");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("PurchaseCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsedPoints")
                        .HasColumnType("int");

                    b.HasKey("MemberId", "ShowingStart", "FilmID", "RoomID", "SeatID");

                    b.HasIndex("SeatID", "RoomID");

                    b.HasIndex("ShowingStart", "FilmID", "RoomID");

                    b.ToTable("MemberPurchases");
                });

            modelBuilder.Entity("CinePlus.Entities.NormalPurchase", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ShowingStart")
                        .HasColumnType("datetime2");

                    b.Property<int>("FilmID")
                        .HasColumnType("int");

                    b.Property<int>("RoomID")
                        .HasColumnType("int");

                    b.Property<int>("SeatID")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("PurchaseCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "ShowingStart", "FilmID", "RoomID", "SeatID");

                    b.HasIndex("SeatID", "RoomID");

                    b.HasIndex("ShowingStart", "FilmID", "RoomID");

                    b.ToTable("NormalPurchases");
                });

            modelBuilder.Entity("CinePlus.Entities.Performer", b =>
                {
                    b.Property<int>("ArtistID")
                        .HasColumnType("int");

                    b.Property<int>("FilmID")
                        .HasColumnType("int");

                    b.HasKey("ArtistID", "FilmID");

                    b.HasIndex("FilmID");

                    b.ToTable("Performers");
                });

            modelBuilder.Entity("CinePlus.Entities.Room", b =>
                {
                    b.Property<int>("RoomID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoomName")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("RoomID");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("CinePlus.Entities.Seat", b =>
                {
                    b.Property<int>("SeatID")
                        .HasColumnType("int");

                    b.Property<int>("RoomID")
                        .HasColumnType("int");

                    b.Property<int>("Row")
                        .HasColumnType("int");

                    b.HasKey("SeatID", "RoomID");

                    b.HasIndex("RoomID");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("CinePlus.Entities.Showing", b =>
                {
                    b.Property<DateTime>("ShowingStart")
                        .HasColumnType("datetime2");

                    b.Property<int>("FilmID")
                        .HasColumnType("int");

                    b.Property<int>("RoomID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ShowingEnd")
                        .HasColumnType("datetime2");

                    b.HasKey("ShowingStart", "FilmID", "RoomID");

                    b.HasIndex("FilmID");

                    b.HasIndex("RoomID");

                    b.ToTable("Showings");
                });

            modelBuilder.Entity("CinePlus.Entities.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Nick")
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    b.HasKey("UserID");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("CinePlus.Entities.Member", b =>
                {
                    b.HasBaseType("CinePlus.Entities.User");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Member");
                });

            modelBuilder.Entity("CinePlus.Entities.Director", b =>
                {
                    b.HasOne("CinePlus.Entities.Artist", "Artist")
                        .WithMany("Directors")
                        .HasForeignKey("ArtistID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CinePlus.Entities.Film", "Film")
                        .WithMany("Directors")
                        .HasForeignKey("FilmID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");

                    b.Navigation("Film");
                });

            modelBuilder.Entity("CinePlus.Entities.MemberPurchase", b =>
                {
                    b.HasOne("CinePlus.Entities.Member", "Member")
                        .WithMany("MemberPurchases")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CinePlus.Entities.Seat", "Seat")
                        .WithMany("MemberPurchases")
                        .HasForeignKey("SeatID", "RoomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CinePlus.Entities.Showing", "Showing")
                        .WithMany("MemberPurchases")
                        .HasForeignKey("ShowingStart", "FilmID", "RoomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Seat");

                    b.Navigation("Showing");
                });

            modelBuilder.Entity("CinePlus.Entities.NormalPurchase", b =>
                {
                    b.HasOne("CinePlus.Entities.User", "User")
                        .WithMany("NormalPurchases")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CinePlus.Entities.Seat", "Seat")
                        .WithMany("NormalPurchases")
                        .HasForeignKey("SeatID", "RoomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CinePlus.Entities.Showing", "Showing")
                        .WithMany("NormalPurchases")
                        .HasForeignKey("ShowingStart", "FilmID", "RoomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seat");

                    b.Navigation("Showing");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CinePlus.Entities.Performer", b =>
                {
                    b.HasOne("CinePlus.Entities.Artist", "Artist")
                        .WithMany("Performers")
                        .HasForeignKey("ArtistID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CinePlus.Entities.Film", "Film")
                        .WithMany("Performers")
                        .HasForeignKey("FilmID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");

                    b.Navigation("Film");
                });

            modelBuilder.Entity("CinePlus.Entities.Seat", b =>
                {
                    b.HasOne("CinePlus.Entities.Room", "Room")
                        .WithMany("Seats")
                        .HasForeignKey("RoomID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("CinePlus.Entities.Showing", b =>
                {
                    b.HasOne("CinePlus.Entities.Film", "Film")
                        .WithMany("Showings")
                        .HasForeignKey("FilmID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CinePlus.Entities.Room", "Room")
                        .WithMany("Showings")
                        .HasForeignKey("RoomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("CinePlus.Entities.Artist", b =>
                {
                    b.Navigation("Directors");

                    b.Navigation("Performers");
                });

            modelBuilder.Entity("CinePlus.Entities.Film", b =>
                {
                    b.Navigation("Directors");

                    b.Navigation("Performers");

                    b.Navigation("Showings");
                });

            modelBuilder.Entity("CinePlus.Entities.Room", b =>
                {
                    b.Navigation("Seats");

                    b.Navigation("Showings");
                });

            modelBuilder.Entity("CinePlus.Entities.Seat", b =>
                {
                    b.Navigation("MemberPurchases");

                    b.Navigation("NormalPurchases");
                });

            modelBuilder.Entity("CinePlus.Entities.Showing", b =>
                {
                    b.Navigation("MemberPurchases");

                    b.Navigation("NormalPurchases");
                });

            modelBuilder.Entity("CinePlus.Entities.User", b =>
                {
                    b.Navigation("NormalPurchases");
                });

            modelBuilder.Entity("CinePlus.Entities.Member", b =>
                {
                    b.Navigation("MemberPurchases");
                });
#pragma warning restore 612, 618
        }
    }
}
