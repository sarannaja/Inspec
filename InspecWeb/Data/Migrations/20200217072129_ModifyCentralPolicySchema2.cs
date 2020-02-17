using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class ModifyCentralPolicySchema2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subdistrict_Districts_DistrictId",
                table: "Subdistrict");

            migrationBuilder.DropForeignKey(
                name: "FK_Village_Subdistrict_SubdistrictId",
                table: "Village");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subdistrict",
                table: "Subdistrict");

            migrationBuilder.RenameTable(
                name: "Subdistrict",
                newName: "Subdistricts");

            migrationBuilder.RenameIndex(
                name: "IX_Subdistrict_DistrictId",
                table: "Subdistricts",
                newName: "IX_Subdistricts_DistrictId");

            migrationBuilder.AddColumn<long>(
                name: "FiscalYearId",
                table: "CentralPolicies",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "CentralPolicies",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subdistricts",
                table: "Subdistricts",
                column: "Id");

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

            migrationBuilder.CreateTable(
                name: "InstructionOrders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<string>(nullable: false),
                    Order = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CreateBy = table.Column<string>(nullable: false),
                    Detail = table.Column<string>(nullable: false),
                    File = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructionOrders", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "Id", "Name", "ProvinceId" },
                values: new object[,]
                {
                    { 1L, "เขตพระนคร", 1L },
                    { 28L, "เขตสาทร", 1L },
                    { 29L, "เขตบางซื่อ", 1L },
                    { 30L, "เขตจตุจักร", 1L },
                    { 31L, "เขตบางคอแหลม", 1L },
                    { 32L, "เขตประเวศ", 1L },
                    { 33L, "เขตคลองเตย", 1L },
                    { 34L, "เขตสวนหลวง", 1L },
                    { 35L, "เขตจอมทอง", 1L },
                    { 36L, "เขตดอนเมือง", 1L },
                    { 37L, "เขตราชเทวี", 1L },
                    { 27L, "เขตบึงกุ่ม", 1L },
                    { 38L, "เขตลาดพร้าว", 1L },
                    { 40L, "เขตบางแค", 1L },
                    { 41L, "เขตหลักสี่", 1L },
                    { 42L, "เขตสายไหม", 1L },
                    { 43L, "เขตคันนายาว", 1L },
                    { 44L, "เขตสะพานสูง", 1L },
                    { 46L, "เขตคลองสามวา", 1L },
                    { 47L, "เขตบางนา", 1L },
                    { 48L, "เขตทวีวัฒนา", 1L },
                    { 49L, "เขตทุ่งครุ", 1L },
                    { 50L, "เขตบางบอน", 1L },
                    { 39L, "เขตวัฒนา", 1L },
                    { 26L, "เขตดินแดง", 1L },
                    { 45L, "เขตวังทองหลาง", 1L },
                    { 24L, "เขตราษฎร์บูรณะ", 1L },
                    { 2L, "เขตดุสิต", 1L },
                    { 3L, "เขตหนองจอก", 1L },
                    { 4L, "เขตบางรัก", 1L },
                    { 5L, "เขตบางเขน", 1L },
                    { 6L, "เขตบางกะปิ", 1L },
                    { 7L, "เขตปทุมวัน", 1L },
                    { 8L, "เขตป้อมปราบศัตรูพ่าย", 1L },
                    { 9L, "เขตพระโขนง", 1L },
                    { 25L, "เขตบางพลัด", 1L },
                    { 11L, "เขตลาดกระบัง", 1L },
                    { 12L, "เขตยานนาวา", 1L },
                    { 10L, "เขตมีนบุรี", 1L },
                    { 14L, "เขตพญาไท", 1L },
                    { 15L, "เขตธนบุรี", 1L },
                    { 16L, "เขตบางกอกใหญ่", 1L },
                    { 17L, "เขตห้วยขวาง", 1L },
                    { 18L, "เขตคลองสาน", 1L },
                    { 19L, "เขตตลิ่งชัน", 1L },
                    { 20L, "เขตบางกอกน้อย", 1L },
                    { 21L, "เขตบางขุนเทียน", 1L },
                    { 22L, "เขตภาษีเจริญ", 1L },
                    { 23L, "เขตหนองแขม", 1L },
                    { 13L, "เขตสัมพันธวงศ์", 1L }
                });

            migrationBuilder.InsertData(
                table: "Governmentinspectionplans",
                columns: new[] { "Id", "CreatedAt", "File", "Title", "Year" },
                values: new object[,]
                {
                    { 12L, null, "NULL", "แผนยุทธศาสตร์การตรวจราชการแบบบูรณาการเพื่อมุ่งผลสัมฤทธิ์ตามนโยบายรัฐบาลประจำปีงบประมาณ พ.ศ. 2551", "2551" },
                    { 11L, null, "plan52.pdf", "แผนการตรวจราชการแบบบูรณาการเพื่อมุ่งผลสัมฤทธิ์ตามนโยบายรัฐบาลประจำปีงบประมาณ พ.ศ. 2552", "2552" },
                    { 10L, null, "NULL", "แผนการตรวจราชการแบบบูรณาการเพื่อมุ่งผลสัมฤทธิ์ตามนโยบายรัฐบาลประจำปีงบประมาณ พ.ศ.2553ของผู้ตรวจราชการ", "2553" },
                    { 9L, null, "plan54.zip", "แผนการตรวจราชการแบบบูรณาการ ประจำปีงบประมาณ พ.ศ.2554", "2554" },
                    { 7L, null, "plan56.zip", "แผนการตรวจราชการแบบบูรรณาการ ของผู้ตรวจราชการ ประจำปีงบประมาณ พ.ศ. 2556", "2556" },
                    { 8L, null, "plan55.zip", "แผนการตรวจราชการแบบบูรณาการ ของผู้ตรวจราชการ ประจำปีงบประมาณ พ.ศ. 2555", "2555" },
                    { 5L, null, "insp_plan58.zip", "แผนการตรวจราชการแบบบูรณาการประจำปีงบประมาณ พ.ศ. 2558", "2558" },
                    { 4L, null, "plan59.pdf", "แผนการตรวจราชการแบบบูรณาการประจำปีงบประมาณ พ.ศ. 2559", "2559" },
                    { 3L, null, "plan60.pdf", "แผนการตรวจราชการแบบบูรณาการ ของผู้ตรวจราชการ ประจำปีงบประมาณ พ.ศ. 2560", "2560" },
                    { 2L, null, "plan_61.pdf", "แผนการตรวจราชการแบบบบูรณาการของผู้ตรวจราชการ ประจำปีงบประมาณ พ.ศ. 2561", "2561" },
                    { 1L, null, "inps_plan63.pdf", "แผนการตรวจราชการแบบบบูรณาการของผู้ตรวจราชการ ประจำปีงบประมาณ พ.ศ. 2563", "2563" },
                    { 6L, null, "plan57.zip", "แผนการตรวจราชการแบบบูรรณาการ ของผู้ตรวจราชการ ประจำปีงบประมาณ พ.ศ. 2557", "2557" }
                });

            migrationBuilder.InsertData(
                table: "InspectionOrders",
                columns: new[] { "Id", "CreateBy", "CreatedAt", "File", "Name", "Order", "Year" },
                values: new object[,]
                {
                    { 13L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com52.tif", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2551", "คำสั่งสำนักนายกรัฐมนตรี ที่ 226/2550", "2551" },
                    { 12L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com52.pdf", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2552", "คำสั่งสำนักนายกรัฐมนตรี ที่ 21/2552", "2552" },
                    { 10L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com54.pdf", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2554", "คำสั่งสำนักนายกรัฐมนตรี ที่ 8/2554", "2554" },
                    { 9L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com55.pdf", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2555", "คำสั่งสำนักนายกรัฐมนตรี ที่ 19/2555", "2555" },
                    { 8L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com56.pdf", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2556", "คำสั่งสำนักนายกรัฐมนตรี ที่ 302/2555", "2556" },
                    { 7L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2557", "คำสั่งสำนักนายกรัฐมนตรี ที่ 328/2556 ", "2557" },
                    { 11L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com53.pdf", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2553", "คำสั่งสำนักนายกรัฐมนตรี ที่ 8/2553", "2553" },
                    { 5L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com59.pdf", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2559", "คำสั่งสำนักนายกรัฐมนตรี ที่ 396/2558", "2559" },
                    { 4L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com60.pdf", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2560", "คำสั่งสำนักนายกรัฐมนตรี ที่ 264/2559", "2560" },
                    { 3L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com61.pdf", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2561", "คำสั่งสำนักนายกรัฐมนตรี ที่ 15/2561", "2561" },
                    { 2L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com62.pdf", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2562", "คำสั่งสำนักนายกรัฐมนตรี ที่ 343/2561", "2562" },
                    { 1L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com63.pdf", "การตรวจราชการประจำปีงบประมาณ พ.ศ. 2563", "คำสั่งสำนักนายกรัฐมนตรี ที่ 361/2562", "2563" },
                    { 6L, "ผู้ตรวจราชการสำนักนายกรัฐมนตรี", null, "insp_com58.pdf", "คำสั่งการตรวจราชการประจำปีงบประมาณ พ.ศ.2558", "คำสั่งสำนักนายกรัฐมนตรี ที่ 256/2557", "2558" }
                });

            migrationBuilder.InsertData(
                table: "InstructionOrders",
                columns: new[] { "Id", "CreateBy", "CreatedAt", "Detail", "File", "Name", "Order", "Year" },
                values: new object[,]
                {
                    { 12L, "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", null, "¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã(à¢µÊèÇ¹¡ÅÒ§,18),¾Ñ¹µÓÃÇ¨â· à¸ÕÂÃÃÑµ¹ì ÇÔàªÕÂÃÊÃÃ¤ì(à¢µ1,2),¹ÒÂä¾âÃ¨¹ì ÍÒ¨ÃÑ¡ÉÒ(à¢µ3,4),¹ÒÂÇÃ¾Ñ¹¸ì àÂç¹·ÃÑ¾Âì(à¢µ5,9),¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ(à¢µ6),¹Ò§»ÃÐÀÒÈÃÕ ºØ­ÇÔàÈÉ(à¢µ7,15),¾.µ.·.¾§Èì¾Ã ¾ÃÒÁ³ìàÊ¹èËì(à¢µ8),¹ÒÂÍÔÊÃÐ ÈÔÃÔÇÃÀÒ(à¢µ10),¹Ò§ÍÑ¨¨ÔÁÒ ¨Ñ¹·ÃìÊØÇÒ¹ÔªÂì(à¢µ11,14),¹Ò§ÀÑ·ÃÀÃ °ÔµÔÂÒÀÃ³ì(à¢µ12,17),¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì(à¢µ13),¹ÒÂÊØÃÈÑ¡´Ôì àÃÕÂ§à¤Ã×Í(à¢µ16)", "", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", " ¤ÓÊÑè§ ¹Ã. ·Õè 215/2560 Å§ÇÑ¹·Õè 6 ¡Ñ¹ÂÒÂ¹ 2560", "2560" },
                    { 11L, "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", null, "¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã(à¢µÊèÇ¹¡ÅÒ§,3,9,14),¾Ñ¹µÓÃÇ¨â· à¸ÕÂÃÃÑµ¹ì ÇÔàªÕÂÃÊÃÃ¤ì(à¢µ1,2,5),¹ÒÂâª¤ªÑÂ à´ªÍÁÃ¸Ñ­(à¢µ4),¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ(à¢µ6, 7, 18),¹ÒÂ¾ÈÔ¹ â¡ÁÅÇÔª­ì(à¢µ8),¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì(à¢µ10, 11, 13),¹ÒÂÈÑ¡´Ôì ÊÁºØ­âµ(à¢µ12),¹ÒÂÊØÃÈÑ¡´Ôì àÃÕÂ§à¤Ã×Í(à¢µ15, 16, 17)", "", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", " ¤ÓÊÑè§ ¹Ã. ·Õè 306/2560 Å§ÇÑ¹·Õè 20 ¾ÄÈ¨Ô¡ÒÂ¹ 2560", "2560" },
                    { 10L, "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", null, "¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã(à¢µÊèÇ¹¡ÅÒ§,3,9,14),¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ(à¢µ1,6,7),¹ÒÂÊØÃÈÑ¡´Ôì àÃÕÂ§à¤Ã×Í(à¢µ2,15,16),¹ÒÂâª¤ªÑÂ à´ªÍÁÃ¸Ñ­(à¢µ4),¾Ñ¹µÓÃÇ¨â· à¸ÕÂÃÃÑµ¹ì ÇÔàªÕÂÃÊÃÃ¤ì(à¢µ5, 17, 18),¹ÒÂ¾ÈÔ¹ â¡ÁÅÇÔª­ì(à¢µ8),¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì(à¢µ10, 11, 13),¹ÒÂÈÑ¡´Ôì ÊÁºØ­âµ(à¢µ12)", "", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", " ¤ÓÊÑè§ ¹Ã. ·Õè 336/2560 Å§ÇÑ¹·Õè 13 ¸Ñ¹ÇÒ¤Á 2560", "2560" },
                    { 9L, "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", null, "¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã(à¢µÊèÇ¹¡ÅÒ§,3,9,14),¾Ñ¹µÓÃÇ¨â· à¸ÕÂÃÃÑµ¹ì ÇÔàªÕÂÃÊÃÃ¤ì(à¢µ1,2,5),¹ÒÂâª¤ªÑÂ à´ªÍÁÃ¸Ñ­(à¢µ4),¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ(à¢µ6,7),¹ÒÂ¾ÈÔ¹ â¡ÁÅÇÔª­ì(à¢µ8),¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì(à¢µ10,11),¹ÒÂÈÑ¡´Ôì ÊÁºØ­âµ(à¢µ12), ¹ÒÂ¡ÃÕ±Ò Ê¾âª¤(à¢µ13), ¹ÒÂÊØÃÈÑ¡´Ôì àÃÕÂ§à¤Ã×Í(à¢µ15,16,17,18)", "", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", "¤ÓÊÑè§ ¹Ã. ·Õè 76/2561 Å§ÇÑ¹·Õè 2 àÁÉÒÂ¹ 2561", "2561" },
                    { 8L, "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", null, "¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã(à¢µÊèÇ¹¡ÅÒ§,3,9,14),¾Ñ¹µÓÃÇ¨â· à¸ÕÂÃÃÑµ¹ì ÇÔàªÕÂÃÊÃÃ¤ì(à¢µ1,5),¹ÒÂ¹ÃÔÊªÑÂ »éÍÁàÊ×Í(à¢µ2),¹ÒÂâª¤ªÑÂ à´ªÍÁÃ¸Ñ­(à¢µ4),¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ(à¢µ6,7),¹ÒÂ¾ÈÔ¹ â¡ÁÅÇÔª­ì(à¢µ8),¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì(à¢µ10,11),¹ÒÂÈÑ¡´Ôì ÊÁºØ­âµ(à¢µ12), ¹ÒÂ¡ÃÕ±Ò Ê¾âª¤(à¢µ13), ¹ÒÂÊØÃÈÑ¡´Ôì àÃÕÂ§à¤Ã×Í(à¢µ15,16,17,18)", "", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", "¤ÓÊÑè§ ¹Ã. ·Õè 76/2561 Å§ÇÑ¹·Õè 2 àÁÉÒÂ¹ 2561", "2561" },
                    { 7L, "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", null, "¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã(à¢µÊèÇ¹¡ÅÒ§,3,9,14),¾Ñ¹µÓÃÇ¨â· à¸ÕÂÃÃÑµ¹ì ÇÔàªÕÂÃÊÃÃ¤ì(à¢µ1,5),¹ÒÂ¹ÃÔÊªÑÂ »éÍÁàÊ×Í(à¢µ2),¹ÒÂâª¤ªÑÂ à´ªÍÁÃ¸Ñ­(à¢µ4),¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ(à¢µ6,7),¹ÒÂ³Ã§¤ì àª×éÍºØ­ªèÇÂ(à¢µ8),¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì(à¢µ10,11),¹ÒÂÈÑ¡´Ôì ÊÁºØ­âµ(à¢µ12), ¹ÒÂ¡ÃÕ±Ò Ê¾âª¤(à¢µ13), ¹ÒÂÊØÃÈÑ¡´Ôì àÃÕÂ§à¤Ã×Í(à¢µ15,16,17,18)", "", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", "¤ÓÊÑè§ ¹Ã. ·Õè 100/2561 Å§ÇÑ¹·Õè 27 àÁÉÒÂ¹ 2561", "2561" },
                    { 4L, "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", null, "¾ÅàÍ¡ ªÑÂÇÑ²¹ì â¦ÊÔµÒÀÒ (à¢µÊèÇ¹¡ÅÒ§), ¹Ò§ÊÒÇÊØÃØè§ÅÑ¡É³ì àÁ¦ÐÍÓ¹ÇÂªÑÂ (à¢µ1,12), ¹Ò§ÊØÁÔµÃÒ ÍµÔÈÑ¾·ì (à¢µ2,14.17), ¹ÒÂ³Ã§¤ì àª×éÍºØ­ªèÇÂ (à¢µ3,9), ¹ÒÂ¾ÕÃÐ ·Í§â¾¸Ôì (à¢µ4,18), ¹ÒÂ¸ÊÃ³ìÍÑ±²ì ¸¹Ô·¸Ô¾Ñ¹¸ì (à¢µ5,7,10), ¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ (à¢µ6,16), ¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã (à¢µ8,13), ¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì (à¢µ11,15)", "", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", "¤ÓÊÑè§ Ê»¹. ·Õè 262/2561 Å§ÇÑ¹·Õè 16 µØÅÒ¤Á 2561", "2561" },
                    { 5L, "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", null, "¾Ñ¹µÓÃÇ¨â· à¸ÕÂÃÃÑµ¹ì ÇÔàªÕÂÃÊÃÃ¤ì(à¢µÊèÇ¹¡ÅÒ§,2,3),¹ÒÂ³Ã§¤ì àª×éÍºØ­ªèÇÂ(à¢µ1,4,7),¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ(à¢µ5,6),¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã(à¢µ8,9),¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì(à¢µ10,11),¹ÒÂ¡ÃÕ±Ò Ê¾âª¤(à¢µ13,14),¹Ò§´Ç§µÒ µÑ¹âª(à¢µ15,16),¹ÒÂ¾ÕÃÐ ·Í§â¾¸Ôì(à¢µ15,16,17,18)", "", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", "¤ÓÊÑè§ ¹Ã. ·Õè 255/2561 Å§ÇÑ¹·Õè 5 µØÅÒ¤Á 2561", "2561" },
                    { 3L, "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", null, "¾ÅàÍ¡ ªÑÂÇÑ²¹ì â¦ÊÔµÒÀÒ (à¢µÊèÇ¹¡ÅÒ§), ¹Ò§ÊÒÇÊØÃØè§ÅÑ¡É³ì àÁ¦ÐÍÓ¹ÇÂªÑÂ (à¢µ1,12), ¹Ò§ÊØÁÔµÃÒ ÍµÔÈÑ¾·ì (à¢µ2,14.17), ¹ÒÂ³Ã§¤ì àª×éÍºØ­ªèÇÂ (à¢µ3,9), ¹ÒÂ¾ÕÃÐ ·Í§â¾¸Ôì (à¢µ4,18), ¹ÒÂ¸ÊÃ³ìÍÑ±²ì ¸¹Ô·¸Ô¾Ñ¹¸ì (à¢µ5,7,10), ¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ (à¢µ6,16), ¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã (à¢µ8,13), ¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì (à¢µ11,15)", "", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", "¤ÓÊÑè§ ¹Ã. ·Õè 5/2562 Å§ÇÑ¹·Õè 7 Á¡ÃÒ¤Á 2562", "2562" },
                    { 2L, "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", null, "¾ÅàÍ¡ ªÑÂÇÑ²¹ì â¦ÊÔµÒÀÒ (à¢µÊèÇ¹¡ÅÒ§), ¹Ò§ÊÒÇÊØÃØè§ÅÑ¡É³ì àÁ¦ÐÍÓ¹ÇÂªÑÂ (à¢µ1,12), ¹Ò§ÊØÁÔµÃÒ ÍµÔÈÑ¾·ì (à¢µ2,14.17), ¹ÒÂ³Ã§¤ì àª×éÍºØ­ªèÇÂ (à¢µ3,9), ¹ÒÂ¾ÕÃÐ ·Í§â¾¸Ôì (à¢µ4,18), ¹ÒÂ¸ÊÃ³ìÍÑ±²ì ¸¹Ô·¸Ô¾Ñ¹¸ì (à¢µ5,7,10), ¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ (à¢µ6,16), ¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã (à¢µ8,13), ¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì (à¢µ11,15)", "", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", "¤ÓÊÑè§ ¹Ã. ·Õè 102/2562 Å§ÇÑ¹·Õè 10 ¾ÄÉÀÒ¤Á 2562", "2562" },
                    { 1L, "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", null, "¾ÅàÍ¡ ªÑÂÇÑ²¹ì â¦ÊÔµÒÀÒ (à¢µÊèÇ¹¡ÅÒ§), ¹Ò§ÊÒÇÊØÃØè§ÅÑ¡É³ì àÁ¦ÐÍÓ¹ÇÂªÑÂ (à¢µ1,12), ¹Ò§ÊØÁÔµÃÒ ÍµÔÈÑ¾·ì (à¢µ2,14.17), ¹ÒÂ³Ã§¤ì àª×éÍºØ­ªèÇÂ (à¢µ3,9), ¹ÒÂ¾ÕÃÐ ·Í§â¾¸Ôì (à¢µ4,18), ¹ÒÂ¸ÊÃ³ìÍÑ±²ì ¸¹Ô·¸Ô¾Ñ¹¸ì (à¢µ5,7,10), ¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ (à¢µ6,16), ¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã (à¢µ8,13), ¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì (à¢µ11,15)", "", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", "¤ÓÊÑè§ ¹Ã ·Õè 313/2562 Å§ÇÑ¹·Õè 28 µØÅÒ¤Á 2562", "2562" },
                    { 13L, "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", null, "¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã(à¢µÊèÇ¹¡ÅÒ§,18),¾Ñ¹µÓÃÇ¨â· à¸ÕÂÃÃÑµ¹ì ÇÔàªÕÂÃÊÃÃ¤ì(à¢µ1,2),¹ÒÂä¾âÃ¨¹ì ÍÒ¨ÃÑ¡ÉÒ(à¢µ3,4),¹ÒÂÇÃ¾Ñ¹¸ì àÂç¹·ÃÑ¾Âì(à¢µ5,9),¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ(à¢µ6),¹Ò§»ÃÐÀÒÈÃÕ ºØ­ÇÔàÈÉ(à¢µ7,15),¹ÒÂÊØÃÈÑ¡´Ôì àÃÕÂ§à¤Ã×Í(à¢µ8,16),¹ÒÂÍÔÊÃÐ ÈÔÃÔÇÃÀÒ(à¢µ10),¹Ò§ÍÑ¨¨ÔÁÒ ¨Ñ¹·ÃìÊØÇÒ¹ÔªÂì(à¢µ11,14),¹Ò§ÀÑ·ÃÀÃ °ÔµÔÂÒÀÃ³ì(à¢µ12,17),¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì(à¢µ13)", "", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", " ¤ÓÊÑè§ ¹Ã. ·Õè 100/2560 Å§ÇÑ¹·Õè 28 ÁÕ¹Ò¤Á 2560", "2560" },
                    { 6L, "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", null, "¾Ñ¹µÓÃÇ¨â· à¸ÕÂÃÃÑµ¹ì ÇÔàªÕÂÃÊÃÃ¤ì(à¢µÊèÇ¹¡ÅÒ§,2,3),¹ÒÂ³Ã§¤ì àª×éÍºØ­ªèÇÂ(à¢µ1,4,7),¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ(à¢µ5,6),¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã(à¢µ8,9),¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì(à¢µ10,11),¹ÒÂ¡ÃÕ±Ò Ê¾âª¤(à¢µ13,14), ¹ÒÂ¾ÕÃÐ ·Í§â¾¸Ôì(à¢µ15,16,17,18)", "", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", "¤ÓÊÑè§ ¹Ã. ·Õè 206/2561 Å§ÇÑ¹·Õè 24 ÊÔ§ËÒ¤Á 2561", "2561" },
                    { 14L, "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", null, "¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã(à¢µÊèÇ¹¡ÅÒ§,6,18),¾Ñ¹µÓÃÇ¨â· à¸ÕÂÃÃÑµ¹ì ÇÔàªÕÂÃÊÃÃ¤ì(à¢µ1,2),¹ÒÂä¾âÃ¨¹ì ÍÒ¨ÃÑ¡ÉÒ(à¢µ3,4),¹ÒÂÇÃ¾Ñ¹¸ì àÂç¹·ÃÑ¾Âì(à¢µ5,9),¹Ò§»ÃÐÀÒÈÃÕ ºØ­ÇÔàÈÉ(à¢µ7, 15),¹ÒÂÊØÃÈÑ¡´Ôì àÃÕÂ§à¤Ã×Í(à¢µ8, 16),¹ÒÂÍÔÊÃÐ ÈÔÃÔÇÃÀÒ(à¢µ10, 13),¹Ò§ÍÑ¨¨ÔÁÒ ¨Ñ¹·ÃìÊØÇÒ¹ÔªÂì(à¢µ11, 14),¹Ò§ÀÑ·ÃÀÃ °ÔµÔÂÒÀÃ³ì(à¢µ12, 17)", "", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", " ¤ÓÊÑè§ ¹Ã.·Õè 3 / 2560 Å§ÇÑ¹·Õè 5 Á¡ÃÒ¤Á 2560", "2560" }
                });

            migrationBuilder.InsertData(
                table: "Subdistricts",
                columns: new[] { "Id", "DistrictId", "Name" },
                values: new object[,]
                {
                    { 1L, 1L, "พระบรมมหาราชวัง" },
                    { 88L, 22L, "บางหว้า" },
                    { 87L, 21L, "แสมดำ" },
                    { 86L, 21L, "ท่าข้าม" },
                    { 85L, 20L, "อรุณอมรินทร์" },
                    { 84L, 20L, "บางขุนศรี" },
                    { 83L, 20L, "บางขุนนนท์" },
                    { 82L, 20L, "บ้านช่างหล่อ" },
                    { 81L, 20L, "ศิริราช" },
                    { 80L, 19L, "บางเชือกหนัง" },
                    { 79L, 19L, "บางระมาด" },
                    { 78L, 19L, "บางพรม" },
                    { 77L, 19L, "ฉิมพลี" },
                    { 89L, 22L, "บางด้วน" },
                    { 76L, 19L, "ตลิ่งชัน" },
                    { 74L, 18L, "คลองต้นไทร" },
                    { 73L, 18L, "บางลำภูล่าง" },
                    { 72L, 18L, "คลองสาน" },
                    { 71L, 18L, "สมเด็จเจ้าพระยา" },
                    { 70L, 17L, "สามเสนนอก" },
                    { 69L, 17L, "บางกะปิ" },
                    { 68L, 17L, "ห้วยขวาง" },
                    { 67L, 16L, "วัดท่าพระ" },
                    { 66L, 16L, "วัดอรุณ" },
                    { 65L, 15L, "สำเหร่" },
                    { 64L, 15L, "ดาวคะนอง" },
                    { 63L, 15L, "ตลาดพลู" },
                    { 75L, 19L, "คลองชักพระ" },
                    { 62L, 15L, "บุคคโล" },
                    { 90L, 22L, "บางจาก" },
                    { 92L, 22L, "คลองขวาง" },
                    { 118L, 31L, "วัดพระยาไกร" },
                    { 117L, 31L, "บางคอแหลม" },
                    { 116L, 30L, "จตุจักร" },
                    { 115L, 30L, "จอมพล" },
                    { 114L, 30L, "จันทรเกษม" },
                    { 113L, 30L, "เสนานิคม" },
                    { 112L, 30L, "ลาดยาว" },
                    { 111L, 29L, "วงศ์สว่าง" },
                    { 110L, 29L, "บางซื่อ" },
                    { 109L, 28L, "ทุ่งมหาเมฆ" },
                    { 108L, 28L, "ยานนาวา" },
                    { 107L, 28L, "ทุ่งวัดดอน" },
                    { 91L, 22L, "บางแวก" },
                    { 106L, 27L, "นวลจันทร์" },
                    { 104L, 27L, "คลองกุ่ม" },
                    { 103L, 26L, "ดินแดง" },
                    { 102L, 25L, "บางยี่ขัน" },
                    { 101L, 25L, "บางอ้อ" },
                    { 100L, 25L, "บางอ้อ" },
                    { 99L, 25L, "บางพลัด" },
                    { 98L, 24L, "บางปะกอก" },
                    { 97L, 24L, "ราษฎร์บูรณะ" },
                    { 96L, 23L, "หนองค้างพลู" },
                    { 95L, 23L, "หนองแขม" },
                    { 94L, 22L, "คูหาสวรรค์" },
                    { 93L, 22L, "ปากคลองภาษีเจริญ" },
                    { 105L, 27L, "นวมินทร์" },
                    { 61L, 15L, "บางยี่เรือ" },
                    { 60L, 15L, "หิรัญรูจี" },
                    { 59L, 15L, "วัดกัลยาณ์" },
                    { 27L, 4L, "สีลม" },
                    { 26L, 4L, "มหาพฤฒาราม" },
                    { 25L, 3L, "ลำต้อยติ่ง" },
                    { 24L, 3L, "ลำผักชี" },
                    { 23L, 3L, "คู้ฝั่งเหนือ" },
                    { 22L, 3L, "โคกแฝด" },
                    { 21L, 3L, "คลองสิบสอง" },
                    { 20L, 3L, "คลองสิบ" },
                    { 19L, 3L, "หนองจอก" },
                    { 18L, 3L, "กระทุ่มราย" },
                    { 17L, 2L, "ถนนนครไชยศรี" },
                    { 16L, 2L, "สี่แยกมหานาค" },
                    { 28L, 4L, "สุริยวงศ์" },
                    { 15L, 2L, "สวนจิตรลดา" },
                    { 13L, 2L, "ดุสิต" },
                    { 12L, 1L, "วัดสามพระยา" },
                    { 11L, 1L, "บางขุนพรหม" },
                    { 10L, 1L, "บ้านพานถม" },
                    { 9L, 1L, "ชนะสงคราม" },
                    { 8L, 1L, "ตลาดยอด" },
                    { 7L, 1L, "บวรนิเวศ" },
                    { 6L, 1L, "เสาชิงช้า" },
                    { 5L, 1L, "ศาลเจ้าพ่อเสือ" },
                    { 4L, 1L, "สำราญราษฎร์" },
                    { 3L, 1L, "วัดราชบพิธ" },
                    { 2L, 1L, "วังบูรพาภิรมย์" },
                    { 14L, 2L, "วชิรพยาบาล" },
                    { 29L, 4L, "บางรัก" },
                    { 30L, 4L, "สี่พระยา" },
                    { 31L, 5L, "อนุสาวรีย์" },
                    { 58L, 14L, "สามเสนใน" },
                    { 57L, 13L, "ตลาดน้อย" },
                    { 56L, 13L, "สัมพันธวงศ์" },
                    { 55L, 13L, "จักรวรรดิ" },
                    { 54L, 12L, "บางโพงพาง" },
                    { 53L, 12L, "ช่องนนทรี" },
                    { 52L, 11L, "ขุมทอง" },
                    { 51L, 11L, "ทับยาว" },
                    { 50L, 11L, "ลำปลาทิว" },
                    { 49L, 11L, "คลองสามประเวศ" },
                    { 48L, 11L, "คลองสองต้นนุ่น" },
                    { 47L, 11L, "ลาดกระบัง" },
                    { 46L, 10L, "แสนแสบ" },
                    { 45L, 10L, "มีนบุรี" },
                    { 44L, 9L, "บางจาก" },
                    { 43L, 8L, "วัดโสมนัส" },
                    { 42L, 8L, "บ้านบาตร" },
                    { 41L, 8L, "คลองมหานาค" },
                    { 40L, 8L, "วัดเทพศิรินทร์" },
                    { 39L, 8L, "ป้อมปราบ" },
                    { 38L, 7L, "ลุมพินี" },
                    { 37L, 7L, "ปทุมวัน" },
                    { 36L, 7L, "วังใหม่" },
                    { 35L, 7L, "รองเมือง" },
                    { 34L, 6L, "หัวหมาก" },
                    { 33L, 6L, "คลองจั่น" },
                    { 32L, 5L, "ท่าแร้ง" },
                    { 119L, 31L, "บางโคล่" },
                    { 120L, 32L, "ประเวศ" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicies_FiscalYearId",
                table: "CentralPolicies",
                column: "FiscalYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralPolicies_FiscalYear_FiscalYearId",
                table: "CentralPolicies",
                column: "FiscalYearId",
                principalTable: "FiscalYear",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subdistricts_Districts_DistrictId",
                table: "Subdistricts",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Village_Subdistricts_SubdistrictId",
                table: "Village",
                column: "SubdistrictId",
                principalTable: "Subdistricts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CentralPolicies_FiscalYear_FiscalYearId",
                table: "CentralPolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_Subdistricts_Districts_DistrictId",
                table: "Subdistricts");

            migrationBuilder.DropForeignKey(
                name: "FK_Village_Subdistricts_SubdistrictId",
                table: "Village");

            migrationBuilder.DropTable(
                name: "Governmentinspectionplans");

            migrationBuilder.DropTable(
                name: "InspectionOrders");

            migrationBuilder.DropTable(
                name: "InstructionOrders");

            migrationBuilder.DropIndex(
                name: "IX_CentralPolicies_FiscalYearId",
                table: "CentralPolicies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subdistricts",
                table: "Subdistricts");

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 47L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 50L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 47L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 50L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 51L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 52L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 53L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 54L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 55L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 56L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 57L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 58L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 59L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 60L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 61L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 62L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 63L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 64L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 65L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 66L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 67L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 68L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 69L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 70L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 71L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 72L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 73L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 74L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 75L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 76L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 77L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 78L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 79L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 80L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 81L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 82L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 83L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 84L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 85L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 86L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 87L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 88L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 89L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 90L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 91L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 92L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 93L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 94L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 95L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 96L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 97L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 98L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 99L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 100L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 101L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 102L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 103L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 104L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 105L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 106L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 107L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 108L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 109L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 110L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 111L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 112L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 113L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 114L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 115L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 116L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 117L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 118L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 119L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 120L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DropColumn(
                name: "FiscalYearId",
                table: "CentralPolicies");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CentralPolicies");

            migrationBuilder.RenameTable(
                name: "Subdistricts",
                newName: "Subdistrict");

            migrationBuilder.RenameIndex(
                name: "IX_Subdistricts_DistrictId",
                table: "Subdistrict",
                newName: "IX_Subdistrict_DistrictId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subdistrict",
                table: "Subdistrict",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subdistrict_Districts_DistrictId",
                table: "Subdistrict",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Village_Subdistrict_SubdistrictId",
                table: "Village",
                column: "SubdistrictId",
                principalTable: "Subdistrict",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
