using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateSubjectCentralPolicyProvinceUserGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubjectCentralPolicyProvinceUserGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubquestionCentralPolicyProvinceId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectCentralPolicyProvinceUserGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectCentralPolicyProvinceUserGroups_SubquestionCentralPolicyProvinces_SubquestionCentralPolicyProvinceId",
                        column: x => x.SubquestionCentralPolicyProvinceId,
                        principalTable: "SubquestionCentralPolicyProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectCentralPolicyProvinceUserGroups_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCentralPolicyProvinceUserGroups_SubquestionCentralPolicyProvinceId",
                table: "SubjectCentralPolicyProvinceUserGroups",
                column: "SubquestionCentralPolicyProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCentralPolicyProvinceUserGroups_UserId",
                table: "SubjectCentralPolicyProvinceUserGroups",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectCentralPolicyProvinceUserGroups");

          
        }
    }
}
