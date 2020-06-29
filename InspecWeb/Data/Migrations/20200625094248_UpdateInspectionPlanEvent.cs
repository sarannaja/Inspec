using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class UpdateInspectionPlanEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CentralPolicyEvents_InspectionPlanEvents_InspectionPlanEventId",
                table: "CentralPolicyEvents");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralPolicyEvents_InspectionPlanEvents_InspectionPlanEventId",
                table: "CentralPolicyEvents",
                column: "InspectionPlanEventId",
                principalTable: "InspectionPlanEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CentralPolicyEvents_InspectionPlanEvents_InspectionPlanEventId",
                table: "CentralPolicyEvents");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralPolicyEvents_InspectionPlanEvents_InspectionPlanEventId",
                table: "CentralPolicyEvents",
                column: "InspectionPlanEventId",
                principalTable: "InspectionPlanEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
