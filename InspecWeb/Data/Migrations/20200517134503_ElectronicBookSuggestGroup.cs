using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class ElectronicBookSuggestGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Step",
                table: "SubjectCentralPolicyProvinces",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ElectronicBookSuggestGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectronicBookId = table.Column<long>(nullable: false),
                    SubjectCentralPolicyProvinceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicBookSuggestGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectronicBookSuggestGroups_ElectronicBooks_ElectronicBookId",
                        column: x => x.ElectronicBookId,
                        principalTable: "ElectronicBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElectronicBookSuggestGroups_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                        column: x => x.SubjectCentralPolicyProvinceId,
                        principalTable: "SubjectCentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

           

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookSuggestGroups_ElectronicBookId",
                table: "ElectronicBookSuggestGroups",
                column: "ElectronicBookId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookSuggestGroups_SubjectCentralPolicyProvinceId",
                table: "ElectronicBookSuggestGroups",
                column: "SubjectCentralPolicyProvinceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectronicBookSuggestGroups");

            migrationBuilder.DropColumn(
                name: "Step",
                table: "SubjectCentralPolicyProvinces");

        
        }
    }
}
