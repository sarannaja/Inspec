using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateAnswerSubquestionFilesSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnswerSubquestionFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectCentralPolicyProvinceId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerSubquestionFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerSubquestionFiles_SubjectCentralPolicyProvinces_SubjectCentralPolicyProvinceId",
                        column: x => x.SubjectCentralPolicyProvinceId,
                        principalTable: "SubjectCentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerSubquestionFiles_SubjectCentralPolicyProvinceId",
                table: "AnswerSubquestionFiles",
                column: "SubjectCentralPolicyProvinceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerSubquestionFiles");

        }
    }
}
