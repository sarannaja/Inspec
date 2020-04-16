using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class UpdateCentralPolicyUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProvinceId",
                table: "CentralPolicyUsers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyUsers_ProvinceId",
                table: "CentralPolicyUsers",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralPolicyUsers_Provinces_ProvinceId",
                table: "CentralPolicyUsers",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CentralPolicyUsers_Provinces_ProvinceId",
                table: "CentralPolicyUsers");

            migrationBuilder.DropIndex(
                name: "IX_CentralPolicyUsers_ProvinceId",
                table: "CentralPolicyUsers");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "CentralPolicyUsers");
        }
    }
}
