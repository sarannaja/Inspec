using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateExportReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExportReportHead",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceId = table.Column<long>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    Ministry = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportReportHead", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExportReportHead_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExportReportBody",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExportReportHeadId = table.Column<long>(nullable: false),
                    Subject = table.Column<string>(nullable: true),
                    Problem = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    Suggestion = table.Column<string>(nullable: true),
                    Report = table.Column<string>(nullable: true),
                    File = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportReportBody", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExportReportBody_ExportReportHead_ExportReportHeadId",
                        column: x => x.ExportReportHeadId,
                        principalTable: "ExportReportHead",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExportReportBody_ExportReportHeadId",
                table: "ExportReportBody",
                column: "ExportReportHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportReportHead_ProvinceId",
                table: "ExportReportHead",
                column: "ProvinceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExportReportBody");

            migrationBuilder.DropTable(
                name: "ExportReportHead");
        }
    }
}
