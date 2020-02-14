using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class ModelsInspectionOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InspectionOrders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<string>(nullable: false),
                    Order = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CreateBy = table.Column<string>(nullable: false),
                    File = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionOrders", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "InspectionOrders",
                columns: new[] { "Id", "CreateBy", "CreatedAt", "File", "Name", "Order", "Year" },
                values: new object[,]
                {
                    { 1L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com63.pdf", "การตรวจราชการประจำปีงบประมาณ พ.ศ. 2563", "คำสั่งสำนักนายกรัฐมนตรี ที่ 361/2562", "2563" },
                    { 2L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com62.pdf", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2562", "คำสั่งสำนักนายกรัฐมนตรี ที่ 343/2561", "2562" },
                    { 3L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com61.pdf", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2561", "คำสั่งสำนักนายกรัฐมนตรี ที่ 15/2561", "2561" },
                    { 4L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com60.pdf", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2560", "คำสั่งสำนักนายกรัฐมนตรี ที่ 264/2559", "2560" },
                    { 5L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com59.pdf", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2559", "คำสั่งสำนักนายกรัฐมนตรี ที่ 396/2558", "2559" },
                    { 6L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com58.pdf", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2558", "คำสั่งสำนักนายกรัฐมนตรี ที่ 256/2557", "2558" },
                    { 7L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2557", "คำสั่งสำนักนายกรัฐมนตรี ที่ 328/2556 ", "2557" },
                    { 8L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com56.pdf", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2556", "คำสั่งสำนักนายกรัฐมนตรี ที่ 302/2555", "2556" },
                    { 9L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com55.pdf", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2555", "คำสั่งสำนักนายกรัฐมนตรี ที่ 19/2555", "2555" },
                    { 10L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com54.pdf", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2554", "คำสั่งสำนักนายกรัฐมนตรี ที่ 8/2554", "2554" },
                    { 11L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com53.pdf", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2553", "คำสั่งสำนักนายกรัฐมนตรี ที่ 8/2553", "2553" },
                    { 12L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com52.pdf", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2552", "คำสั่งสำนักนายกรัฐมนตรี ที่ 21/2552", "2552" },
                    { 13L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com52.tif", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2551", "คำสั่งสำนักนายกรัฐมนตรี ที่ 226/2550", "2551" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InspectionOrders");
        }
    }
}
