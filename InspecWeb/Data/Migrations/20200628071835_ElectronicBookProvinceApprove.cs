using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class ElectronicBookProvinceApprove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectronicBookAccepts_ElectronicBookGroups_ElectronicBookGroupId",
                table: "ElectronicBookAccepts");

            migrationBuilder.DropIndex(
                name: "IX_ElectronicBookAccepts_ElectronicBookGroupId",
                table: "ElectronicBookAccepts");

            migrationBuilder.DropColumn(
                name: "ElectronicBookGroupId",
                table: "ElectronicBookAccepts");

            migrationBuilder.AddColumn<string>(
                name: "CreateBy",
                table: "ElectronicBookAccepts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ElectronicBookAccepts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ElectronicBookAccepts",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ElectronicBookId",
                table: "ElectronicBookAccepts",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProvinceId",
                table: "ElectronicBookAccepts",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ElectronicBookAccepts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ElectronicBookProvinceApproveFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectronicBookAcceptId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicBookProvinceApproveFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectronicBookProvinceApproveFiles_ElectronicBookAccepts_ElectronicBookAcceptId",
                        column: x => x.ElectronicBookAcceptId,
                        principalTable: "ElectronicBookAccepts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookAccepts_CreateBy",
                table: "ElectronicBookAccepts",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookAccepts_ElectronicBookId",
                table: "ElectronicBookAccepts",
                column: "ElectronicBookId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookAccepts_ProvinceId",
                table: "ElectronicBookAccepts",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookProvinceApproveFiles_ElectronicBookAcceptId",
                table: "ElectronicBookProvinceApproveFiles",
                column: "ElectronicBookAcceptId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectronicBookAccepts_AspNetUsers_CreateBy",
                table: "ElectronicBookAccepts",
                column: "CreateBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ElectronicBookAccepts_ElectronicBooks_ElectronicBookId",
                table: "ElectronicBookAccepts",
                column: "ElectronicBookId",
                principalTable: "ElectronicBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ElectronicBookAccepts_Provinces_ProvinceId",
                table: "ElectronicBookAccepts",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectronicBookAccepts_AspNetUsers_CreateBy",
                table: "ElectronicBookAccepts");

            migrationBuilder.DropForeignKey(
                name: "FK_ElectronicBookAccepts_ElectronicBooks_ElectronicBookId",
                table: "ElectronicBookAccepts");

            migrationBuilder.DropForeignKey(
                name: "FK_ElectronicBookAccepts_Provinces_ProvinceId",
                table: "ElectronicBookAccepts");

            migrationBuilder.DropTable(
                name: "ElectronicBookProvinceApproveFiles");

            migrationBuilder.DropIndex(
                name: "IX_ElectronicBookAccepts_CreateBy",
                table: "ElectronicBookAccepts");

            migrationBuilder.DropIndex(
                name: "IX_ElectronicBookAccepts_ElectronicBookId",
                table: "ElectronicBookAccepts");

            migrationBuilder.DropIndex(
                name: "IX_ElectronicBookAccepts_ProvinceId",
                table: "ElectronicBookAccepts");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "ElectronicBookAccepts");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ElectronicBookAccepts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ElectronicBookAccepts");

            migrationBuilder.DropColumn(
                name: "ElectronicBookId",
                table: "ElectronicBookAccepts");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "ElectronicBookAccepts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ElectronicBookAccepts");

            migrationBuilder.AddColumn<long>(
                name: "ElectronicBookGroupId",
                table: "ElectronicBookAccepts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookAccepts_ElectronicBookGroupId",
                table: "ElectronicBookAccepts",
                column: "ElectronicBookGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectronicBookAccepts_ElectronicBookGroups_ElectronicBookGroupId",
                table: "ElectronicBookAccepts",
                column: "ElectronicBookGroupId",
                principalTable: "ElectronicBookGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
