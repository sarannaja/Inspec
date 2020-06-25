using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class EbookGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectronicBookGroups_CentralPolicyProvinces_CentralPolicyProvinceId",
                table: "ElectronicBookGroups");

            migrationBuilder.DropIndex(
                name: "IX_ElectronicBookGroups_CentralPolicyProvinceId",
                table: "ElectronicBookGroups");

            migrationBuilder.DropColumn(
                name: "CentralPolicyProvinceId",
                table: "ElectronicBookGroups");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "ElectronicBooks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "ElectronicBooks",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CentralPolicyEventId",
                table: "ElectronicBookGroups",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookGroups_CentralPolicyEventId",
                table: "ElectronicBookGroups",
                column: "CentralPolicyEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectronicBookGroups_CentralPolicyEvents_CentralPolicyEventId",
                table: "ElectronicBookGroups",
                column: "CentralPolicyEventId",
                principalTable: "CentralPolicyEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectronicBookGroups_CentralPolicyEvents_CentralPolicyEventId",
                table: "ElectronicBookGroups");

            migrationBuilder.DropIndex(
                name: "IX_ElectronicBookGroups_CentralPolicyEventId",
                table: "ElectronicBookGroups");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "ElectronicBooks");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "ElectronicBooks");

            migrationBuilder.DropColumn(
                name: "CentralPolicyEventId",
                table: "ElectronicBookGroups");

            migrationBuilder.AddColumn<long>(
                name: "CentralPolicyProvinceId",
                table: "ElectronicBookGroups",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookGroups_CentralPolicyProvinceId",
                table: "ElectronicBookGroups",
                column: "CentralPolicyProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectronicBookGroups_CentralPolicyProvinces_CentralPolicyProvinceId",
                table: "ElectronicBookGroups",
                column: "CentralPolicyProvinceId",
                principalTable: "CentralPolicyProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
