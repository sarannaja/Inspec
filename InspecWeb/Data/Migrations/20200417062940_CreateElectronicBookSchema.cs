using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateElectronicBookSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ElectronicBookId",
                table: "CentralPolicyUsers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "ElectronicBooks",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    Detail = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectronicBooks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyUsers_ElectronicBookId",
                table: "CentralPolicyUsers",
                column: "ElectronicBookId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBooks_UserId",
                table: "ElectronicBooks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralPolicyUsers_ElectronicBooks_ElectronicBookId",
                table: "CentralPolicyUsers",
                column: "ElectronicBookId",
                principalTable: "ElectronicBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CentralPolicyUsers_ElectronicBooks_ElectronicBookId",
                table: "CentralPolicyUsers");

            migrationBuilder.DropTable(
                name: "ElectronicBooks");

            migrationBuilder.DropIndex(
                name: "IX_CentralPolicyUsers_ElectronicBookId",
                table: "CentralPolicyUsers");

            migrationBuilder.DropColumn(
                name: "ElectronicBookId",
                table: "CentralPolicyUsers");
        }
    }
}
