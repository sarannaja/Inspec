using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class EditSubjectCentralPolicyProvincesSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectCentralPolicyProvinceGroups_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceGroups");

            migrationBuilder.DropIndex(
                name: "IX_SubjectCentralPolicyProvinceGroups_SubjectCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceGroups");

            migrationBuilder.DropColumn(
                name: "SubjectCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceGroups");

            migrationBuilder.AddColumn<long>(
                name: "SubquestionCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceGroups",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCentralPolicyProvinceGroups_SubquestionCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceGroups",
                column: "SubquestionCentralPolicyProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectCentralPolicyProvinceGroups_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceGroups",
                column: "SubquestionCentralPolicyProvinceId",
                principalTable: "SubquestionCentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectCentralPolicyProvinceGroups_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceGroups");

            migrationBuilder.DropIndex(
                name: "IX_SubjectCentralPolicyProvinceGroups_SubquestionCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceGroups");

            migrationBuilder.DropColumn(
                name: "SubquestionCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceGroups");

            migrationBuilder.AddColumn<long>(
                name: "SubjectCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceGroups",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCentralPolicyProvinceGroups_SubjectCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceGroups",
                column: "SubjectCentralPolicyProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectCentralPolicyProvinceGroups_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceGroups",
                column: "SubjectCentralPolicyProvinceId",
                principalTable: "SubjectCentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
