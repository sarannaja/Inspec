using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class ElectronicBookAccept : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElectronicBookAccepts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectronicBookGroupId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicBookAccepts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectronicBookAccepts_ElectronicBookGroups_ElectronicBookGroupId",
                        column: x => x.ElectronicBookGroupId,
                        principalTable: "ElectronicBookGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElectronicBookAccepts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookAccepts_ElectronicBookGroupId",
                table: "ElectronicBookAccepts",
                column: "ElectronicBookGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookAccepts_UserId",
                table: "ElectronicBookAccepts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectronicBookAccepts");
        }
    }
}
