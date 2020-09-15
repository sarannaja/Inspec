using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("trainingSignin")]
        public IActionResult TrainingSignin(TrainingLoginViewModel model)
        {
            System.Console.WriteLine("Username: " + model.username);
            System.Console.WriteLine("PhaseId: " + model.trainingPhaseId);
            var trainingRegisterData = _context.TrainingRegisters
                .Where(x => x.UserId == model.username)
                .FirstOrDefault();

            System.Console.WriteLine("Data: " + trainingRegisterData);

            if (trainingRegisterData == null)
            {
                return Ok(false);
            }
            else
            {
                var TrainingData = new TrainingLogin
                {
                    Username = model.username,
                    TrainingPhaseId = model.trainingPhaseId,
                    RegisterDate = DateTime.Now,
                };

                _context.TrainingLogins.Add(TrainingData);
                _context.SaveChanges();

                return Ok(true);
            }
        }

    }
}
