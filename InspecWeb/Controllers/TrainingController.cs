using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.Services;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Image = Xceed.Document.NET.Image;

//using EmailService;
using Xceed.Document.NET;
using Xceed.Words.NET;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class TrainingController : Controller
    {
        private readonly ApplicationDbContext _context;
        public static IWebHostEnvironment _environment;
        private readonly IMailService mailService;
        private static Random random = new Random();
        //private readonly IEmailSender _emailSender;
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        //public MailController(IMailService mailService)
        //{
        //    this.mailService = mailService;
        //}

        public TrainingController(
            ApplicationDbContext context,
            IWebHostEnvironment environment
            , IMailService mailService
        //IEmailSender emailSender
        )
        {
            _context = context;
            _environment = environment;
            this.mailService = mailService;
            //_emailSender = emailSender;
            //_emailSender = emailSender;
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

        //----------zone training------------
        // GET: api/Training
        [HttpGet("ShowPage")]
        public IEnumerable<Training> GetTrainingsShowPage()
        {
            var trainingdata = from P in _context.Trainings
                               .Where(m => m.Status == 1)
                               select P;
            return trainingdata;
        }

        // GET: api/Training/trainingsurveycount
        [HttpGet("trainingsurveycount")]
        public IEnumerable<object> GetTrainingCountSurvey()
        {
            var trainingdata = from P in _context.Trainings
                               select P;

            var result = new List<object>();


            var survey = _context.TrainingSurveyTopics.ToList();

            //var survey = _context.Trainings
            //    .Include(m => m.TrainingSurveys).ToList();

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
                    .Where(x => x.TrainingSurveyTopicId == test.Id)
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
                    trainingdata = test,
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
        [HttpGet("listsurvey/{surveytopicid}")]
        public IActionResult GetListTrainingSurvey(long surveytopicid)
        {
            var districtdata = _context.TrainingSurveys
                .Include(m => m.TrainingSurveyTopic)
                .Where(m => m.TrainingSurveyTopicId == surveytopicid);

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
                .Where(m => m.TrainingId == trainingid)
                .Where(p => p.UserId == userid).Count();

            if (TrainingRegistersdata > 0)
            {
                return Ok(true);
            }
            else
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
                    var xxx = _context.Trainings.Where(m => m.Id == Trainingdata.Id).FirstOrDefault();
                    return Ok(xxx);

                }

            }
            return Ok( new { status = true});
            


        }

        // PUT : api/training/edit/:id
        [HttpPut("maintraining/edit/{id}")]
        public async Task<IActionResult> EditTraininglecturer([FromForm] TrainingViewModel model, long id)
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

            var training = _context.Trainings.Find(id);

            if (model.files != null)
            {
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

                        System.Console.WriteLine("Start Uplond4.2");
                        //_context.TrainingLecturers.Add(Trainingdata);

                        //training.Name = model.Name;
                        //training.Detail = model.Detail;
                        //training.Generation = model.Generation;
                        //training.Year = model.Year;
                        //training.CourseCode = model.CourseCode;
                        //training.StartDate = model.StartDate;
                        //training.EndDate = model.EndDate;
                        //training.RegisStartDate = model.RegisStartDate;
                        //training.RegisEndDate = model.RegisEndDate;
                        training.Image = random + filename;
                        //training.CreatedAt = date;


                        System.Console.WriteLine("Start Uplond4.3");
                    }

                }
            }

            training.Name = model.Name;
            training.Detail = model.Detail;
            training.Generation = model.Generation;
            training.Year = model.Year;
            training.CourseCode = model.CourseCode;
            training.StartDate = model.StartDate;
            training.EndDate = model.EndDate;
            training.RegisStartDate = model.RegisStartDate;
            training.RegisEndDate = model.RegisEndDate;
            //training.Image = random + filename;
            training.CreatedAt = date;
            _context.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            System.Console.WriteLine("Start Uplond4.4");

            return Ok(training);

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
        [HttpPost("registerlist/{id}/{trainingid}")]
        public async Task<IActionResult> EditRegisterList(long id, long status, long trainingid)
        {
            var training = _context.TrainingRegisters.Find(id);
            training.Status = status;

            string Emailregis = _context.TrainingRegisters
                .Where(m => m.Id == id)
                .Select(m => m.Email)
                .FirstOrDefault();

            System.Console.WriteLine(Emailregis);

            var datatraining = _context.Trainings
                            .Where(m => m.Id == trainingid)
                            .ToList();

            var databody = _context.TrainingProgramFiles
                            .Include(m => m.TrainingProgram)
                            .ThenInclude(m => m.TrainingPhase)
                            .ThenInclude(m => m.Training)
                            .Where(m => m.TrainingProgram.TrainingPhase.TrainingId == trainingid)
                            .ToList();

            System.Console.WriteLine(datatraining[0].Name);

            List<string> termsList = new List<string>();
            string textbodyHead = "<h1>" + datatraining[0].Name + "</h1>";
            string textbody = "";
            string Host = $"<a href='{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/upload/";
            string textFoot = "<br /><br /> ระบบตรวจราชการอิเล็กทรอนิกส์ <br /> สำนักงานปลัดสำนักนายกรัฐมนตรี";
            //string EndHost = "></a>";

            if (databody.Count > 0) {
                foreach (var data in databody)
                {

                    textbody = textbody + Host + data.Name + "' > " + data.TrainingProgram.ProgramTopic + " วันที่ " + data.TrainingProgram.ProgramDate + " (" + data.TrainingProgram.MinuteStartDate + "-" + data.TrainingProgram.MinuteEndDate + ")" + "</a><br />";
                    //termsList.Add(data.Name);

                }
            }
            
            //string xxx = termsList.ToString().Replace(",", " <br>");

            //return Ok(textbody);

            var mailbody = "";
            if (status == 1)
            {
                mailbody = textbodyHead + "<br /> ท่านได้รับอนุมัติสิทธิ์ในการเข้าร่วมอบรมหลักสูตร ท่านสามารถดาวน์โหลดไฟล์เพื่อประกอบการฝึกอบรมตาม วัน/เวลา การอบรม <br />" + textbody + textFoot;
            }
            else if (status == 2)
            {
                mailbody = textbodyHead + "<br /> ท่านไม่ผ่านสมัครเข้าร่วมอบรมหลักสูตร เนื่องจากท่านไม่ตรงตามเงื่อนไขคุณสมบัติของหลักสูตรอบรม <br />" + textFoot;
            }


            ///----------------email
            try
            {
                var send = new MailRequest
                {
                    //ToEmail = "toey.aphisit@outlook.com",
                    ToEmail = Emailregis,
                    Body = mailbody,
                    Subject = "ระบบตรวจราชการอิเล็กทรอนิกส์"
                    //Host = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}",
                };
                await mailService.SendEmailAsync(send);


            }
            catch (Exception ex)
            {

                throw ex;
            }
            //---------------


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

            return Ok(true);

            //  var datas = _context.TrainingDocuments
            //     .Include(m => m.Training)
            //     .Where(m => m.TrainingId == trainingid).ToList();

            // var text = '';
            // foreach(data in datas)
            // {
            //     text = text + data.name;
            // }
        }

        //GET api/Training/trainingid
        [HttpGet("testtest20201116")]
        public IActionResult GetDetailTrainingtest()
        {
            var databody = _context.TrainingProgramFiles
                            .Include(m => m.TrainingProgram)
                            .ThenInclude(m => m.TrainingPhase)
                            .ThenInclude(m => m.Training)
                            .Where(m => m.TrainingProgram.TrainingPhase.TrainingId == 1)
                            .ToList();


            return Ok(databody.Count);

        }


        //GET api/Training/trainingid
        [HttpGet("testtest2020")]
        public IActionResult GetDetailTraining2()
        {
            var districtdata = _context.TrainingProgramFiles
                                .Include(m => m.TrainingProgram)
                                .ThenInclude(m => m.TrainingPhase)
                                .Where(m => m.TrainingProgram.TrainingPhase.TrainingId == 1);

            List<string> termsList = new List<string>();
            string xxx = "";
            foreach (var data in districtdata)
            {
                xxx = xxx + data.Name + " <br />";
                //termsList.Add(data.Name);
                
            }
            //string xxx = termsList.ToString().Replace(",", " <br>");
            string Host = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/upload/";
            return Ok(districtdata);

        }


        [HttpPost("registerlist2/{trainingId}")]
        public async Task<IActionResult> EditRegisterList2(long[] traningregisterid, long status ,long trainingId)
        {
            foreach (var id in traningregisterid)
            {

                var training = _context.TrainingRegisters.Find(id);
                training.Status = status;

                string Emailregis = _context.TrainingRegisters
                .Where(m => m.Id == id)
                .Select(m => m.Email)
                .FirstOrDefault();

                System.Console.WriteLine(Emailregis);

                var databody = _context.TrainingProgramFiles
                                .Include(m => m.TrainingProgram)
                                .ThenInclude(m => m.TrainingPhase)
                                .ThenInclude(m => m.Training)
                                .Where(m => m.TrainingProgram.TrainingPhase.TrainingId == trainingId)
                                .ToList();

                List<string> termsList = new List<string>();
                string textbodyHead = "<h1>" + databody[0].TrainingProgram.TrainingPhase.Training.Name + "</h1>";
                string textbody = "";
                string Host = $"<a href='{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/upload/";
                string textFoot = "<br /><br /> ระบบตรวจราชการอิเล็กทรอนิกส์ <br /> สำนักงานปลัดสำนักนายกรัฐมนตรี";
                //string EndHost = "></a>";

                foreach (var data in databody)
                {

                    textbody = textbody + Host + data.Name + "' > " + data.TrainingProgram.ProgramTopic + " วันที่ " + data.TrainingProgram.ProgramDate + " (" + data.TrainingProgram.MinuteStartDate + "-" + data.TrainingProgram.MinuteEndDate + ")" + "</a><br />";
                    //termsList.Add(data.Name);

                }
                //string xxx = termsList.ToString().Replace(",", " <br>");

                //return Ok(textbody);

                var mailbody = "";
                if (status == 1)
                {
                    mailbody = textbodyHead + "<br /> ท่านได้รับอนุมัติสิทธิ์ในการเข้าร่วมอบรมหลักสูตร ท่านสามารถดาวน์โหลดไฟล์เพื่อประกอบการฝึกอบรมตาม วัน/เวลา การอบรม <br />" + textbody + textFoot;
                }
                else if (status == 2)
                {
                    mailbody = textbodyHead + "<br /> ท่านไม่ผ่านสมัครเข้าร่วมอบรมหลักสูตร เนื่องจากท่านไม่ตรงตามเงื่อนไขคุณสมบัติของหลักสูตรอบรม <br />" + textFoot;
                }


                ///----------------email
                try
                {
                    var send = new MailRequest
                    {
                        //ToEmail = "toey.aphisit@outlook.com",
                        ToEmail = Emailregis,
                        Body = mailbody,
                        Subject = "ระบบตรวจราชการอิเล็กทรอนิกส์"
                        //Host = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}",
                    };
                    await mailService.SendEmailAsync(send);
                   

                }
                catch (Exception ex)
                {

                    throw ex;
                }
                //---------------

                _context.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();

             
            }
            return Ok(true);
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

        //GET api/Training/trainingid
        [HttpGet("approve/get/{trainingid}")]
        public IActionResult GetTrainingRegisterApprove(long trainingid)
        {
            var districtdata = _context.TrainingRegisters
                .Include(m => m.Training)
                .Where(m => m.TrainingId == trainingid && m.Status == 1);

            return Ok(districtdata);

            //return _context.TrainingRegisters
            //           .Include(m => m.Training)
            //           .Where(m => m.TrainingId == trainingid);

        }


        //GET api/Training/trainingid
        [HttpGet("trainingregisterlist/get/{trainingid}")]
        public IActionResult GetTrainingRegisterList(long trainingid)
        {
            var districtdata = _context.TrainingRegisters
                .Include(m => m.Training)
                .Include(m => m.ProvincialDepartments)
                .Where(m => m.TrainingId == trainingid)
                .OrderBy(m => m.IDCode);

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
        [HttpPost("trainingsurvey/{surveyid}")]
        public TrainingSurvey Post(string name, int surveytype, long surveyid)
        {
            var date = DateTime.Now;

            var trainingdata = new TrainingSurvey
            {
                TrainingSurveyTopicId = surveyid,
                Name = name,
                SurveyType = surveytype,
                CreatedAt = date

            };

            _context.TrainingSurveys.Add(trainingdata);
            _context.SaveChanges();

            return trainingdata;
        }

        // POST api/training/trainingsurveytopic/add/surveyid
        [HttpPost("trainingsurveytopic/add")]
        public TrainingSurveyTopic TrainingSurveyTopic_Add(string name)
        {
            var date = DateTime.Now;

            var trainingdata = new TrainingSurveyTopic
            {
                Name = name,
                SurveyType = 1,
                CreatedAt = date

            };

            _context.TrainingSurveyTopics.Add(trainingdata);
            _context.SaveChanges();

            return trainingdata;
        }

        // PUT : api/training/trainingsurveytopic/edit/:id
        [HttpPut("trainingsurveytopic/edit/{id}")]
        public TrainingSurveyTopic TrainingSurveyTopic_Edit(long id, string name)
        {
            var training = _context.TrainingSurveyTopics.Find(id);
            training.Name = name;
            _context.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return training;
        }


        // PUT : api/training/edit/:id
        [HttpPut("survey/edit/{id}")]
        public TrainingSurvey EditTrainingsurvey(long id, string name, int surveytype)
        {
            var training = _context.TrainingSurveys.Find(id);
            training.Name = name;
            training.SurveyType = surveytype;
            _context.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return training;

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
                    TrainingLecturerJoinSurveyId = model.TrainingLecturerJoinSurveyId,
                    TrainingSurveyId = item.trainingsurveyId,
                    Username = model.Username,
                    Name = model.Name,
                    Posoition = model.Position,
                    SurveyType = item.SurveyType,
                    Score = item.score,
                    AnswerText = item.ansText,
                    AnswerYorN = item.ansYesOrNo,
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
        public IActionResult GetProgram(long phaseid)
        {
            var districtdata = _context.TrainingPrograms
                .Include(m => m.TrainingPhase)
                .Include(m => m.TrainingProgramLecturers)
                .Include(m => m.TrainingProgramFiles)
                .Where(m => m.TrainingPhaseId == phaseid);

            return Ok(districtdata);

        }
        //GET api/training/program
        [HttpGet("programdetail/{programid}")]
        public IActionResult GetProgramDetail(long programid)
        {
            var districtdata = _context.TrainingPrograms
                .Include(m => m.TrainingPhase)
                .Include(m => m.TrainingProgramLecturers)
                .Include(m => m.TrainingProgramFiles)
                .Where(m => m.Id == programid)
                .FirstOrDefault();

            return Ok(districtdata);

        }

        //GET api/training/program
        [HttpGet("TrainingProgramDate/get/{trainingid}")]
        public IActionResult GetTrainingProgramDate(long trainingid)
        {
            var result = new List<object>();

            var districtdata = _context.TrainingPrograms
                .Include(m => m.TrainingPhase)
                .ThenInclude(m => m.Training)
                .Where(m => m.TrainingPhase.TrainingId == trainingid);

            foreach (var test in districtdata)
            {
                //System.Console.WriteLine("in1");
                //var test2 = _context.TrainingProgramLoginQRCodes
                //    .Where(x => x.ProgramDate == test.ProgramDate).FirstOrDefault();

                var checkmorning = _context.TrainingProgramLoginQRCodes
                .Where(m => m.ProgramDate == test.ProgramDate)
                .Select(m => m.Morning);

                var checkafternoon = _context.TrainingProgramLoginQRCodes
                .Where(m => m.ProgramDate == test.ProgramDate)
                .Select(m => m.Afternoon);

                var programloginid = _context.TrainingProgramLoginQRCodes
                .Where(m => m.ProgramDate == test.ProgramDate)
                .Select(m => m.Id);



                result.Add(new
                {
                    programDate = test.ProgramDate,
                    trainingPhaseId = test.TrainingPhaseId,
                    programloginId = programloginid,
                    xMorning = checkmorning,
                    xAfternoon = checkafternoon
                });




                //if (test2 != null)
                //{
                //    foreach (var test3 in test2)
                //    {
                //        System.Console.WriteLine("in2");
                //        result.Add(new
                //        {
                //            Name = test.ProgramDate,
                //            Count = test3.Morning,
                //            Count2 = test3.Afternoon,
                //        });
                //    }
                //}

            }

            return Ok(result);

        }

        //GET api/training/program
        [HttpGet("TrainingProgramLoginQRDate/get/{programdate}")]
        public IActionResult GetTrainingProgramLoginQRDate(DateTime programdate)
        {

            var districtdata = _context.TrainingProgramLoginQRCodes
                .Where(m => m.ProgramDate == programdate);


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
        [HttpPost("program/add")]
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
            return Ok(trainingprogramdata);

        }

        // DELETE api/training/program/delete/{trainingid}
        [HttpDelete("program/delete/{trainingid}")]
        public void DeleteTrainingProgram(long trainingid)
        {
            var trainingdata = _context.TrainingPrograms.Find(trainingid);

            _context.TrainingPrograms.Remove(trainingdata);
            _context.SaveChanges();
        }

        [HttpPut("program/edit/{programid}")]
        public async Task<IActionResult> Put([FromForm] TrainingProgramViewModel model, long programid)
        {
            var programdata = _context.TrainingPrograms.Find(programid);
            {
                programdata.ProgramDate = model.ProgramDate;
                programdata.MinuteStartDate = model.MinuteStartDate;
                programdata.MinuteEndDate = model.MinuteEndDate;
                programdata.ProgramType = model.ProgramType;
                programdata.ProgramTopic = model.ProgramTopic;
                programdata.ProgramDetail = model.ProgramDetail;
                programdata.ProgramLocation = model.ProgramLocation;
                programdata.ProgramToDress = model.ProgramToDress;
            };

            //_context.CentralPolicies.Add(centralpolicydata);
            _context.Entry(programdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            if (model.RemoveLecturer != null)
            {
                System.Console.WriteLine("edit" + model.RemoveLecturer.Length);
                foreach (var removelecturerId in model.RemoveLecturer)
                {
                    var removedata = _context.TrainingProgramLecturers
                    .Where(x => x.TrainingLecturerId == removelecturerId && x.TrainingProgramId == programid)
                    .ToList();

                    foreach (var remove in removedata)
                    {
                        _context.TrainingProgramLecturers.Remove(remove);
                    }
                    _context.SaveChanges();
                }
            }

            if (model.AddLecturer != null)
            {
                foreach (var addlecturerId in model.AddLecturer)
                {
                    System.Console.WriteLine("addID: " + addlecturerId);
                    var trainingprogramlecturerdata = new TrainingProgramLecturer
                    {
                        TrainingProgramId = programid,
                        TrainingLecturerId = addlecturerId
                    };
                    _context.TrainingProgramLecturers.Add(trainingprogramlecturerdata);
                    _context.SaveChanges();
                }
            }
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
                            TrainingProgramId = programid,
                            Name = random + filename,
                        };
                        _context.TrainingProgramFiles.Add(trainingprogramfiledata);
                        _context.SaveChanges();
                    }
                }
            }
            return Ok(programdata);
        }

        // DELETE api/training/program/delete/{trainingid}
        [HttpDelete("program/deletefiles/{filesid}")]
        public void DeleteTrainingProgramFiles(long filesid)
        {
            var trainingprogramfilesdata = _context.TrainingProgramFiles.Find(filesid);

            _context.TrainingProgramFiles.Remove(trainingprogramfilesdata);
            _context.SaveChanges();
        }
        //------end training program---------






        //------zone training lecturer-------
        //GET api/training/program
        [HttpGet("lecturer")]
        public IEnumerable<TrainingLecturer> GetTrainingLecturers()
        {
            //var data = from P in _context.TrainingLecturers
            //           .Include(m => m.TrainingLecturerTypes)
            //           select P;
            var data = _context.TrainingLecturers
                .Include(m => m.TrainingLecturerTypes)
                .ToList();

            return data;
        }

        //------zone training lecturer-------
        //GET api/training/lecturer/{id}
        [HttpGet("lecturer/{id}")]
        public IEnumerable<TrainingLecturer> GetTrainingLecturersByid(long id)
        {
            var data = _context.TrainingLecturers
                .Include(m => m.TrainingLecturerTypes)
                .Where(m => m.Id == id)
                .ToList();

            return data;
        }

        //GET api/training/lecturerlist
        [HttpGet("lecturerlist/{trainingid}")]
        public IActionResult GetTrainingLecturersList(long trainingid)
        {
            //var trainingdata = from P in _context.Trainings
            //                   select P;

            var result = new List<object>();


            //var survey = _context.TrainingSurveyTopics.ToList();

            var districtdata = _context.TrainingProgramLecturers
                .Include(m => m.TrainingLecturer)
                .Include(m => m.TrainingProgram)
                .ThenInclude(m => m.TrainingPhase)
                .ThenInclude(m => m.Training)

                .Where(m => m.TrainingProgram.TrainingPhase.TrainingId == trainingid);

            //foreach (var test in districtdata)
            //{
            //    var test2 = _context.TrainingLecturerJoinSurveys
            //        .Where(x => x.TrainingSurveyTopicId == test.Id)
            //        .ToList();

            //    result.Add(new
            //    {
            //        Id = test.Id,
            //        Name = test.TrainingLecturer.LecturerName,
            //        Count = test2.Count()
            //    });
            //}


            return Ok(districtdata);
        }

        // GET: api/Training
        [HttpGet("lecturerlist2/{trainingid}")]
        public IEnumerable<object> GetTrainingLecturersList2(long trainingid)
        {
            var result = new List<object>();

            var survey = _context.TrainingProgramLecturers
                .Include(m => m.TrainingLecturer)
                .Include(m => m.TrainingProgram)
                .ThenInclude(m => m.TrainingPhase)
                .ThenInclude(m => m.Training)

                .Where(m => m.TrainingProgram.TrainingPhase.TrainingId == trainingid).ToList();
            foreach (var test in survey)
            {
                var test2 = _context.TrainingLecturerJoinSurveys
                    .Include(m => m.TrainingSurveyTopic)
                    .Where(m => m.LecturerId == test.TrainingLecturerId && m.TrainingId == trainingid).ToList();


                var SurveyTopicName = "";
                long vTrainingLecturerJoinSurveysId = 0;
                foreach (var xxx in test2)
                {
                    vTrainingLecturerJoinSurveysId = xxx.Id;
                    SurveyTopicName = xxx.TrainingSurveyTopic.Name;
                }

                result.Add(new
                {
                    TrainingLecturerJoinSurveysId = test2,
                    trainingLecturerId = test.TrainingLecturerId,
                    trainingLecturerName = test.TrainingLecturer.LecturerName,
                    SurveyName = SurveyTopicName
                });



            }
            return result;
        }

        // POST : api/training/lecturerjoinsurvey/save
        [HttpPost("lecturerjoinsurvey/save")]
        public TrainingLecturerJoinSurvey addTraininglecturerJoinSurvey(long trainingsurveytopicId, long lecturerId, long trainingId)
        {
            var date = DateTime.Now;

            var trainingdata = new TrainingLecturerJoinSurvey
            {
                TrainingSurveyTopicId = trainingsurveytopicId,
                LecturerId = lecturerId,
                TrainingId = trainingId,
                CreatedAt = date

            };

            _context.TrainingLecturerJoinSurveys.Add(trainingdata);
            _context.SaveChanges();

            return trainingdata;
        }

        // PUT : api/training/lecturerjoinsurvey/edit/:id
        [HttpPut("lecturerjoinsurvey/edit/{id}")]
        public void editTraininglecturerJoinSurvey(long trainingsurveytopicId, long id)
        {
            var training = _context.TrainingLecturerJoinSurveys.Find(id);
            training.TrainingSurveyTopicId = trainingsurveytopicId;


            _context.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }


        // POST : api/training/lecturer/save
        //[HttpPost("lecturer/save")]
        //public TrainingLecturer addTraininglecturer(string lecturername, string lecturerphone, string lectureremail, string education, string workhistory, string experience, string detailplus)
        //{
        //    var date = DateTime.Now;

        //    var trainingdata = new TrainingLecturer
        //    {
        //        LecturerName = lecturername
        //        ,
        //        Phone = lecturerphone
        //        ,
        //        Email = lectureremail
        //        ,
        //        Education = education
        //        ,
        //        WorkHistory = workhistory
        //        ,
        //        Experience = experience
        //        ,
        //        DetailPlus = detailplus
        //        ,
        //        CreatedAt = date

        //    };

        //    _context.TrainingLecturers.Add(trainingdata);
        //    _context.SaveChanges();

        //    return trainingdata;
        //}

        // POST : api/training/lecturer/save
        [HttpPost("lecturer/save")]
        public async Task<IActionResult> addTraininglecturer([FromForm] TrainingLecturerViewModel model)
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


            foreach (var formFile in model.ImageProfile.Select((value, index) => new { Value = value, Index = index }))
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
                    var Trainingdata = new TrainingLecturer
                    {
                        LecturerType = model.LecturerType,
                        LecturerName = model.LecturerName,
                        Phone = model.Phone,
                        Email = model.Email,
                        Education = model.Education,
                        WorkHistory = model.WorkHistory,
                        Experience = model.Experience,
                        DetailPlus = model.DetailPlus,
                        CreatedAt = date,
                        ImageProfile = random + filename
                    };
                    System.Console.WriteLine("Start Uplond4.2");
                    _context.TrainingLecturers.Add(Trainingdata);
                    _context.SaveChanges();
                    System.Console.WriteLine("Start Uplond4.2");
                }
            }
            return Ok(new { status = true });

        }


        // PUT : api/training/edit/:id
        //[HttpPut("lecturer/edit/{id}")]
        //public void EditTraininglecturer(long id, string lecturername, string lecturerphone, string lectureremail, string education, string workhistory, string experience)
        //{
        //    var training = _context.TrainingLecturers.Find(id);
        //    training.LecturerName = lecturername;
        //    training.Phone = lecturerphone;
        //    training.Email = lectureremail;
        //    training.Education = education;
        //    training.WorkHistory = workhistory;
        //    training.Experience = experience;
        //    _context.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //    _context.SaveChanges();

        //}


        // PUT : api/training/edit/:id
        [HttpPut("lecturer/edit/{id}")]
        public async Task<IActionResult> EditTraininglecturer([FromForm] TrainingLecturerViewModel model, long id)
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


            foreach (var formFile in model.ImageProfile.Select((value, index) => new { Value = value, Index = index }))
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
                    //var Trainingdata = new TrainingLecturer
                    //{
                    //    LecturerType = model.LecturerType,
                    //    LecturerName = model.LecturerName,
                    //    Phone = model.Phone,
                    //    Email = model.Email,
                    //    Education = model.Education,
                    //    WorkHistory = model.WorkHistory,
                    //    Experience = model.Experience,
                    //    DetailPlus = model.DetailPlus,
                    //    CreatedAt = date,
                    //    ImageProfile = random + filename
                    //};
                    System.Console.WriteLine("Start Uplond4.2");
                    //_context.TrainingLecturers.Add(Trainingdata);
                    var training = _context.TrainingLecturers.Find(id);
                    training.LecturerType = model.LecturerType;
                    training.LecturerName = model.LecturerName;
                    training.Phone = model.Phone;
                    training.Email = model.Email;
                    training.Education = model.Education;
                    training.WorkHistory = model.WorkHistory;
                    training.Experience = model.Experience;
                    training.DetailPlus = model.DetailPlus;
                    training.CreatedAt = date;
                    training.ImageProfile = random + filename;

                    _context.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();
                    System.Console.WriteLine("Start Uplond4.3");
                }
            }
            return Ok(new { status = true });

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
        [HttpGet("phase/detail/{id}")]
        public IActionResult GetTrainingPhaseDetail(long id)
        {
            var districtdata = _context.TrainingPhases
                .Include(m => m.Training)
                .Where(m => m.Id == id);

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
        [HttpPost("phase/add")]
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

            return Ok(trainingphasedata);
        }

        // PUT : api/training/phase/edit/:id
        [HttpPut("phase/edit/{id}")]
        public void EditTrainingPhase(long id, long PhaseNo, DateTime StartDate, DateTime EndDate, string Title, string Detail, string Location, long Group)
        {
            var training = _context.TrainingPhases.Find(id);
            training.PhaseNo = PhaseNo;
            training.StartDate = StartDate;
            training.EndDate = EndDate;
            training.Title = Title;
            training.Detail = Detail;
            training.Location = Location;
            training.Group = Group;

            _context.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

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
                    .Include(x => x.User)
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
                .Include(m => m.TrainingProgram.TrainingProgramFiles)
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
        // PUT : api/training/edit/:id
        [HttpPut("Updateidcode")]
        public void Updateidcode([FromBody] TrainingViewModel model)
        {
            foreach (var code in model.TrainingCode)
            {
                System.Console.WriteLine("ID" + code.id);
                System.Console.WriteLine("Code" + code.code);
                var training = _context.TrainingRegisters.Find(code.id);
                training.IDCode = code.code;

                _context.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }

        }


        // POST api/training/programlogin/add/trainingid
        [HttpPost("programlogin/add/{trainingid}/{programdate}")]
        public TrainingProgramLoginQRCode InsertTrainingProgramLogin(long trainingid, long programlogintype, DateTime programdate)
        {
            var date = DateTime.Now;
            var vmorning = 0;
            var vafternoon = 0;
            if (programlogintype == 1)
            {
                vmorning = 1;
                vafternoon = 0;
            }
            else if (programlogintype == 2)
            {
                vmorning = 0;
                vafternoon = 1;
            }
            else if (programlogintype == 3)
            {
                vmorning = 1;
                vafternoon = 1;
            }

            var trainingdata = new TrainingProgramLoginQRCode
            {
                ProgramDate = programdate,
                TrainingId = trainingid,
                Morning = vmorning,
                Afternoon = vafternoon,

                CreatedAt = date

            };

            _context.TrainingProgramLoginQRCodes.Add(trainingdata);
            _context.SaveChanges();

            return trainingdata;
        }

        // PUT : api/training/programlogin/update/:id
        [HttpPut("programlogin/update/{id}")]
        public void UpdateTrainingProgramLogin(long id, long programlogintype)
        {
            System.Console.WriteLine("programtype: " + programlogintype);
            var date = DateTime.Now;

            var vmorning = 0;
            var vafternoon = 0;
            if (programlogintype == 1)
            {
                vmorning = 1;
                vafternoon = 0;
            }
            else if (programlogintype == 2)
            {
                vmorning = 0;
                vafternoon = 1;
            }
            else if (programlogintype == 3)
            {
                vmorning = 1;
                vafternoon = 1;
            }


            var training = _context.TrainingProgramLoginQRCodes.Find(id);
            training.Morning = vmorning;
            training.Afternoon = vafternoon;
            training.UpdatedAt = date;

            _context.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        [HttpPut("register2/group")]
        public void EditRegisterGroup2(long[] traningregisterid, long approve1, long approve2, long approve3, long approve4, long approve5, long approve6, long approve7, long approve8, long approve9, long approve10)
        {
            foreach (var id in traningregisterid)
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
        //GET api/Training/plan
        [HttpGet("programlogin/get/{id}")]
        public IActionResult GetTrainingProgramLogin(long id)
        {
            var data = _context.TrainingProgramLoginQRCodes
                .Where(m => m.Id == id).FirstOrDefault();

            return Ok(data);

        }

        //GET api/Training/plan
        [HttpGet("programloginCountCourse/get/{id}")]
        public IActionResult GetTrainingProgramLoginCountCourse(long id)
        {
            var data = _context.TrainingProgramLoginQRCodes
                .Where(m => m.Id == id)
                .Sum(p => p.Morning);

            var data2 = _context.TrainingProgramLoginQRCodes
                .Where(m => m.Id == id)
                .Sum(p => p.Afternoon);

            return Ok(data + data2);

        }

        ////GET api/Training/plan
        //[HttpGet("login/get/{trainingid}")]
        //public IActionResult GetTrainingLoginStudy(long trainingid)
        //{
        //    var data = _context.TrainingLogins
        //        .Where(m => m.TrainingPhaseId == 1);
        //        //.Sum(m => m.DateType);

        //    return Ok(data);

        //}

        // GET: api/Training
        [HttpGet("loginrate/get/{trainingid}")]
        public IEnumerable<object> GetTrainingLoginStudy(long trainingid)
        {

            var cntmorning = _context.TrainingProgramLoginQRCodes
                .Where(m => m.TrainingId == trainingid)
                .Sum(p => p.Morning);

            var cntafternoon = _context.TrainingProgramLoginQRCodes
                .Where(m => m.TrainingId == trainingid)
                .Sum(p => p.Afternoon);

            long CountCourse = (cntmorning + cntafternoon);

            var result = new List<object>();

            var data = _context.TrainingRegisters
                 .Where(m => m.TrainingId == trainingid).ToList();

            foreach (var test in data)
            {
                var test2 = _context.TrainingLogins
                    .Where(x => x.Username == test.UserName && x.TrainingId == trainingid)
                    .ToList();

                double aaa = test2.Count();
                double sss = (aaa / CountCourse) * 100;
                result.Add(new
                {
                    Id = test.Id,
                    Registerdata = test,
                    Name = test.Name,
                    Count = test2.Count(),
                    CountCourse = CountCourse,
                    RateCourse = sss.ToString("##.##")
                });

            }
            return result;
        }

        //GET api/Training/plan
        [HttpGet("testtest/get/{id}")]
        public IActionResult testtest()
        {
            var idSet = _context.TrainingRegisters.Select(x => x.UserName).ToHashSet();
            var notFoundItems = _context.TrainingLogins.Where(item => idSet.Contains(item.Username));



            return Ok(notFoundItems);

        }


        //GET api/Training/plan
        [HttpGet("surveylecturer/get/{username}")]
        public IActionResult GetTrainingSurveyLecturerList(string username)
        {

            var result = new List<GetTrainingSurveyLecturerListViewModel>();

            var data = _context.TrainingRegisters
                .Include(m => m.Training)
                .Where(m => m.UserName == username).ToList();


            var result2 = new List<object>();


            foreach (var test in data)
            {
                var test2 = _context.TrainingLecturerJoinSurveys
                    .Include(m => m.TrainingLecturer)
                    .Include(m => m.TrainingSurveyTopic)
                    .Where(x => x.TrainingId == test.TrainingId)
                    .ToList();

                foreach (var xxxx in test2)
                {
                    var ansdata = _context.TrainingSurveyAnswers
                    .Include(m => m.TrainingSurvey)
                    .Where(x => x.Username == username && x.TrainingLecturerJoinSurveyId == xxxx.Id).ToList();

                    result.Add(new GetTrainingSurveyLecturerListViewModel
                    {
                        TrainingId = test.Training.Id,
                        TrainingName = test.Training.Name,
                        TrainingLecturerJoinSurveys = test2,
                        AnsCount = ansdata.Count()
                    });
                }

                //result.Add(new GetTrainingSurveyLecturerListViewModel
                //{
                //    TrainingId = test.Training.Id,
                //    TrainingName = test.Training.Name,
                //    TrainingLecturerJoinSurveys = test2,
                //    //AnsCount = ansdata.Count()
                //});




            }

            return Ok(result.OrderBy(m => m.AnsCount));
            //return Ok(result);

        }


        //ประวัติการฝึกอบรมของบุคลากร
        //GET api/Training/historyreport/get/{username}
        [HttpGet("historyreport/get/{username}")]
        public IActionResult GetTrainingHistoryReport(string username)
        {
            var result = new List<object>();

            var data = _context.TrainingRegisters
                .Include(m => m.Training)
                .Where(m => m.UserName == username).ToList();

            return Ok(data);
        }

        //ส่วนประกอบข้อมูลประเมิน(แบบความพอใจ)
        //GET api/Training/historyreport/get/{username}
        [HttpGet("answerlike/get/{trainingLecturerJoinSurveysId}/")]
        public IActionResult GetTrainingAnswerLikeReport(long trainingLecturerJoinSurveysId)
        {
            var result = new List<object>();

            long dataTrainingSurveyTopicId = _context.TrainingLecturerJoinSurveys
                .Where(m => m.Id == trainingLecturerJoinSurveysId )
                .Select(m => m.TrainingSurveyTopicId)
              .FirstOrDefault();

            var data = _context.TrainingSurveys
                .Where(m => m.TrainingSurveyTopicId == dataTrainingSurveyTopicId && m.SurveyType == 1)
              .ToList();

            foreach (var item in data)
            {
                var dataScore = _context.TrainingSurveyAnswers
                .Include(m => m.TrainingSurvey)
                .Where(m => m.TrainingSurveyId == item.Id && m.SurveyType == 1)
                .Select(m => m.Score)
                .ToList();

                var dataScoreverygood = _context.TrainingSurveyAnswers
                .Include(m => m.TrainingSurvey)
                .Where(m => m.TrainingSurveyId == item.Id && m.SurveyType == 1 && m.Score == 4)
                .Select(m => m.Score)
                .ToList();

                var dataScoregood = _context.TrainingSurveyAnswers
                .Include(m => m.TrainingSurvey)
                .Where(m => m.TrainingSurveyId == item.Id && m.SurveyType == 1 && m.Score == 3)
                .Select(m => m.Score)
                .ToList();

                var dataScorefair = _context.TrainingSurveyAnswers
                .Include(m => m.TrainingSurvey)
                .Where(m => m.TrainingSurveyId == item.Id && m.SurveyType == 1 && m.Score == 2)
                .Select(m => m.Score)
                .ToList();

                var dataScoredown = _context.TrainingSurveyAnswers
                .Include(m => m.TrainingSurvey)
                .Where(m => m.TrainingSurveyId == item.Id && m.SurveyType == 1 && m.Score == 1)
                .Select(m => m.Score)
                .ToList();


                result.Add(new
                {
                    SurveyId = item.Id,
                    SurveyName = item.Name,
                    ScoreSum = dataScore.Sum(),
                    ScoreVerygood = dataScoreverygood.Count(),
                    ScoreGood = dataScoregood.Count(),
                    ScoreFair = dataScorefair.Count(),
                    ScoreDown = dataScoredown.Count(),
                });
            }

            return Ok(result);
        }

        //ส่วนประกอบข้อมูลประเมิน(แบบปลายเปิด)
        //GET api/Training/historyreport/get/{username}
        [HttpGet("answeropen/get/{trainingLecturerJoinSurveysId}/")]
        public IActionResult GetTrainingAnswerOpenReport(long trainingLecturerJoinSurveysId)
        {
            var result = new List<object>();

            

            var data = _context.TrainingSurveyAnswers
                .Include(m => m.TrainingSurvey)
                .Where(m => m.TrainingLecturerJoinSurveyId == trainingLecturerJoinSurveysId && m.SurveyType == 2).ToList();

            return Ok(data);
        }


        //ส่วนประกอบข้อมูลประเมิน(แบบใช่หรือไม่)
        //GET api/Training/historyreport/get/{username}
        [HttpGet("answeryesno/get/{vTrainingSurveyTopicId}/")]
        public IActionResult GetTrainingAnswerYes(long vTrainingSurveyTopicId)
        {
            var result = new List<object>();

            long dataTrainingSurveyTopicId = _context.TrainingLecturerJoinSurveys
                .Where(m => m.Id == vTrainingSurveyTopicId)
                .Select(m => m.TrainingSurveyTopicId)
              .FirstOrDefault();

            var data = _context.TrainingSurveys
                .Where(m => m.TrainingSurveyTopicId == dataTrainingSurveyTopicId && m.SurveyType == 3)
              .ToList();

            foreach (var item in data)
            {
                var dataYessum = _context.TrainingSurveyAnswers
                .Include(m => m.TrainingSurvey)
                .Where(m => m.TrainingSurveyId == item.Id && m.SurveyType == 3 && m.AnswerYorN == 1)
                .Select(m => m.AnswerYorN)
                .ToList();

                var dataNosum = _context.TrainingSurveyAnswers
                .Include(m => m.TrainingSurvey)
                .Where(m => m.TrainingSurveyId == item.Id && m.SurveyType == 3 && m.AnswerYorN == 0)
                .Select(m => m.AnswerYorN)
                .ToList();

                result.Add(new
                {
                    SurveyId = item.Id,
                    SurveyName = item.Name,
                    AnsYesSum = dataYessum.Count(),
                    AnsNoSum = dataNosum.Count()
                });
            }

            return Ok(result);
        }


        //------zone training register-------
        // PUT api/values/5
        [HttpGet("testemail")]
        public IActionResult Gettestemail()
        {
            //<!-- ห้ามลบ -->
            //var message = new Message(new string[] { "fantasy_tey@hotmail.com" }, "Test email", "This is the content from our email.");
            //_emailSender.SendEmail(message);
            //<!-- END ห้ามลบ -->

            //var provincedata = _context.Provinces
            //                 .Include(p => p.Sectors)
            //                 .Include(p => p.ProvincesGroups);
            return Ok("");


        }
        [HttpGet("printNamePlatebyPalm/{id}")]
        public IActionResult printNamePlatebyPalm(long id)
        {
            var registerData = _context.TrainingRegisters
                    .Include(p => p.Training)
                    .Include(x => x.User)
                    .Where(x => x.Id == id)
                    .FirstOrDefault();
            return Ok(registerData);
        }

        //[HttpPost("send")]
        //public async Task<IActionResult> SendMail()
        //{
        //    try
        //    {
        //       var email =   _emailSender.SendEmailAsync("k12clubpalm@gmail.com", "Confirm your email", "sssssssss");
        //        return Ok(email);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //}



        //ประเภทกิจกรรม
        //GET api/Training/programtype/get
        [HttpGet("programtype/get")]
        public IActionResult GetTrainingProgramType()
        {
            var result = new List<object>();

            var data = _context.TrainingProgramTypes.ToList();

            return Ok(data);
        }

        // POST api/training/programtype/add
        [HttpPost("programtype/add")]
        public TrainingProgramType TrainingPrgramType_Add(string name)
        {
            var date = DateTime.Now;

            var trainingdata = new TrainingProgramType
            {
                Name = name,
                CreatedAt = date

            };

            _context.TrainingProgramTypes.Add(trainingdata);
            _context.SaveChanges();

            return trainingdata;
        }

        // PUT : api/training/edit/:id
        [HttpPut("programtype/edit/{id}")]
        public void EditTrainingProgramType(long id, string name)
        {
            var training = _context.TrainingProgramTypes.Find(id);
            training.Name = name;
            _context.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/Training//values/5
        [HttpDelete("programtype/delete/{id}")]
        public void DeleteTrainingProgramType(long id)
        {
            var trainingdata = _context.TrainingProgramTypes.Find(id);

            _context.TrainingProgramTypes.Remove(trainingdata);
            _context.SaveChanges();
        }


        //เช็คตอบแบบสอบถามทำไปแล้วยัง
        //GET api/Training/programtype/get
        [HttpGet("checkalreadysurvey/get/{username}")]
        public IActionResult GetCheckAlreadysurvey(string username)
        {
            var result = new List<object>();

            var data = _context.TrainingSurveyAnswers
                .Where(x => x.Username == username)
                .ToList();

            return Ok(data);
        }




        //ประเภทวิทยากร
        //GET api/Training/programtype/get
        [HttpGet("lecturertype/get")]
        public IActionResult GetTrainingLecturerType()
        {
            var result = new List<object>();

            var data = _context.TrainingLecturerTypes.ToList();

            return Ok(data);
        }

        // POST api/training/programtype/add
        [HttpPost("lecturertype/add")]
        public TrainingLecturerType TrainingLecturerType_Add(string name)
        {
            var date = DateTime.Now;

            var trainingdata = new TrainingLecturerType
            {
                Name = name,
                CreatedAt = date

            };

            _context.TrainingLecturerTypes.Add(trainingdata);
            _context.SaveChanges();

            return trainingdata;
        }

        // PUT : api/training/edit/:id
        [HttpPut("lecturertype/edit/{id}")]
        public TrainingLecturerType EditTrainingLecturerType(long id, string name)
        {
            var training = _context.TrainingLecturerTypes.Find(id);
            training.Name = name;
            _context.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return training;
        }

        //รายงานข้อมูลบุคคลของวิทยากร
        // PUT : api/training/edit/:id
        [HttpPost("reportlecturer")]
        public IActionResult CreateReport(long trainingLecturerid, string trainingname, int year)
        {
            var lecturerdata = _context.TrainingLecturers
                .Where(m => m.Id == trainingLecturerid)
                .FirstOrDefault();
            if (!Directory.Exists(_environment.WebRootPath + "//Uploads//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//Uploads//"); //สร้าง Folder Upload ใน wwwroot
            }
            var filePath = _environment.WebRootPath + "//Uploads//";
            var filePath2 = _environment.WebRootPath + "//img//";
            var filename = "รายงานวิทยากร" + ".docx";
            var createfile = filePath + filename;
            var myImageFullPath = filePath + lecturerdata.ImageProfile;
            var myImageFullPath2 = filePath2 + "user.png";

            System.Console.WriteLine("1");
            System.Console.WriteLine("in create");
            //if (model.reporttype == "รายหน่วยงาน")
            //{
            using (DocX document = DocX.Create(createfile))
            {
                System.Console.WriteLine("2");
                document.SetDefaultFont(new Xceed.Document.NET.Font("ThSarabunNew"));
                document.AddHeaders();
                document.AddFooters();

                // Force the first page to have a different Header and Footer.
                document.DifferentFirstPage = true;
                // Force odd & even pages to have different Headers and Footers.
                document.DifferentOddAndEvenPages = true;

                // Insert a Paragraph into the first Header.
                document.Footers.First.InsertParagraph("วันที่ออกรายงาน: ").Append(DateTime.Now.ToString("dd MMMM yyyy HH:mm", new CultureInfo("th-TH"))).Append(" น.").Alignment = Alignment.right;
                // Insert a Paragraph into the even Header.
                document.Footers.Even.InsertParagraph("วันที่ออกรายงาน: ").Append(DateTime.Now.ToString("dd MMMM yyyy HH:mm", new CultureInfo("th-TH"))).Append(" น.").Alignment = Alignment.right;
                // Insert a Paragraph into the odd Header.
                document.Footers.Odd.InsertParagraph("วันที่ออกรายงาน: ").Append(DateTime.Now.ToString("dd MMMM yyyy HH:mm", new CultureInfo("th-TH"))).Append(" น.").Alignment = Alignment.right;

                // Add the page number in the first Footer.
                document.Headers.First.InsertParagraph("").AppendPageNumber(PageNumberFormat.normal).Alignment = Alignment.center;
                // Add the page number in the even Footers.
                document.Headers.Even.InsertParagraph("").AppendPageNumber(PageNumberFormat.normal).Alignment = Alignment.center;
                // Add the page number in the odd Footers.
                document.Headers.Odd.InsertParagraph("").AppendPageNumber(PageNumberFormat.normal).Alignment = Alignment.center;


                //Image image = document.AddImage(myImageFullPath);
                //Picture picture = image.CreatePicture(85, 85);
                //var logo = document.InsertParagraph();
                //logo.AppendPicture(picture).Alignment = Alignment.center;


                // Add a title
                var title1 = document.InsertParagraph("ข้อมูลบุคคลกรของวิทยากร");
                title1.FontSize(18d);
                title1.SpacingBefore(15d);
                title1.SpacingAfter(15d);
                title1.Bold();
                title1.Alignment = Alignment.center;

                var title2 = document.InsertParagraph(trainingname + "/" + year);
                title2.FontSize(16d);
                title2.SpacingBefore(15d);
                title2.SpacingAfter(15d);
                title2.Bold();
                title2.Alignment = Alignment.center;

                //var title3 = document.InsertParagraph("วิชา");
                //title3.FontSize(16d);
                //title3.SpacingBefore(15d);
                //title3.SpacingAfter(15d);
                //title3.Bold();
                //title3.Alignment = Alignment.center;

                System.Console.WriteLine("3");

                if (lecturerdata.ImageProfile == null)
                {
                    System.Console.WriteLine("3.1");
                    Image image = document.AddImage(myImageFullPath2);
                    Picture picture = image.CreatePicture(85, 85);
                    var logo = document.InsertParagraph();
                    logo.AppendPicture(picture).Alignment = Alignment.left;
                    logo.SpacingAfter(10d);

                }
                else if (!System.IO.File.Exists(myImageFullPath))
                {
                    System.Console.WriteLine("3.2");
                    Image image = document.AddImage(myImageFullPath2);
                    Picture picture = image.CreatePicture(85, 85);
                    var logo = document.InsertParagraph();
                    logo.AppendPicture(picture).Alignment = Alignment.left;
                    logo.SpacingAfter(10d);
                }
                else
                {
                    System.Console.WriteLine("3.3");
                    Image image = document.AddImage(myImageFullPath);
                    Picture picture = image.CreatePicture(85, 85);
                    var logo = document.InsertParagraph();
                    logo.AppendPicture(picture).Alignment = Alignment.left;
                    logo.SpacingAfter(10d);
                }



                var name = document.InsertParagraph("ชื่อ-นามสกุล : " + lecturerdata.LecturerName);
                //region2.Alignment = Alignment.center;
                name.SpacingAfter(10d);
                name.FontSize(16d);

                var phone = document.InsertParagraph("หมายเลขโทรศัพท์ : " + lecturerdata.Phone);
                //region2.Alignment = Alignment.center;
                phone.SpacingAfter(10d);
                phone.FontSize(16d);

                var email = document.InsertParagraph("อีเมล : " + lecturerdata.Email);
                //region2.Alignment = Alignment.center;
                email.SpacingAfter(10d);
                email.FontSize(16d);

                var education = document.InsertParagraph("ประวัติการศึกษา : " + lecturerdata.Education);
                //region2.Alignment = Alignment.center;
                education.SpacingAfter(10d);
                education.FontSize(16d);

                var workhistory = document.InsertParagraph("ประวัติการทำงาน : " + lecturerdata.WorkHistory);
                //region2.Alignment = Alignment.center;
                workhistory.SpacingAfter(10d);
                workhistory.FontSize(16d);

                var experience = document.InsertParagraph("ประสบการณ์บรรยาย : " + lecturerdata.Experience);
                //region2.Alignment = Alignment.center;
                experience.SpacingAfter(10d);
                experience.FontSize(16d);

                System.Console.WriteLine("11");
                document.Save();
                Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");
            }
            //}
            return Ok(new { data = filename });
        }

          // PUT : api/trainingsetting/edit/:id
        [HttpPut("trainingsetting/edit/{id}")]
        public Training EditTrainingSetting(long id, int status)
        {
            var training = _context.Trainings.Find(id);
            training.Status = status;
            _context.Entry(training).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return training;
        }
        
        //GET api/training/check_TrainingProgramLoginQRDate/get
        [HttpGet("check_TrainingProgramLoginQRDate/get/{trainingid}")]
        public IActionResult GetCheckTrainingProgramLoginQRDate(long trainingid)
        {

            var data = _context.TrainingProgramLoginQRCodes
                .Where(m => m.TrainingId == trainingid)
                .ToList();


            return Ok(data);

        }


        // POST api/values
        [HttpPost("summaryreport/group/add/{phaseid}/{group}")]
        public async Task<IActionResult> InsertTrainingSummaryReportGroup([FromForm] TrainingSummaryReportGroupViewModel model, long phaseid, long group)
        {
            var date = DateTime.Now;

            System.Console.WriteLine("Start Uplond" + phaseid);
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
                    var Trainingdata = new TrainingSummaryReportPhase
                    {
                        TrainingPhaseId = phaseid,
                        File = random + filename,
                        Detail = model.Detail,
                        Group = group,

                        CreatedAt = date
                    };
                    System.Console.WriteLine("Start Uplond4.2");
                    _context.TrainingSummaryReportPhases.Add(Trainingdata);
                    _context.SaveChanges();
                    System.Console.WriteLine("Start Uplond4.2");
                }
            }
            return Ok(new { status = true });
        }
        //----------------------------------


        //GET api/training/check_TrainingProgramLoginQRDate/get
        [HttpGet("summaryreport/group/get/{phaseid}/{group}")]
        public IActionResult GetTrainingSummaryReporGroup(long phaseid, long group)
        {
            var data = _context.TrainingSummaryReportPhases
                .Where(m => m.TrainingPhaseId == phaseid && m.Group == group)
                .ToList();
            return Ok(data);
        }


        // DELETE api/Training/trainingsurvey/values/5
        [HttpDelete("summaryreport/group/delete/{id}")]
        public void DeleteTrainingSummaryReporGroup(long id)
        {
            var trainingdata = _context.TrainingSummaryReportPhases.Find(id);
            _context.TrainingSummaryReportPhases.Remove(trainingdata);
            _context.SaveChanges();
        }




        // POST api/values
        [HttpPost("summaryreport/project/add/{trainingid}")]
        public async Task<IActionResult> InsertTrainingSummaryReportProject([FromForm] TrainingSummaryReportProjectViewModel model, long trainingid)
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
                    var Trainingdata = new TrainingSummaryReport
                    {
                        TrainingId = trainingid,
                        File = random + filename,
                        Detail = model.Detail,

                        CreatedAt = date
                    };
                    System.Console.WriteLine("Start Uplond4.2");
                    _context.TrainingSummaryReports.Add(Trainingdata);
                    _context.SaveChanges();
                    System.Console.WriteLine("Start Uplond4.2");
                }
            }
            return Ok(new { status = true });
        }
        //----------------------------------


        //GET api/training/check_TrainingProgramLoginQRDate/get
        [HttpGet("summaryreport/project/get/{trainingid}")]
        public IActionResult GetTrainingSummaryReporProject(long trainingid)
        {
            var data = _context.TrainingSummaryReports
                .Where(m => m.TrainingId == trainingid)
                .ToList();
            return Ok(data);
        }


        // DELETE api/Training/trainingsurvey/values/5
        [HttpDelete("summaryreport/project/delete/{id}")]
        public void DeleteTrainingSummaryReporProject(long id)
        {
            var trainingdata = _context.TrainingSummaryReports.Find(id);
            _context.TrainingSummaryReports.Remove(trainingdata);
            _context.SaveChanges();
        }



    }



}
