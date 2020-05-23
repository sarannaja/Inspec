using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class AnswerRequestOrderFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestOrders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetailRequestOrder = table.Column<string>(nullable: false),
                    AnswerDetail = table.Column<string>(nullable: true),
                    AnswerProblem = table.Column<string>(nullable: true),
                    AnswerCounsel = table.Column<string>(nullable: true),
                    CentralPolicyId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    AnswerUserId = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestOrders_CentralPolicies_CentralPolicyId",
                        column: x => x.CentralPolicyId,
                        principalTable: "CentralPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestOrders_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestOrders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnswerRequestOrderFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestOrderId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerRequestOrderFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerRequestOrderFiles_RequestOrders_RequestOrderId",
                        column: x => x.RequestOrderId,
                        principalTable: "RequestOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestOrderFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestOrderId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestOrderFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestOrderFiles_RequestOrders_RequestOrderId",
                        column: x => x.RequestOrderId,
                        principalTable: "RequestOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            

            migrationBuilder.CreateIndex(
                name: "IX_AnswerRequestOrderFiles_RequestOrderId",
                table: "AnswerRequestOrderFiles",
                column: "RequestOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestOrderFiles_RequestOrderId",
                table: "RequestOrderFiles",
                column: "RequestOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestOrders_CentralPolicyId",
                table: "RequestOrders",
                column: "CentralPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestOrders_ProvinceId",
                table: "RequestOrders",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestOrders_UserId",
                table: "RequestOrders",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerRequestOrderFiles");

            migrationBuilder.DropTable(
                name: "RequestOrderFiles");

            migrationBuilder.DropTable(
                name: "RequestOrders");
        }
    }
}
