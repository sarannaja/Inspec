using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class TrainingLecturer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LecturerName",
                table: "TrainingPrograms");

            migrationBuilder.AddColumn<int>(
                name: "Generation",
                table: "Trainings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Trainings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Trainings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "TrainingRegisters",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProgramDetail",
                table: "TrainingPrograms",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MinuteStartDate",
                table: "TrainingPrograms",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "MinuteEndDate",
                table: "TrainingPrograms",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "LecturerId",
                table: "TrainingPrograms",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProgramTopic",
                table: "TrainingPrograms",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Generation",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "TrainingRegisters");

            migrationBuilder.DropColumn(
                name: "LecturerId",
                table: "TrainingPrograms");

            migrationBuilder.DropColumn(
                name: "ProgramTopic",
                table: "TrainingPrograms");

            migrationBuilder.AlterColumn<string>(
                name: "ProgramDetail",
                table: "TrainingPrograms",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "MinuteStartDate",
                table: "TrainingPrograms",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "MinuteEndDate",
                table: "TrainingPrograms",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LecturerName",
                table: "TrainingPrograms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

        }
    }
}
