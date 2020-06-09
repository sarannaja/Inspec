using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class TrainingRegisterPlus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Posoition",
                table: "TrainingSurveyAnswers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CardId",
                table: "TrainingRegisters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "TrainingRegisters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardId",
                table: "TrainingRegisters");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "TrainingRegisters");

            migrationBuilder.AlterColumn<int>(
                name: "Posoition",
                table: "TrainingSurveyAnswers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

        }
    }
}
