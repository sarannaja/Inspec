using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateCentralPolicyEventQuestionAndUpdateInspectionPlanEventAndUpdateCentralPolicyEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeadlineDate",
                table: "CentralPolicyEvents",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NotificationDate",
                table: "CentralPolicyEvents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeadlineDate",
                table: "CentralPolicyEvents");

            migrationBuilder.DropColumn(
                name: "NotificationDate",
                table: "CentralPolicyEvents");
        }
    }
}
