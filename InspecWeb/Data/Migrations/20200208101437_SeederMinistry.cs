using Microsoft.EntityFrameworkCore.Migrations;

namespace InspecWeb.Data.Migrations
{
    public partial class SeederMinistry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Ministries",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Name",
                value: "สำนักนายกรัฐมนตรี");

            migrationBuilder.UpdateData(
                table: "Ministries",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Name",
                value: "กระทรวงกลาโหม");

            migrationBuilder.InsertData(
                table: "Ministries",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { 19L, null, "กระทรวงอุตสาหกรรม" },
                    { 18L, null, "กระทรวงสาธารณสุข" },
                    { 17L, null, "กระทรวงการอุดมศึกษา วิทยาศาสตร์ วิจัยและนวัตกรรม" },
                    { 16L, null, "กระทรวงวัฒนธรรม" },
                    { 15L, null, "กระทรวงแรงงาน" },
                    { 14L, null, "กระทรวงยุติธรรม" },
                    { 13L, null, "กระทรวงมหาดไทย" },
                    { 12L, null, "กระทรวงพาณิชย์" },
                    { 10L, null, "กระทรวงดิจิทัลเพื่อเศรษฐกิจและสังคม" },
                    { 9L, null, "กระทรวงทรัพยากรธรรมชาติและสิ่งแวดล้อม" },
                    { 8L, null, "กระทรวงคมนาคม" },
                    { 7L, null, "กระทรวงเกษตรและสหกรณ์" },
                    { 6L, null, "กระทรวงการพัฒนาสังคมและความมั่นคงของมนุษย์" },
                    { 5L, null, "กระทรวงการท่องเที่ยวและกีฬา" },
                    { 4L, null, "กระทรวงการต่างประเทศ" },
                    { 11L, null, "กระทรวงพลังงาน" },
                    { 3L, null, "กระทรวงการคลัง" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Ministries",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Ministries",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Ministries",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Ministries",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Ministries",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Ministries",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Ministries",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Ministries",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Ministries",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Ministries",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Ministries",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Ministries",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Ministries",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Ministries",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Ministries",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Ministries",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Ministries",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.UpdateData(
                table: "Ministries",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Name",
                value: "มหาดไทย");

            migrationBuilder.UpdateData(
                table: "Ministries",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Name",
                value: "สาธาระณะสุข");
        }
    }
}
