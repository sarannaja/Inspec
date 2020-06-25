using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class ElectronicBookInvite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElectronicBookInvite",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectronicBookId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicBookInvite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectronicBookInvite_ElectronicBooks_ElectronicBookId",
                        column: x => x.ElectronicBookId,
                        principalTable: "ElectronicBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookInvite_ElectronicBookId",
                table: "ElectronicBookInvite",
                column: "ElectronicBookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectronicBookInvite");
        }
    }
}
