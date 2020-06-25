using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class UpdateElectronicBookSuggestionGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detail",
                table: "ElectronicBookSuggestGroups");

            migrationBuilder.DropColumn(
                name: "Problem",
                table: "ElectronicBookSuggestGroups");

            migrationBuilder.AlterColumn<long>(
                name: "SubjectCentralPolicyProvinceId",
                table: "ElectronicBookSuggestGroups",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "CentralPolicyEventId",
                table: "ElectronicBookSuggestGroups",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookSuggestGroups_CentralPolicyEventId",
                table: "ElectronicBookSuggestGroups",
                column: "CentralPolicyEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectronicBookSuggestGroups_CentralPolicyEvents_CentralPolicyEventId",
                table: "ElectronicBookSuggestGroups",
                column: "CentralPolicyEventId",
                principalTable: "CentralPolicyEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectronicBookSuggestGroups_CentralPolicyEvents_CentralPolicyEventId",
                table: "ElectronicBookSuggestGroups");

            migrationBuilder.DropIndex(
                name: "IX_ElectronicBookSuggestGroups_CentralPolicyEventId",
                table: "ElectronicBookSuggestGroups");

            migrationBuilder.DropColumn(
                name: "CentralPolicyEventId",
                table: "ElectronicBookSuggestGroups");

            migrationBuilder.AlterColumn<long>(
                name: "SubjectCentralPolicyProvinceId",
                table: "ElectronicBookSuggestGroups",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Detail",
                table: "ElectronicBookSuggestGroups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Problem",
                table: "ElectronicBookSuggestGroups",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
