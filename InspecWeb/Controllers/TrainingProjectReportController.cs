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
    public class TrainingProjectReportController : Controller
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

        public TrainingProjectReportController(ApplicationDbContext context, IWebHostEnvironment environment)
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

        [HttpGet("getTrainingProjectReports")]
        public IActionResult GetTrainingProjectReports()
        {
            var trainingProjectReports = _context.TrainingProjectReports
                .ToList();

            return Ok(trainingProjectReports);
        }

        [HttpGet("getTrainingProjectReportById/{id}")]
        public IActionResult GetOldReportById(long id)
        {
            var oldReportData = _context.TrainingProjectReports
                .Include(x => x.TrainingProjectReportFiles)
                .Include(x => x.TrainingProjectReportModelDirectoryFiles)
                .Include(x => x.TrainingProjectReportPracticeGuideFiles)
                .Include(x => x.TrainingProjectReportProjectDocumentFiles)
                .Include(x => x.TrainingProjectReportTrainingDetailFiles)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return Ok(oldReportData);
        }

        [HttpPost("addTrainingProjectReport")]
        public async Task<IActionResult> PostTrainingProjectReport([FromForm] TrainingProjectReportViewModel model)
        {
            System.Console.WriteLine(model.Year);
            System.Console.WriteLine(model.CourseType);
            var TrainingProjectReportData = new TrainingProjectReport
            {
                Year = model.Year,
                courseType = model.CourseType,
                CreatedBy = model.userId,
                CreatedAt = DateTime.Now,
            };

            System.Console.WriteLine("in3" + TrainingProjectReportData);
            _context.TrainingProjectReports.Add(TrainingProjectReportData);
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

            // เอกสารรายงานผลการฝึกอบรม
            if (model.reportFiles != null)
            {
                System.Console.WriteLine("Start Upload 2");
                foreach (var formFile in model.reportFiles.Select((value, index) => new { Value = value, Index = index }))
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
                        var TrainingProjectReportFileData = new TrainingProjectReportFile
                        {
                            TrainingProjectReportId = TrainingProjectReportData.Id,
                            Name = random + filename,
                            Description = filename,
                            // Type = model.Type
                        };
                        System.Console.WriteLine("Start Upload 4.2");
                        _context.TrainingProjectReportFiles.Add(TrainingProjectReportFileData);
                        System.Console.WriteLine("Start Upload 4.2.5");
                        _context.SaveChanges();
                        System.Console.WriteLine("Start Upload 4.3");
                    }
                    System.Console.WriteLine("Start Upload 5");
                }
            }

            // เอกสารโครงการ
            if (model.projectDocumentFiles != null)
            {
                System.Console.WriteLine("Start Upload 2");
                foreach (var formFile in model.projectDocumentFiles.Select((value, index) => new { Value = value, Index = index }))
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
                        var ProjectDocumentFileData = new TrainingProjectReportProjectDocumentFile
                        {
                            TrainingProjectReportId = TrainingProjectReportData.Id,
                            Name = random + filename,
                            Description = filename,
                            // Type = model.Type
                        };
                        System.Console.WriteLine("Start Upload 4.2");
                        _context.TrainingProjectReportProjectDocumentFiles.Add(ProjectDocumentFileData);
                        System.Console.WriteLine("Start Upload 4.2.5");
                        _context.SaveChanges();
                        System.Console.WriteLine("Start Upload 4.3");
                    }
                    System.Console.WriteLine("Start Upload 5");
                }
            }

            // เอกสารรายละเอียดการฝึกอบรม
            if (model.trainingDetailFiles != null)
            {
                System.Console.WriteLine("Start Upload 2");
                foreach (var formFile in model.trainingDetailFiles.Select((value, index) => new { Value = value, Index = index }))
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
                        var TrainingDetailFileData = new TrainingProjectReportTrainingDetailFile
                        {
                            TrainingProjectReportId = TrainingProjectReportData.Id,
                            Name = random + filename,
                            Description = filename,
                            // Type = model.Type
                        };
                        System.Console.WriteLine("Start Upload 4.2");
                        _context.TrainingProjectReportTrainingDetailFiles.Add(TrainingDetailFileData);
                        System.Console.WriteLine("Start Upload 4.2.5");
                        _context.SaveChanges();
                        System.Console.WriteLine("Start Upload 4.3");
                    }
                    System.Console.WriteLine("Start Upload 5");
                }
            }

            // เอกสารทำเนียบรุ่น
            if (model.modelDirectoryFiles != null)
            {
                System.Console.WriteLine("Start Upload 2");
                foreach (var formFile in model.modelDirectoryFiles.Select((value, index) => new { Value = value, Index = index }))
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
                        var ModelDirectoryFileData = new TrainingProjectReportModelDirectoryFile
                        {
                            TrainingProjectReportId = TrainingProjectReportData.Id,
                            Name = random + filename,
                            Description = filename,
                            // Type = model.Type
                        };
                        System.Console.WriteLine("Start Upload 4.2");
                        _context.TrainingProjectReportModelDirectoryFiles.Add(ModelDirectoryFileData);
                        System.Console.WriteLine("Start Upload 4.2.5");
                        _context.SaveChanges();
                        System.Console.WriteLine("Start Upload 4.3");
                    }
                    System.Console.WriteLine("Start Upload 5");
                }
            }

            // เอกสารรายงานการฝึกปฏิบัติ
            if (model.practiceGuideFiles != null)
            {
                System.Console.WriteLine("Start Upload 2");
                foreach (var formFile in model.practiceGuideFiles.Select((value, index) => new { Value = value, Index = index }))
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
                        var PracticeGuideFileData = new TrainingProjectReportPracticeGuideFile
                        {
                            TrainingProjectReportId = TrainingProjectReportData.Id,
                            Name = random + filename,
                            Description = filename,
                            // Type = model.Type
                        };
                        System.Console.WriteLine("Start Upload 4.2");
                        _context.TrainingProjectReportPracticeGuideFiles.Add(PracticeGuideFileData);
                        System.Console.WriteLine("Start Upload 4.2.5");
                        _context.SaveChanges();
                        System.Console.WriteLine("Start Upload 4.3");
                    }
                    System.Console.WriteLine("Start Upload 5");
                }
            }

            return Ok(new { status = true });
        }

        [HttpPut("editTrainingProjectReport/{id}")]
        public async Task<IActionResult> EditTrainingProjectReport(long id, [FromForm] TrainingProjectReportViewModel model)
        {
            System.Console.WriteLine("in edit ja: " + id);
            System.Console.WriteLine(model.Year);
            System.Console.WriteLine(model.CourseType);
            var trainingProjectReport = _context.TrainingProjectReports
                .Where(x => x.Id == id)
                .FirstOrDefault();
            {
                trainingProjectReport.Year = model.Year;
                trainingProjectReport.courseType = model.CourseType;
            }
            _context.Entry(trainingProjectReport).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

            if (model.reportFiles != null)
            {
                System.Console.WriteLine("Start Upload 2");
                foreach (var formFile in model.reportFiles.Select((value, index) => new { Value = value, Index = index }))
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
                        var TrainingProjectReportFileData = new TrainingProjectReportFile
                        {
                            TrainingProjectReportId = id,
                            Name = random + filename,
                            Description = filename,
                            // Type = model.Type
                        };
                        System.Console.WriteLine("Start Upload 4.2");
                        _context.TrainingProjectReportFiles.Add(TrainingProjectReportFileData);
                        System.Console.WriteLine("Start Upload 4.2.5");
                        _context.SaveChanges();
                        System.Console.WriteLine("Start Upload 4.3");
                    }
                    System.Console.WriteLine("Start Upload 5");
                }
            }

            // เอกสารโครงการ
            if (model.projectDocumentFiles != null)
            {
                System.Console.WriteLine("Start Upload 2");
                foreach (var formFile in model.projectDocumentFiles.Select((value, index) => new { Value = value, Index = index }))
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
                        var ProjectDocumentFileData = new TrainingProjectReportProjectDocumentFile
                        {
                            TrainingProjectReportId = id,
                            Name = random + filename,
                            Description = filename,
                            // Type = model.Type
                        };
                        System.Console.WriteLine("Start Upload 4.2");
                        _context.TrainingProjectReportProjectDocumentFiles.Add(ProjectDocumentFileData);
                        System.Console.WriteLine("Start Upload 4.2.5");
                        _context.SaveChanges();
                        System.Console.WriteLine("Start Upload 4.3");
                    }
                    System.Console.WriteLine("Start Upload 5");
                }
            }

            // เอกสารรายละเอียดการฝึกอบรม
            if (model.trainingDetailFiles != null)
            {
                System.Console.WriteLine("Start Upload 2");
                foreach (var formFile in model.trainingDetailFiles.Select((value, index) => new { Value = value, Index = index }))
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
                        var TrainingDetailFileData = new TrainingProjectReportTrainingDetailFile
                        {
                            TrainingProjectReportId = id,
                            Name = random + filename,
                            Description = filename,
                            // Type = model.Type
                        };
                        System.Console.WriteLine("Start Upload 4.2");
                        _context.TrainingProjectReportTrainingDetailFiles.Add(TrainingDetailFileData);
                        System.Console.WriteLine("Start Upload 4.2.5");
                        _context.SaveChanges();
                        System.Console.WriteLine("Start Upload 4.3");
                    }
                    System.Console.WriteLine("Start Upload 5");
                }
            }

            // เอกสารทำเนียบรุ่น
            if (model.modelDirectoryFiles != null)
            {
                System.Console.WriteLine("Start Upload 2");
                foreach (var formFile in model.modelDirectoryFiles.Select((value, index) => new { Value = value, Index = index }))
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
                        var ModelDirectoryFileData = new TrainingProjectReportModelDirectoryFile
                        {
                            TrainingProjectReportId = id,
                            Name = random + filename,
                            Description = filename,
                            // Type = model.Type
                        };
                        System.Console.WriteLine("Start Upload 4.2");
                        _context.TrainingProjectReportModelDirectoryFiles.Add(ModelDirectoryFileData);
                        System.Console.WriteLine("Start Upload 4.2.5");
                        _context.SaveChanges();
                        System.Console.WriteLine("Start Upload 4.3");
                    }
                    System.Console.WriteLine("Start Upload 5");
                }
            }

            // เอกสารรายงานการฝึกปฏิบัติ
            if (model.practiceGuideFiles != null)
            {
                System.Console.WriteLine("Start Upload 2");
                foreach (var formFile in model.practiceGuideFiles.Select((value, index) => new { Value = value, Index = index }))
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
                        var PracticeGuideFileData = new TrainingProjectReportPracticeGuideFile
                        {
                            TrainingProjectReportId = id,
                            Name = random + filename,
                            Description = filename,
                            // Type = model.Type
                        };
                        System.Console.WriteLine("Start Upload 4.2");
                        _context.TrainingProjectReportPracticeGuideFiles.Add(PracticeGuideFileData);
                        System.Console.WriteLine("Start Upload 4.2.5");
                        _context.SaveChanges();
                        System.Console.WriteLine("Start Upload 4.3");
                    }
                    System.Console.WriteLine("Start Upload 5");
                }
            }
            return Ok(new { status = true });
        }

        [HttpDelete("deleteTrainingProjectReportFile/{id}/{fileType}")]
        public void DeleteFile(long id, string fileType)
        {
            if (fileType == "trainingProjectReport") {
                var trainingProjectFileData = _context.TrainingProjectReportFiles.Find(id);
                _context.TrainingProjectReportFiles.Remove(trainingProjectFileData);
                _context.SaveChanges();
            } else if (fileType == "modelDirectory") {
                var modelDirectoryFileData = _context.TrainingProjectReportModelDirectoryFiles.Find(id);
                _context.TrainingProjectReportModelDirectoryFiles.Remove(modelDirectoryFileData);
                _context.SaveChanges();
            } else if (fileType == "practiceGuide") {
                var practiceGuideFileData = _context.TrainingProjectReportPracticeGuideFiles.Find(id);
                _context.TrainingProjectReportPracticeGuideFiles.Remove(practiceGuideFileData);
                _context.SaveChanges();
            } else if (fileType == "trainingDetail") {
                var trainingDetailFileData = _context.TrainingProjectReportTrainingDetailFiles.Find(id);
                _context.TrainingProjectReportTrainingDetailFiles.Remove(trainingDetailFileData);
                _context.SaveChanges();
            } else if (fileType == "projectDocument") {
                var projectDocumentFileData = _context.TrainingProjectReportProjectDocumentFiles.Find(id);
                _context.TrainingProjectReportProjectDocumentFiles.Remove(projectDocumentFileData);
                _context.SaveChanges();
            }
        }

        [HttpDelete("deleteTrainingProjectReport/{id}")]
        public void DeleteFile(long id)
        {
            var trainingProjectReportData = _context.TrainingProjectReports.Find(id);
            _context.TrainingProjectReports.Remove(trainingProjectReportData);
            _context.SaveChanges();
        }
    }
}