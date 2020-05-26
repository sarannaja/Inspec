using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class UpdateCentralPolicyProvince : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Step",
                table: "SubjectCentralPolicyProvinces");

            migrationBuilder.DropColumn(
                name: "link",
                table: "SubjectCentralPolicyProvinces");

            migrationBuilder.AddColumn<string>(
                name: "Step",
                table: "CentralPolicyProvinces",
                nullable: true);

         
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Step",
                table: "CentralPolicyProvinces");

            migrationBuilder.AddColumn<string>(
                name: "Step",
                table: "SubjectCentralPolicyProvinces",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "link",
                table: "SubjectCentralPolicyProvinces",
                type: "nvarchar(max)",
                nullable: true);

           
        }
    }
}
