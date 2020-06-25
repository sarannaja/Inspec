using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class UpdateElectronicBookSuggestionGroup2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectronicBookSuggestGroups_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "ElectronicBookSuggestGroups");

            migrationBuilder.DropIndex(
                name: "IX_ElectronicBookSuggestGroups_SubjectCentralPolicyProvinceId",
                table: "ElectronicBookSuggestGroups");

            migrationBuilder.DropColumn(
                name: "SubjectCentralPolicyProvinceId",
                table: "ElectronicBookSuggestGroups");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SubjectCentralPolicyProvinceId",
                table: "ElectronicBookSuggestGroups",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookSuggestGroups_SubjectCentralPolicyProvinceId",
                table: "ElectronicBookSuggestGroups",
                column: "SubjectCentralPolicyProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectronicBookSuggestGroups_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "ElectronicBookSuggestGroups",
                column: "SubjectCentralPolicyProvinceId",
                principalTable: "SubjectCentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
