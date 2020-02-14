using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class SeederDistrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "Id", "Name", "ProvinceId" },
                values: new object[,]
                {
                    { 1L, "เขตพระนคร", 1L },
                    { 28L, "เขตสาทร", 1L },
                    { 29L, "เขตบางซื่อ", 1L },
                    { 30L, "เขตจตุจักร", 1L },
                    { 31L, "เขตบางคอแหลม", 1L },
                    { 32L, "เขตประเวศ", 1L },
                    { 33L, "เขตคลองเตย", 1L },
                    { 34L, "เขตสวนหลวง", 1L },
                    { 35L, "เขตจอมทอง", 1L },
                    { 36L, "เขตดอนเมือง", 1L },
                    { 37L, "เขตราชเทวี", 1L },
                    { 38L, "เขตลาดพร้าว", 1L },
                    { 39L, "เขตวัฒนา", 1L },
                    { 40L, "เขตบางแค", 1L },
                    { 41L, "เขตหลักสี่", 1L },
                    { 42L, "เขตสายไหม", 1L },
                    { 43L, "เขตคันนายาว", 1L },
                    { 44L, "เขตสะพานสูง", 1L },
                    { 45L, "เขตวังทองหลาง", 1L },
                    { 46L, "เขตคลองสามวา", 1L },
                    { 47L, "เขตบางนา", 1L },
                    { 48L, "เขตทวีวัฒนา", 1L },
                    { 27L, "เขตบึงกุ่ม", 1L },
                    { 26L, "เขตดินแดง", 1L },
                    { 25L, "เขตบางพลัด", 1L },
                    { 24L, "เขตราษฎร์บูรณะ", 1L },
                    { 2L, "เขตดุสิต", 1L },
                    { 3L, "เขตหนองจอก", 1L },
                    { 4L, "เขตบางรัก", 1L },
                    { 5L, "เขตบางเขน", 1L },
                    { 6L, "เขตบางกะปิ", 1L },
                    { 7L, "เขตปทุมวัน", 1L },
                    { 8L, "เขตป้อมปราบศัตรูพ่าย", 1L },
                    { 9L, "เขตพระโขนง", 1L },
                    { 10L, "เขตมีนบุรี", 1L },
                    { 11L, "เขตลาดกระบัง", 1L },
                    { 49L, "เขตทุ่งครุ", 1L },
                    { 12L, "เขตยานนาวา", 1L },
                    { 14L, "เขตพญาไท", 1L },
                    { 15L, "เขตธนบุรี", 1L },
                    { 16L, "เขตบางกอกใหญ่", 1L },
                    { 17L, "เขตห้วยขวาง", 1L },
                    { 18L, "เขตคลองสาน", 1L },
                    { 19L, "เขตตลิ่งชัน", 1L },
                    { 20L, "เขตบางกอกน้อย", 1L },
                    { 21L, "เขตบางขุนเทียน", 1L },
                    { 22L, "เขตภาษีเจริญ", 1L },
                    { 23L, "เขตหนองแขม", 1L },
                    { 13L, "เขตสัมพันธวงศ์", 1L },
                    { 50L, "เขตบางบอน", 1L }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 47L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 50L);
        }
    }
}
