using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateDepartmentSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Ministries",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinistryId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Ministries_MinistryId",
                        column: x => x.MinistryId,
                        principalTable: "Ministries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Ministries",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[] { 1L, null, "มหาดไทย" });

            migrationBuilder.InsertData(
                table: "Ministries",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[] { 2L, null, "สาธาระณะสุข" });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_MinistryId",
                table: "Departments",
                column: "MinistryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DeleteData(
                table: "Ministries",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Ministries",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Ministries",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
