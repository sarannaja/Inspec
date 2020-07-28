using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class modyfyuser20200722 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FiscalYearId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0L);
       
        }
    }
}
