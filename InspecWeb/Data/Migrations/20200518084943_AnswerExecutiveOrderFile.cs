using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class AnswerExecutiveOrderFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnswerExecutiveOrderFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExecutiveOrderId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerExecutiveOrderFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerExecutiveOrderFiles_ExecutiveOrders_ExecutiveOrderId",
                        column: x => x.ExecutiveOrderId,
                        principalTable: "ExecutiveOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            

            migrationBuilder.CreateIndex(
                name: "IX_AnswerExecutiveOrderFiles_ExecutiveOrderId",
                table: "AnswerExecutiveOrderFiles",
                column: "ExecutiveOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerExecutiveOrderFiles");

            
        }
    }
}
