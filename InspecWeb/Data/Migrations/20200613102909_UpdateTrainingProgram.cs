using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class UpdateTrainingProgram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPrograms_Trainings_TrainingId",
                table: "TrainingPrograms");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPrograms_TrainingId",
                table: "TrainingPrograms");

            migrationBuilder.DropColumn(
                name: "LecturerId",
                table: "TrainingPrograms");

            migrationBuilder.DropColumn(
                name: "ProgramTopic",
                table: "TrainingPrograms");

            migrationBuilder.DropColumn(
                name: "ProgramType",
                table: "TrainingPrograms");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "TrainingPrograms");

            migrationBuilder.AddColumn<string>(
                name: "ProgramLocation",
                table: "TrainingPrograms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProgramToDress",
                table: "TrainingPrograms",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TrainingPhaseId",
                table: "TrainingPrograms",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_TrainingPhaseId",
                table: "TrainingPrograms",
                column: "TrainingPhaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPrograms_TrainingPhases_TrainingPhaseId",
                table: "TrainingPrograms",
                column: "TrainingPhaseId",
                principalTable: "TrainingPhases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPrograms_TrainingPhases_TrainingPhaseId",
                table: "TrainingPrograms");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPrograms_TrainingPhaseId",
                table: "TrainingPrograms");

            migrationBuilder.DropColumn(
                name: "ProgramLocation",
                table: "TrainingPrograms");

            migrationBuilder.DropColumn(
                name: "ProgramToDress",
                table: "TrainingPrograms");

            migrationBuilder.DropColumn(
                name: "TrainingPhaseId",
                table: "TrainingPrograms");

            migrationBuilder.AddColumn<string>(
                name: "LecturerId",
                table: "TrainingPrograms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProgramTopic",
                table: "TrainingPrograms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProgramType",
                table: "TrainingPrograms",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TrainingId",
                table: "TrainingPrograms",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_TrainingId",
                table: "TrainingPrograms",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPrograms_Trainings_TrainingId",
                table: "TrainingPrograms",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
