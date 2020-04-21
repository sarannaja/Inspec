using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectronicBookController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        // GET: api/ElectronicBook
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ElectronicBook/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ElectronicBook
        [HttpPost]
        public void Post([FromBody] ElectronicBookViewModel model)
        {
            var ElectronicBookdata = new ElectronicBook
            {
                Detail = model.Detail,
            };

            _context.ElectronicBooks.Add(ElectronicBookdata);
            _context.SaveChanges();

            var CentralPolicyId = model.Inputelectronicbook[0].CentralPolicyId;
            var ProvinceId = model.Inputelectronicbook[0].ProvinceId;

            var centralpolicyprovinceid = _context.CentralPolicyProvinces
                .Where(m => m.CentralPolicyId == CentralPolicyId)
                .Where(m => m.ProvinceId == ProvinceId)
                .Select(m => m.Id).First();

            var ElectronicBookgroupdata = new ElectronicBookGroup
            {
                ElectronicBookId = ElectronicBookdata.Id,
                CentralPolicyProvinceId = centralpolicyprovinceid
            };

            foreach (var itemUserPeopleId in model.UserPeopleId)
            {
                var CentralPolicyGroupdata = new CentralPolicyGroup
                {
                };
                _context.CentralPolicyGroups.Add(CentralPolicyGroupdata);
                _context.SaveChanges();

                    var CentralPolicyUserdata = new CentralPolicyUser
                    {
                        CentralPolicyId = CentralPolicyId,
                        ProvinceId = ProvinceId,
                        ElectronicBookId = ElectronicBookdata.Id,
                        CentralPolicyGroupId = CentralPolicyGroupdata.Id,
                        UserId = itemUserPeopleId.Id,

                    };
                    _context.CentralPolicyUsers.Add(CentralPolicyUserdata);
                    _context.SaveChanges();
            }

            foreach (var itemUserMinistryId in model.UserMinistryId)
            {
                var CentralPolicyGroupdata = new CentralPolicyGroup
                {
                };
                _context.CentralPolicyGroups.Add(CentralPolicyGroupdata);
                _context.SaveChanges();

                var CentralPolicyUserdata = new CentralPolicyUser
                {
                    CentralPolicyId = CentralPolicyId,
                    ProvinceId = ProvinceId,
                    ElectronicBookId = ElectronicBookdata.Id,
                    CentralPolicyGroupId = CentralPolicyGroupdata.Id,
                    UserId = itemUserMinistryId.Id,
                };
                _context.CentralPolicyUsers.Add(CentralPolicyUserdata);
                _context.SaveChanges();
            }
        }

        // PUT: api/ElectronicBook/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
