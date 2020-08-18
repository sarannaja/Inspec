using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class UpdateInspectionPlanEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProvincialDepartmentIdCreatedBy",
                table: "InspectionPlanEvents",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProvincialDepartmentsId",
                table: "InspectionPlanEvents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InspectionPlanEvents_ProvincialDepartmentsId",
                table: "InspectionPlanEvents",
                column: "ProvincialDepartmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_InspectionPlanEvents_ProvincialDepartments_ProvincialDepartmentsId",
                table: "InspectionPlanEvents",
                column: "ProvincialDepartmentsId",
                principalTable: "ProvincialDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InspectionPlanEvents_ProvincialDepartments_ProvincialDepartmentsId",
                table: "InspectionPlanEvents");

            migrationBuilder.DropIndex(
                name: "IX_InspectionPlanEvents_ProvincialDepartmentsId",
                table: "InspectionPlanEvents");

            migrationBuilder.DropColumn(
                name: "ProvincialDepartmentIdCreatedBy",
                table: "InspectionPlanEvents");

            migrationBuilder.DropColumn(
                name: "ProvincialDepartmentsId",
                table: "InspectionPlanEvents");
        }
    }
}
