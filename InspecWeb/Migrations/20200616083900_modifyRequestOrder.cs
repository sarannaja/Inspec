using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Migrations
{
    public partial class modifyRequestOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestOrders_CentralPolicies_CentralPolicyId",
                table: "RequestOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestOrders_Provinces_ProvinceId",
                table: "RequestOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestOrders_AspNetUsers_UserId",
                table: "RequestOrders");

            migrationBuilder.DropIndex(
                name: "IX_RequestOrders_CentralPolicyId",
                table: "RequestOrders");

            migrationBuilder.DropIndex(
                name: "IX_RequestOrders_ProvinceId",
                table: "RequestOrders");

            migrationBuilder.DropIndex(
                name: "IX_RequestOrders_UserId",
                table: "RequestOrders");

            migrationBuilder.DropColumn(
                name: "AnswerUserId",
                table: "RequestOrders");

            migrationBuilder.DropColumn(
                name: "CentralPolicyId",
                table: "RequestOrders");

            migrationBuilder.DropColumn(
                name: "DetailRequestOrder",
                table: "RequestOrders");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "RequestOrders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RequestOrders");

            migrationBuilder.RenameColumn(
                name: "AnswerDetail",
                table: "RequestOrders",
                newName: "Answerdetail");

            migrationBuilder.AddColumn<string>(
                name: "Answer_by",
                table: "RequestOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Commanded_by",
                table: "RequestOrders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Commanded_date",
                table: "RequestOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "RequestOrders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "RequestOrders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Subjectdetail",
                table: "RequestOrders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "User_Answer_byId",
                table: "RequestOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User_Commanded_byId",
                table: "RequestOrders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "beaware_date",
                table: "RequestOrders",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "publics",
                table: "RequestOrders",
                nullable: false,
                defaultValue: 0L);

           
        }
    }
}
