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
    public class ProvinceController : Controller
    {
        private readonly ApplicationDbContext _context;
        public static IWebHostEnvironment _environment;
        public ProvinceController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            //<!-- ห้ามลบ -->
            //var message = new Message(new string[] { "mrbuctico@gmail.com" }, "Test email", "This is the content from our email.");
            //_emailSender.SendEmail(message);
            //<!-- END ห้ามลบ -->

            var provincedata = _context.Provinces
                             .Include(p => p.Sectors)
                             .Include(p => p.ProvincesGroups);
            return Ok(provincedata);


        }

        [HttpGet("centralpolicyprovinces")]
        public IActionResult Get2()
        {
            var provincedata = _context.Provinces;

            return Ok(provincedata);
        }
        // GET: api/values
        [HttpGet("getonlyprovince")]
        public IActionResult Getonlyprovince()
        {


            var provincedata = _context.Provinces
                .ToList();
            return Ok(provincedata);


        }

        //<!-- Get ภาค 20200720 -->
        [HttpGet("getsectordata")]
        public IActionResult getsectordata()
        {
            var sectordata = _context.Sectors;

            return Ok(sectordata);
        }
        //<!-- END Get ภาค 20200720 -->

        //<!-- Get กลุ่มจังหวัด 20200720 -->
        [HttpGet("getprovincegroupdata")]
        public IActionResult getprovincegroupdata()
        {
            var provincegroupdata = _context.ProvincesGroups;

            return Ok(provincegroupdata);
        }
        //<!-- END Get กลุ่มจังหวัด 20200720 -->

        // POST api/values
        [HttpPost]
        public Province Post([FromForm] ProviceRequest request)
        {
            var date = DateTime.Now;
           // Console.WriteLine("province 1 :" + request.SectorId + " : " + request.Link + " : " + request.Name);
            var provincedata = new Province
            {
                Name = request.Name,
                NameEN = request.NameEN,
                Link = request.Link,
                SectorId = request.SectorId,
                ProvincesGroupId = request.ProvincesGroupId,
                ShortnameEN = request.ShortnameEN,
                ShortnameTH = request.ShortnameTH,
                CreatedAt = date
            };
           // Console.WriteLine("province 2 :" + request.ProvincesGroupId);
            _context.Provinces.Add(provincedata);
            _context.SaveChanges();
           // Console.WriteLine("province 3 :");
            return provincedata;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Province Put([FromForm] ProviceRequest request, long id)
        {
            var province = _context.Provinces.Find(id);
            province.Name = request.Name;
            province.Link = request.Link;
            province.SectorId = request.SectorId;
            province.ProvincesGroupId = request.ProvincesGroupId;
            province.NameEN = request.NameEN;
            province.ShortnameEN = request.ShortnameEN;
            province.ShortnameTH = request.ShortnameTH;
            _context.Entry(province).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return province;


        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var provincedata = _context.Provinces.Find(id);

            _context.Provinces.Remove(provincedata);
            _context.SaveChanges();
        }

        // <!-- excel -->
        [HttpGet("excelprovince")]
        public IActionResult excelprovince()
        {

            var provincedata = _context.Provinces
                              .Include(p => p.Sectors)
                              .Include(p => p.ProvincesGroups);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("ข้อมูลจังหวัด");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ภาค";
                worksheet.Cell(currentRow, 2).Value = "กลุ่มจังหวัด";
                worksheet.Cell(currentRow, 3).Value = "จังหวัด";
                foreach (var provinces in provincedata)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = provinces.Sectors.Name;
                    worksheet.Cell(currentRow, 2).Value = provinces.ProvincesGroups.Name;
                    worksheet.Cell(currentRow, 3).Value = provinces.Name;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "province.xlsx");
                }
            }
        }
        // <!-- END excel -->

        [HttpGet("wordprovince")] // 9.5.9 (1)รายข้อสั่งการผู้บริหาร
        public IActionResult wordprovince()
        {

            var provincedata = _context.Provinces
                             .Include(p => p.Sectors)
                             .Include(p => p.ProvincesGroups).ToList();



            System.Console.WriteLine("1 : " );

            if (!Directory.Exists(_environment.WebRootPath + "//reportprovince//")) //ถ้ามีไฟล์อยู่แล้ว
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//reportprovince//"); //สร้าง Folder reportexecutive ใน wwwroot
            }

            var filePath = _environment.WebRootPath + "/reportprovince/"; // เก็บไฟล์ logo 
            var filename = "province" + ".docx"; // ชื่อไฟล์
            var createfile = filePath + filename; //
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("2");
      
            using (DocX document = DocX.Create(createfile)) //สร้าง
            {


                System.Console.WriteLine("3");

                // Add a title
                document.InsertParagraph("ข้อมูลจังหวัด").FontSize(16d)
                    .SpacingBefore(15d)
                    .SpacingAfter(15d)
                    .Bold() //ตัวหนา
                    .Alignment = Alignment.center;

             

                int dataCount = 0;
                dataCount = provincedata.Count; //เอาที่ select มาใช้
                dataCount += 1;
                System.Console.WriteLine("Data Count: " + dataCount);
                // Add a table in a document of 1 row and 3 columns.
                var columnWidths = new float[] { 300f, 300f, 300f, 300f };
                var t = document.InsertTable(dataCount, columnWidths.Length);

                System.Console.WriteLine("4");

                // Set the table's column width and background 
                t.SetWidths(columnWidths);
                t.AutoFit = AutoFit.Contents;

                var row = t.Rows.First();

                row.Cells[0].Paragraphs.First().Append("ลำดับที่");
                row.Cells[1].Paragraphs.First().Append("ภาค");
                row.Cells[2].Paragraphs.First().Append("กลุ่มจังหวัด");
                row.Cells[3].Paragraphs.First().Append("จังหวัด");

          
                // Add rows in the table.
                int j = 0;
                for (int i = 0; i < provincedata.Count; i++)
                {
                    j += 1;
               
                    System.Console.WriteLine("5: " + j);
                    t.Rows[j].Cells[0].Paragraphs[0].Append(j.ToString());
                    t.Rows[j].Cells[1].Paragraphs[0].Append(provincedata[i].Sectors.Name.ToString());  
                    t.Rows[j].Cells[2].Paragraphs[0].Append(provincedata[i].ProvincesGroups.Name.ToString());
                    t.Rows[j].Cells[3].Paragraphs[0].Append(provincedata[i].Name.ToString());


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

public class ProviceRequest
{
    public long Id { get; set; }
    public long SectorId { get; set; }
    public long ProvincesGroupId { get; set; }
    public string Name { get; set; }
    public string Link { get; set; }
    public string NameEN { get; set; }

    public string ShortnameEN { get; set; }
    public string ShortnameTH { get; set; }
}
