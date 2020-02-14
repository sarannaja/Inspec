using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class ModifySubdistrictSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subdistrict_Districts_DistrictId",
                table: "Subdistrict");

            migrationBuilder.DropForeignKey(
                name: "FK_Village_Subdistrict_SubdistrictId",
                table: "Village");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subdistrict",
                table: "Subdistrict");

            migrationBuilder.RenameTable(
                name: "Subdistrict",
                newName: "Subdistricts");

            migrationBuilder.RenameIndex(
                name: "IX_Subdistrict_DistrictId",
                table: "Subdistricts",
                newName: "IX_Subdistricts_DistrictId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subdistricts",
                table: "Subdistricts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subdistricts_Districts_DistrictId",
                table: "Subdistricts",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Village_Subdistricts_SubdistrictId",
                table: "Village",
                column: "SubdistrictId",
                principalTable: "Subdistricts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subdistricts_Districts_DistrictId",
                table: "Subdistricts");

            migrationBuilder.DropForeignKey(
                name: "FK_Village_Subdistricts_SubdistrictId",
                table: "Village");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subdistricts",
                table: "Subdistricts");

            migrationBuilder.RenameTable(
                name: "Subdistricts",
                newName: "Subdistrict");

            migrationBuilder.RenameIndex(
                name: "IX_Subdistricts_DistrictId",
                table: "Subdistrict",
                newName: "IX_Subdistrict_DistrictId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subdistrict",
                table: "Subdistrict",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subdistrict_Districts_DistrictId",
                table: "Subdistrict",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Village_Subdistrict_SubdistrictId",
                table: "Village",
                column: "SubdistrictId",
                principalTable: "Subdistrict",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
