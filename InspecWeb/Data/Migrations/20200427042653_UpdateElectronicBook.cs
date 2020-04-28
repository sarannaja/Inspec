using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class UpdateElectronicBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ElectronicBooks",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ElectronicBookFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectronicBookId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicBookFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectronicBookFiles_ElectronicBooks_ElectronicBookId",
                        column: x => x.ElectronicBookId,
                        principalTable: "ElectronicBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookFiles_ElectronicBookId",
                table: "ElectronicBookFiles",
                column: "ElectronicBookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectronicBookFiles");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ElectronicBooks");
        }
    }
}
