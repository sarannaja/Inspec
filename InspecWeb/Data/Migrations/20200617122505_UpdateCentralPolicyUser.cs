using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class UpdateCentralPolicyUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CentralPolicyUsers_ElectronicBooks_ElectronicBookId",
                table: "CentralPolicyUsers");

            migrationBuilder.DropIndex(
                name: "IX_CentralPolicyUsers_ElectronicBookId",
                table: "CentralPolicyUsers");

            migrationBuilder.DropColumn(
                name: "ElectronicBookId",
                table: "CentralPolicyUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ElectronicBookId",
                table: "CentralPolicyUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyUsers_ElectronicBookId",
                table: "CentralPolicyUsers",
                column: "ElectronicBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralPolicyUsers_ElectronicBooks_ElectronicBookId",
                table: "CentralPolicyUsers",
                column: "ElectronicBookId",
                principalTable: "ElectronicBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
