using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Migrations
{
    public partial class modyfyuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CentralPolicyEvents_ElectronicBooks_ElectronicBookId",
                table: "CentralPolicyEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_CentralPolicyUsers_ElectronicBooks_ElectronicBookId",
                table: "CentralPolicyUsers");

            migrationBuilder.DropIndex(
                name: "IX_CentralPolicyUsers_ElectronicBookId",
                table: "CentralPolicyUsers");

            migrationBuilder.DropIndex(
                name: "IX_CentralPolicyEvents_ElectronicBookId",
                table: "CentralPolicyEvents");

            migrationBuilder.DropColumn(
                name: "ElectronicBookId",
                table: "CentralPolicyUsers");

            migrationBuilder.DropColumn(
                name: "ElectronicBookId",
                table: "CentralPolicyEvents");

            migrationBuilder.AddColumn<long>(
                name: "SubjectGroupId",
                table: "SubjectCentralPolicyProvinces",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProvincialDepartmentId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "SubjectGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralPolicyId = table.Column<long>(nullable: false),
                    ProvinceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectGroups_CentralPolicies_CentralPolicyId",
                        column: x => x.CentralPolicyId,
                        principalTable: "CentralPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectGroups_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            
        }
    }
}
