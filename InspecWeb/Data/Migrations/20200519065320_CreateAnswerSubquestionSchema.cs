using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateAnswerSubquestionSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnswerSubquestionOutsiders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubquestionCentralPolicyProvinceId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Position = table.Column<string>(nullable: false),
                    Phonenumber = table.Column<string>(nullable: false),
                    Answer = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerSubquestionOutsiders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerSubquestionOutsiders_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                        column: x => x.SubquestionCentralPolicyProvinceId,
                        principalTable: "SubquestionCentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerSubquestions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubquestionCentralPolicyProvinceId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerSubquestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerSubquestions_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                        column: x => x.SubquestionCentralPolicyProvinceId,
                        principalTable: "SubquestionCentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerSubquestions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerSubquestionOutsiders_SubquestionCentralPolicyProvinceId",
                table: "AnswerSubquestionOutsiders",
                column: "SubquestionCentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerSubquestions_SubquestionCentralPolicyProvinceId",
                table: "AnswerSubquestions",
                column: "SubquestionCentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerSubquestions_UserId",
                table: "AnswerSubquestions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerSubquestionOutsiders");

            migrationBuilder.DropTable(
                name: "AnswerSubquestions");
        }
    }
}
