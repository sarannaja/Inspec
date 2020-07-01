using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class UpdateTrainingRegister : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Group",
                table: "TrainingRegisters");

            migrationBuilder.AddColumn<long>(
                name: "Group1",
                table: "TrainingRegisters",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Group10",
                table: "TrainingRegisters",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Group2",
                table: "TrainingRegisters",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Group3",
                table: "TrainingRegisters",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Group4",
                table: "TrainingRegisters",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Group5",
                table: "TrainingRegisters",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Group6",
                table: "TrainingRegisters",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Group7",
                table: "TrainingRegisters",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Group8",
                table: "TrainingRegisters",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Group9",
                table: "TrainingRegisters",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Group1",
                table: "TrainingRegisters");

            migrationBuilder.DropColumn(
                name: "Group10",
                table: "TrainingRegisters");

            migrationBuilder.DropColumn(
                name: "Group2",
                table: "TrainingRegisters");

            migrationBuilder.DropColumn(
                name: "Group3",
                table: "TrainingRegisters");

            migrationBuilder.DropColumn(
                name: "Group4",
                table: "TrainingRegisters");

            migrationBuilder.DropColumn(
                name: "Group5",
                table: "TrainingRegisters");

            migrationBuilder.DropColumn(
                name: "Group6",
                table: "TrainingRegisters");

            migrationBuilder.DropColumn(
                name: "Group7",
                table: "TrainingRegisters");

            migrationBuilder.DropColumn(
                name: "Group8",
                table: "TrainingRegisters");

            migrationBuilder.DropColumn(
                name: "Group9",
                table: "TrainingRegisters");

            migrationBuilder.AddColumn<long>(
                name: "Group",
                table: "TrainingRegisters",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

        }
    }
}
