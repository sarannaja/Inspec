using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Migrations
{
    public partial class CreateTrainingRegisterCondition2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrainingRegisterConditions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisterId = table.Column<long>(nullable: false),
                    ConditionId = table.Column<long>(nullable: false),
                    Status = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingRegisterConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingRegisterConditions_TrainingConditions_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "TrainingConditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingRegisterConditions_TrainingRegisters_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "TrainingRegisters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingRegisterConditions_ConditionId",
                table: "TrainingRegisterConditions",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingRegisterConditions_RegisterId",
                table: "TrainingRegisterConditions",
                column: "RegisterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingRegisterConditions");
        }
    }
}
