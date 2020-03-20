using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class CreateRelationSeederSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 20L);

            
            migrationBuilder.InsertData(
                table: "FiscalYearRelations",
                columns: new[] { "Id", "FiscalYearId", "ProvinceId", "RegionId" },
                values: new object[,]
                {
                    { 1L, 1L, 1L, 1L },
                    { 55L, 1L, 28L, 14L },
                    { 54L, 1L, 21L, 14L },
                    { 53L, 1L, 11L, 14L },
                    { 52L, 1L, 48L, 13L },
                    { 51L, 1L, 43L, 13L },
                    { 50L, 1L, 6L, 13L },
                    { 49L, 1L, 4L, 13L },
                    { 48L, 1L, 57L, 12L },
                    { 47L, 1L, 44L, 12L },
                    { 46L, 1L, 20L, 12L },
                    { 45L, 1L, 74L, 11L },
                    { 44L, 1L, 71L, 11L },
                    { 43L, 1L, 70L, 11L },
                    { 42L, 1L, 55L, 11L },
                    { 41L, 1L, 27L, 11L },
                    { 56L, 1L, 69L, 14L },
                    { 57L, 1L, 46L, 15L },
                    { 58L, 1L, 56L, 15L },
                    { 59L, 1L, 73L, 15L },
                    { 75L, 1L, 23L, 19L },
                    { 74L, 1L, 5L, 19L },
                    { 73L, 1L, 75L, 18L },
                    { 72L, 1L, 66L, 18L },
                    { 71L, 1L, 40L, 18L },
                    { 70L, 1L, 38L, 18L },
                    { 69L, 1L, 17L, 18L },
                    { 40L, 1L, 63L, 10L },
                    { 68L, 1L, 41L, 17L },
                    { 66L, 1L, 26L, 17L },
                    { 65L, 1L, 13L, 17L },
                    { 64L, 1L, 54L, 16L },
                    { 63L, 1L, 53L, 16L },
                    { 62L, 1L, 45L, 16L },
                    { 61L, 1L, 14L, 16L },
                    { 60L, 1L, 77L, 15L },
                    { 67L, 1L, 34L, 17L },
                    { 76L, 1L, 37L, 19L },
                    { 39L, 1L, 31L, 10L },
                    { 37L, 1L, 16L, 10L },
                    { 16L, 1L, 39L, 5L },
                    { 15L, 1L, 30L, 5L },
                    { 14L, 1L, 67L, 4L },
                    { 13L, 1L, 51L, 4L },
                    { 12L, 1L, 3L, 4L },
                    { 11L, 1L, 60L, 3L },
                    { 10L, 1L, 29L, 3L },
                    { 9L, 1L, 24L, 3L },
                    { 8L, 1L, 19L, 3L },
                    { 7L, 1L, 72L, 2L },
                    { 6L, 1L, 65L, 2L },
                    { 5L, 1L, 64L, 2L },
                    { 4L, 1L, 52L, 2L },
                    { 3L, 1L, 33L, 2L },
                    { 2L, 1L, 10L, 2L },
                    { 17L, 1L, 61L, 5L },
                    { 18L, 1L, 62L, 5L },
                    { 19L, 1L, 12L, 6L },
                    { 20L, 1L, 22L, 6L },
                    { 36L, 1L, 7L, 10L },
                    { 35L, 1L, 50L, 9L },
                    { 34L, 1L, 9L, 9L },
                    { 33L, 1L, 8L, 9L },
                    { 32L, 1L, 47L, 8L },
                    { 31L, 1L, 32L, 8L },
                    { 30L, 1L, 25L, 8L },
                    { 38L, 1L, 18L, 10L },
                    { 29L, 1L, 59L, 7L },
                    { 27L, 1L, 42L, 7L },
                    { 26L, 1L, 35L, 7L },
                    { 25L, 1L, 15L, 7L },
                    { 24L, 1L, 2L, 7L },
                    { 23L, 1L, 68L, 6L },
                    { 22L, 1L, 58L, 6L },
                    { 21L, 1L, 36L, 6L },
                    { 28L, 1L, 49L, 7L },
                    { 77L, 1L, 76L, 19L }
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CentralPolicyEvents");

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 47L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 50L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 51L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 52L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 53L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 54L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 55L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 56L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 57L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 58L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 59L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 60L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 61L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 62L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 63L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 64L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 65L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 66L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 67L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 68L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 69L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 70L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 71L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 72L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 73L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 74L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 75L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 76L);

            migrationBuilder.DeleteData(
                table: "FiscalYearRelations",
                keyColumn: "Id",
                keyValue: 77L);

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[] { 20L, null, "เขตตรวจราชการที่ 19" });
        }
    }
}
