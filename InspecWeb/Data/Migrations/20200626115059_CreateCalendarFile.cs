using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateCalendarFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalendarFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyId = table.Column<long>(nullable: false),
                    InspectionPlanEventId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalendarFiles_CentralPolicies_CentralPolicyId",
                        column: x => x.CentralPolicyId,
                        principalTable: "CentralPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalendarFiles_InspectionPlanEvents_InspectionPlanEventId",
                        column: x => x.InspectionPlanEventId,
                        principalTable: "InspectionPlanEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalendarFiles_CentralPolicyId",
                table: "CalendarFiles",
                column: "CentralPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarFiles_InspectionPlanEventId",
                table: "CalendarFiles",
                column: "InspectionPlanEventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarFiles");
        }
    }
}
