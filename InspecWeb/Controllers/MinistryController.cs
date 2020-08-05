using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel; //excel
using System.IO; //excel
using Microsoft.EntityFrameworkCore;

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class MinistryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MinistryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Ministry> Get()
        {
            var Ministrydata = from P in _context.Ministries
                               select P;
            return Ministrydata;
        }

        //<!-- Get กระทรวง 20200720 -->
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var ministrydata = _context.Ministries
               .Where(x => x.Id == id).FirstOrDefault();

            return Ok(ministrydata);
        }
        //<!-- END Get กระทรวง 20200720-->

        //<!-- Get  กระทรวง สำหรับหน่วยงานภูมิภาคไปใช้ 20200720 -->
        [HttpGet("ministryfirst2/{id}")]
        public IActionResult ministryfirst2(long id)
        {
            var departmentdata = _context.Departments
               .Where(x => x.Id == id).FirstOrDefault();

            var ministrydata = _context.Ministries
              .Where(x => x.Id == departmentdata.MinistryId).FirstOrDefault();

            return Ok(ministrydata);

           
        }
        //<!-- END Get กระทรวง สำหรับหน่วยงานภูมิภาคไปใช้ 20200720 -->


        // POST api/values
        [HttpPost]
        public Ministry Post(string name)
        {
            var date = DateTime.Now;

            var Ministrydata = new Ministry
            {
                Name = name,
                CreatedAt = date
            };

            _context.Ministries.Add(Ministrydata);
            _context.SaveChanges();

            return Ministrydata;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, string name)
        {
            var Ministry = _context.Ministries.Find(id);
            Ministry.Name = name;
            _context.Entry(Ministry).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var Ministrydata = _context.Ministries.Find(id);

            _context.Ministries.Remove(Ministrydata);
            _context.SaveChanges();
        }

        // <!-- ministry excel -->
        [HttpGet("excelministry")]
        public IActionResult excelministry()
        {

            var ministries = _context.Departments
                .Include(m => m.Ministries);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("ข้อมูลกระทรวง");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ชื่อกระทรวง";
                worksheet.Cell(currentRow, 2).Value = "กรม";
                foreach (var ministry in ministries)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = ministry.Ministries.Name;
                    worksheet.Cell(currentRow, 2).Value = ministry.Name;
                }
             
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "ministry.xlsx");
                }
            }
        }
        // <!-- END ministry excel -->
    }
}