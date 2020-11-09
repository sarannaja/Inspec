using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel; //excel
using System.IO; //excel
using Microsoft.AspNetCore.Hosting;
using Xceed.Words.NET;
using Xceed.Document.NET;
using System.Drawing;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class DistrictController : Controller
    {
        private readonly ApplicationDbContext _context;
        public static IWebHostEnvironment _environment;
        public DistrictController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<District> Get()
        {
            var districtdata = from P in _context.Districts
                               select P;
            return districtdata;

           
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var districtdata = _context.Districts
                .Include(m => m.Province)
                .Where(m => m.ProvinceId == id);

            return Ok(districtdata);
        }

        [HttpPost]
        public District Post([FromForm] DistrictRequest request)
        {
            var date = DateTime.Now;
            Console.WriteLine("district 1 :" + request.Name + " : " + request.ProvincesId );
            var districtdata = new District
            {
                ProvinceId = request.ProvincesId,
                Name = request.Name,
            };
            Console.WriteLine("district 2 :");
            _context.Districts.Add(districtdata);
            _context.SaveChanges();
            Console.WriteLine("district 3 :");
            return districtdata;
        }

        [HttpPut("{id}")]
        public District Put([FromForm] DistrictRequest request, long id)
        {
            var districtdata = _context.Districts.Find(id);
            districtdata.Name = request.Name;
        
            _context.Entry(districtdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return districtdata;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var districtdata = _context.Districts.Find(id);

            _context.Districts.Remove(districtdata);
            _context.SaveChanges();
        }

        // <!-- excel -->
        [HttpGet("exceldistrict/{id}")]
        public IActionResult exceldistrict(long id)
        {

            var districtdata = _context.Districts
               .Include(m => m.Province)
               .Where(m => m.ProvinceId == id);

            var province_name = _context.Provinces
                .Where(m => m.Id == id)
                .Select(m => m.Name)
                .FirstOrDefault();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("ข้อมูลอำเภอ");
                var currentRow = 1;
                var currentRow2 = 2;

                worksheet.Cell(currentRow, 1).Value = "จังหวัด : "+ province_name;
                worksheet.Cell(currentRow, 2).Value = "";
                worksheet.Cell(currentRow, 3).Value = "";
                worksheet.Cell(currentRow2, 1).Value = "อำเภอ";

                foreach (var data in districtdata)
                {
                    currentRow2++;
                    worksheet.Cell(currentRow2, 1).Value = data.Name;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Districts.xlsx");
                }
            }
        }
        // <!-- END excel -->

        [HttpGet("worddistrict/{id}")] // 9.5.9 (1)รายข้อสั่งการผู้บริหาร
        public IActionResult worddistrict(long id)
        {

            var districtdata = _context.Districts
             .Include(m => m.Province)
             .Where(m => m.ProvinceId == id).ToList();

            var province_name = _context.Provinces
                .Where(m => m.Id == id)
                .Select(m => m.Name)
                .FirstOrDefault();



            System.Console.WriteLine("1 : ");

            if (!Directory.Exists(_environment.WebRootPath + "//reportdistrict//")) //ถ้ามีไฟล์อยู่แล้ว
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//reportdistrict//"); //สร้าง Folder reportexecutive ใน wwwroot
            }

            var filePath = _environment.WebRootPath + "/reportdistrict/"; // เก็บไฟล์ logo 
            var filename = "district" + ".docx"; // ชื่อไฟล์
            var createfile = filePath + filename; //
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("2");

            using (DocX document = DocX.Create(createfile)) //สร้าง
            {


                System.Console.WriteLine("3");

                // Add a title
                document.InsertParagraph("ข้อมูลอำเภอของจังหวัด : " + province_name).FontSize(16d)
                    .SpacingBefore(15d)
                    .SpacingAfter(15d)
                    .Bold() //ตัวหนา
                    .Alignment = Alignment.center;



                int dataCount = 0; 
                dataCount = districtdata.Count; //เอาที่ select มาใช้
                dataCount += 1;
                System.Console.WriteLine("Data Count: " + dataCount);
                // Add a table in a document of 1 row and 3 columns.
                var columnWidths = new float[] { 300f, 300f };
                var t = document.InsertTable(dataCount, columnWidths.Length);

                System.Console.WriteLine("4");

                // Set the table's column width and background 
                t.SetWidths(columnWidths);
                t.AutoFit = AutoFit.Contents;

                var row = t.Rows.First();

                row.Cells[0].Paragraphs.First().Append("ลำดับที่");
                row.Cells[1].Paragraphs.First().Append("ภาค");


                // Add rows in the table.
                int j = 0;
                for (int i = 0; i < districtdata.Count; i++)
                {
                    j += 1;

                    System.Console.WriteLine("5: " + j);
                    t.Rows[j].Cells[0].Paragraphs[0].Append(j.ToString());
                    t.Rows[j].Cells[1].Paragraphs[0].Append(districtdata[i].Name.ToString());



                }

                // Set a blank border for the table's top/bottom borders.
                var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                //t.SetBorder(TableBorderType.Bottom, blankBorder);
                //t.SetBorder(TableBorderType.Top, blankBorder);

                System.Console.WriteLine("6");
                document.Save(); //save เอกสาร
                Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");

                return Ok(new { data = filename });
            }
        }
    }
}

public class DistrictRequest
{
    public long Id { get; set; }
    public long ProvincesId { get; set; }
    public string Name { get; set; }
  
}
