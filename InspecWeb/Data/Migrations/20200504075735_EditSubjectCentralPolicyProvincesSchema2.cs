using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class EditSubjectCentralPolicyProvincesSchema2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "SubjectCentralPolicyProvinces",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "SubjectCentralPolicyProvinces",
                nullable: true);

          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "SubjectCentralPolicyProvinces");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "SubjectCentralPolicyProvinces");

          
        }
    }
}
