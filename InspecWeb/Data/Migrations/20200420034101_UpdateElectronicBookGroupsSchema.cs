using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class UpdateElectronicBookGroupsSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ElectronicBookId",
                table: "ElectronicBookGroups",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookGroups_ElectronicBookId",
                table: "ElectronicBookGroups",
                column: "ElectronicBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectronicBookGroups_ElectronicBooks_ElectronicBookId",
                table: "ElectronicBookGroups",
                column: "ElectronicBookId",
                principalTable: "ElectronicBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectronicBookGroups_ElectronicBooks_ElectronicBookId",
                table: "ElectronicBookGroups");

            migrationBuilder.DropIndex(
                name: "IX_ElectronicBookGroups_ElectronicBookId",
                table: "ElectronicBookGroups");

            migrationBuilder.DropColumn(
                name: "ElectronicBookId",
                table: "ElectronicBookGroups");
        }
    }
}
