using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateFiscalYearRelationSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InspectionPlanEvents_CentralPolicies_CentralPolicyId",
                table: "InspectionPlanEvents");

            migrationBuilder.DropIndex(
                name: "IX_InspectionPlanEvents_CentralPolicyId",
                table: "InspectionPlanEvents");

            migrationBuilder.DropColumn(
                name: "CentralPolicyId",
                table: "InspectionPlanEvents");

            migrationBuilder.CreateTable(
                name: "FiscalYearRelations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FiscalYearId = table.Column<long>(nullable: false),
                    RegionId = table.Column<long>(nullable: false),
                    ProvinceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalYearRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiscalYearRelations_FiscalYear_FiscalYearId",
                        column: x => x.FiscalYearId,
                        principalTable: "FiscalYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FiscalYearRelations_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FiscalYearRelations_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FiscalYearRelations_FiscalYearId",
                table: "FiscalYearRelations",
                column: "FiscalYearId");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalYearRelations_ProvinceId",
                table: "FiscalYearRelations",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalYearRelations_RegionId",
                table: "FiscalYearRelations",
                column: "RegionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FiscalYearRelations");

            migrationBuilder.AddColumn<long>(
                name: "CentralPolicyId",
                table: "InspectionPlanEvents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_InspectionPlanEvents_CentralPolicyId",
                table: "InspectionPlanEvents",
                column: "CentralPolicyId");

            migrationBuilder.AddForeignKey(
                name: "FK_InspectionPlanEvents_CentralPolicies_CentralPolicyId",
                table: "InspectionPlanEvents",
                column: "CentralPolicyId",
                principalTable: "CentralPolicies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
