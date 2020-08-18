using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class UpdateReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ZoneId",
                table: "ImportReports",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ImportReports_ZoneId",
                table: "ImportReports",
                column: "ZoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImportReports_Sector_ZoneId",
                table: "ImportReports",
                column: "ZoneId",
                principalTable: "Sector",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImportReports_Sector_ZoneId",
                table: "ImportReports");

            migrationBuilder.DropIndex(
                name: "IX_ImportReports_ZoneId",
                table: "ImportReports");

            migrationBuilder.DropColumn(
                name: "ZoneId",
                table: "ImportReports");
        }
    }
}
