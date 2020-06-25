using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class electronicBookUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ElectronicBooks",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ElectronicBookInvite",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBooks_CreatedBy",
                table: "ElectronicBooks",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookInvite_UserId",
                table: "ElectronicBookInvite",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectronicBookInvite_AspNetUsers_UserId",
                table: "ElectronicBookInvite",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ElectronicBooks_AspNetUsers_CreatedBy",
                table: "ElectronicBooks",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectronicBookInvite_AspNetUsers_UserId",
                table: "ElectronicBookInvite");

            migrationBuilder.DropForeignKey(
                name: "FK_ElectronicBooks_AspNetUsers_CreatedBy",
                table: "ElectronicBooks");

            migrationBuilder.DropIndex(
                name: "IX_ElectronicBooks_CreatedBy",
                table: "ElectronicBooks");

            migrationBuilder.DropIndex(
                name: "IX_ElectronicBookInvite_UserId",
                table: "ElectronicBookInvite");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ElectronicBooks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ElectronicBookInvite",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
