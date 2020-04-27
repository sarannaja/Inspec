using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateSubquestionGroupSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubquestionGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyProvinceId = table.Column<long>(nullable: false),
                    SubquestionId = table.Column<long>(nullable: false),
                    ProvincialDepartmentId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubquestionGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubquestionGroups_CentralPolicyProvinces_CentralPolicyProvinceId",
                        column: x => x.CentralPolicyProvinceId,
                        principalTable: "CentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubquestionGroups_ProvincialDepartments_ProvincialDepartmentId",
                        column: x => x.ProvincialDepartmentId,
                        principalTable: "ProvincialDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubquestionGroups_Subquestions_SubquestionId",
                        column: x => x.SubquestionId,
                        principalTable: "Subquestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubquestionGroups_CentralPolicyProvinceId",
                table: "SubquestionGroups",
                column: "CentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_SubquestionGroups_ProvincialDepartmentId",
                table: "SubquestionGroups",
                column: "ProvincialDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubquestionGroups_SubquestionId",
                table: "SubquestionGroups",
                column: "SubquestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubquestionGroups");
        }
    }
}
