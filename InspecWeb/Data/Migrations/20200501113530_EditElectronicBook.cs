using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class EditElectronicBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Detail",
                table: "ElectronicBooks",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Problem",
                table: "ElectronicBooks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Suggestion",
                table: "ElectronicBooks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Forward",
                table: "CentralPolicyUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvitedBy",
                table: "CentralPolicyUsers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ElectronicBookId",
                table: "CentralPolicyEvents",
                nullable: false,
                defaultValue: 0L);

      
       

            migrationBuilder.CreateIndex(
                name: "IX_CentralPolicyEvents_ElectronicBookId",
                table: "CentralPolicyEvents",
                column: "ElectronicBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_CentralPolicyEvents_ElectronicBooks_ElectronicBookId",
                table: "CentralPolicyEvents",
                column: "ElectronicBookId",
                principalTable: "ElectronicBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CentralPolicyEvents_ElectronicBooks_ElectronicBookId",
                table: "CentralPolicyEvents");

            migrationBuilder.DropIndex(
                name: "IX_CentralPolicyEvents_ElectronicBookId",
                table: "CentralPolicyEvents");

            migrationBuilder.DropColumn(
                name: "Problem",
                table: "ElectronicBooks");

            migrationBuilder.DropColumn(
                name: "Suggestion",
                table: "ElectronicBooks");

            migrationBuilder.DropColumn(
                name: "Forward",
                table: "CentralPolicyUsers");

            migrationBuilder.DropColumn(
                name: "InvitedBy",
                table: "CentralPolicyUsers");

            migrationBuilder.DropColumn(
                name: "ElectronicBookId",
                table: "CentralPolicyEvents");

            migrationBuilder.AlterColumn<string>(
                name: "Detail",
                table: "ElectronicBooks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

       
   
        }
    }
}
