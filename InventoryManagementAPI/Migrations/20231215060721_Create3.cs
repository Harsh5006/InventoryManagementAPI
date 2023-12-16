using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryManagementAPI.Migrations
{
    public partial class Create3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_AspNetUsers_IdentityUserId1",
                table: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_IdentityUserId1",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "IdentityUserId1",
                table: "ApplicationUser");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_AspNetUsers_IdentityUserId",
                table: "ApplicationUser",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_AspNetUsers_IdentityUserId",
                table: "ApplicationUser");

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId1",
                table: "ApplicationUser",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_IdentityUserId1",
                table: "ApplicationUser",
                column: "IdentityUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_AspNetUsers_IdentityUserId1",
                table: "ApplicationUser",
                column: "IdentityUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
