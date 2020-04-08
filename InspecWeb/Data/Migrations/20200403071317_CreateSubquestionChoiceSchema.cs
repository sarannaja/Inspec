using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateSubquestionChoiceSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Subquestions");



            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Subquestions",
                nullable: false,
                defaultValue: "");

      

            migrationBuilder.CreateTable(
                name: "SubquestionChoices",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubquestionId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubquestionChoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubquestionChoices_Subquestions_SubquestionId",
                        column: x => x.SubquestionId,
                        principalTable: "Subquestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

     
            migrationBuilder.CreateIndex(
                name: "IX_SubquestionChoices_SubquestionId",
                table: "SubquestionChoices",
                column: "SubquestionId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropTable(
                name: "SubquestionChoices");

  

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Subquestions");




       
        }
    }
}
