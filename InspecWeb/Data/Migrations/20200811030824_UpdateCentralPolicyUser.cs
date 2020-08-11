using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class UpdateCentralPolicyUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Forward",
                table: "CentralPolicyUsers");

            migrationBuilder.AddColumn<string>(
                name: "ForwardDepartment",
                table: "CentralPolicyUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForwardEmail",
                table: "CentralPolicyUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForwardName",
                table: "CentralPolicyUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForwardPhone",
                table: "CentralPolicyUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForwardPosition",
                table: "CentralPolicyUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForwardDepartment",
                table: "CentralPolicyUsers");

            migrationBuilder.DropColumn(
                name: "ForwardEmail",
                table: "CentralPolicyUsers");

            migrationBuilder.DropColumn(
                name: "ForwardName",
                table: "CentralPolicyUsers");

            migrationBuilder.DropColumn(
                name: "ForwardPhone",
                table: "CentralPolicyUsers");

            migrationBuilder.DropColumn(
                name: "ForwardPosition",
                table: "CentralPolicyUsers");

            migrationBuilder.AddColumn<string>(
                name: "Forward",
                table: "CentralPolicyUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
