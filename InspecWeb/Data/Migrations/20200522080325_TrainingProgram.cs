using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class TrainingProgram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrainingPrograms",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingId = table.Column<long>(nullable: false),
                    LecturerName = table.Column<string>(nullable: false),
                    ProgramDetail = table.Column<string>(nullable: false),
                    ProgramDate = table.Column<DateTime>(nullable: false),
                    MinuteStartDate = table.Column<DateTime>(nullable: false),
                    MinuteEndDate = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPrograms", x => x.Id);
                });


            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "TrainingRegisters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "TrainingRegisters",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Status",
                table: "TrainingRegisters",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "TrainingRegisters",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserType",
                table: "TrainingRegisters",
                nullable: false,
                defaultValue: 0L);


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingPrograms");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "TrainingRegisters");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "TrainingRegisters");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TrainingRegisters");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TrainingRegisters");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "TrainingRegisters");

        }
    }
}
