using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class UpdateReportCommander : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SendCommander",
                table: "ImportReports",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImportReports_SendCommander",
                table: "ImportReports",
                column: "SendCommander");

            migrationBuilder.AddForeignKey(
                name: "FK_ImportReports_AspNetUsers_SendCommander",
                table: "ImportReports",
                column: "SendCommander",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImportReports_AspNetUsers_SendCommander",
                table: "ImportReports");

            migrationBuilder.DropIndex(
                name: "IX_ImportReports_SendCommander",
                table: "ImportReports");

            migrationBuilder.DropColumn(
                name: "SendCommander",
                table: "ImportReports");
        }
    }
}
