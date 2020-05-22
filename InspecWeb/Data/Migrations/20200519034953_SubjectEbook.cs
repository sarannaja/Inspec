using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class SubjectEbook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Detail",
                table: "ElectronicBookSuggestGroups",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Problem",
                table: "ElectronicBookSuggestGroups",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Suggestion",
                table: "ElectronicBookSuggestGroups",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detail",
                table: "ElectronicBookSuggestGroups");

            migrationBuilder.DropColumn(
                name: "Problem",
                table: "ElectronicBookSuggestGroups");

            migrationBuilder.DropColumn(
                name: "Suggestion",
                table: "ElectronicBookSuggestGroups");
        }
    }
}
