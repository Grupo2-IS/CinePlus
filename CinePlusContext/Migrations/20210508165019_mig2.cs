using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CinePlusContext.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShowingSeats");

            migrationBuilder.CreateTable(
                name: "MemberPurchases",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_MemberPurchases", x => new { x.MemberId, x.ShowingStart, x.FilmID, x.RoomID, x.SeatID });
                    table.ForeignKey(
                        name: "FK_MemberPurchases_Seats_SeatID_RoomID",
                        columns: x => new { x.SeatID, x.RoomID },
                        principalTable: "Seats",
                        principalColumns: new[] { "SeatID", "RoomID" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberPurchases_Showings_ShowingStart_FilmID_RoomID",
                        columns: x => new { x.ShowingStart, x.FilmID, x.RoomID },
                        principalTable: "Showings",
                        principalColumns: new[] { "ShowingStart", "FilmID", "RoomID" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberPurchases_Users_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NormalPurchases",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SeatID = table.Column<int>(type: "int", nullable: false),
                    FilmID = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    ShowingStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    PurchaseCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NormalPurchases", x => new { x.UserId, x.ShowingStart, x.FilmID, x.RoomID, x.SeatID });
                    table.ForeignKey(
                        name: "FK_NormalPurchases_Seats_SeatID_RoomID",
                        columns: x => new { x.SeatID, x.RoomID },
                        principalTable: "Seats",
                        principalColumns: new[] { "SeatID", "RoomID" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NormalPurchases_Showings_ShowingStart_FilmID_RoomID",
                        columns: x => new { x.ShowingStart, x.FilmID, x.RoomID },
                        principalTable: "Showings",
                        principalColumns: new[] { "ShowingStart", "FilmID", "RoomID" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NormalPurchases_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberPurchases_SeatID_RoomID",
                table: "MemberPurchases",
                columns: new[] { "SeatID", "RoomID" });

            migrationBuilder.CreateIndex(
                name: "IX_MemberPurchases_ShowingStart_FilmID_RoomID",
                table: "MemberPurchases",
                columns: new[] { "ShowingStart", "FilmID", "RoomID" });

            migrationBuilder.CreateIndex(
                name: "IX_NormalPurchases_SeatID_RoomID",
                table: "NormalPurchases",
                columns: new[] { "SeatID", "RoomID" });

            migrationBuilder.CreateIndex(
                name: "IX_NormalPurchases_ShowingStart_FilmID_RoomID",
                table: "NormalPurchases",
                columns: new[] { "ShowingStart", "FilmID", "RoomID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberPurchases");

            migrationBuilder.DropTable(
                name: "NormalPurchases");

            migrationBuilder.CreateTable(
                name: "ShowingSeats",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ShowingStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FilmID = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    SeatID = table.Column<int>(type: "int", nullable: false),
                    PayWithPoints = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    PurchaseCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsedPoints = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_ShowingSeats_SeatID_RoomID",
                table: "ShowingSeats",
                columns: new[] { "SeatID", "RoomID" });

            migrationBuilder.CreateIndex(
                name: "IX_ShowingSeats_ShowingStart_FilmID_RoomID",
                table: "ShowingSeats",
                columns: new[] { "ShowingStart", "FilmID", "RoomID" });
        }
    }
}
