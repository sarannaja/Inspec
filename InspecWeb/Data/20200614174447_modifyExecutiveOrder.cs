using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data
{
    public partial class modifyExecutiveOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExecutiveOrders_Provinces_ProvinceId",
                table: "ExecutiveOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ExecutiveOrders_AspNetUsers_UserId",
                table: "ExecutiveOrders");

            migrationBuilder.DropIndex(
                name: "IX_ExecutiveOrders_ProvinceId",
                table: "ExecutiveOrders");

            migrationBuilder.DropIndex(
                name: "IX_ExecutiveOrders_UserId",
                table: "ExecutiveOrders");

            migrationBuilder.DropColumn(
                name: "AnswerUserId",
                table: "ExecutiveOrders");

            migrationBuilder.DropColumn(
                name: "DetailExecutiveOrder",
                table: "ExecutiveOrders");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "ExecutiveOrders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ExecutiveOrders");

            migrationBuilder.RenameColumn(
                name: "AnswerDetail",
                table: "ExecutiveOrders",
                newName: "Answerdetail");

            migrationBuilder.AlterColumn<long>(
                name: "CentralPolicyId",
                table: "ExecutiveOrders",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Answer_by",
                table: "ExecutiveOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Commanded_by",
                table: "ExecutiveOrders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Commanded_date",
                table: "ExecutiveOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ExecutiveOrders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "ExecutiveOrders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Subjectdetail",
                table: "ExecutiveOrders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "User_Answer_byId",
                table: "ExecutiveOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User_Commanded_byId",
                table: "ExecutiveOrders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "beaware_date",
                table: "ExecutiveOrders",
                nullable: true);

        }
    }
}
