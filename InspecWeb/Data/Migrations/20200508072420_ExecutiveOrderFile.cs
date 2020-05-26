using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class ExecutiveOrderFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProvinceId",
                table: "ExecutiveOrders",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "ExecutiveOrderFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExecutiveOrderId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutiveOrderFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExecutiveOrderFiles_ExecutiveOrders_ExecutiveOrderId",
                        column: x => x.ExecutiveOrderId,
                        principalTable: "ExecutiveOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExecutiveOrders_ProvinceId",
                table: "ExecutiveOrders",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutiveOrderFiles_ExecutiveOrderId",
                table: "ExecutiveOrderFiles",
                column: "ExecutiveOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExecutiveOrders_Provinces_ProvinceId",
                table: "ExecutiveOrders",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExecutiveOrders_Provinces_ProvinceId",
                table: "ExecutiveOrders");

            migrationBuilder.DropTable(
                name: "ExecutiveOrderFiles");

            migrationBuilder.DropIndex(
                name: "IX_ExecutiveOrders_ProvinceId",
                table: "ExecutiveOrders");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "ExecutiveOrders");

            
        }
    }
}
