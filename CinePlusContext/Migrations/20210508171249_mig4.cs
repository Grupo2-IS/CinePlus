using Microsoft.EntityFrameworkCore.Migrations;

namespace CinePlusContext.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberPurchases_Members_MemberId",
                table: "MemberPurchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.AddColumn<int>(
                name: "MemberID",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Members_UserID",
                table: "Members",
                column: "UserID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberPurchases_Members_MemberId",
                table: "MemberPurchases",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "MemberID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberPurchases_Members_MemberId",
                table: "MemberPurchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_UserID",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MemberID",
                table: "Members");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberPurchases_Members_MemberId",
                table: "MemberPurchases",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
