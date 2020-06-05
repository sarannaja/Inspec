using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class EditAnswerSubquestionFileSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AnswerSubquestionFiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "AnswerSubquestionFiles",
                nullable: true);

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AnswerSubquestionFiles");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AnswerSubquestionFiles");

        }
    }
}
