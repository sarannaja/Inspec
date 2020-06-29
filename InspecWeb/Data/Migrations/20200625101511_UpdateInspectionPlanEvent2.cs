using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class UpdateInspectionPlanEvent2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CentralPolicyUsers_InspectionPlanEvents_InspectionPlanEventId",
                table: "CentralPolicyUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralPolicyUsers_InspectionPlanEvents_InspectionPlanEventId",
                table: "CentralPolicyUsers",
                column: "InspectionPlanEventId",
                principalTable: "InspectionPlanEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CentralPolicyUsers_InspectionPlanEvents_InspectionPlanEventId",
                table: "CentralPolicyUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralPolicyUsers_InspectionPlanEvents_InspectionPlanEventId",
                table: "CentralPolicyUsers",
                column: "InspectionPlanEventId",
                principalTable: "InspectionPlanEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
