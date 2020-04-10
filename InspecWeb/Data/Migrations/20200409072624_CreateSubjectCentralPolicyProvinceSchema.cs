using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateSubjectCentralPolicyProvinceSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "CentralPolicyDateProvinces",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralPolicyDateProvinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectCentralPolicyProvinces",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyProvinceId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectCentralPolicyProvinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectCentralPolicyProvinces_CentralPolicyProvinces_CentralPolicyProvinceId",
                        column: x => x.CentralPolicyProvinceId,
                        principalTable: "CentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectDateCentralPolicyProvinces",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectCentralPolicyProvinceId = table.Column<long>(nullable: false),
                    CentralPolicyDateProvinceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectDateCentralPolicyProvinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectDateCentralPolicyProvinces_CentralPolicyDateProvinces_CentralPolicyDateProvinceId",
                        column: x => x.CentralPolicyDateProvinceId,
                        principalTable: "CentralPolicyDateProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectDateCentralPolicyProvinces_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                        column: x => x.SubjectCentralPolicyProvinceId,
                        principalTable: "SubjectCentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubquestionCentralPolicyProvinces",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectCentralPolicyProvinceId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubquestionCentralPolicyProvinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubquestionCentralPolicyProvinces_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                        column: x => x.SubjectCentralPolicyProvinceId,
                        principalTable: "SubjectCentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubquestionChoiceCentralPolicyProvinces",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubquestionCentralPolicyProvinceId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubquestionChoiceCentralPolicyProvinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubquestionChoiceCentralPolicyProvinces_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                        column: x => x.SubquestionCentralPolicyProvinceId,
                        principalTable: "SubquestionCentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_SubjectCentralPolicyProvinces_CentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinces",
                column: "CentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectDateCentralPolicyProvinces_CentralPolicyDateProvinceId",
                table: "SubjectDateCentralPolicyProvinces",
                column: "CentralPolicyDateProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectDateCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SubjectDateCentralPolicyProvinces",
                column: "SubjectCentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_SubquestionCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SubquestionCentralPolicyProvinces",
                column: "SubjectCentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_SubquestionChoiceCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                table: "SubquestionChoiceCentralPolicyProvinces",
                column: "SubquestionCentralPolicyProvinceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectDateCentralPolicyProvinces");

            migrationBuilder.DropTable(
                name: "SubquestionChoiceCentralPolicyProvinces");

            migrationBuilder.DropTable(
                name: "CentralPolicyDateProvinces");

            migrationBuilder.DropTable(
                name: "SubquestionCentralPolicyProvinces");

            migrationBuilder.DropTable(
                name: "SubjectCentralPolicyProvinces");

        }
    }
}
