using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CinePlusContext.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    ArtistID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.ArtistID);
                });

            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    FilmID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    FilmLength = table.Column<TimeSpan>(type: "time", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.FilmID);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nick = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    FilmID = table.Column<int>(type: "int", nullable: false),
                    ArtistID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => new { x.ArtistID, x.FilmID });
                    table.ForeignKey(
                        name: "FK_Directors_Artists_ArtistID",
                        column: x => x.ArtistID,
                        principalTable: "Artists",
                        principalColumn: "ArtistID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Directors_Films_FilmID",
                        column: x => x.FilmID,
                        principalTable: "Films",
                        principalColumn: "FilmID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Performers",
                columns: table => new
                {
                    FilmID = table.Column<int>(type: "int", nullable: false),
                    ArtistID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performers", x => new { x.ArtistID, x.FilmID });
                    table.ForeignKey(
                        name: "FK_Performers_Artists_ArtistID",
                        column: x => x.ArtistID,
                        principalTable: "Artists",
                        principalColumn: "ArtistID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Performers_Films_FilmID",
                        column: x => x.FilmID,
                        principalTable: "Films",
                        principalColumn: "FilmID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    SeatID = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    Row = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => new { x.SeatID, x.RoomID });
                    table.ForeignKey(
                        name: "FK_Seats_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "RoomID");
                });

            migrationBuilder.CreateTable(
                name: "Showings",
                columns: table => new
                {
                    FilmID = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    ShowingStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShowingEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Showings", x => new { x.ShowingStart, x.FilmID, x.RoomID });
                    table.ForeignKey(
                        name: "FK_Showings_Films_FilmID",
                        column: x => x.FilmID,
                        principalTable: "Films",
                        principalColumn: "FilmID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Showings_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShowingSeats",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SeatID = table.Column<int>(type: "int", nullable: false),
                    FilmID = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    ShowingStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    PayWithPoints = table.Column<bool>(type: "bit", nullable: false),
                    UsedPoints = table.Column<int>(type: "int", nullable: false),
                    PurchaseCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowingSeats", x => new { x.UserId, x.ShowingStart, x.FilmID, x.RoomID, x.SeatID });
                    table.ForeignKey(
                        name: "FK_ShowingSeats_Seats_SeatID_RoomID",
                        columns: x => new { x.SeatID, x.RoomID },
                        principalTable: "Seats",
                        principalColumns: new[] { "SeatID", "RoomID" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShowingSeats_Showings_ShowingStart_FilmID_RoomID",
                        columns: x => new { x.ShowingStart, x.FilmID, x.RoomID },
                        principalTable: "Showings",
                        principalColumns: new[] { "ShowingStart", "FilmID", "RoomID" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShowingSeats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Directors_FilmID",
                table: "Directors",
                column: "FilmID");

            migrationBuilder.CreateIndex(
                name: "IX_Performers_FilmID",
                table: "Performers",
                column: "FilmID");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_RoomID",
                table: "Seats",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Showings_FilmID",
                table: "Showings",
                column: "FilmID");

            migrationBuilder.CreateIndex(
                name: "IX_Showings_RoomID",
                table: "Showings",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_ShowingSeats_SeatID_RoomID",
                table: "ShowingSeats",
                columns: new[] { "SeatID", "RoomID" });

            migrationBuilder.CreateIndex(
                name: "IX_ShowingSeats_ShowingStart_FilmID_RoomID",
                table: "ShowingSeats",
                columns: new[] { "ShowingStart", "FilmID", "RoomID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Directors");

            migrationBuilder.DropTable(
                name: "Performers");

            migrationBuilder.DropTable(
                name: "ShowingSeats");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Showings");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
