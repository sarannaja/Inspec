using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class ElectronicBookInviteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectronicBookInvite_ElectronicBooks_ElectronicBookId",
                table: "ElectronicBookInvite");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectronicBookInvite_ElectronicBooks_ElectronicBookId",
                table: "ElectronicBookInvite",
                column: "ElectronicBookId",
                principalTable: "ElectronicBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectronicBookInvite_ElectronicBooks_ElectronicBookId",
                table: "ElectronicBookInvite");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectronicBookInvite_ElectronicBooks_ElectronicBookId",
                table: "ElectronicBookInvite",
                column: "ElectronicBookId",
                principalTable: "ElectronicBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
