using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class UpdateCommander : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Command",
                table: "ReportCommanders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Commander",
                table: "ReportCommanders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Command",
                table: "ReportCommanders");

            migrationBuilder.DropColumn(
                name: "Commander",
                table: "ReportCommanders");
        }
    }
}
