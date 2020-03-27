using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class EditInspectionPlanEventAndInspectionPlanEnentProvince : Migration
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

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "InspectionPlanEvents");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "InspectionPlanEvents");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "InspectionPlanEvents");

            migrationBuilder.DropColumn(
                name: "PlanDate",
                table: "InspectionPlanEventProvinces");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "UserProvinces",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndPlanDate",
                table: "InspectionPlanEventProvinces",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartPlanDate",
                table: "InspectionPlanEventProvinces",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProvinces",
                table: "UserProvinces",
                columns: new[] { "UserID", "ProvinceId" });

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProvinces",
                table: "UserProvinces");

            migrationBuilder.DropColumn(
                name: "EndPlanDate",
                table: "InspectionPlanEventProvinces");

            migrationBuilder.DropColumn(
                name: "StartPlanDate",
                table: "InspectionPlanEventProvinces");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "UserProvinces",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "InspectionPlanEvents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "InspectionPlanEvents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "InspectionPlanEvents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PlanDate",
                table: "InspectionPlanEventProvinces",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProvinces",
                table: "UserProvinces",
                column: "Id");

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
