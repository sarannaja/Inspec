using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmailService;
using Xceed.Document.NET;
using Xceed.Words.NET;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class TrainingController : Controller
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
        private readonly IEmailSender _emailSender;

        public TrainingController(ApplicationDbContext context, IWebHostEnvironment environment, IEmailSender emailSender)
        {
            _context = context;
            _environment = environment;
            _emailSender = emailSender;
        }

        //----------zone training------------
        // GET: api/Training
        [HttpGet]
        public IEnumerable<Training> Get()
        {
            var trainingdata = from P in _context.Trainings
                               select P;
            return trainingdata;

            //return 
            //_context.Provinces
            //   .Include(p => p.Districts)
            //   .Where(p => p.Id == 1)
            //   .ToList();
        }

        // GET: api/Training
        [HttpGet("trainingsurveycount")]
        public IEnumerable<object> GetTrainingCountSurvey()
        {
            var trainingdata = from P in _context.Trainings
                               select P;

            var result = new List<object>();

            var survey = _context.Trainings
                .Include(m => m.TrainingSurveys).ToList();

            //var survey2 = _context.Trainings
            //  .Include(m => m.TrainingSurveys)
            //  .GroupBy(g => new { Name = g.Name , TranId = g.Id, Id = g.Id})
            //  .Select(g => new
            //  {
            //      g.Key.Id,
            //      g.Key.Name,
            //      g.Key.TranId
            //  }).ToList();

            foreach (var test in survey)
            {
                var test2 = _context.TrainingSurveys
                    .Where(x => x.TrainingId == test.Id)
                    .ToList();

                result.Add(new
                {
                    Id = test.Id,
                    Name = test.Name,
                    Count = test2.Count()
                });
            }

            //      var survey3 = _context.TrainingSurveys

            //          var 

            //.Include(m => m.Training)
            //.GroupBy(g => new { Name = g.Training.Name , TrainId = g.TrainingId })
            //.Select(g => new
            //{

            //    g.Key.Name,
            //}).ToList();

            return result;
        }

        // GET: api/Training
        [HttpGet("trainingregistercount")]
        public IEnumerable<object> GetTrainingCountRegister()
        {
            var trainingdata = from P in _context.Trainings
                               select P;

            var result = new List<object>();

            var register = _context.Trainings
                .Include(m => m.TrainingSurveys).ToList();

            //var survey2 = _context.Trainings
            //  .Include(m => m.TrainingSurveys)
            //  .GroupBy(g => new { Name = g.Name , TranId = g.Id, Id = g.Id})
            //  .Select(g => new
            //  {
            //      g.Key.Id,
            //      g.Key.Name,
            //      g.Key.TranId
            //  }).ToList();

            foreach (var test in register)
            {
                var test2 = _context.TrainingRegisters
                    .Where(x => x.TrainingId == test.Id)
                    .ToList();

                var test3 = _context.TrainingRegisters
                    .Where(x => x.TrainingId == test.Id && x.Status == 1)
                    .ToList();

                result.Add(new
                {
                    Id = test.Id,
                    Name = test.Name,
                    Start = test.RegisStartDate,
                    End = test.RegisEndDate,
                    Count = test2.Count(),
                    ApproveCount = test3.Count()
                });
            }

            //      var survey3 = _context.TrainingSurveys

            //          var 

            //.Include(m => m.Training)
            //.GroupBy(g => new { Name = g.Training.Name , TrainId = g.TrainingId })
            //.Select(g => new
            //{

            //    g.Key.Name,
            //}).ToList();

            return result;
        }



        //GET api/Training/trainingid
        [HttpGet("listsurvey/{trainingid}")]
        public IActionResult GetListTrainingSurvey(long trainingid)
        {
            var districtdata = _context.TrainingSurveys
                .Include(m => m.Training)
                .Where(m => m.TrainingId == trainingid);

            return Ok(districtdata);

        }

        //GET api/Training/trainingid
        [HttpGet("detail/{trainingid}")]
        public IActionResult GetDetailTraining(long trainingid)
        {
            var districtdata = _context.Trainings
                //.Include(m => m.Training)
                .Where(m => m.Id == trainingid);

            return Ok(districtdata);

        }

        //GET api/Training/trainingid
        [HttpGet("checktrainingregister/{trainingid}/{userid}")]
        public IActionResult Checktrainingregister(long trainingid, string userid)
        {
            var TrainingRegistersdata = _context.TrainingRegisters
                //.Include(m => m.Training)
                .Where(m => m.Id == trainingid)
                .Where(p => p.UserId == userid).Count();

            if(TrainingRegistersdata > 0) { 
                return Ok(true);
            } else
            {
                return Ok(false);
            }
        }
        // GET: api/values
        //[HttpGet]
        //public IEnumerable<TrainingRegister> GetTrainingRegsiter()
        //{
        //    var trainingregisterdata = from P in _context.TrainingRegisters
        //                               select P;
        //    return trainingregisterdata;
        //}


        // GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] TrainingViewModel model)
        {
            var date = DateTime.Now;
            System.Console.WriteLine("Start Uplond");
            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                System.Console.WriteLine("Start Uplond2");
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }

            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//Uploads//";


            foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
            //foreach (var formFile in data.files)
            {
                System.Console.WriteLine("Start Uplond3");
                var random = RandomString(10);
                string filePath2 = formFile.Value.FileName;
                string filename = Path.GetFileName(filePath2);
                string ext = Path.GetExtension(filename);

                if (formFile.Value.Length > 0)
                {
                    System.Console.WriteLine("Start Uplond4");
                    // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                    using (var stream = System.IO.File.Create(filePath + random + filename))
                    {
                        await formFile.Value.CopyToAsync(stream);
                    }
                    System.Console.WriteLine("Start Uplond4.1");
                    var Trainingdata = new Training
                    {
                        Name = model.Name,
                        Detail = model.Detail,
                        Generation = model.Generation,
                        Year = model.Year,
                        CourseCode = model.CourseCode,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        RegisStartDate = model.RegisStartDate,
                        RegisEndDate = model.RegisEndDate,
                        Image = random + filename,
                        CreatedAt = date
                    };
                    System.Console.WriteLine("Start Uplond4.2");
                    _context.Trainings.Add(Trainingdata);
                    _context.SaveChanges();
                    System.Console.WriteLine("Start Uplond4.2");
                }
            }
            return Ok(new { status = true });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, string name, string detail, DateTime start_date, DateTime end_date, DateTime regis_start_date, DateTime regis_end_date, string image)
        {
            var training = _context.Trainings.Find(id);
            training.Name = name;
            training.Detail = detail;
            training.StartDate = start_date;
            training.EndDate = end_date;
            training.RegisStartDate = regis_start_date;
            training.RegisEndDate = regis_end_date;
            training.Image = image;
            _context.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var trainingdata = _context.Trainings.Find(id);

            _context.Trainings.Remove(trainingdata);
            _context.SaveChanges();
        }
        //--------end zone training----------






        //------zone training register-------
        // PUT api/values/5
        [HttpPut("registerlist/{id}")]
        public void EditRegisterList(long id, long status)
        {
            var training = _context.TrainingRegisters.Find(id);
            training.Status = status;



            // if (status == 1){

            //     // var datas = _context.TrainingDocuments
            //     // .Include(m => m.Training)
            //     // .Where(m => m.TrainingId == 1).ToList();

            //     // var text = "";
            //     // foreach(var data2 in datas)
            //     // {
            //     //     // System.Console.WriteLine("data: " + data2.Name);
            //     //     text = text + data2.Name + "\n";
            //     // }


            // var message = new Message(new string[] { "fantasy_tey@hotmail.com" }, "Test email", "This is the content from our email.");
            // _emailSender.SendEmail(message);

            //     var message = new Message(new string[] { "fantasy_tey@hotmail.com" }, "อบรมหลักสูตร", "เอกสารไฟล์แนบที่ 1 \n เอกสารไฟล์แนบที่ 2 \n เอกสารไฟล์แนบที่ 3 \n");
            //     _emailSender.SendEmail(message);

            //     // var message = new Message(new string[] { "fantasy_tey@hotmail.com" }, "อบรมหลักสูตร", text);
            //     // _emailSender.SendEmail(message);
            // }

            _context.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            //  var datas = _context.TrainingDocuments
            //     .Include(m => m.Training)
            //     .Where(m => m.TrainingId == trainingid).ToList();

            // var text = '';
            // foreach(data in datas)
            // {
            //     text = text + data.name;
            // }
        }

        [HttpPut("registerlist2")]
        public void EditRegisterList2(long[] traningregisterid, long status)
        {
            foreach (var id in traningregisterid)
            {
                var training = _context.TrainingRegisters.Find(id);
                training.Status = status;

                _context.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        [HttpPut("editRegisterConditionList")]
        public void EditRegisterConditionList([FromBody] RegisterConditionViewModel model)
        {
            var training = _context.TrainingRegisters
                .Where(m => m.Id == model.traningregisterid)
                .FirstOrDefault();


            foreach (var eachmodel in model.traningregistercondition)
            {
                System.Console.WriteLine("TEST" + eachmodel.id);
                System.Console.WriteLine("TEST" + eachmodel.status);

                var traningregisterconditiondata = _context.TrainingRegisterConditions
                    .Where(m => m.RegisterId == training.Id && m.ConditionId == eachmodel.id).FirstOrDefault();

                if (traningregisterconditiondata == null)
                {
                    if (eachmodel.status)
                    {
                        var Trainingdata = new TrainingRegisterCondition
                        {
                            RegisterId = training.Id,
                            ConditionId = eachmodel.id,
                            Status = 1,
                        };

                        _context.TrainingRegisterConditions.Add(Trainingdata);
                        _context.SaveChanges();
                    }
                    else
                    {
                        var Trainingdata = new TrainingRegisterCondition
                        {
                            RegisterId = training.Id,
                            ConditionId = eachmodel.id,
                            Status = 0,
                        };

                        _context.TrainingRegisterConditions.Add(Trainingdata);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    if (eachmodel.status)
                    {
                        var traningregisterconditiondata2 = _context.TrainingRegisterConditions.Find(traningregisterconditiondata.Id);
                        traningregisterconditiondata2.Status = 1;

                        _context.Entry(traningregisterconditiondata2).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();
                    }
                    else
                    {
                        var traningregisterconditiondata2 = _context.TrainingRegisterConditions.Find(traningregisterconditiondata.Id);
                        traningregisterconditiondata2.Status = 0;

                        _context.Entry(traningregisterconditiondata2).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();
                    }
                }
            }
        }

        //GET:TEST api/Training/listdocument/trainingid
        [HttpGet("listdocument2/{trainingid}")]
        public IActionResult GetListTrainingDocument2(long trainingid)
        {
            var datas = _context.TrainingDocuments
                .Include(m => m.Training)
                .Where(m => m.TrainingId == trainingid).ToList();

            var text = "";
            foreach (var data2 in datas)
            {
                // System.Console.WriteLine("data: " + data2.Name);
                text = text + data2.Name + "\n";
            }

            //return text;
            return Ok(text);
        }

        //GET api/Training/trainingid
        [HttpGet("{trainingid}")]
        public IActionResult Get2(long trainingid)
        {
            var districtdata = _context.TrainingRegisters
                .Include(m => m.Training)
                .Where(m => m.TrainingId == trainingid);

            return Ok(districtdata);

            //return _context.TrainingRegisters
            //           .Include(m => m.Training)
            //           .Where(m => m.TrainingId == trainingid);

        }


        // PUT api/training/register/group/:id
        //[HttpPut("register/group/{id}")]
        //public void EditRegisterGroup(long id, long group)
        //{
        //    var training = _context.TrainingRegisters.Find(id);
        //    training.Group = group;

        //    _context.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //    _context.SaveChanges();

        //}

        //GET api/Training/registerlist/:id
        // [HttpGet("registerlist/{id}")]
        // public IEnumerable<object> GetTrainingRegisterGroup(long id)
        // {
        //     var trainingdata = from P in _context.TrainingRegisters
        //                        select P;

        //     //return Ok(trainingdata);

        //     var result = new List<object>();

        //     // // var survey = _context.Trainings
        //     // //     .Include(m => m.TrainingSurveys).ToList();
        //     foreach (var test in trainingdata)
        //     {
        //         var test2 = _context.TrainingRegisterGroups
        //             .Where(x => x.RegisterId == test.Id)
        //             .ToList();

        //         result.Add(new
        //         {
        //             Id = test.Id,
        //             Name = test.Name,
        //             //Count = test2.Count()
        //         });
        //     }
        //     return result;

        // }

        //----------------------------------




        //------zone training register font-end-------
        // POST api/training/trainingsurvey/trainingid
        [HttpPost("trainingregister/{trainingid}")]
        public async Task<IActionResult> InsertTrainingRegister([FromForm] TrainingRegisterViewModel model)
        {
            var date = DateTime.Now;

            var trainingdata = new TrainingRegister
            {
                UserName = model.username,
                ProvincialDepartmentId = model.departmentid,
                TrainingId = model.trainingid,
                Name = model.name,
                Phone = model.phone,
                CardId = model.cardid,
                Position = model.position,
                Department = model.department,
                Status = 0,
                UserId = model.userid,
                UserType = 1,
                Email = model.email,
                CreatedAt = date,
                Type = model.type,
                Nickname = model.nickname,
                RetiredDate = model.retireddate,
                BirthDate = model.birthdate,
                OfficeAddress = model.officeaddress,
                Fax = model.fax,
                CollaboratorName = model.collaboratorname,
                CollaboratorPhone = model.collaboratorphone,
                CollaboratorPhoneOffice = model.collaboratorphoneoffice,
                CollaboratorEmail = model.collaboratoremail,
            };

            _context.TrainingRegisters.Add(trainingdata);
            _context.SaveChanges();

            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }

            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//Uploads//";

            System.Console.WriteLine("Start Upload 2");

            if (model.files != null)
            {
                foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
                {
                    System.Console.WriteLine("Start Upload 3");
                    var random = RandomString(10);
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);

                    if (formFile.Value.Length > 0)
                    {
                        System.Console.WriteLine("Start Upload 4");
                        using (var stream = System.IO.File.Create(filePath + random + filename))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }

                        System.Console.WriteLine("Start Upload 4.1");
                        var TrainingRegisterFile = new TrainingRegisterFile
                        {
                            RegisterId = trainingdata.Id,
                            Name = random + filename,
                            Type = "รูปถ่ายหน้าตรง",
                        };

                        System.Console.WriteLine("Start Upload 4.2");
                        _context.TrainingRegisterFiles.Add(TrainingRegisterFile);
                        _context.SaveChanges();

                        System.Console.WriteLine("Start Upload 4.3");
                    }
                    System.Console.WriteLine("Start Upload 5");
                }
            }

            if (model.CertificationFiles != null)
            {
                foreach (var formFile in model.CertificationFiles.Select((value, index) => new { Value = value, Index = index }))
                {
                    System.Console.WriteLine("Start Upload 3");
                    var random = RandomString(10);
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);

                    if (formFile.Value.Length > 0)
                    {
                        System.Console.WriteLine("Start Upload 4");
                        using (var stream = System.IO.File.Create(filePath + random + filename))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }

                        System.Console.WriteLine("Start Upload 4.1");
                        var TrainingRegisterFile = new TrainingRegisterFile
                        {
                            RegisterId = trainingdata.Id,
                            Name = random + filename,
                            Type = "เอกสาารการรับรองของผู้บังคับบัญชา",
                        };

                        System.Console.WriteLine("Start Upload 4.2");
                        _context.TrainingRegisterFiles.Add(TrainingRegisterFile);
                        _context.SaveChanges();

                        System.Console.WriteLine("Start Upload 4.3");
                    }
                    System.Console.WriteLine("Start Upload 5");
                }
            }

            if (model.idcardFiles != null)
            {
                foreach (var formFile in model.idcardFiles.Select((value, index) => new { Value = value, Index = index }))
                {
                    System.Console.WriteLine("Start Upload 3");
                    var random = RandomString(10);
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);

                    if (formFile.Value.Length > 0)
                    {
                        System.Console.WriteLine("Start Upload 4");
                        using (var stream = System.IO.File.Create(filePath + random + filename))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }

                        System.Console.WriteLine("Start Upload 4.1");
                        var TrainingRegisterFile = new TrainingRegisterFile
                        {
                            RegisterId = trainingdata.Id,
                            Name = random + filename,
                            Type = "สำเนาบัตรประชาชน",
                        };

                        System.Console.WriteLine("Start Upload 4.2");
                        _context.TrainingRegisterFiles.Add(TrainingRegisterFile);
                        _context.SaveChanges();

                        System.Console.WriteLine("Start Upload 4.3");
                    }
                    System.Console.WriteLine("Start Upload 5");
                }
            }

            if (model.GovernmentpassportFiles != null)
            {
                foreach (var formFile in model.GovernmentpassportFiles.Select((value, index) => new { Value = value, Index = index }))
                {
                    System.Console.WriteLine("Start Upload 3");
                    var random = RandomString(10);
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);

                    if (formFile.Value.Length > 0)
                    {
                        System.Console.WriteLine("Start Upload 4");
                        using (var stream = System.IO.File.Create(filePath + random + filename))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }

                        System.Console.WriteLine("Start Upload 4.1");
                        var TrainingRegisterFile = new TrainingRegisterFile
                        {
                            RegisterId = trainingdata.Id,
                            Name = random + filename,
                            Type = "สำเนาหนังสือเดินทางราชการ",
                        };

                        System.Console.WriteLine("Start Upload 4.2");
                        _context.TrainingRegisterFiles.Add(TrainingRegisterFile);
                        _context.SaveChanges();

                        System.Console.WriteLine("Start Upload 4.3");
                    }
                    System.Console.WriteLine("Start Upload 5");
                }
            }

            return Ok(trainingdata);
        }


        //--------------------------------------------


        //------zone training survey---------
        // POST api/training/trainingsurvey/trainingid
        [HttpPost("trainingsurvey/{trainingid}")]
        public TrainingSurvey Post(string name, long trainingid)
        {
            var date = DateTime.Now;

            var trainingdata = new TrainingSurvey
            {
                TrainingId = trainingid,
                Name = name,
                SurveyType = 1,
                CreatedAt = date

            };

            _context.TrainingSurveys.Add(trainingdata);
            _context.SaveChanges();

            return trainingdata;
        }


        // PUT : api/training/edit/:id
        [HttpPut("survey/edit/{id}")]
        public void EditTrainingsurvey(long id, string name)
        {
            var training = _context.TrainingSurveys.Find(id);
            training.Name = name;
            _context.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/Training/trainingsurvey/values/5
        [HttpDelete("trainingsurvey/{trainingid}")]
        public void DeleteTrainingsurvey(long trainingid)
        {
            var trainingdata = _context.TrainingSurveys.Find(trainingid);

            _context.TrainingSurveys.Remove(trainingdata);
            _context.SaveChanges();
        }
        //----------------------------------


        // POST api/training/trainingsurveyanswer/trainingid
        [HttpPost("trainingsurveyanswer")]
        public IActionResult InsertSurveyAnswer([FromBody] TrainingSurveyAnswerViewModel model)
        {
            var date = DateTime.Now;

            foreach (var item in model.inputtrainingsurveyanswer)
            {

                var trainingdata = new TrainingSurveyAnswer
                {

                    TrainingSurveyId = item.trainingsurveyId,
                    Name = model.Name,
                    Posoition = model.Position,
                    Score = item.score,
                    CreatedAt = date

                };
                _context.TrainingSurveyAnswers.Add(trainingdata);
                _context.SaveChanges();
            }



            return Ok(new { status = "Save Success" });
        }
        //----------------------------------


        //------zone training document-------
        // GET: api/Training
        [HttpGet("trainingdocumentcount")]
        public IEnumerable<object> GetTrainingCountDocument()
        {
            var result = new List<object>();

            var survey = _context.Trainings
                .Include(m => m.TrainingSurveys).ToList();
            foreach (var test in survey)
            {
                var test2 = _context.TrainingDocuments
                    .Where(x => x.TrainingId == test.Id)
                    .ToList();

                result.Add(new
                {
                    Id = test.Id,
                    Name = test.Name,
                    Count = test2.Count()
                });
            }
            return result;
        }

        //GET api/Training/listdocument/trainingid
        [HttpGet("listdocument/{trainingid}")]
        public IActionResult GetListTrainingDocument(long trainingid)
        {
            var data = _context.TrainingDocuments
                .Include(m => m.Training)
                .Where(m => m.TrainingId == trainingid);

            return Ok(data);
        }

        // DELETE api/Training/trainingsurvey/values/5
        [HttpDelete("deletedocument/{trainingid}")]
        public void DeleteTrainingdocument(long trainingid)
        {
            var trainingdata = _context.TrainingDocuments.Find(trainingid);

            _context.TrainingDocuments.Remove(trainingdata);
            _context.SaveChanges();
        }

        // POST api/values
        [HttpPost("insertdocument/{trainingid}")]
        public async Task<IActionResult> InsertDocument([FromForm] TrainingDocumentViewModel model, long trainingid)
        {
            var date = DateTime.Now;

            System.Console.WriteLine("Start Uplond" + trainingid);
            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                System.Console.WriteLine("Start Uplond2");
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }

            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//Uploads//";


            foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
            //foreach (var formFile in data.files)
            {
                System.Console.WriteLine("Start Uplond3");
                var random = RandomString(10);
                string filePath2 = formFile.Value.FileName;
                string filename = Path.GetFileName(filePath2);
                string ext = Path.GetExtension(filename);

                if (formFile.Value.Length > 0)
                {
                    System.Console.WriteLine("Start Uplond4");
                    // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                    using (var stream = System.IO.File.Create(filePath + random + filename))
                    {
                        await formFile.Value.CopyToAsync(stream);
                    }
                    System.Console.WriteLine("Start Uplond4.1");
                    var Trainingdata = new TrainingDocument
                    {
                        TrainingId = trainingid,
                        Name = random + filename,
                        Detail = model.Detail,

                        CreatedAt = date
                    };
                    System.Console.WriteLine("Start Uplond4.2");
                    _context.TrainingDocuments.Add(Trainingdata);
                    _context.SaveChanges();
                    System.Console.WriteLine("Start Uplond4.2");
                }
            }
            return Ok(new { status = true });
        }
        //----------------------------------

        //------zone training report-------
        //GET api/Training/historyreport
        [HttpGet("historyreport/{name}")]
        public IActionResult GetHistoryReport(string name)
        {
            var districtdata = _context.TrainingRegisters
                .Include(m => m.Training)
                .Where(m => m.Name == name);

            return Ok(districtdata);

        }


        // //GET api/Training/trainingid
        // [HttpGet("{trainingid}")]
        // public IActionResult Get2(long trainingid)
        // {
        //     var districtdata = _context.TrainingRegisters
        //         .Include(m => m.Training)
        //         .Where(m => m.TrainingId == trainingid);

        //     return Ok(districtdata);

        //     //return _context.TrainingRegisters
        //     //           .Include(m => m.Training)
        //     //           .Where(m => m.TrainingId == trainingid);

        // }

        //------end training report--------






        //------zone training program-------
        //GET api/training/program
        [HttpGet("program/{phaseid}")]
        public IActionResult GetHistoryReport(long phaseid)
        {
            var districtdata = _context.TrainingPrograms
                .Include(m => m.TrainingPhase)
                .Where(m => m.TrainingPhaseId == phaseid);

            return Ok(districtdata);

        }

        // POST api/training/program/trainingid
        //[HttpPost("program/save/{trainingid}")]
        // public TrainingProgram InsertTrainingProgram(long trainingid, long programtype, string programtopic, string programdetail, DateTime programdate, string minutestart, string minuteend, string lecturername)
        // {
        //     var date = DateTime.Now;
        //     System.Console.WriteLine("Start InsertTrainingProgram");
        //     var trainingdata = new TrainingProgram
        //     {
        //         TrainingId = trainingid,
        //         ProgramType = programtype,
        //         ProgramDetail = programdetail,
        //         ProgramDate = programdate,
        //         MinuteStartDate = minutestart,
        //         MinuteEndDate = minuteend,
        //         LecturerId = lecturername,
        //         ProgramTopic = programtopic,
        //         CreatedAt = date
        //     };

        //     _context.TrainingPrograms.Add(trainingdata);
        //     _context.SaveChanges();

        //     return trainingdata;
        // }

        // POST api/values
        [HttpPost("program")]
        public async Task<IActionResult> Post([FromForm] TrainingProgramViewModel model)
        {

            var date = DateTime.Now;
            var trainingprogramdata = new TrainingProgram
            {
                TrainingPhaseId = model.TrainingPhaseId,
                ProgramDate = model.ProgramDate,
                MinuteStartDate = model.MinuteStartDate,
                MinuteEndDate = model.MinuteEndDate,
                ProgramType = model.ProgramType,
                ProgramTopic = model.ProgramTopic,
                ProgramDetail = model.ProgramDetail,
                ProgramLocation = model.ProgramLocation,
                ProgramToDress = model.ProgramToDress,
                CreatedAt = date
            };

            _context.TrainingPrograms.Add(trainingprogramdata);
            _context.SaveChanges();

            foreach (var id in model.TrainingLecturerId)
            {
                var trainingprogramlecturerdata = new TrainingProgramLecturer
                {
                    TrainingProgramId = trainingprogramdata.Id,
                    TrainingLecturerId = id
                };
                _context.TrainingProgramLecturers.Add(trainingprogramlecturerdata);
                _context.SaveChanges();

            }

            int index = 0;
            int indexend = 0;

            //int maxSize = Int32.Parse(ConfigurationManager.AppSettings["MaxFileSize"]);
            //var size = data.files.Sum(f => f.Length);

            //ตรวจสอบว่ามี Folder Upload ใน wwwroot มั้ย
            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }

            //var BaseUrl = url.ActionContext.HttpContext.Request.Scheme;
            // path ที่เก็บไฟล์
            var filePath = _environment.WebRootPath + "//Uploads//";


            if (model.files != null)
            {
                foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
                //foreach (var formFile in data.files)
                {
                    var random = RandomString(10);
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);

                    if (formFile.Value.Length > 0)
                    {
                        // using (var stream = System.IO.File.Create(filePath + formFile.Value.FileName))
                        using (var stream = System.IO.File.Create(filePath + random + filename))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }

                        var trainingprogramfiledata = new TrainingProgramFile
                        {
                            TrainingProgramId = trainingprogramdata.Id,
                            Name = random + filename,
                        };
                        _context.TrainingProgramFiles.Add(trainingprogramfiledata);
                        _context.SaveChanges();
                    }
                }
            }
            return Ok(new { status = true });

        }

        // DELETE api/training/program/delete/{trainingid}
        [HttpDelete("program/delete/{trainingid}")]
        public void DeleteTrainingProgram(long trainingid)
        {
            var trainingdata = _context.TrainingPrograms.Find(trainingid);

            _context.TrainingPrograms.Remove(trainingdata);
            _context.SaveChanges();
        }
        //------end training program---------






        //------zone training lecturer-------
        //GET api/training/program
        [HttpGet("lecturer")]
        public IEnumerable<TrainingLecturer> GetTrainingLecturers()
        {
            var data = from P in _context.TrainingLecturers
                       select P;
            return data;
        }

        // POST : api/training/lecturer/save
        [HttpPost("lecturer/save")]
        public TrainingLecturer addTraininglecturer(string lecturername, string lecturerphone, string lectureremail, string education, string workhistory, string experience)
        {
            var date = DateTime.Now;

            var trainingdata = new TrainingLecturer
            {
                LecturerName = lecturername
                ,
                Phone = lecturerphone
                ,
                Email = lectureremail
                ,
                Education = education
                ,
                WorkHistory = workhistory
                ,
                Experience = experience
                ,
                CreatedAt = date

            };

            _context.TrainingLecturers.Add(trainingdata);
            _context.SaveChanges();

            return trainingdata;
        }

        // PUT : api/training/edit/:id
        [HttpPut("lecturer/edit/{id}")]
        public void EditTraininglecturer(long id, string lecturername, string lecturerphone, string lectureremail, string education, string workhistory, string experience)
        {
            var training = _context.TrainingLecturers.Find(id);
            training.LecturerName = lecturername;
            training.Phone = lecturerphone;
            training.Email = lectureremail;
            training.Education = education;
            training.WorkHistory = workhistory;
            training.Experience = experience;
            _context.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE : api/training/lecturer/delete/:id
        [HttpDelete("lecturer/delete/{id}")]
        public void DeleteTrainingLecturer(long id)
        {
            var trainingdata = _context.TrainingLecturers.Find(id);

            _context.TrainingLecturers.Remove(trainingdata);
            _context.SaveChanges();
        }
        //------end training lecturer---------




        //GET api/training/program
        [HttpGet("testword/")]
        public void Gettest()
        {

            if (!Directory.Exists(_environment.WebRootPath + "//DocumentReport_Training//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//DocumentReport_Training//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "/DocumentReport_Training/";
            var filename = "DOC.docx";
            var createfile = filePath + filename;



            using (DocX document = DocX.Create(createfile))
            {
                var subject = document.InsertParagraph();
                subject.Append("p' teay").FontSize(18).Alignment = Alignment.center;
                subject.SpacingAfter(40d);

                //var i4 = document.InsertParagraph();

                //i4.AppendPicture(picture3).Alignment = Alignment.center;
                //i4.AppendPicture(picture3).Alignment = Alignment.center;

                document.Save();
                Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");

            }
        }



        //GET api/training/program
        [HttpGet("exportpassport/")]
        public void exportpassport()
        {

            if (!Directory.Exists(_environment.WebRootPath + "//DocumentReport_Training//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//DocumentReport_Training//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "/DocumentReport_Training/";
            var filename = "DOC2.docx";
            var createfile = filePath + filename;



            // Create a document
            using (var document = DocX.Create(createfile))
            {
                // Add a title
                document.InsertParagraph("Columns width").FontSize(15d).SpacingAfter(50d).Alignment = Alignment.center;

                // Insert a title paragraph.
                var p = document.InsertParagraph("In the following table, the cell's left margin has been removed for rows 2-6 as well as the top/bottom table's borders.").Bold();
                p.Alignment = Alignment.center;
                p.SpacingAfter(40d);

                // Add a table in a document of 1 row and 3 columns.
                var columnWidths = new float[] { 500f };
                var t = document.InsertTable(1, columnWidths.Length);

                // Set the table's column width and background 
                t.SetWidths(columnWidths);
                t.AutoFit = AutoFit.Contents;

                var row = t.Rows.First();

                // Fill in the columns of the first row in the table.
                for (int i = 0; i < row.Cells.Count; ++i)
                {
                    row.Cells[i].Paragraphs.First().Append("หัวข้อ " + i);
                }

                // Add rows in the table.
                for (int i = 0; i < 5; i++)
                {
                    var newRow = t.InsertRow();

                    // Fill in the columns of the new rows.
                    for (int j = 0; j < newRow.Cells.Count; ++j)
                    {
                        var newCell = newRow.Cells[j];
                        newCell.Paragraphs.First().Append("ข้อมูล " + i);
                        // Remove the left margin of the new cells.
                        newCell.MarginLeft = 0;
                    }
                }

                // Set a blank border for the table's top/bottom borders.
                // var blankBorder = new Border( BorderStyle.Tcbs_none, 0, 0, Color.White );
                // t.SetBorder( TableBorderType.Bottom, blankBorder );
                // t.SetBorder( TableBorderType.Top, blankBorder );

                document.Save();
                Console.WriteLine("\tCreated: ColumnsWidth.docx\n");
            }

        }




        //-------------------------------------zone training phase---------------------------------------------
        //GET api/training/phase
        [HttpGet("phase/{trainingid}")]
        public IActionResult GetTrainingPhase(long trainingid)
        {
            var districtdata = _context.TrainingPhases
                .Include(m => m.Training)
                .Where(m => m.TrainingId == trainingid);

            return Ok(districtdata);

        }

        //GET api/training/phase
        [HttpGet("phase/count/{trainingid}")]
        public IActionResult GetTrainingPhasecount(long trainingid)
        {
            var districtdata = _context.TrainingPhases
                .Include(m => m.Training)
                .Where(m => m.TrainingId == trainingid).Count();

            return Ok(districtdata);

        }

        // POST api/training/trainingphase
        [HttpPost("phase")]
        public IActionResult Postphase([FromForm] TrainingphaseViewModel model)
        {
            var date = DateTime.Now;
            System.Console.WriteLine("Start");
            var trainingphasedata = new TrainingPhase
            {

                TrainingId = model.TrainingId,
                PhaseNo = model.PhaseNo,
                Title = model.Title,
                Detail = model.Detail,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Location = model.Location,
                Group = model.Group,
                CreatedAt = date
            };
            _context.TrainingPhases.Add(trainingphasedata);
            _context.SaveChanges();

            return Ok(new { status = true });
        }

        // DELETE : api/training/phase/delete/:id
        [HttpDelete("phase/delete/{id}")]
        public void DeleteTrainingPhase(long id)
        {
            var trainingdata = _context.TrainingPhases.Find(id);

            _context.TrainingPhases.Remove(trainingdata);
            _context.SaveChanges();
        }

        //-----------------------------------end zone training phase-------------------------------------------

        //-------------------------------------zone training condition---------------------------------------------

        // POST api/training/condition/add/trainingid
        [HttpPost("condition/add/{trainingid}")]
        public TrainingCondition InsertTrainingCondition(string name, long trainingid, int startyear, int endyear, int conditiontype)
        {
            var date = DateTime.Now;

            var trainingdata = new TrainingCondition
            {
                TrainingId = trainingid,
                Name = name,
                StartYear = startyear,
                EndYear = endyear,
                ConditionType = conditiontype,
                CreatedAt = date

            };

            _context.TrainingConditions.Add(trainingdata);
            _context.SaveChanges();

            return trainingdata;
        }


        // PUT : api/training/edit/:id
        [HttpPut("condition/edit/{id}")]
        public void EditTrainingCondtion(string name, long id, int startyear, int endyear, int conditiontype)
        {
            var training = _context.TrainingConditions.Find(id);
            training.Name = name;
            training.StartYear = startyear;
            training.EndYear = endyear;
            training.ConditionType = conditiontype;

            _context.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }


        //GET api/training/condition
        [HttpGet("condition/{trainingid}")]
        public IActionResult GetTrainingCondition(long trainingid)
        {
            var trainingdata = _context.TrainingConditions
                .Include(m => m.Training)
                .Include(m => m.TrainingRegisterConditions)
                .ThenInclude(m => m.TrainingRegister)
                .Where(m => m.TrainingId == trainingid);

            return Ok(trainingdata);

        }

        // DELETE : api/training/condition/delete/:id
        [HttpDelete("condition/delete/{id}")]
        public void DeleteTrainingCondition(long id)
        {
            var trainingdata = _context.TrainingConditions.Find(id);

            _context.TrainingConditions.Remove(trainingdata);
            _context.SaveChanges();
        }
        //-----------------------------------end zone training condition-------------------------------------------

        //GET api/Training/trainingid
        [HttpGet("peopledetail/{id}")]
        public IActionResult Peopledetail(long id)
        {
            var districtdata = _context.TrainingRegisters
                .Where(m => m.Id == id).FirstOrDefault();

            return Ok(districtdata);

        }

        [HttpPost("printNamePlate")]
        public IActionResult PrintNamePlate(TrainingViewModel model)
        {
            List<object> termsList = new List<object>();

            foreach (var id in model.RegisterId)
            {
                var registerData = _context.TrainingRegisters
                    .Include(p => p.Training)
                    .Where(x => x.Id == id)
                    .FirstOrDefault();
                termsList.Add(registerData);
            }
            return Ok(termsList);
        }

        //GET api/Training/plan
        [HttpGet("plan/{id}")]
        public IActionResult Plan(long id)
        {
            var districtdata = _context.TrainingProgramLecturers
                .Include(m => m.TrainingProgram)
                .ThenInclude(m => m.TrainingPhase)
                .Include(m => m.TrainingLecturer)
                .Where(m => m.TrainingProgram.TrainingPhaseId == id);

            return Ok(districtdata);

        }

       // PUT api/training/register/group/:id
       [HttpPut("register/group/{id}")]
        public void EditRegisterGroup(long id, long approve1, long approve2, long approve3, long approve4, long approve5, long approve6, long approve7, long approve8, long approve9, long approve10)
        {
            var training = _context.TrainingRegisters.Find(id);
            training.Group1 = approve1;
            training.Group2 = approve2;
            training.Group3 = approve3;
            training.Group4 = approve4;
            training.Group5 = approve5;
            training.Group6 = approve6;
            training.Group7 = approve7;
            training.Group8 = approve8;
            training.Group9 = approve9;
            training.Group10 = approve10;
            _context.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }
    }

}
