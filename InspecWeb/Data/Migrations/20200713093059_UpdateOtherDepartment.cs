using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class UpdateOtherDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectronicBookOtherAccepts_Provinces_ProvinceId",
                table: "ElectronicBookOtherAccepts");

            migrationBuilder.DropIndex(
                name: "IX_ElectronicBookOtherAccepts_ProvinceId",
                table: "ElectronicBookOtherAccepts");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "ElectronicBookOtherAccepts");

            migrationBuilder.AddColumn<long>(
                name: "ProvincialDepartmentId",
                table: "ElectronicBookOtherAccepts",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookOtherAccepts_ProvincialDepartmentId",
                table: "ElectronicBookOtherAccepts",
                column: "ProvincialDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectronicBookOtherAccepts_ProvincialDepartments_ProvincialDepartmentId",
                table: "ElectronicBookOtherAccepts",
                column: "ProvincialDepartmentId",
                principalTable: "ProvincialDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectronicBookOtherAccepts_ProvincialDepartments_ProvincialDepartmentId",
                table: "ElectronicBookOtherAccepts");

            migrationBuilder.DropIndex(
                name: "IX_ElectronicBookOtherAccepts_ProvincialDepartmentId",
                table: "ElectronicBookOtherAccepts");

            migrationBuilder.DropColumn(
                name: "ProvincialDepartmentId",
                table: "ElectronicBookOtherAccepts");

            migrationBuilder.AddColumn<long>(
                name: "ProvinceId",
                table: "ElectronicBookOtherAccepts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicBookOtherAccepts_ProvinceId",
                table: "ElectronicBookOtherAccepts",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectronicBookOtherAccepts_Provinces_ProvinceId",
                table: "ElectronicBookOtherAccepts",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
