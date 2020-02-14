using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static InspecWeb.Models.CentralPolicyViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CentralPolicyController : Controller
    {
        private readonly ApplicationDbContext _context;
        public static IWebHostEnvironment _environment;
        public CentralPolicyController(ApplicationDbContext context , IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<CentralPolicy> Get()
        {
            var centralpolicydata = from P in _context.CentralPolicies
                                    select P;
            return centralpolicydata;

            //return 
            //_context.Provinces
            //   .Include(p => p.Districts)
            //   .Where(p => p.Id == 1)
            //   .ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [RequestSizeLimit(100_100_100_100_100_100)]
        public async Task<IActionResult> Post(CentralPolicyClass model)
        {
            var date = DateTime.Now;

            var centralpolicydata = new CentralPolicy
            {
                Title = model.title,
                StartDate = model.start_date,
                EndDate = model.end_date,
                CreatedAt = date,
                CreatedBy = "test user",
            };
            _context.CentralPolicies.Add(centralpolicydata);
            _context.SaveChanges();
            var filePath = _environment.WebRootPath + "//Uploads//" + centralpolicydata.Id.ToString() + "//";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            //Upload File And Post to Database
            foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
            {
                var getFilename = formFile.Value.FileName;
                string fileName = Path.GetFileName(getFilename);
                var ext = Path.GetExtension(fileName);
                if (formFile.Value.Length > 0)
                {
                    using (var stream = System.IO.File.Create(filePath + fileName))
                    {
                        await formFile.Value.CopyToAsync(stream);
                    };
                }
                var postDatabase = new CentralPolicyFile
                {
                    Name = fileName,
                    CentralPolicyId = centralpolicydata.Id
                };
                _context.CentralPolicyFiles.Add(postDatabase);
            }
            //var centralpolicyfile = new CentralPolicyFile
            //{
            //    CentralPolicyId = centralpolicydata.Id,
            //    Name = model.files[0].FileName,
            //};

            //foreach (var subject in model.subjects)
            //{
            //    var subjectdata = new Subject
            //    {
            //        CentralPolicyId = centralpolicydata.Id,
            //        Name = subject.Name,
            //        Answer = "answer"
            //    };
            //    _context.Subjects.Add(subjectdata);
            //}


            //_context.CentralPolicyFiles.Add(centralpolicyfile);


            _context.SaveChanges();

            return Ok(centralpolicydata);
        }

        //[HttpPost]
        //[RequestSizeLimit(100_100_100_100_100_100)]
        //public async Task<IActionResult> PostTest(List<IFormFile> files , CentralPolicy data)
        //{
        //    var date = DateTime.Now;
        //    //return Ok(title);
        //    var centralpolicydata = new CentralPolicy
        //    {
        //        Title = data.Title,
        //        StartDate = date,
        //        EndDate = date,
        //        //StartDate = model.start_date,
        //        //EndDate = model.end_date,
        //        CreatedAt = date,
        //        CreatedBy = "test user",
        //    };
        //    var filePath = _environment.WebRootPath + "//Uploads//" + centralpolicydata.Id.ToString() + "//";
        //    if (!Directory.Exists(filePath))
        //    {
        //        Directory.CreateDirectory(filePath);
        //    }
        //    //Upload File And Post to Database
        //    foreach (var formFile in files.Select((value, index) => new { Value = value, Index = index }))
        //    {
        //        var getFilename = formFile.Value.FileName;
        //        string fileName = Path.GetFileName(getFilename);
        //        var ext = Path.GetExtension(fileName);
        //        if (formFile.Value.Length > 0)
        //        {
        //            using (var stream = System.IO.File.Create(filePath + fileName))
        //            {
        //                await formFile.Value.CopyToAsync(stream);
        //            };
        //        }
        //        var postDatabase = new CentralPolicyFile
        //        {
        //            Name = fileName,
        //            CentralPolicyId = centralpolicydata.Id
        //        };
        //        //_context.CentralPolicyFiles.Add(postDatabase);
        //    }
        //    _context.CentralPolicies.Add(centralpolicydata);
        //    _context.SaveChanges();

        //    return Ok(centralpolicydata);
        //    //return Ok(centralpolicydata);
        //}



        //[HttpPost]
        //public CentralPolicy Post(string title, DateTime start_date, DateTime end_date, string[] subjects, string files)
        //{
        //    var date = DateTime.Now;

        //    var centralpolicydata = new CentralPolicy
        //    {
        //        Title = title,
        //        StartDate = start_date,
        //        EndDate = end_date,
        //        CreatedAt = date,
        //        CreatedBy = "test user",
        //    };

        //    var centralpolicyfile = new CentralPolicyFile
        //    {
        //        CentralPolicyId = centralpolicydata.Id,
        //        Name = files,
        //    };

        //    foreach (string subject in subjects)
        //    {
        //        var subjectdata = new Subject
        //        {
        //            CentralPolicyId = centralpolicydata.Id,
        //            Name = subject,
        //            Answer = "answer"
        //        };
        //        _context.Subjects.Add(subjectdata);
        //    }

        //    _context.CentralPolicies.Add(centralpolicydata);
        //    _context.CentralPolicyFiles.Add(centralpolicyfile);


        //    _context.SaveChanges();

        //    return centralpolicydata;
        //}

    }
}