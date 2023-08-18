using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Image = Xceed.Document.NET.Image;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class OldReportController : Controller
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

        public OldReportController(ApplicationDbContext context, IWebHostEnvironment environment)
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

        [HttpPost("importOldReport")]
        public async Task<IActionResult> PostOldReport([FromForm] OldReportViewModel model)
        {
            System.Console.WriteLine(model.Year);
            System.Console.WriteLine(model.CentralPolicyType);
            System.Console.WriteLine(model.Name);
            System.Console.WriteLine(model.ReportType);
            System.Console.WriteLine(model.userId);
            System.Console.WriteLine(model.files);
            var oldReportData = new OldReport
            {
                Year = model.Year,
                CentralPolicyType = model.CentralPolicyType,
                Name = model.Name,
                ReportType = model.ReportType,
                CreatedBy = model.userId,
                CreatedAt = DateTime.Now,
                Round = model.Round
            };

            System.Console.WriteLine("in3" + oldReportData);
            _context.OldReports.Add(oldReportData);
            System.Console.WriteLine("in4");
            _context.SaveChanges();
            System.Console.WriteLine("finished.");

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }

            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//Uploads//";

            if (model.files != null)
            {
                System.Console.WriteLine("Start Upload 2");
                foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
                //foreach (var formFile in data.files)
                {
                    System.Console.WriteLine("Start Upload 3");
                    var random = RandomString(10);
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);

                    if (formFile.Value.Length > 0)
                    {
                        System.Console.WriteLine("Start Upload 4");
                        // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                        using (var stream = System.IO.File.Create(filePath + random + filename))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }
                        System.Console.WriteLine("Start Upload 4.1");
                        var OldReportFileData = new OldReportFile
                        {
                            OldReportId = oldReportData.Id,
                            Name = random + filename,
                            Description = filename,
                            // Type = model.Type
                        };
                        System.Console.WriteLine("Start Upload 4.2");
                        _context.OldReportFiles.Add(OldReportFileData);
                        System.Console.WriteLine("Start Upload 4.2.5");
                        _context.SaveChanges();
                        System.Console.WriteLine("Start Upload 4.3");
                    }
                    System.Console.WriteLine("Start Upload 5");
                }
            }

            return Ok(new { status = true });
        }

        [HttpGet("getOldReports")]
        public IActionResult GetOldReports()
        {
            var oldReportData = _context.OldReports
                .Include(x => x.OldReportFiles)
                .ToList();

            return Ok(oldReportData);
        }

        [HttpGet("getOldReport/{oldReportId}")]
        public IActionResult GetOldReportById(long oldReportId)
        {
            var oldReportData = _context.OldReports
                .Include(x => x.OldReportFiles)
                .Where(x => x.Id == oldReportId)
                .FirstOrDefault();

            return Ok(oldReportData);
        }

        [HttpPut("editOldReport/{oldReportId}")]
        public async Task<IActionResult> EditOldReport(long oldReportId, [FromForm] OldReportViewModel model)
        {
            System.Console.WriteLine("in edit ja: " + oldReportId);
            System.Console.WriteLine(model.Year);
            System.Console.WriteLine(model.CentralPolicyType);
            System.Console.WriteLine(model.Name);
            System.Console.WriteLine(model.ReportType);
            System.Console.WriteLine(model.userId);
            System.Console.WriteLine(model.files);
            var oldReport = _context.OldReports
                .Include(x => x.OldReportFiles)
                .Where(x => x.Id == oldReportId)
                .FirstOrDefault();
            {
                oldReport.Year = model.Year;
                oldReport.CentralPolicyType = model.CentralPolicyType;
                oldReport.Name = model.Name;
                oldReport.ReportType = model.ReportType;
                oldReport.Round = model.Round;
            }
            _context.Entry(oldReport).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            System.Console.WriteLine("Edit 1");

            System.Console.WriteLine("Start Upload");

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }

            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//Uploads//";

            if (model.files != null)
            {
                System.Console.WriteLine("Start Upload 1");
                // Remove old file.
                //var oldReportFile1 = _context.OldReportFiles.Find(oldReportId);
                //System.Console.WriteLine("Start Upload 1.1");
                //_context.OldReportFiles.Remove(oldReportFile1);
                //System.Console.WriteLine("Start Upload 1.2");
                //_context.SaveChanges();

                System.Console.WriteLine("Start Upload 2");
                foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
                //foreach (var formFile in data.files)
                {
                    System.Console.WriteLine("Start Upload 3");
                    var random = RandomString(10);
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    Console.WriteLine("filenameJa => " + filename);
                    string ext = Path.GetExtension(filename);

                    if (formFile.Value.Length > 0)
                    {
                        System.Console.WriteLine("Start Upload 4");
                        // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                        using (var stream = System.IO.File.Create(filePath + random + filename))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }
                        System.Console.WriteLine("Start Upload 4.1");
                        //var oldReportFileData = new OldReportFile
                        //{
                        //    OldReportId = oldReport.Id,
                        //    Name = random + "_" + filename,
                        //    Description = filename,
                        //    // Type = model.Type
                        //};
                        //System.Console.WriteLine("Start Upload 4.2");
                        //_context.OldReportFiles.Add(oldReportFileData);
                        //_context.SaveChanges();

                        var oldReportFileData = _context.OldReportFiles
                            .Where(x => x.OldReportId == oldReportId)
                            .FirstOrDefault();
                            {
                                //oldReportFileData.OldReportId = oldReportId;
                                oldReportFileData.Name = random + filename;
                                oldReportFileData.Description = filename;
                            }
                        _context.Entry(oldReportFileData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();
                        System.Console.WriteLine("Start Upload 4.3");
                    }
                    System.Console.WriteLine("Start Upload 5");
                }
            }
            return Ok(new { status = true });
        }

        [HttpDelete("deleteOldReport/{oldReportId}")]
        public void DeleteFile(long oldReportId)
        {
            var oldReportData = _context.OldReports.Find(oldReportId);
            _context.OldReports.Remove(oldReportData);
            _context.SaveChanges();
        }

    }
}