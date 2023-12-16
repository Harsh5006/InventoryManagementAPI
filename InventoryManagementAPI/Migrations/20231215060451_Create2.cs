using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryManagementAPI.Migrations
{
    public partial class Create2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_AspNetUsers_IdentityUserId",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_ApplicationUser_UserId1",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_UserId1",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUser",
                table: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_IdentityUserId",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ApplicationUser");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Requests",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityUserId",
                table: "ApplicationUser",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId1",
                table: "ApplicationUser",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUser",
                table: "ApplicationUser",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_UserId",
                table: "Requests",
                column: "UserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_ApplicationUser_UserId",
                table: "Requests",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "IdentityUserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_AspNetUsers_IdentityUserId1",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_ApplicationUser_UserId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_UserId",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUser",
                table: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_IdentityUserId1",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "IdentityUserId1",
                table: "ApplicationUser");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Requests",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityUserId",
                table: "ApplicationUser",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ApplicationUser",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUser",
                table: "ApplicationUser",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_UserId1",
                table: "Requests",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_IdentityUserId",
                table: "ApplicationUser",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_AspNetUsers_IdentityUserId",
                table: "ApplicationUser",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_ApplicationUser_UserId1",
                table: "Requests",
                column: "UserId1",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
