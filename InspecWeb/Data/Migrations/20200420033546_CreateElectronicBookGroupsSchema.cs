using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateElectronicBookGroupsSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElectronicBookGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyProvinceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicBookGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectronicBookGroups_CentralPolicyProvinces_CentralPolicyProvinceId",
                        column: x => x.CentralPolicyProvinceId,
                        principalTable: "CentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookGroups_CentralPolicyProvinceId",
                table: "ElectronicBookGroups",
                column: "CentralPolicyProvinceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectronicBookGroups");
        }
    }
}
