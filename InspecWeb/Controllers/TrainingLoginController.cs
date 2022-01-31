using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class TrainingLoginController : Controller
    {
        public static IWebHostEnvironment _environment;

        private readonly ApplicationDbContext _context;

        public TrainingLoginController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpGet("getTrainingByPhaseId/{trainingPhaseId}")]
        public IActionResult GetTrainingData(long trainingPhaseId)
        {
            var trainingId = _context.TrainingPhases
                 .Where(x => x.Id == trainingPhaseId)
                 .Select(x => x.TrainingId)
                 .FirstOrDefault();

            var trainingData = _context.Trainings
                .Where(x => x.Id == trainingId)
                .FirstOrDefault();

            return Ok(trainingData);
        }

        //GET api/training/TrainingProgramLogin/get/:??
        [HttpGet("TrainingProgramLogin/get/{trainingprogramloginid}")]
        public IActionResult GetTrainingProgramLogin(long trainingprogramloginid)
        {
            var result = new List<object>();

            var data = _context.TrainingProgramLoginQRCodes
                //.Include(m => m.TrainingPhase)
                //.ThenInclude(m => m.Training)
                //.Include(m => m.TrainingProgramLoginQRCodes)
                .Where(m => m.Id == trainingprogramloginid)
                .FirstOrDefault();

            return Ok(data);

        }


        [HttpPost("trainingSignin")]
        public IActionResult TrainingSignin(TrainingLoginViewModel model)
        {
            System.Console.WriteLine("Username: " + model.username);
            System.Console.WriteLine("TrainingId: " + model.trainingPhaseId);
            System.Console.WriteLine("trainingProgramLoginId: " + model.trainingProgramLoginId);
            System.Console.WriteLine("dateType: " + model.dateType);

            var trainingId = _context.TrainingPhases
                .Where(x => x.Id == model.trainingPhaseId)
                .Select(x => x.TrainingId)
                .FirstOrDefault();

            var trainingRegisterData = _context.TrainingRegisters
                .Where(x => x.UserName == model.username && x.TrainingId == trainingId && x.Status == 1)
                .FirstOrDefault();

            System.Console.WriteLine("Data: " + trainingRegisterData);

            var trainingLoginData = _context.TrainingLogins
                //.Where(x => x.Username == model.username && x.TrainingId == model.trainingPhaseId && x.TrainingProgramLoginId == model.trainingProgramLoginId && x.DateType == model.dateType)
                .Where(x => x.Username == model.username && x.TrainingId == trainingId && x.TrainingProgramLoginId == model.trainingProgramLoginId && x.DateType == model.dateType)
                .FirstOrDefault();
            //.FirstOrDefault();

            if (trainingRegisterData == null)
            {
                return Ok(new { status = 100 });
            }
            else if (trainingRegisterData != null && trainingLoginData != null)
            {
                return Ok(new { status = 200 });
            }
            else if (trainingRegisterData != null && trainingLoginData == null)
            {
                var TrainingData = new TrainingLogin
                {
                    Username = model.username,
                    //TrainingId = model.trainingPhaseId,
                    TrainingId = trainingId,
                    RegisterDate = DateTime.Now,
                    TrainingProgramLoginId = model.trainingProgramLoginId,
                    DateType = model.dateType,
                };

                _context.TrainingLogins.Add(TrainingData);
                _context.SaveChanges();

                return Ok(new { status = 300 });
            }
            return Ok(true);
        }

        [HttpGet("getUserLogIn/{programid}/{programType}/{trainingId}")]
        public IActionResult GetUserLogIn(long programid, long programType, long trainingId)
        {

            var result = new List<object>();

            var trainingregisters = _context.TrainingRegisters
                .Where(m => m.TrainingId == trainingId && m.Status == 1)
                .ToList();

            foreach (var user in trainingregisters)
            {

                var traininglogins = _context.TrainingLogins
                    .Where(x => x.Username == user.UserName && x.DateType == programType && x.TrainingProgramLoginId == programid)
                    .FirstOrDefault();

                //result.Add(name);

                result.Add(new
                {
                    TrainingRegister = trainingregisters,
                    TrainingLogin = traininglogins

                });
            }

            return Ok(result);





            //List<object> termsList = new List<object>();
            //var userLogIn = _context.TrainingLogins
            //    .Where(x => x.TrainingProgramLoginId == id && x.DateType == programType)
            //    .ToList();

            //foreach (var user in userLogIn)
            //{

            //    var name = _context.TrainingRegisters
            //        .Where(x => x.Email == user.Username)
            //        .FirstOrDefault();
            //    termsList.Add(name);
            //}

            //return Ok(termsList);
        }

        //GET api/training/program
        [HttpGet("TrainingProgramDate/get/{trainingid}")]
        public IActionResult GetTrainingProgramDate(long trainingid)
        {
            var result = new List<object>();

            var districtdata = _context.TrainingPrograms
                .Include(m => m.TrainingPhase)
                .ThenInclude(m => m.Training)
                //.Include(m => m.TrainingProgramLoginQRCodes)
                .Where(m => m.TrainingPhase.TrainingId == trainingid)
                .ToList();

            return Ok(districtdata);

        }

        //GET api/training/program
        [HttpGet("TrainingProgramDate2/get/{trainingid}")]
        public IActionResult GetTrainingProgramDate2(long trainingid)
        {
            var result = new List<object>();

            var districtdata = _context.TrainingProgramLoginQRCodes
                //.Include(m => m.TrainingPhase)
                //.ThenInclude(m => m.Training)
                //.Include(m => m.TrainingProgramLoginQRCodes)
                .Where(m => m.TrainingId == trainingid)
                .OrderBy(m => m.ProgramDate)

                .ToList();

            return Ok(districtdata);

        }

        

        [HttpPost("trainingSignin2")]
        public IActionResult TrainingSignin2(TrainingLoginViewModel model)
        {
            System.Console.WriteLine("Username: " + model.username);
            System.Console.WriteLine("PhaseId: " + model.trainingPhaseId);
            System.Console.WriteLine("trainingProgramLoginId: " + model.trainingProgramLoginId);
            System.Console.WriteLine("dateType: " + model.dateType);

            
                var TrainingData = new TrainingLogin
                {
                    Username = model.username,
                    //TrainingId = model.trainingPhaseId,
                    TrainingId = model.trainingid,
                    RegisterDate = DateTime.Now,
                    TrainingProgramLoginId = model.trainingProgramLoginId,
                    DateType = model.dateType,
                };

                _context.TrainingLogins.Add(TrainingData);
                _context.SaveChanges();

            return Ok(true);
        }

    }
}
