using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateCentralPolicyGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CentralPolicyGroupId",
                table: "CentralPolicyUsers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "CentralPolicyGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralPolicyGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CentralPolicyUserFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyGroupId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralPolicyUserFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CentralPolicyUserFiles_CentralPolicyGroups_CentralPolicyGroupId",
                        column: x => x.CentralPolicyGroupId,
                        principalTable: "CentralPolicyGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

    
            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyUsers_CentralPolicyGroupId",
                table: "CentralPolicyUsers",
                column: "CentralPolicyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyUserFiles_CentralPolicyGroupId",
                table: "CentralPolicyUserFiles",
                column: "CentralPolicyGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralPolicyUsers_CentralPolicyGroups_CentralPolicyGroupId",
                table: "CentralPolicyUsers",
                column: "CentralPolicyGroupId",
                principalTable: "CentralPolicyGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CentralPolicyUsers_CentralPolicyGroups_CentralPolicyGroupId",
                table: "CentralPolicyUsers");

            migrationBuilder.DropTable(
                name: "CentralPolicyUserFiles");

            migrationBuilder.DropTable(
                name: "CentralPolicyGroups");

            migrationBuilder.DropIndex(
                name: "IX_CentralPolicyUsers_CentralPolicyGroupId",
                table: "CentralPolicyUsers");

            migrationBuilder.DropColumn(
                name: "CentralPolicyGroupId",
                table: "CentralPolicyUsers");

        }
    }
}
