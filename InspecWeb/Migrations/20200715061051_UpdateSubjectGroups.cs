using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Migrations
{
    public partial class UpdateSubjectGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectCentralPolicyProvinces_SubjectGroups_SubjectGroupId",
                table: "SubjectCentralPolicyProvinces");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectCentralPolicyProvinces_SubjectGroups_SubjectGroupId",
                table: "SubjectCentralPolicyProvinces",
                column: "SubjectGroupId",
                principalTable: "SubjectGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectCentralPolicyProvinces_SubjectGroups_SubjectGroupId",
                table: "SubjectCentralPolicyProvinces");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectCentralPolicyProvinces_SubjectGroups_SubjectGroupId",
                table: "SubjectCentralPolicyProvinces",
                column: "SubjectGroupId",
                principalTable: "SubjectGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
