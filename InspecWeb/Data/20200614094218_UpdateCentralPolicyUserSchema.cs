using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data
{
    public partial class UpdateCentralPolicyUserSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "InspectionPlanEventId",
                table: "CentralPolicyUsers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyUsers_InspectionPlanEventId",
                table: "CentralPolicyUsers",
                column: "InspectionPlanEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralPolicyUsers_InspectionPlanEvents_InspectionPlanEventId",
                table: "CentralPolicyUsers",
                column: "InspectionPlanEventId",
                principalTable: "InspectionPlanEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CentralPolicyUsers_InspectionPlanEvents_InspectionPlanEventId",
                table: "CentralPolicyUsers");

            migrationBuilder.DropIndex(
                name: "IX_CentralPolicyUsers_InspectionPlanEventId",
                table: "CentralPolicyUsers");

            migrationBuilder.DropColumn(
                name: "InspectionPlanEventId",
                table: "CentralPolicyUsers");
        }
    }
}
