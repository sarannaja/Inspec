using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class EditInspectionPlanEventAndInspectionPlanEnentProvince2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InspectionPlanEventProvinces");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "InspectionPlanEvents",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "ProvinceId",
                table: "InspectionPlanEvents",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "InspectionPlanEvents",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_InspectionPlanEvents_ProvinceId",
                table: "InspectionPlanEvents",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_InspectionPlanEvents_Provinces_ProvinceId",
                table: "InspectionPlanEvents",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InspectionPlanEvents_Provinces_ProvinceId",
                table: "InspectionPlanEvents");

            migrationBuilder.DropIndex(
                name: "IX_InspectionPlanEvents_ProvinceId",
                table: "InspectionPlanEvents");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "InspectionPlanEvents");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "InspectionPlanEvents");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "InspectionPlanEvents");

            migrationBuilder.CreateTable(
                name: "InspectionPlanEventProvinces",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndPlanDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InspectionPlanEventId = table.Column<long>(type: "bigint", nullable: false),
                    ProvinceId = table.Column<long>(type: "bigint", nullable: false),
                    StartPlanDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionPlanEventProvinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectionPlanEventProvinces_InspectionPlanEvents_InspectionPlanEventId",
                        column: x => x.InspectionPlanEventId,
                        principalTable: "InspectionPlanEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InspectionPlanEventProvinces_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InspectionPlanEventProvinces_InspectionPlanEventId",
                table: "InspectionPlanEventProvinces",
                column: "InspectionPlanEventId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionPlanEventProvinces_ProvinceId",
                table: "InspectionPlanEventProvinces",
                column: "ProvinceId");
        }
    }
}
