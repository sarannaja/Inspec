using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class TrainingRegisterGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrainingRegisterGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisterId = table.Column<long>(nullable: false),
                    ProgramId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingRegisterGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingRegisterGroups_TrainingRegisters_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "TrainingRegisters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "IX_TrainingRegisterGroups_RegisterId",
                table: "TrainingRegisterGroups",
                column: "RegisterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingRegisterGroups");

            
        }
    }
}
