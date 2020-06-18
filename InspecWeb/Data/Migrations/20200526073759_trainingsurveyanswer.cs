using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class trainingsurveyanswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TrainingRegisters",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "TrainingSurveyAnswers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingSurveyId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Posoition = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingSurveyAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingSurveyAnswers_TrainingSurveys_TrainingSurveyId",
                        column: x => x.TrainingSurveyId,
                        principalTable: "TrainingSurveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_TrainingSurveyAnswers_TrainingSurveyId",
                table: "TrainingSurveyAnswers",
                column: "TrainingSurveyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingSurveyAnswers");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "TrainingRegisters",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            
        }
    }
}
