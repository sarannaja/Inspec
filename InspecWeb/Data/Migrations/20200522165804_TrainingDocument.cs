using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class TrainingDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrainingDocuments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Detail = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingDocuments_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_TrainingId",
                table: "TrainingPrograms",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingDocuments_TrainingId",
                table: "TrainingDocuments",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPrograms_Trainings_TrainingId",
                table: "TrainingPrograms",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPrograms_Trainings_TrainingId",
                table: "TrainingPrograms");

            migrationBuilder.DropTable(
                name: "TrainingDocuments");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPrograms_TrainingId",
                table: "TrainingPrograms");
        }
    }
}
