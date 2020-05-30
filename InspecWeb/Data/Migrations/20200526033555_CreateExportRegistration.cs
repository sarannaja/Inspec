using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateExportRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExportRegistration",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceId = table.Column<long>(nullable: false),
                    Date = table.Column<DateTime>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    SendDate = table.Column<DateTime>(nullable: true),
                    ExcutiveOerder = table.Column<string>(nullable: true),
                    ExcutiveOerderDate = table.Column<DateTime>(nullable: true),
                    File = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportRegistration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExportRegistration_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExportRegistration_ProvinceId",
                table: "ExportRegistration",
                column: "ProvinceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExportRegistration");
        }
    }
}
