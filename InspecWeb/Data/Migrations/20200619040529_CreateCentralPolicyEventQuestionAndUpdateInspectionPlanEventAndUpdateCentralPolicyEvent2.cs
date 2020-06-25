using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateCentralPolicyEventQuestionAndUpdateInspectionPlanEventAndUpdateCentralPolicyEvent2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "InspectionPlanEvents",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InspectionPlanEventId",
                table: "CentralPolicies",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CentralPolicyEventQuestions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyEventId = table.Column<long>(nullable: false),
                    QuestionPeople = table.Column<string>(nullable: true),
                    NotificationDate = table.Column<DateTime>(nullable: true),
                    DeadlineDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralPolicyEventQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CentralPolicyEventQuestions_CentralPolicyEvents_CentralPolicyEventId",
                        column: x => x.CentralPolicyEventId,
                        principalTable: "CentralPolicyEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicies_InspectionPlanEventId",
                table: "CentralPolicies",
                column: "InspectionPlanEventId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyEventQuestions_CentralPolicyEventId",
                table: "CentralPolicyEventQuestions",
                column: "CentralPolicyEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralPolicies_InspectionPlanEvents_InspectionPlanEventId",
                table: "CentralPolicies",
                column: "InspectionPlanEventId",
                principalTable: "InspectionPlanEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CentralPolicies_InspectionPlanEvents_InspectionPlanEventId",
                table: "CentralPolicies");

            migrationBuilder.DropTable(
                name: "CentralPolicyEventQuestions");

            migrationBuilder.DropIndex(
                name: "IX_CentralPolicies_InspectionPlanEventId",
                table: "CentralPolicies");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "InspectionPlanEvents");

            migrationBuilder.DropColumn(
                name: "InspectionPlanEventId",
                table: "CentralPolicies");
        }
    }
}
