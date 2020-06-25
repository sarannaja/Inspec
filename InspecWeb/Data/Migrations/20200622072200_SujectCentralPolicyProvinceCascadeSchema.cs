using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class SujectCentralPolicyProvinceCascadeSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerSubquestionOutsiders_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                table: "AnswerSubquestionOutsiders");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerSubquestions_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                table: "AnswerSubquestions");

            migrationBuilder.DropForeignKey(
                name: "FK_ElectronicBookSuggestGroups_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "ElectronicBookSuggestGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectCentralPolicyProvinceFiles_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectDateCentralPolicyProvinces_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SubjectDateCentralPolicyProvinces");

            migrationBuilder.DropForeignKey(
                name: "FK_SubquestionCentralPolicyProvinces_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SubquestionCentralPolicyProvinces");

            migrationBuilder.DropForeignKey(
                name: "FK_SuggestionSubject_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SuggestionSubject");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerSubquestionOutsiders_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                table: "AnswerSubquestionOutsiders",
                column: "SubquestionCentralPolicyProvinceId",
                principalTable: "SubquestionCentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerSubquestions_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                table: "AnswerSubquestions",
                column: "SubquestionCentralPolicyProvinceId",
                principalTable: "SubquestionCentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ElectronicBookSuggestGroups_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "ElectronicBookSuggestGroups",
                column: "SubjectCentralPolicyProvinceId",
                principalTable: "SubjectCentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectCentralPolicyProvinceFiles_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceFiles",
                column: "SubjectCentralPolicyProvinceId",
                principalTable: "SubjectCentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectDateCentralPolicyProvinces_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SubjectDateCentralPolicyProvinces",
                column: "SubjectCentralPolicyProvinceId",
                principalTable: "SubjectCentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubquestionCentralPolicyProvinces_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SubquestionCentralPolicyProvinces",
                column: "SubjectCentralPolicyProvinceId",
                principalTable: "SubjectCentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SuggestionSubject_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SuggestionSubject",
                column: "SubjectCentralPolicyProvinceId",
                principalTable: "SubjectCentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerSubquestionOutsiders_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                table: "AnswerSubquestionOutsiders");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerSubquestions_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                table: "AnswerSubquestions");

            migrationBuilder.DropForeignKey(
                name: "FK_ElectronicBookSuggestGroups_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "ElectronicBookSuggestGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectCentralPolicyProvinceFiles_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectDateCentralPolicyProvinces_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SubjectDateCentralPolicyProvinces");

            migrationBuilder.DropForeignKey(
                name: "FK_SubquestionCentralPolicyProvinces_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SubquestionCentralPolicyProvinces");

            migrationBuilder.DropForeignKey(
                name: "FK_SuggestionSubject_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SuggestionSubject");


            migrationBuilder.AddForeignKey(
                name: "FK_AnswerSubquestionOutsiders_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                table: "AnswerSubquestionOutsiders",
                column: "SubquestionCentralPolicyProvinceId",
                principalTable: "SubquestionCentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerSubquestions_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                table: "AnswerSubquestions",
                column: "SubquestionCentralPolicyProvinceId",
                principalTable: "SubquestionCentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ElectronicBookSuggestGroups_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "ElectronicBookSuggestGroups",
                column: "SubjectCentralPolicyProvinceId",
                principalTable: "SubjectCentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectCentralPolicyProvinceFiles_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceFiles",
                column: "SubjectCentralPolicyProvinceId",
                principalTable: "SubjectCentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectDateCentralPolicyProvinces_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SubjectDateCentralPolicyProvinces",
                column: "SubjectCentralPolicyProvinceId",
                principalTable: "SubjectCentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubquestionCentralPolicyProvinces_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SubquestionCentralPolicyProvinces",
                column: "SubjectCentralPolicyProvinceId",
                principalTable: "SubjectCentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SuggestionSubject_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                table: "SuggestionSubject",
                column: "SubjectCentralPolicyProvinceId",
                principalTable: "SubjectCentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
