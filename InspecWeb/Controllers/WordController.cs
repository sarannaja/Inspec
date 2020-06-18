using System;
using System.Collections.Generic;
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
    public class WordController : Controller
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

        public WordController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        // GET: api/values
        [HttpGet]
        public object Get() {


            return "asasa";
            //var result = new List<WordfileViewModel>();
            //var CentralPolicyProvince = _context.CentralPolicyProvinces.Where(x => x.Id == 1).FirstOrDefault();
            ////return CentralPolicyProvince;
            //var subject2 = _context.SubjectCentralPolicyProvinces.Where(x => x.CentralPolicyProvinceId == 1 && x.Type == "noMaster").ToList();
            ////return subject2;
            ////return subject2;
            //var electornicbook = _context.CentralPolicies.Where(x => x.Id == 1).FirstOrDefault();
            //var electornicbookgroup = _context.ElectronicBookGroups.Where(x => x.CentralPolicyProvinceId == 1).FirstOrDefault();
            //var detailbook = _context.ElectronicBooks.Where(x => x.Id == 1).FirstOrDefault();




            ////return detailbook;
            //var groupidfile = _context.CentralPolicyUsers.Where(x => x.CentralPolicyId == CentralPolicyProvince.CentralPolicyId && x.ElectronicBookId == 1).ToList();


            //foreach (var data in groupidfile)
            //{

            //    var file = _context.CentralPolicyUserFiles.Where(x => x.CentralPolicyGroupId == data.CentralPolicyGroupId).Where(x=>x.Type == "CentralPolicyUser Image File").Select(m => m.Name).ToArray();

            //    result.Add(new WordfileViewModel
            //    {
            //        Name = file

            //    });

            //}

            ////foreach =
            ////var file = _context.CentralPolicyUserFiles.Where(x => x.CentralPolicyGroupId == groupidfile.CentralPolicyGroupId).Select(m => m.Name).ToList();


            ////return result;
            //return result;


        }

        // GET api/values/5
        [HttpGet("{id}")]
        public object Get(int id)
        {

          
            return Ok("1212lklkl;k;lskl");

        }

        // POST api/values
        [HttpPost]
        public object Post([FromBody]WordViewModel model)
        {
            System.Console.WriteLine("id" + model.id);
            System.Console.WriteLine("ProvinId" + model.ProvinId);
            System.Console.WriteLine("elecId"+ model.elecId);

            var result = new List<WordsubjectViewModel>();
            var files = new List<WordfileViewModel>();

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "/Uploads/";
            var filename = "DOC" + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + ".docx";
            var createfile = filePath + filename;
            var myImageFullPath = filePath + "logo01.png";
            var CentralPolicyProvince = _context.CentralPolicyProvinces.Where(x => x.Id == model.ProvinId).FirstOrDefault();
            //return CentralPolicyProvince;
            var subject2 = _context.SubjectCentralPolicyProvinces.Where(x => x.CentralPolicyProvinceId == CentralPolicyProvince.Id && x.Type == "noMaster").ToList();
            //return subject2;
            //return subject2;
            var electornicbook = _context.CentralPolicies.Where(x => x.Id == CentralPolicyProvince.CentralPolicyId).FirstOrDefault();
            var electornicbookgroup = _context.ElectronicBookGroups.Where(x => x.CentralPolicyProvinceId == CentralPolicyProvince.ProvinceId).FirstOrDefault();
            var detailbook = _context.ElectronicBooks.Where(x => x.Id == electornicbookgroup.ElectronicBookId).FirstOrDefault();
            ////return detailbook;
            var groupidfile = _context.CentralPolicyUsers.Where(x => x.CentralPolicyId == CentralPolicyProvince.CentralPolicyId).FirstOrDefault();
            //return groupidfile;
            var file = _context.CentralPolicyUserFiles.Where(x => x.CentralPolicyGroupId == groupidfile.CentralPolicyGroupId).Select(m => m.Name).ToArray();
            var description = _context.CentralPolicyUserFiles.Where(x => x.CentralPolicyGroupId == groupidfile.CentralPolicyGroupId).Select(m => m.Description).ToArray();
            //var groupidfile = _context.CentralPolicyUsers.Where(x => x.CentralPolicyId == CentralPolicyProvince.CentralPolicyId && x.ElectronicBookId == 1).ToList();

            System.Console.WriteLine("benz" + file.Length);
            //foreach (var data in groupidfile)
            //{

            //    var file = _context.CentralPolicyUserFiles.Where(x => x.CentralPolicyGroupId == data.CentralPolicyGroupId).Select(m => m.Name).ToArray();

            //    files.Add(new WordfileViewModel
            //    {
            //        Name = file

            //    });

            //}
            //return file;

            var index = 0;

            foreach (var sub in subject2)
            {
                var subjectsugest = _context.ElectronicBookSuggestGroups.Where(x => x.SubjectCentralPolicyProvinceId == sub.Id).FirstOrDefault();

                result.Add(new WordsubjectViewModel
                {
                    Name = subjectsugest.SubjectCentralPolicyProvince.Name , 
                    Detail = subjectsugest.Detail,
                    Problem = subjectsugest.Problem , 
                    Suggestion = subjectsugest.Suggestion

                });

            }

         


          
            ////return file;


            if (model.id == 1)
            {
                using (DocX document = DocX.Create(createfile))
                {

                    Image image = document.AddImage(myImageFullPath);

                    //Image image2 = document.AddImage(filePath + file[0]);
                    Picture picture = image.CreatePicture(90, 90);
                    
                    //Picture picture3 = image2.CreatePicture(150, 300);
                    var i = document.InsertParagraph();
                    i.AppendPicture(picture).Alignment = Alignment.left;
                    //picture.TextWrappingStyle = TextWrappingStyle.Square;
                    //var i3 = document.InsertParagraph();

                    //foreach (var file in files)
                    //{
                    //    for (var k = 0; k < file.Name.Length; k++)
                    //    {
                    //        Image image2 = document.AddImage(filePath + file.Name[k]);
                    //        Picture picture10 = image2.CreatePicture(90, 90);

                    //        i3.AppendPicture(picture10).Alignment = Alignment.center;
                    //        i3.SpacingAfter(20d);
                    //    }
                    //}

                


                    // หัวข้อสมุดตรวจ
                    var subject = document.InsertParagraph();
                    subject.Append(electornicbook.Title).FontSize(18).Alignment = Alignment.center;
                    subject.SpacingAfter(40d);



                    // รูปภาพสมุดตรวจรูปแรก
                    //var i3 = document.InsertParagraph();
                    //i3.AppendPicture(picture3).Alignment = Alignment.center;
                    //i3.SpacingAfter(40d);


                  
                    // รายละเอียดสมุดตรวจอิเล็กทรอนิดส์
                    foreach (var data in result)
                    {
                      
                        index = index + 1;
                      
                        var detail = document.InsertParagraph();
                        detail.Append(index + ". " + data.Name).FontSize(20).Alignment = Alignment.left;
                        detail.Bold();
                        detail.SpacingAfter(10d);


                        // หัวข้อปัญหาและอุปสรรค
                        var problem = document.InsertParagraph();
                        problem.Append("ปัญหาและอุปสรรค").FontSize(16).SpacingAfter(20d).Alignment = Alignment.center;
                        problem.Bold();
                        problem.SpacingAfter(5d);
                       

                        //รายละเอียดปัญหาและอุปสรรค
                        var problemdetail = document.InsertParagraph();
                        problemdetail.Append(data.Problem).FontSize(16).SpacingAfter(5d);


                        // หัวข้อคำแนะนำ
                        var Suggestion = document.InsertParagraph();
                        Suggestion.Append("คำแนะนำ").FontSize(16).SpacingAfter(20d).Alignment = Alignment.center;
                        Suggestion.Bold();
                        // รายละเอียดคำแนะนำ
                        var Suggestiondetail = document.InsertParagraph();
                        Suggestiondetail.Append(data.Suggestion).SpacingAfter(5d).FontSize(16);

                        var iix = document.InsertParagraph();
                        iix.Append("ประเด็นที่ตรวจ").FontSize(16).SpacingAfter(5d).Alignment = Alignment.center;
                        iix.Bold();

                        var ii = document.InsertParagraph();
                        ii.Append(data.Detail).FontSize(16);

                        ii.InsertHorizontalLine(HorizontalBorderPosition.bottom, BorderStyle.Tcbs_single);
                        ii.SpacingAfter(20d);
                    }





                    //var i4 = document.InsertParagraph();

                    //i4.AppendPicture(picture3).Alignment = Alignment.center;
                    //i4.AppendPicture(picture3).Alignment = Alignment.center;

                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");

                }


            }
            else if (model.id == 2)
            {
                using (DocX document = DocX.Create(createfile))
                {

                    Image image = document.AddImage(myImageFullPath);

                    //Image image2 = document.AddImage(filePath + file[0]);
                    Picture picture = image.CreatePicture(90, 90);

                    //Picture picture3 = image2.CreatePicture(150, 300);
                    var i = document.InsertParagraph();
                    i.AppendPicture(picture).Alignment = Alignment.left;
                    //picture.TextWrappingStyle = TextWrappingStyle.Square;

                    var i3 = document.InsertParagraph();
                    Image image2 = document.AddImage(filePath + file[0]);
                    Picture picture10 = image2.CreatePicture(150, 300);

                    i3.AppendPicture(picture10).Alignment = Alignment.center;
                    i3.SpacingAfter(20d);

                    // หัวข้อสมุดตรวจ
                    var subject = document.InsertParagraph();
                    subject.Append(electornicbook.Title).FontSize(18).Alignment = Alignment.center;
                    subject.SpacingAfter(40d);



                    // รูปภาพสมุดตรวจรูปแรก
                    //var i3 = document.InsertParagraph();
                    //i3.AppendPicture(picture3).Alignment = Alignment.center;
                    //i3.SpacingAfter(40d);



                    // รายละเอียดสมุดตรวจอิเล็กทรอนิดส์
                    foreach (var data in result)
                    {

                        index = index + 1;

                        var detail = document.InsertParagraph();
                        detail.Append(index + ". " + data.Name).FontSize(20).Alignment = Alignment.left;
                        detail.Bold();
                        detail.SpacingAfter(10d);


                        // หัวข้อปัญหาและอุปสรรค
                        var problem = document.InsertParagraph();
                        problem.Append("ปัญหาและอุปสรรค").FontSize(16).SpacingAfter(20d).Alignment = Alignment.center;
                        problem.Bold();
                        problem.SpacingAfter(5d);


                        //รายละเอียดปัญหาและอุปสรรค
                        var problemdetail = document.InsertParagraph();
                      
                        problemdetail.Append(data.Problem).FontSize(16).SpacingAfter(5d);
                     


                        // หัวข้อคำแนะนำ
                        var Suggestion = document.InsertParagraph();
                        Suggestion.Append("คำแนะนำ").FontSize(16).SpacingAfter(20d).Alignment = Alignment.center;
                        Suggestion.Bold();
                        // รายละเอียดคำแนะนำ
                        var Suggestiondetail = document.InsertParagraph();
                        Suggestiondetail.Append(data.Suggestion).SpacingAfter(5d).FontSize(16);

                        var iix = document.InsertParagraph();
                        iix.Append("ประเด็นที่ตรวจ").FontSize(16).SpacingAfter(5d).Alignment = Alignment.center;
                        iix.Bold();

                        var ii = document.InsertParagraph();
                        ii.Append(data.Detail).FontSize(16);

                        ii.InsertHorizontalLine(HorizontalBorderPosition.bottom, BorderStyle.Tcbs_single);
                        ii.SpacingAfter(20d);
                    }





                    //var i4 = document.InsertParagraph();

                    //i4.AppendPicture(picture3).Alignment = Alignment.center;
                    //i4.AppendPicture(picture3).Alignment = Alignment.center;

                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");

                }

            }
            else if (model.id == 3)
            {
                using (DocX document = DocX.Create(createfile))
                {

                    Image image = document.AddImage(myImageFullPath);

                    //Image image2 = document.AddImage(filePath + file[0]);
                    Picture picture = image.CreatePicture(90, 90);

                    //Picture picture3 = image2.CreatePicture(150, 300);
                    var i = document.InsertParagraph();
                    i.AppendPicture(picture).Alignment = Alignment.left;
                    //picture.TextWrappingStyle = TextWrappingStyle.Square;


                    var i3 = document.InsertParagraph();
                    Image image2 = document.AddImage(filePath + file[0]);
                    Picture picture10 = image2.CreatePicture(150, 300);

                    i3.AppendPicture(picture10).Alignment = Alignment.center;
                    i3.SpacingAfter(20d);


                    var i4 = document.InsertParagraph();

                    if (file.Length > 2)
                    {
                        for (var j = 1; j < 3; j++)
                        {
                           
                                Image image3 = document.AddImage(filePath + file[j]);
                                Picture picture111 = image3.CreatePicture(90, 90);

                                i4.AppendPicture(picture111).Alignment = Alignment.center;
                                i4.SpacingAfter(20d);
                            
                        }
                    }
                    

                    // หัวข้อสมุดตรวจ
                    var subject = document.InsertParagraph();
                    subject.Append(electornicbook.Title).FontSize(18).Alignment = Alignment.center;
                    subject.SpacingAfter(40d);



                    // รูปภาพสมุดตรวจรูปแรก
                    //var i3 = document.InsertParagraph();
                    //i3.AppendPicture(picture3).Alignment = Alignment.center;
                    //i3.SpacingAfter(40d);



                    // รายละเอียดสมุดตรวจอิเล็กทรอนิดส์
                    foreach (var data in result)
                    {

                        index = index + 1;

                        var detail = document.InsertParagraph();
                        detail.Append(index + ". " + data.Name).FontSize(20).Alignment = Alignment.left;
                        detail.Bold();
                        detail.SpacingAfter(10d);


                        // หัวข้อปัญหาและอุปสรรค
                        var problem = document.InsertParagraph();
                        problem.Append("ปัญหาและอุปสรรค").FontSize(16).SpacingAfter(20d).Alignment = Alignment.center;
                        problem.Bold();
                        problem.SpacingAfter(5d);


                        //รายละเอียดปัญหาและอุปสรรค
                        var problemdetail = document.InsertParagraph();
                        problemdetail.Append(data.Problem).FontSize(16).SpacingAfter(5d);


                        // หัวข้อคำแนะนำ
                        var Suggestion = document.InsertParagraph();
                        Suggestion.Append("คำแนะนำ").FontSize(16).SpacingAfter(20d).Alignment = Alignment.center;
                        Suggestion.Bold();
                        // รายละเอียดคำแนะนำ
                        var Suggestiondetail = document.InsertParagraph();
                        Suggestiondetail.Append(data.Suggestion).SpacingAfter(5d).FontSize(16);

                        var iix = document.InsertParagraph();
                        iix.Append("ประเด็นที่ตรวจ").FontSize(16).SpacingAfter(5d).Alignment = Alignment.center;
                        iix.Bold();

                        var ii = document.InsertParagraph();
                        ii.Append(data.Detail).FontSize(16);

                        ii.InsertHorizontalLine(HorizontalBorderPosition.bottom, BorderStyle.Tcbs_single);
                        ii.SpacingAfter(20d);
                    }





                    //var i4 = document.InsertParagraph();

                    //i4.AppendPicture(picture3).Alignment = Alignment.center;
                    //i4.AppendPicture(picture3).Alignment = Alignment.center;

                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");

                }
            }
            else if (model.id == 4)
            {
                using (DocX document = DocX.Create(createfile))
                {

                    Image image = document.AddImage(myImageFullPath);

                    //Image image2 = document.AddImage(filePath + file[0]);
                    Picture picture = image.CreatePicture(90, 90);

                    //Picture picture3 = image2.CreatePicture(150, 300);
                    var i = document.InsertParagraph();
                    i.AppendPicture(picture).Alignment = Alignment.left;
                    //picture.TextWrappingStyle = TextWrappingStyle.Square;

                    var i3 = document.InsertParagraph();
                    Image image2 = document.AddImage(filePath + file[0]);
                    Picture picture10 = image2.CreatePicture(150, 300);

                    i3.AppendPicture(picture10).Alignment = Alignment.center;
                    i3.SpacingAfter(20d);



                    // หัวข้อสมุดตรวจ
                    var subject = document.InsertParagraph();
                    subject.Append(electornicbook.Title).FontSize(18).Alignment = Alignment.center;
                    subject.SpacingAfter(40d);


                    var i4 = document.InsertParagraph();
                    if (file.Length > 3)
                    {
                        for (var j = 1; j < 4; j++)
                        {
                           
                                Image image3 = document.AddImage(filePath + file[j]);
                                Picture picture111 = image3.CreatePicture(90, 90);

                                i4.AppendPicture(picture111).Alignment = Alignment.center;
                                i4.SpacingAfter(20d);
                            
                        }
                    }

                    // รูปภาพสมุดตรวจรูปแรก
                    //var i3 = document.InsertParagraph();
                    //i3.AppendPicture(picture3).Alignment = Alignment.center;
                    //i3.SpacingAfter(40d);



                    // รายละเอียดสมุดตรวจอิเล็กทรอนิดส์
                    foreach (var data in result)
                    {

                        index = index + 1;

                        var detail = document.InsertParagraph();
                        detail.Append(index + ". " + data.Name).FontSize(20).Alignment = Alignment.left;
                        detail.Bold();
                        detail.SpacingAfter(10d);


                        // หัวข้อปัญหาและอุปสรรค
                        var problem = document.InsertParagraph();
                        problem.Append("ปัญหาและอุปสรรค").FontSize(16).SpacingAfter(20d).Alignment = Alignment.center;
                        problem.Bold();
                        problem.SpacingAfter(5d);


                        //รายละเอียดปัญหาและอุปสรรค
                        var problemdetail = document.InsertParagraph();
                        problemdetail.Append(data.Problem).FontSize(16).SpacingAfter(5d);


                        // หัวข้อคำแนะนำ
                        var Suggestion = document.InsertParagraph();
                        Suggestion.Append("คำแนะนำ").FontSize(16).SpacingAfter(20d).Alignment = Alignment.center;
                        Suggestion.Bold();
                        // รายละเอียดคำแนะนำ
                        var Suggestiondetail = document.InsertParagraph();
                        Suggestiondetail.Append(data.Suggestion).SpacingAfter(5d).FontSize(16);

                        var iix = document.InsertParagraph();
                        iix.Append("ประเด็นที่ตรวจ").FontSize(16).SpacingAfter(5d).Alignment = Alignment.center;
                        iix.Bold();

                        var ii = document.InsertParagraph();
                        ii.Append(data.Detail).FontSize(16);

                        ii.InsertHorizontalLine(HorizontalBorderPosition.bottom, BorderStyle.Tcbs_single);
                        ii.SpacingAfter(20d);
                    }


                  





                    //var i4 = document.InsertParagraph();

                    //i4.AppendPicture(picture3).Alignment = Alignment.center;
                    //i4.AppendPicture(picture3).Alignment = Alignment.center;

                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");

                }
            }
            else if (model.id == 5)
            {
                using (DocX document = DocX.Create(createfile))
                {

                    Image image = document.AddImage(myImageFullPath);

                    //Image image2 = document.AddImage(filePath + file[0]);
                    Picture picture = image.CreatePicture(90, 90);

                    //Picture picture3 = image2.CreatePicture(150, 300);
                    var i = document.InsertParagraph();
                    i.AppendPicture(picture).Alignment = Alignment.left;
                    //picture.TextWrappingStyle = TextWrappingStyle.Square;



                    // หัวข้อสมุดตรวจ
                    var subject = document.InsertParagraph();
                    subject.Append(electornicbook.Title).FontSize(18).Alignment = Alignment.center;
                    subject.SpacingAfter(40d);



                    // รูปภาพสมุดตรวจรูปแรก
                    //var i3 = document.InsertParagraph();
                    //i3.AppendPicture(picture3).Alignment = Alignment.center;
                    //i3.SpacingAfter(40d);



                    // รายละเอียดสมุดตรวจอิเล็กทรอนิดส์
                    foreach (var data in result)
                    {

                        index = index + 1;

                        var detail = document.InsertParagraph();
                        detail.Append(index + ". " + data.Name).FontSize(20).Alignment = Alignment.left;
                        detail.Bold();
                        detail.SpacingAfter(10d);


                        // หัวข้อปัญหาและอุปสรรค
                        var problem = document.InsertParagraph();
                        problem.Append("ปัญหาและอุปสรรค").FontSize(16).SpacingAfter(20d).Alignment = Alignment.center;
                        problem.Bold();
                        problem.SpacingAfter(5d);


                        //รายละเอียดปัญหาและอุปสรรค
                        var problemdetail = document.InsertParagraph();
                        problemdetail.Append(data.Problem).FontSize(16).SpacingAfter(5d);


                        // หัวข้อคำแนะนำ
                        var Suggestion = document.InsertParagraph();
                        Suggestion.Append("คำแนะนำ").FontSize(16).SpacingAfter(20d).Alignment = Alignment.center;
                        Suggestion.Bold();
                        // รายละเอียดคำแนะนำ
                        var Suggestiondetail = document.InsertParagraph();
                        Suggestiondetail.Append(data.Suggestion).SpacingAfter(5d).FontSize(16);

                        var iix = document.InsertParagraph();
                        iix.Append("ประเด็นที่ตรวจ").FontSize(16).SpacingAfter(5d).Alignment = Alignment.center;
                        iix.Bold();

                        var ii = document.InsertParagraph();
                        ii.Append(data.Detail).FontSize(16);

                        ii.InsertHorizontalLine(HorizontalBorderPosition.bottom, BorderStyle.Tcbs_single);
                        ii.SpacingAfter(20d);
                    }



                    var i3 = document.InsertParagraph();
                    Image image2 = document.AddImage(filePath + file[0]);
                    Picture picture10 = image2.CreatePicture(150, 300);

                    i3.AppendPicture(picture10).Alignment = Alignment.center;
                    i3.SpacingAfter(20d);

                    //var i4 = document.InsertParagraph();

                    //i4.AppendPicture(picture3).Alignment = Alignment.center;
                    //i4.AppendPicture(picture3).Alignment = Alignment.center;

                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");

                }
            }
            else if (model.id == 6)
            {
                using (DocX document = DocX.Create(createfile))
                {

                    Image image = document.AddImage(myImageFullPath);

                    //Image image2 = document.AddImage(filePath + file[0]);
                    Picture picture = image.CreatePicture(90, 90);

                    //Picture picture3 = image2.CreatePicture(150, 300);
                    var i = document.InsertParagraph();
                    i.AppendPicture(picture).Alignment = Alignment.left;
                    //picture.TextWrappingStyle = TextWrappingStyle.Square;



                    // หัวข้อสมุดตรวจ
                    var subject = document.InsertParagraph();
                    subject.Append(electornicbook.Title).FontSize(18).Alignment = Alignment.center;
                    subject.SpacingAfter(40d);



                    // รูปภาพสมุดตรวจรูปแรก
                    //var i3 = document.InsertParagraph();
                    //i3.AppendPicture(picture3).Alignment = Alignment.center;
                    //i3.SpacingAfter(40d);



                    // รายละเอียดสมุดตรวจอิเล็กทรอนิดส์
                    foreach (var data in result)
                    {

                        index = index + 1;

                        var detail = document.InsertParagraph();
                        detail.Append(index + ". " + data.Name).FontSize(20).Alignment = Alignment.left;
                        detail.Bold();
                        detail.SpacingAfter(10d);


                        // หัวข้อปัญหาและอุปสรรค
                        var problem = document.InsertParagraph();
                        problem.Append("ปัญหาและอุปสรรค").FontSize(16).SpacingAfter(20d).Alignment = Alignment.center;
                        problem.Bold();
                        problem.SpacingAfter(5d);


                        //รายละเอียดปัญหาและอุปสรรค
                        var problemdetail = document.InsertParagraph();
                        problemdetail.Append(data.Problem).FontSize(16).SpacingAfter(5d);


                        // หัวข้อคำแนะนำ
                        var Suggestion = document.InsertParagraph();
                        Suggestion.Append("คำแนะนำ").FontSize(16).SpacingAfter(20d).Alignment = Alignment.center;
                        Suggestion.Bold();
                        // รายละเอียดคำแนะนำ
                        var Suggestiondetail = document.InsertParagraph();
                        Suggestiondetail.Append(data.Suggestion).SpacingAfter(5d).FontSize(16);

                        var iix = document.InsertParagraph();
                        iix.Append("ประเด็นที่ตรวจ").FontSize(16).SpacingAfter(5d).Alignment = Alignment.center;
                        iix.Bold();

                        var ii = document.InsertParagraph();
                        ii.Append(data.Detail).FontSize(16);

                        ii.InsertHorizontalLine(HorizontalBorderPosition.bottom, BorderStyle.Tcbs_single);
                        ii.SpacingAfter(20d);
                    }



                    var i3 = document.InsertParagraph();
                    Image image2 = document.AddImage(filePath + file[0]);
                    Picture picture10 = image2.CreatePicture(150, 300);

                    i3.AppendPicture(picture10).Alignment = Alignment.center;
                    i3.SpacingAfter(20d);


                    var i4 = document.InsertParagraph();

                    if (file.Length > 2)
                    {
                        for (var j = 1; j < 3; j++)
                        {

                            Image image3 = document.AddImage(filePath + file[j]);
                            Picture picture111 = image3.CreatePicture(90, 90);

                            i4.AppendPicture(picture111).Alignment = Alignment.center;
                            i4.SpacingAfter(20d);

                        }
                    }


                    //var i4 = document.InsertParagraph();

                    //i4.AppendPicture(picture3).Alignment = Alignment.center;
                    //i4.AppendPicture(picture3).Alignment = Alignment.center;

                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");

                }
            }
            else if (model.id == 7)
            {
                using (DocX document = DocX.Create(createfile))
                {

                    Image image = document.AddImage(myImageFullPath);

                    //Image image2 = document.AddImage(filePath + file[0]);
                    Picture picture = image.CreatePicture(90, 90);

                    //Picture picture3 = image2.CreatePicture(150, 300);
                    var i = document.InsertParagraph();
                    i.AppendPicture(picture).Alignment = Alignment.left;
                    //picture.TextWrappingStyle = TextWrappingStyle.Square;



                    // หัวข้อสมุดตรวจ
                    var subject = document.InsertParagraph();
                    subject.Append(electornicbook.Title).FontSize(18).Alignment = Alignment.center;
                    subject.SpacingAfter(40d);



                    // รูปภาพสมุดตรวจรูปแรก
                    //var i3 = document.InsertParagraph();
                    //i3.AppendPicture(picture3).Alignment = Alignment.center;
                    //i3.SpacingAfter(40d);



                    // รายละเอียดสมุดตรวจอิเล็กทรอนิดส์
                    foreach (var data in result)
                    {

                        index = index + 1;

                        var detail = document.InsertParagraph();
                        detail.Append(index + ". " + data.Name).FontSize(20).Alignment = Alignment.left;
                        detail.Bold();
                        detail.SpacingAfter(10d);


                        // หัวข้อปัญหาและอุปสรรค
                        var problem = document.InsertParagraph();
                        problem.Append("ปัญหาและอุปสรรค").FontSize(16).SpacingAfter(20d).Alignment = Alignment.center;
                        problem.Bold();
                        problem.SpacingAfter(5d);


                        //รายละเอียดปัญหาและอุปสรรค
                        var problemdetail = document.InsertParagraph();
                        problemdetail.Append(data.Problem).FontSize(16).SpacingAfter(5d);


                        // หัวข้อคำแนะนำ
                        var Suggestion = document.InsertParagraph();
                        Suggestion.Append("คำแนะนำ").FontSize(16).SpacingAfter(20d).Alignment = Alignment.center;
                        Suggestion.Bold();
                        // รายละเอียดคำแนะนำ
                        var Suggestiondetail = document.InsertParagraph();
                        Suggestiondetail.Append(data.Suggestion).SpacingAfter(5d).FontSize(16);

                        var iix = document.InsertParagraph();
                        iix.Append("ประเด็นที่ตรวจ").FontSize(16).SpacingAfter(5d).Alignment = Alignment.center;
                        iix.Bold();

                        var ii = document.InsertParagraph();
                        ii.Append(data.Detail).FontSize(16);

                        ii.InsertHorizontalLine(HorizontalBorderPosition.bottom, BorderStyle.Tcbs_single);
                        ii.SpacingAfter(20d);
                    }


                    var i3 = document.InsertParagraph();
                    Image image2 = document.AddImage(filePath + file[0]);
                    Picture picture10 = image2.CreatePicture(150, 300);

                    i3.AppendPicture(picture10).Alignment = Alignment.center;
                    i3.SpacingAfter(20d);


                    var i4 = document.InsertParagraph();

                    if (file.Length > 3)
                    {
                        for (var j = 1; j < 4; j++)
                        {

                            Image image3 = document.AddImage(filePath + file[j]);
                            Picture picture111 = image3.CreatePicture(90, 90);

                            i4.AppendPicture(picture111).Alignment = Alignment.center;
                            i4.SpacingAfter(20d);

                        }
                    }



                    //var i4 = document.InsertParagraph();

                    //i4.AppendPicture(picture3).Alignment = Alignment.center;
                    //i4.AppendPicture(picture3).Alignment = Alignment.center;

                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");

                }
            }
            else if (model.id == 8)
            {
                using (DocX document = DocX.Create(createfile))
                {

                    Image image = document.AddImage(myImageFullPath);

                    //Image image2 = document.AddImage(filePath + file[0]);
                    Picture picture = image.CreatePicture(90, 90);

                    //Picture picture3 = image2.CreatePicture(150, 300);
                    var i = document.InsertParagraph();
                    i.AppendPicture(picture).Alignment = Alignment.left;
                    //picture.TextWrappingStyle = TextWrappingStyle.Square;



                    // หัวข้อสมุดตรวจ
                    var subject = document.InsertParagraph();
                    subject.Append(electornicbook.Title).FontSize(18).Alignment = Alignment.center;
                    subject.SpacingAfter(40d);



                    // รูปภาพสมุดตรวจรูปแรก
                    //var i3 = document.InsertParagraph();
                    //i3.AppendPicture(picture3).Alignment = Alignment.center;
                    //i3.SpacingAfter(40d);



                    // รายละเอียดสมุดตรวจอิเล็กทรอนิดส์
                    foreach (var data in result)
                    {

                        index = index + 1;

                        var detail = document.InsertParagraph();
                        detail.Append(index + ". " + data.Name).FontSize(20).Alignment = Alignment.left;
                        detail.Bold();
                        detail.SpacingAfter(10d);


                        // หัวข้อปัญหาและอุปสรรค
                        var problem = document.InsertParagraph();
                        problem.Append("ปัญหาและอุปสรรค").FontSize(16).SpacingAfter(20d).Alignment = Alignment.center;
                        problem.Bold();
                        problem.SpacingAfter(5d);


                        //รายละเอียดปัญหาและอุปสรรค
                        var problemdetail = document.InsertParagraph();
                        problemdetail.Append(data.Problem).FontSize(16).SpacingAfter(5d);


                        // หัวข้อคำแนะนำ
                        var Suggestion = document.InsertParagraph();
                        Suggestion.Append("คำแนะนำ").FontSize(16).SpacingAfter(20d).Alignment = Alignment.center;
                        Suggestion.Bold();
                        // รายละเอียดคำแนะนำ
                        var Suggestiondetail = document.InsertParagraph();
                        Suggestiondetail.Append(data.Suggestion).SpacingAfter(5d).FontSize(16);

                        var iix = document.InsertParagraph();
                        iix.Append("ประเด็นที่ตรวจ").FontSize(16).SpacingAfter(5d).Alignment = Alignment.center;
                        iix.Bold();

                        var ii = document.InsertParagraph();
                        ii.Append(data.Detail).FontSize(16);

                        ii.InsertHorizontalLine(HorizontalBorderPosition.bottom, BorderStyle.Tcbs_single);
                        ii.SpacingAfter(20d);
                    }


                  

                   
                        for (var j = 0; j < file.Length; j++)
                        {
                            var i4 = document.InsertParagraph();

                            Image image3 = document.AddImage(filePath + file[j]);
                            Picture picture111 = image3.CreatePicture(150, 150);

                            i4.Append(description[j]).AppendPicture(picture111).Alignment = Alignment.left;
                            i4.SpacingAfter(10d);
                            
                        }
                    


                    //var i4 = document.InsertParagraph();

                    //i4.AppendPicture(picture3).Alignment = Alignment.center;
                    //i4.AppendPicture(picture3).Alignment = Alignment.center;

                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");

                }
            }
            else if (model.id == 9)
            {
                using (DocX document = DocX.Create(createfile))
                {

                    Image image = document.AddImage(myImageFullPath);

                    //Image image2 = document.AddImage(filePath + file[0]);
                    Picture picture = image.CreatePicture(90, 90);

                    //Picture picture3 = image2.CreatePicture(150, 300);
                    var i = document.InsertParagraph();
                    i.AppendPicture(picture).Alignment = Alignment.left;
                    //picture.TextWrappingStyle = TextWrappingStyle.Square;



                    // หัวข้อสมุดตรวจ
                    var subject = document.InsertParagraph();
                    subject.Append(electornicbook.Title).FontSize(18).Alignment = Alignment.center;
                    subject.SpacingAfter(40d);



                    // รูปภาพสมุดตรวจรูปแรก
                    //var i3 = document.InsertParagraph();
                    //i3.AppendPicture(picture3).Alignment = Alignment.center;
                    //i3.SpacingAfter(40d);



                    // รายละเอียดสมุดตรวจอิเล็กทรอนิดส์
                    foreach (var data in result)
                    {

                        index = index + 1;

                        var detail = document.InsertParagraph();
                        detail.Append(index + ". " + data.Name).FontSize(20).Alignment = Alignment.left;
                        detail.Bold();
                        detail.SpacingAfter(10d);


                        // หัวข้อปัญหาและอุปสรรค
                        var problem = document.InsertParagraph();
                        problem.Append("ปัญหาและอุปสรรค").FontSize(16).SpacingAfter(20d).Alignment = Alignment.center;
                        problem.Bold();
                        problem.SpacingAfter(5d);


                        //รายละเอียดปัญหาและอุปสรรค
                        var problemdetail = document.InsertParagraph();
                        problemdetail.Append(data.Problem).FontSize(16).SpacingAfter(5d);


                        // หัวข้อคำแนะนำ
                        var Suggestion = document.InsertParagraph();
                        Suggestion.Append("คำแนะนำ").FontSize(16).SpacingAfter(20d).Alignment = Alignment.center;
                        Suggestion.Bold();
                        // รายละเอียดคำแนะนำ
                        var Suggestiondetail = document.InsertParagraph();
                        Suggestiondetail.Append(data.Suggestion).SpacingAfter(5d).FontSize(16);

                        var iix = document.InsertParagraph();
                        iix.Append("ประเด็นที่ตรวจ").FontSize(16).SpacingAfter(5d).Alignment = Alignment.center;
                        iix.Bold();

                        var ii = document.InsertParagraph();
                        ii.Append(data.Detail).FontSize(16);

                        ii.InsertHorizontalLine(HorizontalBorderPosition.bottom, BorderStyle.Tcbs_single);
                        ii.SpacingAfter(20d);
                    }



                    for (var j = 0; j < file.Length; j++)
                    {
                        var i4 = document.InsertParagraph();

                        Image image3 = document.AddImage(filePath + file[j]);
                        Picture picture111 = image3.CreatePicture(150, 150);

                        i4.AppendPicture(picture111).Append(description[j]).Alignment = Alignment.left;
                        i4.SpacingAfter(10d);

                    }

                    //var i4 = document.InsertParagraph();

                    //i4.AppendPicture(picture3).Alignment = Alignment.center;
                    //i4.AppendPicture(picture3).Alignment = Alignment.center;

                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");

                }
            }
            else if (model.id == 10)
            {
                using (DocX document = DocX.Create(createfile))
                {

                    Image image = document.AddImage(myImageFullPath);

                    //Image image2 = document.AddImage(filePath + file[0]);
                    Picture picture = image.CreatePicture(90, 90);

                    //Picture picture3 = image2.CreatePicture(150, 300);
                    var i = document.InsertParagraph();
                    i.AppendPicture(picture).Alignment = Alignment.left;
                    //picture.TextWrappingStyle = TextWrappingStyle.Square;



                    // หัวข้อสมุดตรวจ
                    var subject = document.InsertParagraph();
                    subject.Append(electornicbook.Title).FontSize(18).Alignment = Alignment.center;
                    subject.SpacingAfter(40d);



                    // รูปภาพสมุดตรวจรูปแรก
                    //var i3 = document.InsertParagraph();
                    //i3.AppendPicture(picture3).Alignment = Alignment.center;
                    //i3.SpacingAfter(40d);



                    // รายละเอียดสมุดตรวจอิเล็กทรอนิดส์
                    foreach (var data in result)
                    {

                        index = index + 1;

                        var detail = document.InsertParagraph();
                        detail.Append(index + ". " + data.Name).FontSize(20).Alignment = Alignment.left;
                        detail.Bold();
                        detail.SpacingAfter(10d);


                        // หัวข้อปัญหาและอุปสรรค
                        var problem = document.InsertParagraph();
                        problem.Append("ปัญหาและอุปสรรค").FontSize(16).SpacingAfter(20d).Alignment = Alignment.center;
                        problem.Bold();
                        problem.SpacingAfter(5d);


                        //รายละเอียดปัญหาและอุปสรรค
                        var problemdetail = document.InsertParagraph();
                        problemdetail.Append(data.Problem).FontSize(16).SpacingAfter(5d);


                        // หัวข้อคำแนะนำ
                        var Suggestion = document.InsertParagraph();
                        Suggestion.Append("คำแนะนำ").FontSize(16).SpacingAfter(20d).Alignment = Alignment.center;
                        Suggestion.Bold();
                        // รายละเอียดคำแนะนำ
                        var Suggestiondetail = document.InsertParagraph();
                        Suggestiondetail.Append(data.Suggestion).SpacingAfter(5d).FontSize(16);

                        var iix = document.InsertParagraph();
                        iix.Append("ประเด็นที่ตรวจ").FontSize(16).SpacingAfter(5d).Alignment = Alignment.center;
                        iix.Bold();

                        var ii = document.InsertParagraph();
                        ii.Append(data.Detail).FontSize(16);

                        ii.InsertHorizontalLine(HorizontalBorderPosition.bottom, BorderStyle.Tcbs_single);
                        ii.SpacingAfter(20d);
                    }




                    for (var j = 0; j < file.Length; j++)
                    {
                        var i4 = document.InsertParagraph();

                        Image image3 = document.AddImage(filePath + file[j]);
                        Picture picture111 = image3.CreatePicture(150, 300);

                        i4.AppendPicture(picture111).Alignment = Alignment.center;
                        i4.SpacingAfter(10d);
                        var i5 = document.InsertParagraph();
                        i5.Append(description[j]).Alignment = Alignment.center;
                        i5.SpacingAfter(20d);

                    }




                    //var i4 = document.InsertParagraph();

                    //i4.AppendPicture(picture3).Alignment = Alignment.center;
                    //i4.AppendPicture(picture3).Alignment = Alignment.center;

                    document.Save();
                    Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");

                }
            }


            return Ok(new { data = filename });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // GET: api/values
        [HttpGet("Excel")]
        public IEnumerable<ExportRegistration> Get2()
        {
            var data = _context.ExportRegistrations.ToList();

            return data;
        }
    }
}
