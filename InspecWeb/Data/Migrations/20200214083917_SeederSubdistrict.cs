using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class SeederSubdistrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Subdistricts",
                columns: new[] { "Id", "DistrictId", "Name" },
                values: new object[,]
                {
                    { 1L, 1L, "พระบรมมหาราชวัง" },
                    { 88L, 22L, "บางหว้า" },
                    { 87L, 21L, "แสมดำ" },
                    { 86L, 21L, "ท่าข้าม" },
                    { 85L, 20L, "อรุณอมรินทร์" },
                    { 84L, 20L, "บางขุนศรี" },
                    { 83L, 20L, "บางขุนนนท์" },
                    { 82L, 20L, "บ้านช่างหล่อ" },
                    { 81L, 20L, "ศิริราช" },
                    { 80L, 19L, "บางเชือกหนัง" },
                    { 79L, 19L, "บางระมาด" },
                    { 78L, 19L, "บางพรม" },
                    { 77L, 19L, "ฉิมพลี" },
                    { 89L, 22L, "บางด้วน" },
                    { 76L, 19L, "ตลิ่งชัน" },
                    { 74L, 18L, "คลองต้นไทร" },
                    { 73L, 18L, "บางลำภูล่าง" },
                    { 72L, 18L, "คลองสาน" },
                    { 71L, 18L, "สมเด็จเจ้าพระยา" },
                    { 70L, 17L, "สามเสนนอก" },
                    { 69L, 17L, "บางกะปิ" },
                    { 68L, 17L, "ห้วยขวาง" },
                    { 67L, 16L, "วัดท่าพระ" },
                    { 66L, 16L, "วัดอรุณ" },
                    { 65L, 15L, "สำเหร่" },
                    { 64L, 15L, "ดาวคะนอง" },
                    { 63L, 15L, "ตลาดพลู" },
                    { 75L, 19L, "คลองชักพระ" },
                    { 62L, 15L, "บุคคโล" },
                    { 90L, 22L, "บางจาก" },
                    { 92L, 22L, "คลองขวาง" },
                    { 118L, 31L, "วัดพระยาไกร" },
                    { 117L, 31L, "บางคอแหลม" },
                    { 116L, 30L, "จตุจักร" },
                    { 115L, 30L, "จอมพล" },
                    { 114L, 30L, "จันทรเกษม" },
                    { 113L, 30L, "เสนานิคม" },
                    { 112L, 30L, "ลาดยาว" },
                    { 111L, 29L, "วงศ์สว่าง" },
                    { 110L, 29L, "บางซื่อ" },
                    { 109L, 28L, "ทุ่งมหาเมฆ" },
                    { 108L, 28L, "ยานนาวา" },
                    { 107L, 28L, "ทุ่งวัดดอน" },
                    { 91L, 22L, "บางแวก" },
                    { 106L, 27L, "นวลจันทร์" },
                    { 104L, 27L, "คลองกุ่ม" },
                    { 103L, 26L, "ดินแดง" },
                    { 102L, 25L, "บางยี่ขัน" },
                    { 101L, 25L, "บางอ้อ" },
                    { 100L, 25L, "บางอ้อ" },
                    { 99L, 25L, "บางพลัด" },
                    { 98L, 24L, "บางปะกอก" },
                    { 97L, 24L, "ราษฎร์บูรณะ" },
                    { 96L, 23L, "หนองค้างพลู" },
                    { 95L, 23L, "หนองแขม" },
                    { 94L, 22L, "คูหาสวรรค์" },
                    { 93L, 22L, "ปากคลองภาษีเจริญ" },
                    { 105L, 27L, "นวมินทร์" },
                    { 61L, 15L, "บางยี่เรือ" },
                    { 60L, 15L, "หิรัญรูจี" },
                    { 59L, 15L, "วัดกัลยาณ์" },
                    { 27L, 4L, "สีลม" },
                    { 26L, 4L, "มหาพฤฒาราม" },
                    { 25L, 3L, "ลำต้อยติ่ง" },
                    { 24L, 3L, "ลำผักชี" },
                    { 23L, 3L, "คู้ฝั่งเหนือ" },
                    { 22L, 3L, "โคกแฝด" },
                    { 21L, 3L, "คลองสิบสอง" },
                    { 20L, 3L, "คลองสิบ" },
                    { 19L, 3L, "หนองจอก" },
                    { 18L, 3L, "กระทุ่มราย" },
                    { 17L, 2L, "ถนนนครไชยศรี" },
                    { 16L, 2L, "สี่แยกมหานาค" },
                    { 28L, 4L, "สุริยวงศ์" },
                    { 15L, 2L, "สวนจิตรลดา" },
                    { 13L, 2L, "ดุสิต" },
                    { 12L, 1L, "วัดสามพระยา" },
                    { 11L, 1L, "บางขุนพรหม" },
                    { 10L, 1L, "บ้านพานถม" },
                    { 9L, 1L, "ชนะสงคราม" },
                    { 8L, 1L, "ตลาดยอด" },
                    { 7L, 1L, "บวรนิเวศ" },
                    { 6L, 1L, "เสาชิงช้า" },
                    { 5L, 1L, "ศาลเจ้าพ่อเสือ" },
                    { 4L, 1L, "สำราญราษฎร์" },
                    { 3L, 1L, "วัดราชบพิธ" },
                    { 2L, 1L, "วังบูรพาภิรมย์" },
                    { 14L, 2L, "วชิรพยาบาล" },
                    { 29L, 4L, "บางรัก" },
                    { 30L, 4L, "สี่พระยา" },
                    { 31L, 5L, "อนุสาวรีย์" },
                    { 58L, 14L, "สามเสนใน" },
                    { 57L, 13L, "ตลาดน้อย" },
                    { 56L, 13L, "สัมพันธวงศ์" },
                    { 55L, 13L, "จักรวรรดิ" },
                    { 54L, 12L, "บางโพงพาง" },
                    { 53L, 12L, "ช่องนนทรี" },
                    { 52L, 11L, "ขุมทอง" },
                    { 51L, 11L, "ทับยาว" },
                    { 50L, 11L, "ลำปลาทิว" },
                    { 49L, 11L, "คลองสามประเวศ" },
                    { 48L, 11L, "คลองสองต้นนุ่น" },
                    { 47L, 11L, "ลาดกระบัง" },
                    { 46L, 10L, "แสนแสบ" },
                    { 45L, 10L, "มีนบุรี" },
                    { 44L, 9L, "บางจาก" },
                    { 43L, 8L, "วัดโสมนัส" },
                    { 42L, 8L, "บ้านบาตร" },
                    { 41L, 8L, "คลองมหานาค" },
                    { 40L, 8L, "วัดเทพศิรินทร์" },
                    { 39L, 8L, "ป้อมปราบ" },
                    { 38L, 7L, "ลุมพินี" },
                    { 37L, 7L, "ปทุมวัน" },
                    { 36L, 7L, "วังใหม่" },
                    { 35L, 7L, "รองเมือง" },
                    { 34L, 6L, "หัวหมาก" },
                    { 33L, 6L, "คลองจั่น" },
                    { 32L, 5L, "ท่าแร้ง" },
                    { 119L, 31L, "บางโคล่" },
                    { 120L, 32L, "ประเวศ" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 47L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 50L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 51L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 52L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 53L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 54L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 55L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 56L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 57L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 58L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 59L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 60L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 61L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 62L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 63L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 64L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 65L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 66L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 67L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 68L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 69L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 70L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 71L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 72L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 73L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 74L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 75L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 76L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 77L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 78L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 79L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 80L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 81L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 82L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 83L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 84L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 85L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 86L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 87L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 88L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 89L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 90L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 91L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 92L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 93L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 94L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 95L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 96L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 97L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 98L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 99L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 100L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 101L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 102L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 103L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 104L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 105L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 106L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 107L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 108L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 109L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 110L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 111L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 112L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 113L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 114L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 115L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 116L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 117L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 118L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 119L);

            migrationBuilder.DeleteData(
                table: "Subdistricts",
                keyColumn: "Id",
                keyValue: 120L);
        }
    }
}
