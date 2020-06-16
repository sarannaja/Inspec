using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data
{
    public partial class modifyExecutiveOrder2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExecutiveOrders_CentralPolicies_CentralPolicyId",
                table: "ExecutiveOrders");

            migrationBuilder.DropIndex(
                name: "IX_ExecutiveOrders_CentralPolicyId",
                table: "ExecutiveOrders");

            migrationBuilder.DropColumn(
                name: "CentralPolicyId",
                table: "ExecutiveOrders");

        }
    }
}
