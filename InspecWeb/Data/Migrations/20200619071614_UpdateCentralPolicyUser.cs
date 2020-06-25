using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class UpdateCentralPolicyUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CentralPolicyUsers",
                table: "CentralPolicyUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CentralPolicyUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CentralPolicyUsers",
                table: "CentralPolicyUsers",
                column: "Id");

     

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyUsers_CentralPolicyId",
                table: "CentralPolicyUsers",
                column: "CentralPolicyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CentralPolicyUsers",
                table: "CentralPolicyUsers");

            migrationBuilder.DropIndex(
                name: "IX_CentralPolicyUsers_CentralPolicyId",
                table: "CentralPolicyUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CentralPolicyUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CentralPolicyUsers",
                table: "CentralPolicyUsers",
                columns: new[] { "CentralPolicyId", "UserId" });

          
        }
    }
}
