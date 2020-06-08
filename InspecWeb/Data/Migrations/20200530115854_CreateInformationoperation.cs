using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateInformationoperation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Informationoperations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Detail = table.Column<string>(nullable: false),
                    Tel = table.Column<string>(nullable: false),
                    Province = table.Column<string>(nullable: false),
                    District = table.Column<string>(nullable: false),
                    File = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Informationoperations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Informationoperations");


            migrationBuilder.UpdateData(
                table: "ProvincialDepartmentProvince",
                keyColumns: new[] { "ProvincialDepartmentID", "ProvinceId" },
                keyValues: new object[] { 28L, 69L },
                column: "Id",
                value: 1893L);

            migrationBuilder.UpdateData(
                table: "ProvincialDepartmentProvince",
                keyColumns: new[] { "ProvincialDepartmentID", "ProvinceId" },
                keyValues: new object[] { 28L, 70L },
                column: "Id",
                value: 1894L);

            migrationBuilder.UpdateData(
                table: "ProvincialDepartmentProvince",
                keyColumns: new[] { "ProvincialDepartmentID", "ProvinceId" },
                keyValues: new object[] { 28L, 71L },
                column: "Id",
                value: 1895L);

            migrationBuilder.UpdateData(
                table: "ProvincialDepartmentProvince",
                keyColumns: new[] { "ProvincialDepartmentID", "ProvinceId" },
                keyValues: new object[] { 28L, 72L },
                column: "Id",
                value: 1896L);

            migrationBuilder.UpdateData(
                table: "ProvincialDepartmentProvince",
                keyColumns: new[] { "ProvincialDepartmentID", "ProvinceId" },
                keyValues: new object[] { 28L, 73L },
                column: "Id",
                value: 1897L);

            migrationBuilder.UpdateData(
                table: "ProvincialDepartmentProvince",
                keyColumns: new[] { "ProvincialDepartmentID", "ProvinceId" },
                keyValues: new object[] { 28L, 74L },
                column: "Id",
                value: 1898L);

            migrationBuilder.UpdateData(
                table: "ProvincialDepartmentProvince",
                keyColumns: new[] { "ProvincialDepartmentID", "ProvinceId" },
                keyValues: new object[] { 28L, 75L },
                column: "Id",
                value: 1899L);

            migrationBuilder.UpdateData(
                table: "ProvincialDepartmentProvince",
                keyColumns: new[] { "ProvincialDepartmentID", "ProvinceId" },
                keyValues: new object[] { 28L, 76L },
                column: "Id",
                value: 1900L);

            migrationBuilder.UpdateData(
                table: "ProvincialDepartmentProvince",
                keyColumns: new[] { "ProvincialDepartmentID", "ProvinceId" },
                keyValues: new object[] { 28L, 77L },
                column: "Id",
                value: 1901L);
        }
    }
}
