using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class TrainingProgramAndRegister : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Group",
                table: "TrainingRegisters",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProgramType",
                table: "TrainingPrograms",
                nullable: false,
                defaultValue: 0L);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Group",
                table: "TrainingRegisters");

            migrationBuilder.DropColumn(
                name: "ProgramType",
                table: "TrainingPrograms");
        }
    }
}
