using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryManagementAPI.Migrations
{
    public partial class changedRequestClassForRequestStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_RequestStatus_StatusId",
                table: "Requests");

            migrationBuilder.DropTable(
                name: "RequestStatus");

            migrationBuilder.DropIndex(
                name: "IX_Requests_StatusId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Requests");

            migrationBuilder.AlterColumn<string>(
                name: "RequestStatus",
                table: "Requests",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RequestStatus",
                table: "Requests",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Requests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RequestStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_StatusId",
                table: "Requests",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_RequestStatus_StatusId",
                table: "Requests",
                column: "StatusId",
                principalTable: "RequestStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
