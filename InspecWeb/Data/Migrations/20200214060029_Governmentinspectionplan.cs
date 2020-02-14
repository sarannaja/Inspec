using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class Governmentinspectionplan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Governmentinspectionplans",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    File = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governmentinspectionplans", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Governmentinspectionplans",
                columns: new[] { "Id", "CreatedAt", "File", "Title", "Year" },
                values: new object[,]
                {
                    { 1L, null, "inps_plan63.pdf", "แผนการตรวจราชการแบบบบูรณาการของผู้ตรวจราชการ ประจำปีงบประมาณ พ.ศ. 2563", "2563" },
                    { 2L, null, "plan_61.pdf", "แผนการตรวจราชการแบบบบูรณาการของผู้ตรวจราชการ ประจำปีงบประมาณ พ.ศ. 2561", "2561" },
                    { 3L, null, "plan60.pdf", "แผนการตรวจราชการแบบบูรณาการ ของผู้ตรวจราชการ ประจำปีงบประมาณ พ.ศ. 2560", "2560" },
                    { 4L, null, "plan59.pdf", "แผนการตรวจราชการแบบบูรณาการประจำปีงบประมาณ พ.ศ. 2559", "2559" },
                    { 5L, null, "insp_plan58.zip", "แผนการตรวจราชการแบบบูรณาการประจำปีงบประมาณ พ.ศ. 2558", "2558" },
                    { 6L, null, "plan57.zip", "แผนการตรวจราชการแบบบูรรณาการ ของผู้ตรวจราชการ ประจำปีงบประมาณ พ.ศ. 2557", "2557" },
                    { 7L, null, "plan56.zip", "แผนการตรวจราชการแบบบูรรณาการ ของผู้ตรวจราชการ ประจำปีงบประมาณ พ.ศ. 2556", "2556" },
                    { 8L, null, "plan55.zip", "แผนการตรวจราชการแบบบูรณาการ ของผู้ตรวจราชการ ประจำปีงบประมาณ พ.ศ. 2555", "2555" },
                    { 9L, null, "plan54.zip", "แผนการตรวจราชการแบบบูรณาการ ประจำปีงบประมาณ พ.ศ.2554", "2554" },
                    { 10L, null, "NULL", "แผนการตรวจราชการแบบบูรณาการเพื่อมุ่งผลสัมฤทธิ์ตามนโยบายรัฐบาลประจำปีงบประมาณ พ.ศ.2553ของผู้ตรวจราชการ", "2553" },
                    { 11L, null, "plan52.pdf", "แผนการตรวจราชการแบบบูรณาการเพื่อมุ่งผลสัมฤทธิ์ตามนโยบายรัฐบาลประจำปีงบประมาณ พ.ศ. 2552", "2552" },
                    { 12L, null, "NULL", "แผนยุทธศาสตร์การตรวจราชการแบบบูรณาการเพื่อมุ่งผลสัมฤทธิ์ตามนโยบายรัฐบาลประจำปีงบประมาณ พ.ศ. 2551", "2551" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Governmentinspectionplans");
        }
    }
}
