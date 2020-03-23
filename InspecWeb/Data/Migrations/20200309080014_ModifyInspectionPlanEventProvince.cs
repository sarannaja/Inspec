using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class ModifyInspectionPlanEventProvince : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "InspectionPlanEventId",
                table: "InspectionPlanEventProvinces",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_InspectionPlanEventProvinces_InspectionPlanEventId",
                table: "InspectionPlanEventProvinces",
                column: "InspectionPlanEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_InspectionPlanEventProvinces_InspectionPlanEvents_InspectionPlanEventId",
                table: "InspectionPlanEventProvinces",
                column: "InspectionPlanEventId",
                principalTable: "InspectionPlanEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InspectionPlanEventProvinces_InspectionPlanEvents_InspectionPlanEventId",
                table: "InspectionPlanEventProvinces");

            migrationBuilder.DropIndex(
                name: "IX_InspectionPlanEventProvinces_InspectionPlanEventId",
                table: "InspectionPlanEventProvinces");

            migrationBuilder.DropColumn(
                name: "InspectionPlanEventId",
                table: "InspectionPlanEventProvinces");
        }
    }
}
