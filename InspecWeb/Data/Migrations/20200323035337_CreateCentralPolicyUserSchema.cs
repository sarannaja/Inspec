using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateCentralPolicyUserSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "CentralPolicyUsers",
                columns: table => new
                {
                    CentralPolicyId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentralPolicyUsers", x => new { x.UserId, x.CentralPolicyId });
                    table.ForeignKey(
                        name: "FK_CentralPolicyUsers_CentralPolicies_CentralPolicyId",
                        column: x => x.CentralPolicyId,
                        principalTable: "CentralPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CentralPolicyUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyUsers_CentralPolicyId",
                table: "CentralPolicyUsers",
                column: "CentralPolicyId");

        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CentralPolicyUsers");
        }
    }
}
