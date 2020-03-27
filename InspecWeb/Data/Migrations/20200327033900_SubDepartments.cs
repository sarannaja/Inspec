using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class SubDepartments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProvinces_AspNetUsers_UserID",
                table: "UserProvinces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProvinces",
                table: "UserProvinces");

            migrationBuilder.DropIndex(
                name: "IX_UserProvinces_UserID",
                table: "UserProvinces");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "UserProvinces",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProvinces",
                table: "UserProvinces",
                columns: new[] { "UserID", "ProvinceId" });

            migrationBuilder.CreateTable(
                name: "CentralDepartments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CentralDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProvincialDepartments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvincialDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProvincialDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CentralDepartmentProvince",
                columns: table => new
                {
                    CentralDepartmentID = table.Column<long>(nullable: false),
                    ProvinceId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralDepartmentProvince", x => new { x.CentralDepartmentID, x.ProvinceId });
                    table.ForeignKey(
                        name: "FK_CentralDepartmentProvince_CentralDepartments_CentralDepartmentID",
                        column: x => x.CentralDepartmentID,
                        principalTable: "CentralDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CentralDepartmentProvince_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProvincialDepartmentProvince",
                columns: table => new
                {
                    ProvincialDepartmentID = table.Column<long>(nullable: false),
                    ProvinceId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvincialDepartmentProvince", x => new { x.ProvincialDepartmentID, x.ProvinceId });
                    table.ForeignKey(
                        name: "FK_ProvincialDepartmentProvince_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProvincialDepartmentProvince_ProvincialDepartments_ProvincialDepartmentID",
                        column: x => x.ProvincialDepartmentID,
                        principalTable: "ProvincialDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", "¾ÅàÍ¡ ªÑÂÇÑ²¹ì â¦ÊÔµÒÀÒ (à¢µÊèÇ¹¡ÅÒ§), ¹Ò§ÊÒÇÊØÃØè§ÅÑ¡É³ì àÁ¦ÐÍÓ¹ÇÂªÑÂ (à¢µ1,12), ¹Ò§ÊØÁÔµÃÒ ÍµÔÈÑ¾·ì (à¢µ2,14.17), ¹ÒÂ³Ã§¤ì àª×éÍºØ­ªèÇÂ (à¢µ3,9), ¹ÒÂ¾ÕÃÐ ·Í§â¾¸Ôì (à¢µ4,18), ¹ÒÂ¸ÊÃ³ìÍÑ±²ì ¸¹Ô·¸Ô¾Ñ¹¸ì (à¢µ5,7,10), ¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ (à¢µ6,16), ¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã (à¢µ8,13), ¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì (à¢µ11,15)", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", "¤ÓÊÑè§ ¹Ã ·Õè 313/2562 Å§ÇÑ¹·Õè 28 µØÅÒ¤Á 2562" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", "¾ÅàÍ¡ ªÑÂÇÑ²¹ì â¦ÊÔµÒÀÒ (à¢µÊèÇ¹¡ÅÒ§), ¹Ò§ÊÒÇÊØÃØè§ÅÑ¡É³ì àÁ¦ÐÍÓ¹ÇÂªÑÂ (à¢µ1,12), ¹Ò§ÊØÁÔµÃÒ ÍµÔÈÑ¾·ì (à¢µ2,14.17), ¹ÒÂ³Ã§¤ì àª×éÍºØ­ªèÇÂ (à¢µ3,9), ¹ÒÂ¾ÕÃÐ ·Í§â¾¸Ôì (à¢µ4,18), ¹ÒÂ¸ÊÃ³ìÍÑ±²ì ¸¹Ô·¸Ô¾Ñ¹¸ì (à¢µ5,7,10), ¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ (à¢µ6,16), ¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã (à¢µ8,13), ¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì (à¢µ11,15)", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", "¤ÓÊÑè§ ¹Ã. ·Õè 102/2562 Å§ÇÑ¹·Õè 10 ¾ÄÉÀÒ¤Á 2562" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", "¾ÅàÍ¡ ªÑÂÇÑ²¹ì â¦ÊÔµÒÀÒ (à¢µÊèÇ¹¡ÅÒ§), ¹Ò§ÊÒÇÊØÃØè§ÅÑ¡É³ì àÁ¦ÐÍÓ¹ÇÂªÑÂ (à¢µ1,12), ¹Ò§ÊØÁÔµÃÒ ÍµÔÈÑ¾·ì (à¢µ2,14.17), ¹ÒÂ³Ã§¤ì àª×éÍºØ­ªèÇÂ (à¢µ3,9), ¹ÒÂ¾ÕÃÐ ·Í§â¾¸Ôì (à¢µ4,18), ¹ÒÂ¸ÊÃ³ìÍÑ±²ì ¸¹Ô·¸Ô¾Ñ¹¸ì (à¢µ5,7,10), ¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ (à¢µ6,16), ¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã (à¢µ8,13), ¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì (à¢µ11,15)", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", "¤ÓÊÑè§ ¹Ã. ·Õè 5/2562 Å§ÇÑ¹·Õè 7 Á¡ÃÒ¤Á 2562" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", "¾ÅàÍ¡ ªÑÂÇÑ²¹ì â¦ÊÔµÒÀÒ (à¢µÊèÇ¹¡ÅÒ§), ¹Ò§ÊÒÇÊØÃØè§ÅÑ¡É³ì àÁ¦ÐÍÓ¹ÇÂªÑÂ (à¢µ1,12), ¹Ò§ÊØÁÔµÃÒ ÍµÔÈÑ¾·ì (à¢µ2,14.17), ¹ÒÂ³Ã§¤ì àª×éÍºØ­ªèÇÂ (à¢µ3,9), ¹ÒÂ¾ÕÃÐ ·Í§â¾¸Ôì (à¢µ4,18), ¹ÒÂ¸ÊÃ³ìÍÑ±²ì ¸¹Ô·¸Ô¾Ñ¹¸ì (à¢µ5,7,10), ¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ (à¢µ6,16), ¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã (à¢µ8,13), ¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì (à¢µ11,15)", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", "¤ÓÊÑè§ Ê»¹. ·Õè 262/2561 Å§ÇÑ¹·Õè 16 µØÅÒ¤Á 2561" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", "¾Ñ¹µÓÃÇ¨â· à¸ÕÂÃÃÑµ¹ì ÇÔàªÕÂÃÊÃÃ¤ì(à¢µÊèÇ¹¡ÅÒ§,2,3),¹ÒÂ³Ã§¤ì àª×éÍºØ­ªèÇÂ(à¢µ1,4,7),¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ(à¢µ5,6),¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã(à¢µ8,9),¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì(à¢µ10,11),¹ÒÂ¡ÃÕ±Ò Ê¾âª¤(à¢µ13,14),¹Ò§´Ç§µÒ µÑ¹âª(à¢µ15,16),¹ÒÂ¾ÕÃÐ ·Í§â¾¸Ôì(à¢µ15,16,17,18)", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", "¤ÓÊÑè§ ¹Ã. ·Õè 255/2561 Å§ÇÑ¹·Õè 5 µØÅÒ¤Á 2561" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", "¾Ñ¹µÓÃÇ¨â· à¸ÕÂÃÃÑµ¹ì ÇÔàªÕÂÃÊÃÃ¤ì(à¢µÊèÇ¹¡ÅÒ§,2,3),¹ÒÂ³Ã§¤ì àª×éÍºØ­ªèÇÂ(à¢µ1,4,7),¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ(à¢µ5,6),¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã(à¢µ8,9),¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì(à¢µ10,11),¹ÒÂ¡ÃÕ±Ò Ê¾âª¤(à¢µ13,14), ¹ÒÂ¾ÕÃÐ ·Í§â¾¸Ôì(à¢µ15,16,17,18)", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", "¤ÓÊÑè§ ¹Ã. ·Õè 206/2561 Å§ÇÑ¹·Õè 24 ÊÔ§ËÒ¤Á 2561" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", "¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã(à¢µÊèÇ¹¡ÅÒ§,3,9,14),¾Ñ¹µÓÃÇ¨â· à¸ÕÂÃÃÑµ¹ì ÇÔàªÕÂÃÊÃÃ¤ì(à¢µ1,5),¹ÒÂ¹ÃÔÊªÑÂ »éÍÁàÊ×Í(à¢µ2),¹ÒÂâª¤ªÑÂ à´ªÍÁÃ¸Ñ­(à¢µ4),¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ(à¢µ6,7),¹ÒÂ³Ã§¤ì àª×éÍºØ­ªèÇÂ(à¢µ8),¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì(à¢µ10,11),¹ÒÂÈÑ¡´Ôì ÊÁºØ­âµ(à¢µ12), ¹ÒÂ¡ÃÕ±Ò Ê¾âª¤(à¢µ13), ¹ÒÂÊØÃÈÑ¡´Ôì àÃÕÂ§à¤Ã×Í(à¢µ15,16,17,18)", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", "¤ÓÊÑè§ ¹Ã. ·Õè 100/2561 Å§ÇÑ¹·Õè 27 àÁÉÒÂ¹ 2561" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", "¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã(à¢µÊèÇ¹¡ÅÒ§,3,9,14),¾Ñ¹µÓÃÇ¨â· à¸ÕÂÃÃÑµ¹ì ÇÔàªÕÂÃÊÃÃ¤ì(à¢µ1,5),¹ÒÂ¹ÃÔÊªÑÂ »éÍÁàÊ×Í(à¢µ2),¹ÒÂâª¤ªÑÂ à´ªÍÁÃ¸Ñ­(à¢µ4),¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ(à¢µ6,7),¹ÒÂ¾ÈÔ¹ â¡ÁÅÇÔª­ì(à¢µ8),¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì(à¢µ10,11),¹ÒÂÈÑ¡´Ôì ÊÁºØ­âµ(à¢µ12), ¹ÒÂ¡ÃÕ±Ò Ê¾âª¤(à¢µ13), ¹ÒÂÊØÃÈÑ¡´Ôì àÃÕÂ§à¤Ã×Í(à¢µ15,16,17,18)", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", "¤ÓÊÑè§ ¹Ã. ·Õè 76/2561 Å§ÇÑ¹·Õè 2 àÁÉÒÂ¹ 2561" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", "¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã(à¢µÊèÇ¹¡ÅÒ§,3,9,14),¾Ñ¹µÓÃÇ¨â· à¸ÕÂÃÃÑµ¹ì ÇÔàªÕÂÃÊÃÃ¤ì(à¢µ1,2,5),¹ÒÂâª¤ªÑÂ à´ªÍÁÃ¸Ñ­(à¢µ4),¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ(à¢µ6,7),¹ÒÂ¾ÈÔ¹ â¡ÁÅÇÔª­ì(à¢µ8),¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì(à¢µ10,11),¹ÒÂÈÑ¡´Ôì ÊÁºØ­âµ(à¢µ12), ¹ÒÂ¡ÃÕ±Ò Ê¾âª¤(à¢µ13), ¹ÒÂÊØÃÈÑ¡´Ôì àÃÕÂ§à¤Ã×Í(à¢µ15,16,17,18)", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", "¤ÓÊÑè§ ¹Ã. ·Õè 76/2561 Å§ÇÑ¹·Õè 2 àÁÉÒÂ¹ 2561" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", "¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã(à¢µÊèÇ¹¡ÅÒ§,3,9,14),¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ(à¢µ1,6,7),¹ÒÂÊØÃÈÑ¡´Ôì àÃÕÂ§à¤Ã×Í(à¢µ2,15,16),¹ÒÂâª¤ªÑÂ à´ªÍÁÃ¸Ñ­(à¢µ4),¾Ñ¹µÓÃÇ¨â· à¸ÕÂÃÃÑµ¹ì ÇÔàªÕÂÃÊÃÃ¤ì(à¢µ5, 17, 18),¹ÒÂ¾ÈÔ¹ â¡ÁÅÇÔª­ì(à¢µ8),¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì(à¢µ10, 11, 13),¹ÒÂÈÑ¡´Ôì ÊÁºØ­âµ(à¢µ12)", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", " ¤ÓÊÑè§ ¹Ã. ·Õè 336/2560 Å§ÇÑ¹·Õè 13 ¸Ñ¹ÇÒ¤Á 2560" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", "¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã(à¢µÊèÇ¹¡ÅÒ§,3,9,14),¾Ñ¹µÓÃÇ¨â· à¸ÕÂÃÃÑµ¹ì ÇÔàªÕÂÃÊÃÃ¤ì(à¢µ1,2,5),¹ÒÂâª¤ªÑÂ à´ªÍÁÃ¸Ñ­(à¢µ4),¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ(à¢µ6, 7, 18),¹ÒÂ¾ÈÔ¹ â¡ÁÅÇÔª­ì(à¢µ8),¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì(à¢µ10, 11, 13),¹ÒÂÈÑ¡´Ôì ÊÁºØ­âµ(à¢µ12),¹ÒÂÊØÃÈÑ¡´Ôì àÃÕÂ§à¤Ã×Í(à¢µ15, 16, 17)", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", " ¤ÓÊÑè§ ¹Ã. ·Õè 306/2560 Å§ÇÑ¹·Õè 20 ¾ÄÈ¨Ô¡ÒÂ¹ 2560" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", "¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã(à¢µÊèÇ¹¡ÅÒ§,18),¾Ñ¹µÓÃÇ¨â· à¸ÕÂÃÃÑµ¹ì ÇÔàªÕÂÃÊÃÃ¤ì(à¢µ1,2),¹ÒÂä¾âÃ¨¹ì ÍÒ¨ÃÑ¡ÉÒ(à¢µ3,4),¹ÒÂÇÃ¾Ñ¹¸ì àÂç¹·ÃÑ¾Âì(à¢µ5,9),¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ(à¢µ6),¹Ò§»ÃÐÀÒÈÃÕ ºØ­ÇÔàÈÉ(à¢µ7,15),¾.µ.·.¾§Èì¾Ã ¾ÃÒÁ³ìàÊ¹èËì(à¢µ8),¹ÒÂÍÔÊÃÐ ÈÔÃÔÇÃÀÒ(à¢µ10),¹Ò§ÍÑ¨¨ÔÁÒ ¨Ñ¹·ÃìÊØÇÒ¹ÔªÂì(à¢µ11,14),¹Ò§ÀÑ·ÃÀÃ °ÔµÔÂÒÀÃ³ì(à¢µ12,17),¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì(à¢µ13),¹ÒÂÊØÃÈÑ¡´Ôì àÃÕÂ§à¤Ã×Í(à¢µ16)", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", " ¤ÓÊÑè§ ¹Ã. ·Õè 215/2560 Å§ÇÑ¹·Õè 6 ¡Ñ¹ÂÒÂ¹ 2560" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", "¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã(à¢µÊèÇ¹¡ÅÒ§,18),¾Ñ¹µÓÃÇ¨â· à¸ÕÂÃÃÑµ¹ì ÇÔàªÕÂÃÊÃÃ¤ì(à¢µ1,2),¹ÒÂä¾âÃ¨¹ì ÍÒ¨ÃÑ¡ÉÒ(à¢µ3,4),¹ÒÂÇÃ¾Ñ¹¸ì àÂç¹·ÃÑ¾Âì(à¢µ5,9),¹Ò§ÊÒÇ»ÀÑÊÁ¹ ÍÑÁÃÒÅÔ¢Ôµ(à¢µ6),¹Ò§»ÃÐÀÒÈÃÕ ºØ­ÇÔàÈÉ(à¢µ7,15),¹ÒÂÊØÃÈÑ¡´Ôì àÃÕÂ§à¤Ã×Í(à¢µ8,16),¹ÒÂÍÔÊÃÐ ÈÔÃÔÇÃÀÒ(à¢µ10),¹Ò§ÍÑ¨¨ÔÁÒ ¨Ñ¹·ÃìÊØÇÒ¹ÔªÂì(à¢µ11,14),¹Ò§ÀÑ·ÃÀÃ °ÔµÔÂÒÀÃ³ì(à¢µ12,17),¹Ò§ÊÒÇÍÃ¹Øª ÈÃÕ¹¹·ì(à¢µ13)", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", " ¤ÓÊÑè§ ¹Ã. ·Õè 100/2560 Å§ÇÑ¹·Õè 28 ÁÕ¹Ò¤Á 2560" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕ", "¹ÒÂ¨ÔÃÒÂØ ¹Ñ¹·ì¸ÃÒ¸Ã(à¢µÊèÇ¹¡ÅÒ§,6,18),¾Ñ¹µÓÃÇ¨â· à¸ÕÂÃÃÑµ¹ì ÇÔàªÕÂÃÊÃÃ¤ì(à¢µ1,2),¹ÒÂä¾âÃ¨¹ì ÍÒ¨ÃÑ¡ÉÒ(à¢µ3,4),¹ÒÂÇÃ¾Ñ¹¸ì àÂç¹·ÃÑ¾Âì(à¢µ5,9),¹Ò§»ÃÐÀÒÈÃÕ ºØ­ÇÔàÈÉ(à¢µ7, 15),¹ÒÂÊØÃÈÑ¡´Ôì àÃÕÂ§à¤Ã×Í(à¢µ8, 16),¹ÒÂÍÔÊÃÐ ÈÔÃÔÇÃÀÒ(à¢µ10, 13),¹Ò§ÍÑ¨¨ÔÁÒ ¨Ñ¹·ÃìÊØÇÒ¹ÔªÂì(à¢µ11, 14),¹Ò§ÀÑ·ÃÀÃ °ÔµÔÂÒÀÃ³ì(à¢µ12, 17)", "ÁÍºËÁÒÂ¼ÙéµÃÇ¨ÃÒª¡ÒÃÊÓ¹Ñ¡¹ÒÂ¡ÃÑ°Á¹µÃÕÃÑº¼Ô´ªÍºà¢µµÃÇ¨ÃÒª¡ÒÃ", " ¤ÓÊÑè§ ¹Ã.·Õè 3 / 2560 Å§ÇÑ¹·Õè 5 Á¡ÃÒ¤Á 2560" });

            migrationBuilder.CreateIndex(
                name: "IX_CentralDepartmentProvince_ProvinceId",
                table: "CentralDepartmentProvince",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralDepartments_DepartmentId",
                table: "CentralDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvincialDepartmentProvince_ProvinceId",
                table: "ProvincialDepartmentProvince",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvincialDepartments_DepartmentId",
                table: "ProvincialDepartments",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProvinces_AspNetUsers_UserID",
                table: "UserProvinces",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProvinces_AspNetUsers_UserID",
                table: "UserProvinces");

            migrationBuilder.DropTable(
                name: "CentralDepartmentProvince");

            migrationBuilder.DropTable(
                name: "ProvincialDepartmentProvince");

            migrationBuilder.DropTable(
                name: "CentralDepartments");

            migrationBuilder.DropTable(
                name: "ProvincialDepartments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProvinces",
                table: "UserProvinces");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "UserProvinces",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProvinces",
                table: "UserProvinces",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "����Ǩ�Ҫ����ӹѡ��¡�Ѱ�����", "���͡ ����Ѳ�� ��Ե��� (ࢵ��ǹ��ҧ), �ҧ���������ѡɳ� �����ӹ�ª�� (ࢵ1,12), �ҧ���Ե�� ͵��Ѿ�� (ࢵ2,14.17), ��³ç�� ���ͺح���� (ࢵ3,9), ��¾��� �ͧ⾸�� (ࢵ4,18), ��¸�ó��ѱ�� ��Է�Ծѹ�� (ࢵ5,7,10), �ҧ��ǻ����� ������ԢԵ (ࢵ6,16), ��¨����� �ѹ���Ҹ� (ࢵ8,13), �ҧ����ùت ��չ��� (ࢵ11,15)", "�ͺ���¼���Ǩ�Ҫ����ӹѡ��¡�Ѱ������Ѻ�Դ�ͺࢵ��Ǩ�Ҫ���", "����� �� ��� 313/2562 ŧ�ѹ��� 28 ���Ҥ� 2562" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "����Ǩ�Ҫ����ӹѡ��¡�Ѱ�����", "���͡ ����Ѳ�� ��Ե��� (ࢵ��ǹ��ҧ), �ҧ���������ѡɳ� �����ӹ�ª�� (ࢵ1,12), �ҧ���Ե�� ͵��Ѿ�� (ࢵ2,14.17), ��³ç�� ���ͺح���� (ࢵ3,9), ��¾��� �ͧ⾸�� (ࢵ4,18), ��¸�ó��ѱ�� ��Է�Ծѹ�� (ࢵ5,7,10), �ҧ��ǻ����� ������ԢԵ (ࢵ6,16), ��¨����� �ѹ���Ҹ� (ࢵ8,13), �ҧ����ùت ��չ��� (ࢵ11,15)", "�ͺ���¼���Ǩ�Ҫ����ӹѡ��¡�Ѱ������Ѻ�Դ�ͺࢵ��Ǩ�Ҫ���", "����� ��. ��� 102/2562 ŧ�ѹ��� 10 ����Ҥ� 2562" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "����Ǩ�Ҫ����ӹѡ��¡�Ѱ�����", "���͡ ����Ѳ�� ��Ե��� (ࢵ��ǹ��ҧ), �ҧ���������ѡɳ� �����ӹ�ª�� (ࢵ1,12), �ҧ���Ե�� ͵��Ѿ�� (ࢵ2,14.17), ��³ç�� ���ͺح���� (ࢵ3,9), ��¾��� �ͧ⾸�� (ࢵ4,18), ��¸�ó��ѱ�� ��Է�Ծѹ�� (ࢵ5,7,10), �ҧ��ǻ����� ������ԢԵ (ࢵ6,16), ��¨����� �ѹ���Ҹ� (ࢵ8,13), �ҧ����ùت ��չ��� (ࢵ11,15)", "�ͺ���¼���Ǩ�Ҫ����ӹѡ��¡�Ѱ������Ѻ�Դ�ͺࢵ��Ǩ�Ҫ���", "����� ��. ��� 5/2562 ŧ�ѹ��� 7 ���Ҥ� 2562" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "����Ǩ�Ҫ����ӹѡ��¡�Ѱ�����", "���͡ ����Ѳ�� ��Ե��� (ࢵ��ǹ��ҧ), �ҧ���������ѡɳ� �����ӹ�ª�� (ࢵ1,12), �ҧ���Ե�� ͵��Ѿ�� (ࢵ2,14.17), ��³ç�� ���ͺح���� (ࢵ3,9), ��¾��� �ͧ⾸�� (ࢵ4,18), ��¸�ó��ѱ�� ��Է�Ծѹ�� (ࢵ5,7,10), �ҧ��ǻ����� ������ԢԵ (ࢵ6,16), ��¨����� �ѹ���Ҹ� (ࢵ8,13), �ҧ����ùت ��չ��� (ࢵ11,15)", "�ͺ���¼���Ǩ�Ҫ����ӹѡ��¡�Ѱ������Ѻ�Դ�ͺࢵ��Ǩ�Ҫ���", "����� ʻ�. ��� 262/2561 ŧ�ѹ��� 16 ���Ҥ� 2561" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "����Ǩ�Ҫ����ӹѡ��¡�Ѱ�����", "�ѹ���Ǩ� �����ѵ�� ��������ä�(ࢵ��ǹ��ҧ,2,3),��³ç�� ���ͺح����(ࢵ1,4,7),�ҧ��ǻ����� ������ԢԵ(ࢵ5,6),��¨����� �ѹ���Ҹ�(ࢵ8,9),�ҧ����ùت ��չ���(ࢵ10,11),��¡�ձ� ʾ⪤(ࢵ13,14),�ҧ�ǧ�� �ѹ�(ࢵ15,16),��¾��� �ͧ⾸��(ࢵ15,16,17,18)", "�ͺ���¼���Ǩ�Ҫ����ӹѡ��¡�Ѱ������Ѻ�Դ�ͺࢵ��Ǩ�Ҫ���", "����� ��. ��� 255/2561 ŧ�ѹ��� 5 ���Ҥ� 2561" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "����Ǩ�Ҫ����ӹѡ��¡�Ѱ�����", "�ѹ���Ǩ� �����ѵ�� ��������ä�(ࢵ��ǹ��ҧ,2,3),��³ç�� ���ͺح����(ࢵ1,4,7),�ҧ��ǻ����� ������ԢԵ(ࢵ5,6),��¨����� �ѹ���Ҹ�(ࢵ8,9),�ҧ����ùت ��չ���(ࢵ10,11),��¡�ձ� ʾ⪤(ࢵ13,14), ��¾��� �ͧ⾸��(ࢵ15,16,17,18)", "�ͺ���¼���Ǩ�Ҫ����ӹѡ��¡�Ѱ������Ѻ�Դ�ͺࢵ��Ǩ�Ҫ���", "����� ��. ��� 206/2561 ŧ�ѹ��� 24 �ԧ�Ҥ� 2561" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "����Ǩ�Ҫ����ӹѡ��¡�Ѱ�����", "��¨����� �ѹ���Ҹ�(ࢵ��ǹ��ҧ,3,9,14),�ѹ���Ǩ� �����ѵ�� ��������ä�(ࢵ1,5),��¹��ʪ�� ��������(ࢵ2),���⪤��� പ��øѭ(ࢵ4),�ҧ��ǻ����� ������ԢԵ(ࢵ6,7),��³ç�� ���ͺح����(ࢵ8),�ҧ����ùت ��չ���(ࢵ10,11),����ѡ��� ���ح�(ࢵ12), ��¡�ձ� ʾ⪤(ࢵ13), �������ѡ��� ���§����(ࢵ15,16,17,18)", "�ͺ���¼���Ǩ�Ҫ����ӹѡ��¡�Ѱ������Ѻ�Դ�ͺࢵ��Ǩ�Ҫ���", "����� ��. ��� 100/2561 ŧ�ѹ��� 27 ����¹ 2561" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "����Ǩ�Ҫ����ӹѡ��¡�Ѱ�����", "��¨����� �ѹ���Ҹ�(ࢵ��ǹ��ҧ,3,9,14),�ѹ���Ǩ� �����ѵ�� ��������ä�(ࢵ1,5),��¹��ʪ�� ��������(ࢵ2),���⪤��� പ��øѭ(ࢵ4),�ҧ��ǻ����� ������ԢԵ(ࢵ6,7),��¾�Թ ����Ԫ��(ࢵ8),�ҧ����ùت ��չ���(ࢵ10,11),����ѡ��� ���ح�(ࢵ12), ��¡�ձ� ʾ⪤(ࢵ13), �������ѡ��� ���§����(ࢵ15,16,17,18)", "�ͺ���¼���Ǩ�Ҫ����ӹѡ��¡�Ѱ������Ѻ�Դ�ͺࢵ��Ǩ�Ҫ���", "����� ��. ��� 76/2561 ŧ�ѹ��� 2 ����¹ 2561" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "����Ǩ�Ҫ����ӹѡ��¡�Ѱ�����", "��¨����� �ѹ���Ҹ�(ࢵ��ǹ��ҧ,3,9,14),�ѹ���Ǩ� �����ѵ�� ��������ä�(ࢵ1,2,5),���⪤��� പ��øѭ(ࢵ4),�ҧ��ǻ����� ������ԢԵ(ࢵ6,7),��¾�Թ ����Ԫ��(ࢵ8),�ҧ����ùت ��չ���(ࢵ10,11),����ѡ��� ���ح�(ࢵ12), ��¡�ձ� ʾ⪤(ࢵ13), �������ѡ��� ���§����(ࢵ15,16,17,18)", "�ͺ���¼���Ǩ�Ҫ����ӹѡ��¡�Ѱ������Ѻ�Դ�ͺࢵ��Ǩ�Ҫ���", "����� ��. ��� 76/2561 ŧ�ѹ��� 2 ����¹ 2561" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "����Ǩ�Ҫ����ӹѡ��¡�Ѱ�����", "��¨����� �ѹ���Ҹ�(ࢵ��ǹ��ҧ,3,9,14),�ҧ��ǻ����� ������ԢԵ(ࢵ1,6,7),�������ѡ��� ���§����(ࢵ2,15,16),���⪤��� പ��øѭ(ࢵ4),�ѹ���Ǩ� �����ѵ�� ��������ä�(ࢵ5, 17, 18),��¾�Թ ����Ԫ��(ࢵ8),�ҧ����ùت ��չ���(ࢵ10, 11, 13),����ѡ��� ���ح�(ࢵ12)", "�ͺ���¼���Ǩ�Ҫ����ӹѡ��¡�Ѱ������Ѻ�Դ�ͺࢵ��Ǩ�Ҫ���", " ����� ��. ��� 336/2560 ŧ�ѹ��� 13 �ѹ�Ҥ� 2560" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "����Ǩ�Ҫ����ӹѡ��¡�Ѱ�����", "��¨����� �ѹ���Ҹ�(ࢵ��ǹ��ҧ,3,9,14),�ѹ���Ǩ� �����ѵ�� ��������ä�(ࢵ1,2,5),���⪤��� പ��øѭ(ࢵ4),�ҧ��ǻ����� ������ԢԵ(ࢵ6, 7, 18),��¾�Թ ����Ԫ��(ࢵ8),�ҧ����ùت ��չ���(ࢵ10, 11, 13),����ѡ��� ���ح�(ࢵ12),�������ѡ��� ���§����(ࢵ15, 16, 17)", "�ͺ���¼���Ǩ�Ҫ����ӹѡ��¡�Ѱ������Ѻ�Դ�ͺࢵ��Ǩ�Ҫ���", " ����� ��. ��� 306/2560 ŧ�ѹ��� 20 ��Ȩԡ�¹ 2560" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "����Ǩ�Ҫ����ӹѡ��¡�Ѱ�����", "��¨����� �ѹ���Ҹ�(ࢵ��ǹ��ҧ,18),�ѹ���Ǩ� �����ѵ�� ��������ä�(ࢵ1,2),�����è�� �Ҩ�ѡ��(ࢵ3,4),����þѹ�� ��繷�Ѿ��(ࢵ5,9),�ҧ��ǻ����� ������ԢԵ(ࢵ6),�ҧ�������� �ح�����(ࢵ7,15),�.�.�.����� �������ʹ���(ࢵ8),�������� ��������(ࢵ10),�ҧ�Ѩ���� �ѹ������ҹԪ��(ࢵ11,14),�ҧ�ѷ��� �Ե����ó�(ࢵ12,17),�ҧ����ùت ��չ���(ࢵ13),�������ѡ��� ���§����(ࢵ16)", "�ͺ���¼���Ǩ�Ҫ����ӹѡ��¡�Ѱ������Ѻ�Դ�ͺࢵ��Ǩ�Ҫ���", " ����� ��. ��� 215/2560 ŧ�ѹ��� 6 �ѹ��¹ 2560" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "����Ǩ�Ҫ����ӹѡ��¡�Ѱ�����", "��¨����� �ѹ���Ҹ�(ࢵ��ǹ��ҧ,18),�ѹ���Ǩ� �����ѵ�� ��������ä�(ࢵ1,2),�����è�� �Ҩ�ѡ��(ࢵ3,4),����þѹ�� ��繷�Ѿ��(ࢵ5,9),�ҧ��ǻ����� ������ԢԵ(ࢵ6),�ҧ�������� �ح�����(ࢵ7,15),�������ѡ��� ���§����(ࢵ8,16),�������� ��������(ࢵ10),�ҧ�Ѩ���� �ѹ������ҹԪ��(ࢵ11,14),�ҧ�ѷ��� �Ե����ó�(ࢵ12,17),�ҧ����ùت ��չ���(ࢵ13)", "�ͺ���¼���Ǩ�Ҫ����ӹѡ��¡�Ѱ������Ѻ�Դ�ͺࢵ��Ǩ�Ҫ���", " ����� ��. ��� 100/2560 ŧ�ѹ��� 28 �չҤ� 2560" });

            migrationBuilder.UpdateData(
                table: "InstructionOrders",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreateBy", "Detail", "Name", "Order" },
                values: new object[] { "����Ǩ�Ҫ����ӹѡ��¡�Ѱ�����", "��¨����� �ѹ���Ҹ�(ࢵ��ǹ��ҧ,6,18),�ѹ���Ǩ� �����ѵ�� ��������ä�(ࢵ1,2),�����è�� �Ҩ�ѡ��(ࢵ3,4),����þѹ�� ��繷�Ѿ��(ࢵ5,9),�ҧ�������� �ح�����(ࢵ7, 15),�������ѡ��� ���§����(ࢵ8, 16),�������� ��������(ࢵ10, 13),�ҧ�Ѩ���� �ѹ������ҹԪ��(ࢵ11, 14),�ҧ�ѷ��� �Ե����ó�(ࢵ12, 17)", "�ͺ���¼���Ǩ�Ҫ����ӹѡ��¡�Ѱ������Ѻ�Դ�ͺࢵ��Ǩ�Ҫ���", " ����� ��.��� 3 / 2560 ŧ�ѹ��� 5 ���Ҥ� 2560" });

            migrationBuilder.CreateIndex(
                name: "IX_UserProvinces_UserID",
                table: "UserProvinces",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProvinces_AspNetUsers_UserID",
                table: "UserProvinces",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
