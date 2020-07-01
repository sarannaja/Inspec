using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class UpdateTrainingRegisterGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "TrainingRegisterGroups");

            migrationBuilder.AddColumn<long>(
                name: "Group1",
                table: "TrainingRegisterGroups",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Group10",
                table: "TrainingRegisterGroups",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Group2",
                table: "TrainingRegisterGroups",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Group3",
                table: "TrainingRegisterGroups",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Group4",
                table: "TrainingRegisterGroups",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Group5",
                table: "TrainingRegisterGroups",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Group6",
                table: "TrainingRegisterGroups",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Group7",
                table: "TrainingRegisterGroups",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Group8",
                table: "TrainingRegisterGroups",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Group9",
                table: "TrainingRegisterGroups",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TrainingPhaseId",
                table: "TrainingRegisterGroups",
                nullable: false,
                defaultValue: 0L);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Group1",
                table: "TrainingRegisterGroups");

            migrationBuilder.DropColumn(
                name: "Group10",
                table: "TrainingRegisterGroups");

            migrationBuilder.DropColumn(
                name: "Group2",
                table: "TrainingRegisterGroups");

            migrationBuilder.DropColumn(
                name: "Group3",
                table: "TrainingRegisterGroups");

            migrationBuilder.DropColumn(
                name: "Group4",
                table: "TrainingRegisterGroups");

            migrationBuilder.DropColumn(
                name: "Group5",
                table: "TrainingRegisterGroups");

            migrationBuilder.DropColumn(
                name: "Group6",
                table: "TrainingRegisterGroups");

            migrationBuilder.DropColumn(
                name: "Group7",
                table: "TrainingRegisterGroups");

            migrationBuilder.DropColumn(
                name: "Group8",
                table: "TrainingRegisterGroups");

            migrationBuilder.DropColumn(
                name: "Group9",
                table: "TrainingRegisterGroups");

            migrationBuilder.DropColumn(
                name: "TrainingPhaseId",
                table: "TrainingRegisterGroups");

            migrationBuilder.AddColumn<long>(
                name: "ProgramId",
                table: "TrainingRegisterGroups",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            
        }
    }
}
