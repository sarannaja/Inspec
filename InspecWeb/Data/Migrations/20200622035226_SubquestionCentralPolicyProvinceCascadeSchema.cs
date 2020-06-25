using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class SubquestionCentralPolicyProvinceCascadeSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectCentralPolicyProvinceGroups_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_SubquestionChoiceCentralPolicyProvinces_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                table: "SubquestionChoiceCentralPolicyProvinces");


            migrationBuilder.AddForeignKey(
                name: "FK_SubjectCentralPolicyProvinceGroups_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceGroups",
                column: "SubquestionCentralPolicyProvinceId",
                principalTable: "SubquestionCentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubquestionChoiceCentralPolicyProvinces_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                table: "SubquestionChoiceCentralPolicyProvinces",
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

            migrationBuilder.DropForeignKey(
                name: "FK_SubquestionChoiceCentralPolicyProvinces_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                table: "SubquestionChoiceCentralPolicyProvinces");


            migrationBuilder.AddForeignKey(
                name: "FK_SubjectCentralPolicyProvinceGroups_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceGroups",
                column: "SubquestionCentralPolicyProvinceId",
                principalTable: "SubquestionCentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubquestionChoiceCentralPolicyProvinces_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                table: "SubquestionChoiceCentralPolicyProvinces",
                column: "SubquestionCentralPolicyProvinceId",
                principalTable: "SubquestionCentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
