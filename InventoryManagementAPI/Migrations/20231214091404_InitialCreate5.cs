using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryManagementAPI.Migrations
{
    public partial class InitialCreate5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_Products_ProductId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_RequestStatus_StatusId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_AspNetUsers_UserId",
                table: "Request");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Request",
                table: "Request");

            migrationBuilder.RenameTable(
                name: "Request",
                newName: "Requests");

            migrationBuilder.RenameIndex(
                name: "IX_Request_UserId",
                table: "Requests",
                newName: "IX_Requests_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Request_StatusId",
                table: "Requests",
                newName: "IX_Requests_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Request_ProductId",
                table: "Requests",
                newName: "IX_Requests_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Requests",
                table: "Requests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Products_ProductId",
                table: "Requests",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_RequestStatus_StatusId",
                table: "Requests",
                column: "StatusId",
                principalTable: "RequestStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_UserId",
                table: "Requests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Products_ProductId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_RequestStatus_StatusId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_UserId",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Requests",
                table: "Requests");

            migrationBuilder.RenameTable(
                name: "Requests",
                newName: "Request");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_UserId",
                table: "Request",
                newName: "IX_Request_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_StatusId",
                table: "Request",
                newName: "IX_Request_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_ProductId",
                table: "Request",
                newName: "IX_Request_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Request",
                table: "Request",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Products_ProductId",
                table: "Request",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_RequestStatus_StatusId",
                table: "Request",
                column: "StatusId",
                principalTable: "RequestStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_AspNetUsers_UserId",
                table: "Request",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
