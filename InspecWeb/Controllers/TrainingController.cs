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

            foreach(var test in survey)
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

            //var trainingdata = new Training
            //{
            //    Name = model.Name,
            //    Detail = detail,
            //    StartDate = start_date,
            //    EndDate = end_date,
            //    LecturerName = lecturer_name,
            //    RegisStartDate = regis_start_date,
            //    RegisEndDate = regis_end_date,
            //    Image = image,
            //    CreatedAt = date
            //};

            //_context.Trainings.Add(trainingdata);
            //_context.SaveChanges();

            //return trainingdata;
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
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        LecturerName = model.LecturerName,
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
        public void Put(long id, string name, string detail, DateTime start_date, DateTime end_date, string lecturer_name, DateTime regis_start_date, DateTime regis_end_date, string image)
        {
            var training = _context.Trainings.Find(id);
            training.Name = name;
            training.Detail = detail;
            training.StartDate = start_date;
            training.EndDate = end_date;
            training.LecturerName = lecturer_name;
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


        //------zone training register-------
        // PUT api/values/5
        [HttpPut("registerlist/{id}")]
        public void EditRegisterList(long id, long status)
        {
            var training = _context.TrainingRegisters.Find(id);
            training.Status = status;

            if (status == 1){
                var message = new Message(new string[] { "fantasy_tey@hotmail.com" }, "Test email", "This is the content from our email.");
                _emailSender.SendEmail(message);
            }

            _context.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        //----------------------------------


        //------zone training register font-end-------
        // POST api/training/trainingsurvey/trainingid
        [HttpPost("trainingregister/{trainingid}")]
        public TrainingRegister InsertTrainingRegister(string name, long trainingid, string phone, string cardid, string position, long status, string userid, long usertype, string email)
        {
            var date = DateTime.Now;

            var trainingdata = new TrainingRegister
            {
                TrainingId = trainingid,
                Name = name,
                Phone = phone,
                CardId = cardid,
                Position = position,
                Status = 0,
                UserId = "TEST",
                UserType = 1,
                Email = email,
                CreatedAt = date

            };

            _context.TrainingRegisters.Add(trainingdata);
            _context.SaveChanges();

            return trainingdata;
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

            foreach (var item in model.inputtrainingsurveyanswer) {

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

            

            return Ok(new { status = "Save Success" }) ;
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
        public async Task<IActionResult> InsertDocument([FromForm] TrainingDocumentViewModel model , long trainingid)
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
        [HttpGet("program/{trainingid}")]
        public IActionResult GetHistoryReport(long trainingid)
        {
            var districtdata = _context.TrainingPrograms
                .Include(m => m.Training)
                .Where(m => m.TrainingId == trainingid);
                
            return Ok(districtdata);

        }

        // POST api/training/program/trainingid
        [HttpPost("program/save/{trainingid}")]
        public TrainingProgram InsertTrainingProgram(long trainingid, string programtopic, string programdetail, DateTime programdate, string minutestart, string minuteend, string lecturername)
        {
            var date = DateTime.Now;
            System.Console.WriteLine("Start InsertTrainingProgram");
            var trainingdata = new TrainingProgram
            {
                TrainingId = trainingid,
                ProgramDetail = programdetail,
                ProgramDate = programdate,
                MinuteStartDate = minutestart,
                MinuteEndDate = minuteend,
                LecturerId = lecturername,
                ProgramTopic = programtopic,
                CreatedAt = date
            };

            _context.TrainingPrograms.Add(trainingdata);
            _context.SaveChanges();

            return trainingdata;
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
                ,Phone = lecturerphone
                ,Email = lectureremail
                ,Education = education
                ,WorkHistory = workhistory
                ,Experience = experience
                ,CreatedAt = date

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

        

    }
}
