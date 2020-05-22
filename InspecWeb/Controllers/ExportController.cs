using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{id}")]
        public void Get(int id)
        {
            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "/Uploads/";

            var myImageFullPath = filePath + "logo01.png";

            var electornicbook = _context.CentralPolicies.Where(x => x.Id == 1).FirstOrDefault();
            var electornicbookgroup = _context.ElectronicBookGroups.Where(x => x.CentralPolicyProvinceId == 1).FirstOrDefault();
            var detailbook = _context.ElectronicBooks.Where(x => x.Id == electornicbookgroup.ElectronicBookId).FirstOrDefault();
            var groupidfile = _context.CentralPolicyUsers.Where(x => x.CentralPolicyId == 1 && x.ElectronicBookId == 2).FirstOrDefault();
            var file = _context.CentralPolicyUserFiles.Where(x => x.CentralPolicyGroupId == groupidfile.CentralPolicyGroupId).Select(m => m.Name).ToArray();


            if (id == 1)
            {
                using (DocX document = DocX.Create(filePath + "docx" + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + ".docx"))
                {

                    Image image = document.AddImage(myImageFullPath);
                    Image image2 = document.AddImage(filePath + file[0]);
                    Picture picture = image.CreatePicture(90, 90);
                    Picture picture2 = image2.CreatePicture(50, 50);
                    Picture picture3 = image2.CreatePicture(90, 90);
                    var i = document.InsertParagraph();
                    i.AppendPicture(picture).Alignment = Alignment.left;
                    //picture.TextWrappingStyle = TextWrappingStyle.Square;
                    var subject = document.InsertParagraph();
                    subject.Append(electornicbook.Title).FontSize(18).Alignment = Alignment.center;
                    subject.SpacingAfter(40d);

                    var i3 = document.InsertParagraph();
                    i3.AppendPicture(picture3).Alignment = Alignment.center;
                    i3.SpacingAfter(40d);

                    var detail = document.InsertParagraph();

                    detail.Append(detailbook.Detail).FontSize(14).SpacingAfter(30d).Alignment = Alignment.center;
                    detail.SpacingAfter(40d);

                    var problem = document.InsertParagraph();

                    problem.Append("ปัญหาและอุปสรรค").FontSize(16);
                    problem.SpacingAfter(20d);

                    var problemdetail = document.InsertParagraph();

                    problemdetail.Append(detailbook.Problem);

                    var Suggestion = document.InsertParagraph();

                    Suggestion.Append("คำแนะนำ").FontSize(16);
                    problem.SpacingAfter(20d);

                    var Suggestiondetail = document.InsertParagraph();

                    Suggestiondetail.Append(detailbook.Suggestion);

                    //var i4 = document.InsertParagraph();

                    //i4.AppendPicture(picture3).Alignment = Alignment.center;
                    //i4.AppendPicture(picture3).Alignment = Alignment.center;

                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");

                }
            }
            else if (id == 2)
            {
                using (DocX document = DocX.Create(filePath + "docx" + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + ".docx"))
                {

                    Image image = document.AddImage(myImageFullPath);
                    Image image2 = document.AddImage(filePath + file[0]);
                    Picture picture = image.CreatePicture(90, 90);
                    Picture picture2 = image2.CreatePicture(50, 50);
                    Picture picture3 = image2.CreatePicture(90, 90);
                    var i = document.InsertParagraph();
                    i.AppendPicture(picture).Alignment = Alignment.left;
                    //picture.TextWrappingStyle = TextWrappingStyle.Square;
                    var subject = document.InsertParagraph();
                    subject.Append(electornicbook.Title).FontSize(18).Alignment = Alignment.center;
                    subject.SpacingAfter(40d);
                    var i3 = document.InsertParagraph();
                    i3.AppendPicture(picture3).Alignment = Alignment.center;
                    i3.SpacingAfter(40d);
                    var detail = document.InsertParagraph();
                    detail.Append(detailbook.Detail).FontSize(14).SpacingAfter(30d).Alignment = Alignment.center;
                    detail.SpacingAfter(40d);

                    var ix = document.InsertParagraph();

                    if (file.Length > 2)
                    {
                        for (var j = 1; j <= 2; j++)
                        {
                            Image image3 = document.AddImage(filePath + file[j]);
                            Picture picture4 = image3.CreatePicture(90, 90);
                            ix.AppendPicture(picture4).Alignment = Alignment.center;
                        }
                    }
                    //i4.AppendPicture(picture3).Alignment = Alignment.center;
                    //i4.AppendPicture(picture3).Alignment = Alignment.center;
                    //i4.AppendPicture(picture3).Alignment = Alignment.center;
                    //i4.AppendPicture(picture3).Alignment = Alignment.center;

                    //var subject = document.InsertParagraph();
                    //subject.AppendLine("It contains an image added from disk.").Alignment = Alignment.center;
                    //i.SpacingAfter(50);



                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");

                }

            }
            else if (id == 3)
            {
                using (DocX document = DocX.Create(filePath + "docx" + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + ".docx"))
                {

                    Image image = document.AddImage(myImageFullPath);
                    Image image2 = document.AddImage(filePath + file[0]);


                    Picture picture = image.CreatePicture(90, 90);
                    Picture picture2 = image2.CreatePicture(50, 50);
                    Picture picture3 = image2.CreatePicture(150, 150);
                    var i = document.InsertParagraph();
                    i.AppendPicture(picture).Alignment = Alignment.left;
                    //picture.TextWrappingStyle = TextWrappingStyle.Square;
                    var subject = document.InsertParagraph();
                    subject.Append(electornicbook.Title).FontSize(18).Alignment = Alignment.center;
                    subject.SpacingAfter(40d);
                    var i3 = document.InsertParagraph();
                    i3.AppendPicture(picture3).Alignment = Alignment.center;
                    i3.SpacingAfter(40d);
                    var detail = document.InsertParagraph();
                    detail.Append(detailbook.Detail).FontSize(14).SpacingAfter(30d).Alignment = Alignment.center;
                    detail.SpacingAfter(40d);


                    var ix = document.InsertParagraph();


                    foreach (var path in file)
                    {

                        Image image3 = document.AddImage(filePath + path);
                        Picture picture4 = image3.CreatePicture(90, 90);
                        ix.AppendPicture(picture4).Alignment = Alignment.center;
                        //ix.Append(path);

                    }
                    //i4.AppendPicture(picture3).Alignment = Alignment.center;
                    //i4.AppendPicture(picture3).Alignment = Alignment.center;
                    //i4.AppendPicture(picture3).Alignment = Alignment.center;

                    //var subject = document.InsertParagraph();
                    //subject.AppendLine("It contains an image added from disk.").Alignment = Alignment.center;
                    //i.SpacingAfter(50);



                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");

                }
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
