using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class UpdateSubjectGroupAndCentralPolicyEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Land",
                table: "SubjectGroups",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "SubjectGroups",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "HaveSubject",
                table: "CentralPolicyEvents",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Land",
                table: "SubjectGroups");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "SubjectGroups");

            migrationBuilder.DropColumn(
                name: "HaveSubject",
                table: "CentralPolicyEvents");
        }
    }
}
