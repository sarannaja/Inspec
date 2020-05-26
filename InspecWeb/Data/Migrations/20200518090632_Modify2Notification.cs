using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class Modify2Notification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProvinceId",
                table: "Notification",
                nullable: false,
                defaultValue: 0L);
     
        }
    }
}
