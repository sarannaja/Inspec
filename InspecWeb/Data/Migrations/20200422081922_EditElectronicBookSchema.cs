using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class EditElectronicBookSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectronicBooks_AspNetUsers_UserId",
                table: "ElectronicBooks");

            migrationBuilder.DropIndex(
                name: "IX_ElectronicBooks_UserId",
                table: "ElectronicBooks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ElectronicBooks");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ElectronicBooks",
                nullable: true);

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ElectronicBooks");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ElectronicBooks",
                type: "nvarchar(450)",
                nullable: true);

            

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBooks_UserId",
                table: "ElectronicBooks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectronicBooks_AspNetUsers_UserId",
                table: "ElectronicBooks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
