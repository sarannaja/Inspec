using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class EditSubjectSchema3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SubjectDates_SubjectId",
                table: "SubjectDates",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectDates_Subjects_SubjectId",
                table: "SubjectDates",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectDates_Subjects_SubjectId",
                table: "SubjectDates");

            migrationBuilder.DropIndex(
                name: "IX_SubjectDates_SubjectId",
                table: "SubjectDates");
        }
    }
}
