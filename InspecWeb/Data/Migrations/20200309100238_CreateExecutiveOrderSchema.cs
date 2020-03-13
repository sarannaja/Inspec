using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateExecutiveOrderSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExecutiveOrders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetailExecutiveOrder = table.Column<string>(nullable: false),
                    CentralPolicyId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutiveOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExecutiveOrders_CentralPolicies_CentralPolicyId",
                        column: x => x.CentralPolicyId,
                        principalTable: "CentralPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExecutiveOrders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExecutiveOrders_CentralPolicyId",
                table: "ExecutiveOrders",
                column: "CentralPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutiveOrders_UserId",
                table: "ExecutiveOrders",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExecutiveOrders");
        }
    }
}
