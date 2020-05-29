using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateSuggestionSubjectSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuestionPeople",
                table: "CentralPolicyProvinces",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SuggestionSubject",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectCentralPolicyProvinceId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Suggestion = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestionSubject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuggestionSubject_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                        column: x => x.SubjectCentralPolicyProvinceId,
                        principalTable: "SubjectCentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SuggestionSubject_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });



            migrationBuilder.CreateIndex(
                name: "IX_SuggestionSubject_SubjectCentralPolicyProvinceId",
                table: "SuggestionSubject",
                column: "SubjectCentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionSubject_UserId",
                table: "SuggestionSubject",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuggestionSubject");

            migrationBuilder.DropColumn(
                name: "QuestionPeople",
                table: "CentralPolicyProvinces");


        }
    }
}
