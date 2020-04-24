using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectronicBookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ElectronicBookController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ElectronicBook
        [HttpGet("{userId}")]
        public IActionResult Get(string userId)
        {
            
            System.Console.WriteLine("UserIdNaja: " + userId);

            //var user = _context.Users
            //    .Where(x => x.Id == userId)
            //    .Select(x => x.Name)
            //    .First();
            //System.Console.WriteLine("Name: " + user);

            var ebook = _context.ElectronicBookGroups
                .Include(x => x.CentralPolicyProvince)
                .ThenInclude(x => x.CentralPolicy)
                .ThenInclude(x => x.CentralPolicyUser)
                .Include(x => x.ElectronicBook)
                .Where(x => x.ElectronicBook.CreatedBy == userId);
            return Ok(ebook);
        }

        [HttpGet("getElectronicBookById/{centralPolicyUserId}")]
        public IActionResult GetById(long centralPolicyUserId)
        {

            var accept = _context.CentralPolicyUsers.Where(m => m.Id == centralPolicyUserId).FirstOrDefault();

            var centralpolicydata = _context.CentralPolicies
                .Include(m => m.CentralPolicyUser)
                .ThenInclude(m => m.ElectronicBook)
                .Include(m => m.CentralPolicyUser)
                .ThenInclude(m => m.User)
                .Include(m => m.CentralPolicyUser)
                .ThenInclude(m => m.CentralPolicyGroup)
                .ThenInclude(m => m.CentralPolicyUserFiles)
                .Include(m => m.CentralPolicyDates)
                .Include(m => m.CentralPolicyFiles)
                //.Include(m => m.Subjects)
                //.ThenInclude(m => m.Subquestions)
                .Include(m => m.CentralPolicyProvinces)
                .ThenInclude(m => m.Province)
                .Where(m => m.Id == accept.CentralPolicyId).First();

            return Ok(centralpolicydata);
        }

        [HttpPut("editElectronicBookDetail/{id}")]
        public void Put([FromBody] ElectronicBookViewModel model, long id)
        {
            var test = model.Detail;
            System.Console.WriteLine("detail ja: " + test);
            var electronicBookDetail = _context.ElectronicBooks.Find(id);
            {
                electronicBookDetail.Detail = model.Detail;
            }
            _context.Entry(electronicBookDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        // GET api/values/5
        [HttpGet("getcentralpolicyfromprovince/{id}")]
        public IActionResult getcentralpolicyfromprovince(long id)
        {
            var centralpolicyprovincedatas = _context.CentralPolicyProvinces
                .Include(m => m.CentralPolicy)
                .Where(m => m.ProvinceId == id)
                .ToList(); //All

            var electronicbookgroups = _context.ElectronicBookGroups
              .ToList(); //Chose

            List<object> termsList = new List<object>();

            foreach (var centralpolicyprovincedata in centralpolicyprovincedatas)
            {
                for (int i = 0; i < electronicbookgroups.Count(); i++)
                {
                    if (centralpolicyprovincedata.Id == electronicbookgroups[i].CentralPolicyProvinceId)
                        termsList.Add(centralpolicyprovincedata);
                }
            }

            return Ok(termsList);
        }

        // POST: api/ElectronicBook
        [HttpPost]
        public void Post([FromBody] ElectronicBookViewModel model)
        {
            var test1 = model.Detail;
            //var test2 = model.UserId;

            System.Console.WriteLine("Detail: " + test1);
            //System.Console.WriteLine("UserId: " + test2);

            var ElectronicBookdata = new ElectronicBook
            {
                Detail = model.Detail,
                CreatedBy = model.id
            };
            System.Console.WriteLine("1");

            _context.ElectronicBooks.Add(ElectronicBookdata);
            _context.SaveChanges();

            System.Console.WriteLine("2");

            var CentralPolicyId = model.Inputelectronicbook[0].CentralPolicyId;
            var ProvinceId = model.Inputelectronicbook[0].ProvinceId;

            System.Console.WriteLine("3");

            var centralpolicyprovinceid = _context.CentralPolicyProvinces
                .Where(m => m.CentralPolicyId == CentralPolicyId)
                .Where(m => m.ProvinceId == ProvinceId)
                .Select(m => m.Id).First();

            System.Console.WriteLine("3.5" + centralpolicyprovinceid);

            var ElectronicBookgroupdata = new ElectronicBookGroup
            {
                ElectronicBookId = ElectronicBookdata.Id,
                CentralPolicyProvinceId = centralpolicyprovinceid
            };
            _context.ElectronicBookGroups.Add(ElectronicBookgroupdata);
            _context.SaveChanges();

            System.Console.WriteLine("3.8");

            foreach (var itemUserPeopleId in model.UserPeopleId)
            {
                var CentralPolicyGroupdata = new CentralPolicyGroup
                {
                };
                _context.CentralPolicyGroups.Add(CentralPolicyGroupdata);
                _context.SaveChanges();

                System.Console.WriteLine("3.9");

                var CentralPolicyUserdata = new CentralPolicyUser
                    {
                        CentralPolicyId = CentralPolicyId,
                        ProvinceId = ProvinceId,
                        ElectronicBookId = ElectronicBookdata.Id,
                        CentralPolicyGroupId = CentralPolicyGroupdata.Id,
                        UserId = itemUserPeopleId.Id,
                        Status = "รอการตอบรับ"

                    };
                    _context.CentralPolicyUsers.Add(CentralPolicyUserdata);
                    _context.SaveChanges();
            }
            System.Console.WriteLine("4");

            foreach (var itemUserMinistryId in model.UserMinistryId)
            {
                var CentralPolicyGroupdata2 = new CentralPolicyGroup
                {
                };
                _context.CentralPolicyGroups.Add(CentralPolicyGroupdata2);
                _context.SaveChanges();
                System.Console.WriteLine("5");
                var CentralPolicyUserdata2 = new CentralPolicyUser
                {
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    ElectronicBookId = ElectronicBookdata.Id,
                    CentralPolicyGroupId = CentralPolicyGroupdata2.Id,
                    UserId = itemUserMinistryId.Id,
                    Status = "รอการตอบรับ"
                };
                _context.CentralPolicyUsers.Add(CentralPolicyUserdata2);
                _context.SaveChanges();
                System.Console.WriteLine("6");
            }
        }

        // PUT: api/ElectronicBook/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            System.Console.WriteLine("ID: " + id);
            var electronicBookData = _context.ElectronicBooks.Find(id);

            _context.ElectronicBooks.Remove(electronicBookData);
            _context.SaveChanges();
        }
    }
}
