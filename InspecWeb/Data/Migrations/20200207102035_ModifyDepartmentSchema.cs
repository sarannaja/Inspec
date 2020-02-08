using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class ModifyDepartmentSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Ministries_MinistryId",
                table: "Departments");

            migrationBuilder.AlterColumn<long>(
                name: "MinistryId",
                table: "Departments",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CreatedAt", "MinistryId", "Name" },
                values: new object[] { 1L, null, 1L, "ทหารบก" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CreatedAt", "MinistryId", "Name" },
                values: new object[] { 2L, null, 2L, "อย." });

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Ministries_MinistryId",
                table: "Departments",
                column: "MinistryId",
                principalTable: "Ministries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Ministries_MinistryId",
                table: "Departments");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.AlterColumn<long>(
                name: "MinistryId",
                table: "Departments",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Ministries_MinistryId",
                table: "Departments",
                column: "MinistryId",
                principalTable: "Ministries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
