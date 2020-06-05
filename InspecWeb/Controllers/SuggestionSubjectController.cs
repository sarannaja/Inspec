using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class SuggestionSubjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuggestionSubjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<SuggestionSubject> Get()
        {

            var suggestiondata = from P in _context.SuggestionSubjects
                               select P;
            return suggestiondata;
           
        }

        [HttpPost]
        public SuggestionSubject Post(long SubjectCentralPolicyProvinceId, string UserId,string Suggestion)
        {
            System.Console.WriteLine("subjectId" + SubjectCentralPolicyProvinceId);
            var date = DateTime.Now;
            var suggestiondata = new SuggestionSubject
            {
                SubjectCentralPolicyProvinceId = SubjectCentralPolicyProvinceId,
                UserId = UserId,
                Suggestion = Suggestion,
                CreatedAt = date

            };

            _context.SuggestionSubjects.Add(suggestiondata);
            _context.SaveChanges();

            return suggestiondata;
        }
    }
}
