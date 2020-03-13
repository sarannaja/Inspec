using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateCentralPolicyProvinceSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CentralPolicyProvinces",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyId = table.Column<long>(nullable: false),
                    ProvinceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralPolicyProvinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CentralPolicyProvinces_CentralPolicies_CentralPolicyId",
                        column: x => x.CentralPolicyId,
                        principalTable: "CentralPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CentralPolicyProvinces_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyProvinces_CentralPolicyId",
                table: "CentralPolicyProvinces",
                column: "CentralPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyProvinces_ProvinceId",
                table: "CentralPolicyProvinces",
                column: "ProvinceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CentralPolicyProvinces");
        }
    }
}
