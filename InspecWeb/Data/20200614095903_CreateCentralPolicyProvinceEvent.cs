using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data
{
    public partial class CreateCentralPolicyProvinceEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CentralPolicyProvinceEvents",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyProvinceId = table.Column<long>(nullable: false),
                    InspectionPlanEventId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralPolicyProvinceEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CentralPolicyProvinceEvents_CentralPolicyProvinces_CentralPolicyProvinceId",
                        column: x => x.CentralPolicyProvinceId,
                        principalTable: "CentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CentralPolicyProvinceEvents_InspectionPlanEvents_InspectionPlanEventId",
                        column: x => x.InspectionPlanEventId,
                        principalTable: "InspectionPlanEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyProvinceEvents_CentralPolicyProvinceId",
                table: "CentralPolicyProvinceEvents",
                column: "CentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyProvinceEvents_InspectionPlanEventId",
                table: "CentralPolicyProvinceEvents",
                column: "InspectionPlanEventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CentralPolicyProvinceEvents");
        }
    }
}
