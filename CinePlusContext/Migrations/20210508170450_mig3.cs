using Microsoft.EntityFrameworkCore.Migrations;

namespace CinePlusContext.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberPurchases_Users_MemberId",
                table: "MemberPurchases");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Members_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MemberPurchases_Members_MemberId",
                table: "MemberPurchases",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberPurchases_Members_MemberId",
                table: "MemberPurchases");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberPurchases_Users_MemberId",
                table: "MemberPurchases",
                column: "MemberId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
