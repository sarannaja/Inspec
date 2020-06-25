using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class EditSubjectandSubquestionSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Explanation",
                table: "SubquestionCentralPolicyProvinces");

            migrationBuilder.AddColumn<string>(
                name: "Explanation",
                table: "SubjectCentralPolicyProvinces",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Explanation",
                table: "SubjectCentralPolicyProvinces");

            migrationBuilder.AddColumn<string>(
                name: "Explanation",
                table: "SubquestionCentralPolicyProvinces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

        }
    }
}
