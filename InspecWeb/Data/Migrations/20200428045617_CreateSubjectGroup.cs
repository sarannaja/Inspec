using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateSubjectGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubjectCentralPolicyProvinceGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectCentralPolicyProvinceId = table.Column<long>(nullable: false),
                    ProvincialDepartmentId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectCentralPolicyProvinceGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectCentralPolicyProvinceGroups_ProvincialDepartments_ProvincialDepartmentId",
                        column: x => x.ProvincialDepartmentId,
                        principalTable: "ProvincialDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectCentralPolicyProvinceGroups_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                        column: x => x.SubjectCentralPolicyProvinceId,
                        principalTable: "SubjectCentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

         

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCentralPolicyProvinceGroups_ProvincialDepartmentId",
                table: "SubjectCentralPolicyProvinceGroups",
                column: "ProvincialDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCentralPolicyProvinceGroups_SubjectCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceGroups",
                column: "SubjectCentralPolicyProvinceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectCentralPolicyProvinceGroups");

       
        }
    }
}
