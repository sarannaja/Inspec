using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class EditCentralPolicy2Schema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectDates",
                table: "SubjectDates");

            migrationBuilder.DropIndex(
                name: "IX_SubjectDates_SubjectId",
                table: "SubjectDates");



            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subjects",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Answer",
                table: "Subjects",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");



           
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "FiscalYear",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "FiscalYear",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "CentralPolicies",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "CentralPolicies",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectDates",
                table: "SubjectDates",
                columns: new[] { "SubjectId", "CentralPolicyDateId" });

         

            migrationBuilder.UpdateData(
                table: "FiscalYear",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { null, null });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectDates_CentralPolicyDateId",
                table: "SubjectDates",
                column: "CentralPolicyDateId");



           

          

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

          
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectDates",
                table: "SubjectDates");

            migrationBuilder.DropIndex(
                name: "IX_SubjectDates_CentralPolicyDateId",
                table: "SubjectDates");



            

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Answer",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);


            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "FiscalYear",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "FiscalYear",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "CentralPolicies",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "CentralPolicies",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectDates",
                table: "SubjectDates",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "FiscalYear",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectDates_SubjectId",
                table: "SubjectDates",
                column: "SubjectId");
        }
    }
}
