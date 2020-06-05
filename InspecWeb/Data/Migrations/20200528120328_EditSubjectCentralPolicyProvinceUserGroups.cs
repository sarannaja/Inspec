using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class EditSubjectCentralPolicyProvinceUserGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectCentralPolicyProvinceUserGroups_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceUserGroups");

            migrationBuilder.AlterColumn<long>(
                name: "SubquestionCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceUserGroups",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "SubjectCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceUserGroups",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCentralPolicyProvinceUserGroups_SubjectCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceUserGroups",
                column: "SubjectCentralPolicyProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectCentralPolicyProvinceUserGroups_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceUserGroups",
                column: "SubjectCentralPolicyProvinceId",
                principalTable: "SubjectCentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectCentralPolicyProvinceUserGroups_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceUserGroups",
                column: "SubquestionCentralPolicyProvinceId",
                principalTable: "SubquestionCentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectCentralPolicyProvinceUserGroups_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceUserGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectCentralPolicyProvinceUserGroups_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceUserGroups");

            migrationBuilder.DropIndex(
                name: "IX_SubjectCentralPolicyProvinceUserGroups_SubjectCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceUserGroups");

            migrationBuilder.DropColumn(
                name: "SubjectCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceUserGroups");

            migrationBuilder.AlterColumn<long>(
                name: "SubquestionCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceUserGroups",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectCentralPolicyProvinceUserGroups_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceUserGroups",
                column: "SubquestionCentralPolicyProvinceId",
                principalTable: "SubquestionCentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
