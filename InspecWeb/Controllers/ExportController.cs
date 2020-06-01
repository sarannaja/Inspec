using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xceed.Document.NET;
using Xceed.Words.NET;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class ExportController : Controller
    {
        public static IWebHostEnvironment _environment;


        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private readonly ApplicationDbContext _context;

        public ExportController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/values
        [HttpPost("report")]
        public object Post([FromBody] ExportReportViewModel model)
        {
            List<object> termsList = new List<object>();
            ArrayList myAL = new ArrayList();

            var ebook = _context.ElectronicBookGroups
                .Include(x => x.CentralPolicyProvince)
                .ThenInclude(x => x.Province)
                .Include(x => x.CentralPolicyProvince)
                .ThenInclude(x => x.CentralPolicy)
                .ThenInclude(x => x.CentralPolicyUser)
                .Include(x => x.ElectronicBook)
                .Where(x => x.ElectronicBook.CreatedBy == model.userId && x.ElectronicBook.ElectronicBookFiles.Any(x => x.Type == "SignatureProvince File"))
                .ToList();

            foreach (var ebookData in ebook)
            {
                var centralpolicydata = _context.CentralPolicies
              .Include(m => m.CentralPolicyProvinces)
              .ThenInclude(x => x.Province)
              .Include(m => m.CentralPolicyDates)
              .Include(m => m.CentralPolicyFiles)
              .Include(m => m.CentralPolicyProvinces)
              .ThenInclude(x => x.SubjectCentralPolicyProvinces)
              .Where(m => m.Id == ebookData.CentralPolicyProvince.CentralPolicyId)

              .Select(x => x.CentralPolicyProvinces)
              .FirstOrDefault();

                termsList.Add(centralpolicydata);
            }

            return Ok(termsList);
        }


        public void CreateReport(List<object> centralpolicydata, string typeId)
        {
            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "/Uploads/";
            var filename = "DOC" + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + ".docx";
            var createfile = filePath + filename;
            var myImageFullPath = filePath + "logo01.png";

            if (typeId == "1")
            {
                System.Console.WriteLine("in create");
                using (DocX document = DocX.Create(createfile))
                {
                    Image image = document.AddImage(myImageFullPath);
                    Picture picture = image.CreatePicture(90, 90);
                    var logo = document.InsertParagraph();
                    logo.AppendPicture(picture).Alignment = Alignment.left;

                    // Add a title
                    document.InsertParagraph("Columns width").FontSize(15d).SpacingAfter(50d).Alignment = Alignment.center;

                    // Insert a title paragraph.
                    var p = document.InsertParagraph("In the following table, the cell's left margin has been removed for rows 2-6 as well as the top/bottom table's borders.").Bold();
                    p.Alignment = Alignment.center;
                    p.SpacingAfter(40d);

                    // Add a table in a document of 1 row and 3 columns.
                    var columnWidths = new float[] { 200f, 200f, 200f, 200f, 200f, 200f, 200f };
                    var t = document.InsertTable(1, columnWidths.Length);

                    // Set the table's column width and background 
                    t.SetWidths(columnWidths);
                    t.AutoFit = AutoFit.Contents;

                    var row = t.Rows.First();

                    // Fill in the columns of the first row in the table.
                    //for (int i = 0; i < row.Cells.Count; ++i)
                    //{
                    row.Cells[0].Paragraphs.First().Append("ประเด็นการตรวจติดตาม");
                    row.Cells[1].Paragraphs.First().Append("ข้อค้นพบ/ประเด็นปัญหา/ผลการตรวจ");
                    row.Cells[2].Paragraphs.First().Append("หน่วยรับตรวจ/หน่วยงานที่รับผิดชอบ");
                    row.Cells[3].Paragraphs.First().Append("ข้อเสนอแนะของผู้ตรวจราชการ");
                    row.Cells[4].Paragraphs.First().Append("รายงานผลการดำเนินการของหน่วยรับตรวจ");
                    row.Cells[5].Paragraphs.First().Append("เอกสารแนบ (ไฟล์)");
                    row.Cells[6].Paragraphs.First().Append("ความเห็นของ ที่ปรึกษา ผต.ภาคประชาชน");

                    //}

                    // Add rows in the table.
                    for (int i = 0; i < 5; i++)
                    {
                        var newRow = t.InsertRow();

                        // Fill in the columns of the new rows.
                        for (int j = 0; j < newRow.Cells.Count; ++j)
                        {
                            var newCell = newRow.Cells[j];
                            newCell.Paragraphs.First().Append("test" + i);
                            // Remove the left margin of the new cells.
                            newCell.MarginLeft = 0;
                        }
                    }

                    // Set a blank border for the table's top/bottom borders.
                    var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                    //t.SetBorder(TableBorderType.Bottom, blankBorder);
                    //t.SetBorder(TableBorderType.Top, blankBorder);

                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
                }
            }

            //return Ok(new { data = filename });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
