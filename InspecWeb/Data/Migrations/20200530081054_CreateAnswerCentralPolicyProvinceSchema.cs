using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateAnswerCentralPolicyProvinceSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ElectronicBookFiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CentralPolicyUserFiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "CentralPolicyUserFiles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnswerCentralPolicyProvinces",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyProvinceId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerCentralPolicyProvinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerCentralPolicyProvinces_CentralPolicyProvinces_CentralPolicyProvinceId",
                        column: x => x.CentralPolicyProvinceId,
                        principalTable: "CentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerCentralPolicyProvinces_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerCentralPolicyProvinces_CentralPolicyProvinceId",
                table: "AnswerCentralPolicyProvinces",
                column: "CentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerCentralPolicyProvinces_UserId",
                table: "AnswerCentralPolicyProvinces",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerCentralPolicyProvinces");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ElectronicBookFiles");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CentralPolicyUserFiles");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "CentralPolicyUserFiles");

        }
    }
}
