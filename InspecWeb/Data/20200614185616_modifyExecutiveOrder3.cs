using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data
{
    public partial class modifyExecutiveOrder3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "publics",
                table: "ExecutiveOrders",
                nullable: false,
                defaultValue: 0L);

          
        }
    }
}
