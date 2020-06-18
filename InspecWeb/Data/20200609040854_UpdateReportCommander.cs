using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class UpdateReportCommander : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "ReportCommanders");

            migrationBuilder.AddColumn<string>(
                name: "CreateBy",
                table: "ReportCommanders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileExcel",
                table: "ReportCommanders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileWord",
                table: "ReportCommanders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "ReportCommanders");

            migrationBuilder.DropColumn(
                name: "FileExcel",
                table: "ReportCommanders");

            migrationBuilder.DropColumn(
                name: "FileWord",
                table: "ReportCommanders");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "ReportCommanders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
