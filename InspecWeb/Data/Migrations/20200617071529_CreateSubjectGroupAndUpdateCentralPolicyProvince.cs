using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateSubjectGroupAndUpdateCentralPolicyProvince : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SubjectGroupId",
                table: "SubjectCentralPolicyProvinces",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "SubjectGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyId = table.Column<long>(nullable: false),
                    ProvinceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectGroups_CentralPolicies_CentralPolicyId",
                        column: x => x.CentralPolicyId,
                        principalTable: "CentralPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectGroups_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCentralPolicyProvinces_SubjectGroupId",
                table: "SubjectCentralPolicyProvinces",
                column: "SubjectGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGroups_CentralPolicyId",
                table: "SubjectGroups",
                column: "CentralPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGroups_ProvinceId",
                table: "SubjectGroups",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectCentralPolicyProvinces_SubjectGroups_SubjectGroupId",
                table: "SubjectCentralPolicyProvinces",
                column: "SubjectGroupId",
                principalTable: "SubjectGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectCentralPolicyProvinces_SubjectGroups_SubjectGroupId",
                table: "SubjectCentralPolicyProvinces");

            migrationBuilder.DropTable(
                name: "SubjectGroups");

            migrationBuilder.DropIndex(
                name: "IX_SubjectCentralPolicyProvinces_SubjectGroupId",
                table: "SubjectCentralPolicyProvinces");

            migrationBuilder.DropColumn(
                name: "SubjectGroupId",
                table: "SubjectCentralPolicyProvinces");
        }
    }
}
