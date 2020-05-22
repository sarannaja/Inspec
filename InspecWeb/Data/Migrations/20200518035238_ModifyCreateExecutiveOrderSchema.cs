using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class ModifyCreateExecutiveOrderSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnswerCounsel",
                table: "ExecutiveOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnswerDetail",
                table: "ExecutiveOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnswerProblem",
                table: "ExecutiveOrders",
                nullable: true);


        }
    }
}
