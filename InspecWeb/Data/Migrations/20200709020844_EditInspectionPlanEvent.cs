using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class EditInspectionPlanEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "InspectionPlanEvents",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionPlanEvents_CreatedBy",
                table: "InspectionPlanEvents",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_InspectionPlanEvents_AspNetUsers_CreatedBy",
                table: "InspectionPlanEvents",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InspectionPlanEvents_AspNetUsers_CreatedBy",
                table: "InspectionPlanEvents");

            migrationBuilder.DropIndex(
                name: "IX_InspectionPlanEvents_CreatedBy",
                table: "InspectionPlanEvents");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "InspectionPlanEvents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
