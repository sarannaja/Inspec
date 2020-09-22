using System;
using System.Collections.Generic;
using System.IO; //excel
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel; //excel
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using MoreLinq;
//using MoreLinq.Extensions;

namespace InspecWeb.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public static IWebHostEnvironment _environment;

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static UserManager<ApplicationUser> _userManager;
        private static ApplicationDbContext _context;

        public UserController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;

        }

        // <!-- test excel -->
        [HttpGet("api/[controller]/[action]")]
        public IActionResult momomo()
        {
            //UserViewModel[] Momos = {
            //    new UserViewModel { UserName = "admin1@inspec.go.th", Email = "admin1@inspec.go.th" },
            //    new UserViewModel { UserName = "admin2@inspec.go.th", Email = "admin2@inspec.go.th" },
            //    new UserViewModel { UserName = "admin3@inspec.go.th", Email = "admin3@inspec.go.th" },
            //    new UserViewModel { UserName = "admin4@inspec.go.th", Email = "admin4@inspec.go.th" },
            //};

            var users = _context.Users;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Momos");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = "Username";
                foreach (var momo in users)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = momo.Email;
                    worksheet.Cell(currentRow, 2).Value = momo.UserName;
                }
                System.Console.WriteLine("momomo : " + "789");
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "momomo.xlsx");
                }
            }
        }
        // <!-- END test excel -->

        [HttpGet("api/[controller]/[action]/{id}")]
        public IEnumerable<ApplicationUser> getuser(long id)
        {
            var users = _context.Users
                .Include(s => s.UserRegion)
                .ThenInclude(r => r.Region)
                .Include(s => s.UserProvince)
                .ThenInclude(r => r.Province)
                .Include(s => s.Province)
                .Include(s => s.Ministries)
                .Include(x => x.Departments)
                .Include(x => x.ProvincialDepartments)
                .Include(p => p.Sides)
                .Where(m => m.Role_id == id)
                .Where(m => m.Active == 1)
                .Where(m => m.Email != "admin@inspec.go.th")
                .OrderByDescending(m => m.CreatedAt);
          
            return users;
        }

        [HttpGet("api/[controller]/[action]/{id}")]
        public IEnumerable<ApplicationUser> getuserlist(string id)
        {
            var users = _context.Users
                .Include(s => s.UserRegion)
                .ThenInclude(r => r.Region)
                .Include(s => s.UserProvince)
                .ThenInclude(r => r.Province)
                .Include(s => s.Province)
                .Include(s => s.Ministries)
                .Where(m => m.Id == id)
                .Where(m => m.Active == 1);

            return users;
        }

        [HttpGet("api/[controller]/[action]/{id}")]
        public IEnumerable<ApplicationUser> getuserfirst(string id)
        {
            var users = _context.Users
                .Include(s => s.UserRegion)
                .ThenInclude(r => r.Region)
                .Include(s => s.UserProvince)
                .ThenInclude(r => r.Province)
                .Include(s => s.Province)
                .Include(s => s.Ministries)
                .Include(s => s.ProvincialDepartments)
                .Where(m => m.Id == id)
                .Where(m => m.Active == 1).FirstOrDefault();

            yield return users;
        }

        //สำหรับใช้ตรง user
        [HttpGet("api/[controller]/[action]/{id}")]
        public IEnumerable<ProvincialDepartment> getprovincialdepartment(long id)
        {
            var provincialDepartment = _context.ProvincialDepartment
              .Where(x => x.DepartmentId == id)
              .ToList();

            return provincialDepartment;

        }

        //<!-- ข้อมูลผู้ติดต้อ ผู้ตรวจราชการ -->
        [HttpGet("api/[controller]/[action]")]
        public IEnumerable<ApplicationUser> inspector()
        {
            var users = _context.Users
                .Include(s => s.UserRegion)
                .ThenInclude(r => r.Region)
                .Include(s => s.UserProvince)
                .ThenInclude(r => r.Province)
                .Include(s => s.Province)
                .Include(s => s.Ministries)
                .Where(m => m.Role_id == 3)
                .Where(m => m.Position == "ผต.นร." || m.Position == "ผต.นร. ")
                .Where(m => m.Active == 1);

            return users;
        }
        //<!-- END ข้อมูลผู้ติดต้อ ผู้ตรวจราชการ -->

        //<!-- ข้อมูลผู้ติดต้อ เจ้าหน้าที่ประจำเขตตรวจราชการ -->
        [HttpGet("api/[controller]/[action]")]
        public IEnumerable<ApplicationUser> districtofficer()
        {
            var users = _context.Users
                .Include(s => s.UserRegion)
                .ThenInclude(r => r.Region)
                .Include(s => s.UserProvince)
                .ThenInclude(r => r.Province)
                .Include(s => s.Province)
                .Include(s => s.Ministries)
                .Where(m => m.Role_id == 3)
                .Where(m => m.Position != "ผต.นร." || m.Position != "ผต.นร. ")
                .Where(m => m.Active == 1);

            return users;
        }
        //<!-- END ข้อมูลผู้ติดต้อ เจ้าหน้าที่ประจำเขตตรวจราชการ -->

        //<!-- ข้อมูลผู้ติดต้อ เจ้าหน้าที่ประจำเขตตรวจราชการ -->
        [HttpGet("api/[controller]/[action]")]
        public IEnumerable<ApplicationUser> publicsectoradvisor()
        {
            var users = _context.Users
                .Include(s => s.UserRegion)
                .ThenInclude(r => r.Region)
                .Include(s => s.UserProvince)
                .ThenInclude(r => r.Province)
                .Include(s => s.Province)
                .Include(s => s.Ministries)
                .Where(m => m.Role_id == 7)
                .Where(m => m.Active == 1);

            return users;
        }
        //<!-- END ข้อมูลผู้ติดต้อ เจ้าหน้าที่ประจำเขตตรวจราชการ -->

        // POST api/values
        [Route("api/[controller]/changepassword/{id}")]
        [HttpGet]
        public async Task<IActionResult> ChangePassWord(string id)
        {
            var passwordrandom = RandomString(8);
            var user1 = await _context.Users.Where(x => x.Id == id).SingleOrDefaultAsync();
            var tresult = await _userManager.RemovePasswordAsync(user1);
            await _userManager.AddPasswordAsync(user1, passwordrandom);
            user1.Pw = passwordrandom;
            _context.Entry(user1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Ok(user1);
        }
        [Route("api/[controller]")]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] UserViewModel model)
        {
            System.Console.WriteLine("testuser : 1 " + model.Role_id);
            var date = DateTime.Now;
            var Username = "";
            string num = "0";
            var passwordrandom = RandomString(8);
            var namerole = "";

            //     Add a user password only if one does not already exist
            // await _userManager.AddPasswordAsync(user1, passwordrandom);
            if (model.Role_id == 3)
            {
                namerole = "inopm";
            }
            else if (model.Role_id == 4)
            {
                namerole = "govn";
            }
            else if (model.Role_id == 5)
            {
                namerole = "cogo";
            }
            else if (model.Role_id == 6)
            {
                namerole = "inmin";
            }
            else if (model.Role_id == 9)
            {
                namerole = "intdu";

            }
            else if (model.Role_id == 10)
            {
                namerole = "indep";
            }


            System.Console.WriteLine("testuser : 1.2 " + model.Autocreateuser);
            if (model.Autocreateuser == 1)
            {
                if (model.Role_id == 3 || model.Role_id == 6)
                {

                    // count จำนวน
                    var usercount = _context.Users.Where(m => m.Role_id == model.Role_id).Where(m => m.MinistryId == model.MinistryId).Count();
                    var num2 = "0";
                    //ชื่อกระทรวง
                    var ministrydata = _context.Ministries.Find(model.MinistryId);

                    //ถ้าเกิดยังไม่มี username ในกระทรวงนี้
                    if (usercount == 0)
                    {
                        num = "0" + 1.ToString();

                        Username = ministrydata.ShortnameEN + "_" + namerole + num;
                    }
                    else
                    {
                        for (var i = 1; i <= usercount; i++)
                        {

                            if (i >= 10)
                            {
                                num = i.ToString();
                            }
                            else
                            {
                                num = "0" + i.ToString();
                            }

                            //เอาUsername ไปเช็คในระบบ
                            var CheckUsername = ministrydata.ShortnameEN + "_" + namerole + num;
                            var usercount2 = _context.Users.Where(m => m.UserName == CheckUsername).Where(m => m.Role_id == model.Role_id).Count();

                            //ถ้าไม่มีในระบบ
                            if (usercount2 == 0)
                            {

                                Username = ministrydata.ShortnameEN + "_" + namerole + num;

                                break;
                            }
                            else
                            {
                                var num3 = i + 1;
                                if (num3 >= 10)
                                {
                                    num2 = num3.ToString();
                                }
                                else
                                {
                                    num2 = "0" + num3.ToString();
                                }
                                Username = ministrydata.ShortnameEN + "_" + namerole + num2;
                            }

                        }

                    }
                }
                else if (model.Role_id == 10)
                {

                    // count จำนวน
                    var usercount = _context.Users.Where(m => m.Role_id == model.Role_id).Where(m => m.DepartmentId == model.DepartmentId).Count();
                    var num2 = "0";
                    //ชื่อกรม
                    var departmentdata = _context.Departments.Find(model.DepartmentId);

                    //ถ้าเกิดยังไม่มี username ในกรมนี้
                    if (usercount == 0)
                    {
                        num = "0" + 1.ToString();

                        Username = departmentdata.ShortnameEN + "_" + namerole + num;
                    }
                    else
                    {
                        for (var i = 1; i <= usercount; i++)
                        {

                            if (i >= 10)
                            {
                                num = i.ToString();
                            }
                            else
                            {
                                num = "0" + i.ToString();
                            }

                            //เอาUsername ไปเช็คในระบบ
                            var CheckUsername = departmentdata.ShortnameEN + "_" + namerole + num;
                            var usercount2 = _context.Users.Where(m => m.UserName == CheckUsername).Where(m => m.Role_id == model.Role_id).Count();

                            //ถ้าไม่มีในระบบ
                            if (usercount2 == 0)
                            {

                                Username = departmentdata.ShortnameEN + "_" + namerole + num;

                                break;
                            }
                            else
                            {
                                var num3 = i + 1;
                                if (num3 >= 10)
                                {
                                    num2 = num3.ToString();
                                }
                                else
                                {
                                    num2 = "0" + num3.ToString();
                                }
                                Username = departmentdata.ShortnameEN + "_" + namerole + num2;
                            }

                        }
                    }
                }
                else if (model.Role_id == 4 || model.Role_id == 5)
                {
                    // count จำนวน
                    var usercount = _context.Users.Where(m => m.Role_id == model.Role_id)
                        .Include(m => m.UserProvince)
                        .Where(x => x.UserProvince.Any(x => x.ProvinceId == model.UserProvinceId)).Count();
                    var num2 = "0";
                    //ชื่อ จังหวัด
                    var provincedata = _context.Provinces.Find(model.UserProvinceId);

                    //ถ้าเกิดยังไม่มี username ในกรมนี้
                    if (usercount == 0)
                    {
                        num = "0" + 1.ToString();

                        Username = provincedata.ShortnameEN + "_" + namerole + num;
                    }
                    else
                    {
                        for (var i = 1; i <= usercount; i++)
                        {

                            if (i >= 10)
                            {
                                num = i.ToString();
                            }
                            else
                            {
                                num = "0" + i.ToString();
                            }

                            //เอาUsername ไปเช็คในระบบ
                            var CheckUsername = provincedata.ShortnameEN + "_" + namerole + num;
                            var usercount2 = _context.Users.Where(m => m.UserName == CheckUsername).Where(m => m.Role_id == model.Role_id).Count();

                            //ถ้าไม่มีในระบบ
                            if (usercount2 == 0)
                            {

                                Username = provincedata.ShortnameEN + "_" + namerole + num;

                                break;
                            }
                            else
                            {
                                var num3 = i + 1;
                                if (num3 >= 10)
                                {
                                    num2 = num3.ToString();
                                }
                                else
                                {
                                    num2 = "0" + num3.ToString();
                                }
                                Username = provincedata.ShortnameEN + "_" + namerole + num2;
                            }

                        }
                    }
                }
                else if (model.Role_id == 1 || model.Role_id == 2 || model.Role_id == 8 || model.Role_id == 11)
                {
                    Username = model.Email;
                }
                else if (model.Role_id == 7)
                {
                    // count จำนวน
                    var usercount = _context.Users.Where(m => m.Role_id == model.Role_id)
                        .Include(m => m.UserProvince)
                        .Where(m => m.SideId == model.SideId)
                        .Where(x => x.UserProvince.Any(x => x.ProvinceId == model.UserProvinceId)).Count();
                    var num2 = "0";
                    //ชื่อ ด้าน
                    var sidedata = _context.Sides.Find(model.SideId);
                    //ชื่อ จังหวัด
                    var provincedata = _context.Provinces.Find(model.UserProvinceId);

                    //ถ้าเกิดยังไม่มี username ในด้านนี้
                    if (usercount == 0)
                    {
                        num = "0" + 1.ToString();

                        Username = sidedata.ShortnameEN + "_" + provincedata.ShortnameEN + num;
                    }
                    else
                    {
                        for (var i = 1; i <= usercount; i++)
                        {

                            if (i >= 10)
                            {
                                num = i.ToString();
                            }
                            else
                            {
                                num = "0" + i.ToString();
                            }

                            //เอาUsername ไปเช็คในระบบ
                            var CheckUsername = sidedata.ShortnameEN + "_" + provincedata.ShortnameEN + num;
                            var usercount2 = _context.Users
                                .Where(m => m.UserName == CheckUsername)
                                .Where(m => m.Role_id == model.Role_id).Count();

                            //ถ้าไม่มีในระบบ
                            if (usercount2 == 0)
                            {

                                Username = sidedata.ShortnameEN + "_" + provincedata.ShortnameEN + num;

                                break;
                            }
                            else
                            {
                                var num3 = i + 1;
                                if (num3 >= 10)
                                {
                                    num2 = num3.ToString();
                                }
                                else
                                {
                                    num2 = "0" + num3.ToString();
                                }
                                Username = sidedata.ShortnameEN + "_" + provincedata.ShortnameEN + num2;
                            }

                        }
                    }
                }
                else if (model.Role_id == 9)
                {

                    //เช้คว่าเลือกมาแบบหลายจังหวัดหรือไม่
                    if (model.UserProvince.Count() > 1)
                    {
                        // count จำนวน
                        var usercount = _context.Users
                            .Where(m => m.Role_id == model.Role_id && m.UserProvince.Count > 1)
                            .Where(m => m.DepartmentId == model.DepartmentId).Count();
                        var num2 = "0";
                        //ชื่อกรม
                        var departmentdata = _context.Departments.Find(model.DepartmentId);

                        //ถ้าเกิดยังไม่มี username ในหลายจังหวัดนี้
                        if (usercount == 0)
                        {
                            num = "0" + 1.ToString();

                            Username = departmentdata.ShortnameEN + "_" + "reg" + num;
                        }
                        else
                        {
                            for (var i = 1; i <= usercount; i++)
                            {

                                if (i >= 10)
                                {
                                    num = i.ToString();
                                }
                                else
                                {
                                    num = "0" + i.ToString();
                                }

                                //เอาUsername ไปเช็คในระบบ
                                var CheckUsername = departmentdata.ShortnameEN + "_" + "reg" + num;
                                var usercount2 = _context.Users.Where(m => m.UserName == CheckUsername).Where(m => m.Role_id == model.Role_id).Count();

                                //ถ้าไม่มีในระบบ
                                if (usercount2 == 0)
                                {

                                    Username = departmentdata.ShortnameEN + "_" + "reg" + num;

                                    break;
                                }
                                else
                                {
                                    var num3 = i + 1;
                                    if (num3 >= 10)
                                    {
                                        num2 = num3.ToString();
                                    }
                                    else
                                    {
                                        num2 = "0" + num3.ToString();
                                    }
                                    Username = departmentdata.ShortnameEN + "_" + "reg" + num2;
                                }

                            }
                        }
                    }
                    else
                    {
                        foreach (var item2 in model.UserProvince)
                        {
                            // count จำนวน
                            var usercount = _context.Users.Where(m => m.Role_id == model.Role_id && m.UserProvince.Count <= 1)
                             .Include(m => m.UserProvince)
                              .Where(x => x.UserProvince.Any(x => x.ProvinceId == item2))
                              .Where(m => m.DepartmentId == model.DepartmentId).Count();
                            var num2 = "0";
                            //ชื่อกรม
                            var departmentdata = _context.Departments.Find(model.DepartmentId);
                            //ชื่อจังหวัด
                            long setid = 0;
                            setid = item2;
                            var provincedata = _context.Provinces.Find(setid);

                            //ถ้าเกิดยังไม่มี username ในหลายจังหวัดนี้
                            if (usercount == 0)
                            {
                                num = "0" + 1.ToString();

                                Username = departmentdata.ShortnameEN + "_" + provincedata.ShortnameEN + num;
                            }
                            else
                            {
                                for (var i = 1; i <= usercount; i++)
                                {

                                    if (i >= 10)
                                    {
                                        num = i.ToString();
                                    }
                                    else
                                    {
                                        num = "0" + i.ToString();
                                    }

                                    //เอาUsername ไปเช็คในระบบ
                                    var CheckUsername = departmentdata.ShortnameEN + "_" + provincedata.ShortnameEN + num;
                                    var usercount2 = _context.Users.Where(m => m.UserName == CheckUsername).Where(m => m.Role_id == model.Role_id).Count();

                                    //ถ้าไม่มีในระบบ
                                    if (usercount2 == 0)
                                    {

                                        Username = departmentdata.ShortnameEN + "_" + provincedata.ShortnameEN + num;

                                        break;
                                    }
                                    else
                                    {
                                        var num3 = i + 1;
                                        if (num3 >= 10)
                                        {
                                            num2 = num3.ToString();
                                        }
                                        else
                                        {
                                            num2 = "0" + num3.ToString();
                                        }
                                        Username = departmentdata.ShortnameEN + "_" + provincedata.ShortnameEN + num2;
                                    }

                                }
                            }
                        }

                    }



                }

            }
            else
            {
                Username = model.UserName;
            }

            System.Console.WriteLine("testuser : 1.3 " + Username);
            var user = new ApplicationUser { UserName = Username, Email = model.Email };
            //ข้อมูลหลัก
            user.DistrictId = model.DistrictId;
            user.ProvinceId = model.ProvinceId;
            user.SubdistrictId = model.SubdistrictId;
            user.MinistryId = model.MinistryId;
            user.DepartmentId = model.DepartmentId;
            user.ProvincialDepartmentId = model.ProvincialDepartmentId;
            user.Position = model.Position;
            user.Prefix = model.Prefix;
            user.Name = model.Firstnameth + ' ' + model.Lastnameth;
            user.Firstnameth = model.Firstnameth;
            user.Lastnameth = model.Lastnameth;
            user.Role_id = model.Role_id;
            user.CreatedAt = DateTime.Now;
            user.Startdate = model.Startdate;
            user.Enddate = model.Enddate;
            user.Active = 1;
            user.Commandnumberdate = model.Commandnumberdate;//ลงวันที่คำสั่ง
            user.Pw = passwordrandom;
            user.Commandnumber = model.Commandnumber;

            //ข้อมูลรอง
            user.Educational = model.Educational;
            user.Birthday = DateTime.Now;
            user.Officephonenumber = model.Officephonenumber;
            user.PhoneNumber = model.PhoneNumber;
            user.Telegraphnumber = model.Telegraphnumber;
            user.Housenumber = model.Housenumber;
            user.Rold = model.Rold;
            user.Alley = model.Alley;
            user.Postalcode = model.Postalcode;
            user.SideId = model.SideId;
            user.FiscalYearId = model.FiscalYearId;
            user.Autocreateuser = model.Autocreateuser;

            user.Img = null;

            if (!Directory.Exists(_environment.WebRootPath + "//imgprofile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//imgprofile//"); //สร้าง Folder Upload ใน wwwroot
            }

            var filePath = _environment.WebRootPath + "//imgprofile//";
            if (model.files != null)
            {
                foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
                {
                    var random = RandomString(10);
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);

                    if (formFile.Value.Length > 0)
                    {
                        using (var stream = System.IO.File.Create(filePath + random + filename))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }

                        user.Img = random + filename;
                    }
                }
                System.Console.WriteLine("testuser : 2");
            }

            var success = await _userManager.CreateAsync(user, passwordrandom).ConfigureAwait(false);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
            await _userManager.ConfirmEmailAsync(user, code).ConfigureAwait(false);

            System.Console.WriteLine("testuser : 2" + passwordrandom);



            //_context.Entry (user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            // _context.SaveChanges ();
            System.Console.WriteLine("testuser : 3");
            //user ที่อยู่หลายเขต
            int tt = 0;
            List<FiscalYearRelation> termsList = new List<FiscalYearRelation>();

            //สำหรับกรณีของ role ผู้ตรวจเขต
            if (model.Role_id == 3 || model.Role_id == 6 || model.Role_id == 8 || model.Role_id == 10)
            {
                foreach (var item in model.UserRegion)
                {
                    var userregiondata = new UserRegion
                    {
                        UserID = user.Id,
                        RegionId = item
                    };
                    System.Console.WriteLine("testuser : 3.2 :" + item);
                    _context.UserRegions.Add(userregiondata);
                    _context.SaveChanges();

                    System.Console.WriteLine("testuser : 3.3");


                    //yochigang แก้ไข 20200915
                    var userprovince = _context.FiscalYearRelations
                                .Where(m => m.RegionId == item && m.FiscalYearId == model.FiscalYearId)
                                .ToList();

                    // System.Console.WriteLine("UserRegion :" + item);
                    foreach (var item2 in userprovince)
                    {
                        termsList.Add(item2);
                    }
                    tt++;
                    System.Console.WriteLine("testuser : 4.1 :");

                }
            }
            FiscalYearRelation[] terms = termsList.ToArray();

            if (terms.Count() != 0)
            {
                foreach (var x in terms)
                {
                    //System.Console.WriteLine("momomo :" + item2.ProvinceId);
                    var userprovincedata = new UserProvince
                    {
                        UserID = user.Id,
                        ProvinceId = x.ProvinceId
                    };
                    _context.UserProvinces.Add(userprovincedata);
                    _context.SaveChanges();
                }
            }
            ////จังหวัดที่ทำงาน
            if (model.Role_id == 1 || model.Role_id == 2 || model.Role_id == 4 || model.Role_id == 5
                 || model.Role_id == 7 || model.Role_id == 9)
            {

                //  foreach (var item3 in model.UserProvince)
                //   {
                var userregiondata = new UserRegion
                {
                    UserID = user.Id,
                    RegionId = model.UserRegionId
                };
                System.Console.WriteLine("testuser : 4.2");
                _context.UserRegions.Add(userregiondata);
                _context.SaveChanges();


                if (model.Role_id == 9)
                {
                    foreach (var item in model.UserProvince)
                    {
                        var userprovincedata = new UserProvince
                        {
                            UserID = user.Id,
                            ProvinceId = item
                        };
                        System.Console.WriteLine("testuser : 111.2");
                        _context.UserProvinces.Add(userprovincedata);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    var userprovincedata = new UserProvince
                    {
                        UserID = user.Id,
                        ProvinceId = model.UserProvinceId
                    };
                    _context.UserProvinces.Add(userprovincedata);
                    _context.SaveChanges();
                }
                // }
                System.Console.WriteLine("testuser : 5");
            }

            return Ok(new { password = user.Pw });

        }

        [Route("api/[controller]/{editId}")]
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] UserViewModel model, String editId)
        {

            var userdata = _context.Users.Find(editId);
            userdata.Img = model.Img;
            userdata.Signature = model.Signature;
            var passwordrandom = RandomString(8);
            var Username = "";
            string num = "0";

            var namerole = "";

            if (model.Role_id == 3)
            {
                namerole = "inopm";
            }
            else if (model.Role_id == 4)
            {
                namerole = "govn";
            }
            else if (model.Role_id == 5)
            {
                namerole = "cogo";
            }
            else if (model.Role_id == 6)
            {
                namerole = "inmin";
            }
            else if (model.Role_id == 9)
            {
                namerole = "intdu";

            }
            else if (model.Role_id == 10)
            {
                namerole = "indep";
            }

            //สร้าง username อัติโนมัติ
            if (model.Autocreateuser == 1)
            {
                if (model.Role_id == 3 || model.Role_id == 6)
                {

                    var check = _context.Users.Where(m => m.Id == editId).Where(m => m.MinistryId == model.MinistryId);
                    //ถ้าไม่มีการแก้ไข กระทรวง
                    if (check.Count() >= 1)
                    {
                        Username = check.Select(m => m.UserName).FirstOrDefault();
                    }
                    //ถ้ามีการแก้ไข กระทรวง
                    else
                    {

                        // count จำนวน
                        var usercount = _context.Users.Where(m => m.Role_id == model.Role_id).Where(m => m.MinistryId == model.MinistryId).Count();
                        var num2 = "0";
                        //ชื่อกระทรวง
                        var ministrydata = _context.Ministries.Find(model.MinistryId);

                        //ถ้าเกิดยังไม่มี username ในกระทรวงนี้
                        if (usercount == 0)
                        {
                            num = "0" + 1.ToString();

                            Username = ministrydata.ShortnameEN + "_" + namerole + num;
                        }
                        else
                        {
                            for (var i = 1; i <= usercount; i++)
                            {

                                if (i >= 10)
                                {
                                    num = i.ToString();
                                }
                                else
                                {
                                    num = "0" + i.ToString();
                                }

                                //เอาUsername ไปเช็คในระบบ
                                var CheckUsername = ministrydata.ShortnameEN + "_" + namerole + num;
                                var usercount2 = _context.Users.Where(m => m.UserName == CheckUsername).Where(m => m.Role_id == model.Role_id).Count();

                                //ถ้าไม่มีในระบบ
                                if (usercount2 == 0)
                                {

                                    Username = ministrydata.ShortnameEN + "_" + namerole + num;

                                    break;
                                }
                                else
                                {
                                    var num3 = i + 1;
                                    if (num3 >= 10)
                                    {
                                        num2 = num3.ToString();
                                    }
                                    else
                                    {
                                        num2 = "0" + num3.ToString();
                                    }
                                    Username = ministrydata.ShortnameEN + "_" + namerole + num2;
                                }

                            }
                        }

                    }
                }

                else if (model.Role_id == 10)
                {

                    var check = _context.Users.Where(m => m.Id == editId).Where(m => m.DepartmentId == model.DepartmentId);

                    //ถ้าไม่มีการแก้ไข กรม
                    if (check.Count() >= 1)
                    {
                        Username = check.Select(m => m.UserName).FirstOrDefault();
                    }
                    //ถ้ามีการแก้ไขกรม
                    else
                    {

                        // count จำนวน
                        var usercount = _context.Users.Where(m => m.Role_id == model.Role_id).Where(m => m.DepartmentId == model.DepartmentId).Count();
                        var num2 = "0";
                        //ชื่อกรม
                        var departmentdata = _context.Departments.Find(model.DepartmentId);

                        //ถ้าเกิดยังไม่มี username ในกรมนี้
                        if (usercount == 0)
                        {
                            num = "0" + 1.ToString();

                            Username = departmentdata.ShortnameEN + "_" + namerole + num;
                        }
                        else
                        {
                            for (var i = 1; i <= usercount; i++)
                            {

                                if (i >= 10)
                                {
                                    num = i.ToString();
                                }
                                else
                                {
                                    num = "0" + i.ToString();
                                }

                                //เอาUsername ไปเช็คในระบบ
                                var CheckUsername = departmentdata.ShortnameEN + "_" + namerole + num;
                                var usercount2 = _context.Users.Where(m => m.UserName == CheckUsername).Where(m => m.Role_id == model.Role_id).Count();

                                //ถ้าไม่มีในระบบ
                                if (usercount2 == 0)
                                {

                                    Username = departmentdata.ShortnameEN + "_" + namerole + num;

                                    break;
                                }
                                else
                                {
                                    var num3 = i + 1;
                                    if (num3 >= 10)
                                    {
                                        num2 = num3.ToString();
                                    }
                                    else
                                    {
                                        num2 = "0" + num3.ToString();
                                    }
                                    Username = departmentdata.ShortnameEN + "_" + namerole + num2;
                                }

                            }
                        }

                    }

                }
                else if (model.Role_id == 4 || model.Role_id == 5)
                {
                    var check = _context.Users
                        .Include(m => m.UserProvince)
                        .Where(m => m.Id == editId)
                        .Where(x => x.UserProvince.Any(x => x.ProvinceId == model.UserProvinceId));

                    //ถ้าไม่มีการแก้ไข จังหวัด
                    if (check.Count() >= 1)
                    {
                        Username = check.Select(m => m.UserName).FirstOrDefault();
                    }
                    //ถ้ามีการแก้ไข จังหวัด
                    else
                    {

                        // count จำนวน
                        var usercount = _context.Users.Where(m => m.Role_id == model.Role_id)
                            .Include(m => m.UserProvince)
                            .Where(x => x.UserProvince.Any(x => x.ProvinceId == model.UserProvinceId)).Count();
                        var num2 = "0";
                        //ชื่อ จังหวัด
                        var provincedata = _context.Provinces.Find(model.UserProvinceId);

                        //ถ้าเกิดยังไม่มี username ในกรมนี้
                        if (usercount == 0)
                        {
                            num = "0" + 1.ToString();

                            Username = provincedata.ShortnameEN + "_" + namerole + num;
                        }
                        else
                        {
                            for (var i = 1; i <= usercount; i++)
                            {

                                if (i >= 10)
                                {
                                    num = i.ToString();
                                }
                                else
                                {
                                    num = "0" + i.ToString();
                                }

                                //เอาUsername ไปเช็คในระบบ
                                var CheckUsername = provincedata.ShortnameEN + "_" + namerole + num;
                                var usercount2 = _context.Users.Where(m => m.UserName == CheckUsername).Where(m => m.Role_id == model.Role_id).Count();

                                //ถ้าไม่มีในระบบ
                                if (usercount2 == 0)
                                {

                                    Username = provincedata.ShortnameEN + "_" + namerole + num;

                                    break;
                                }
                                else
                                {
                                    var num3 = i + 1;
                                    if (num3 >= 10)
                                    {
                                        num2 = num3.ToString();
                                    }
                                    else
                                    {
                                        num2 = "0" + num3.ToString();
                                    }
                                    Username = provincedata.ShortnameEN + "_" + namerole + num2;
                                }

                            }
                        }

                    }


                }
                else if (model.Role_id == 1 || model.Role_id == 2 || model.Role_id == 8)
                {

                    Username = model.Email;
                }
                else if (model.Role_id == 7)
                {

                    var check = _context.Users
                       .Include(m => m.UserProvince)
                       .Where(m => m.Id == editId)
                       .Where(m => m.SideId == model.SideId)
                       .Where(x => x.UserProvince.Any(x => x.ProvinceId == model.UserProvinceId));

                    //ถ้าไม่มีการแก้ไข ด้าน และ จังหวัด
                    if (check.Count() >= 1)
                    {
                        Username = check.Select(m => m.UserName).FirstOrDefault();
                    }
                    //ถ้ามีการแก้ไข ด้าน และ จังหวัด
                    else
                    {

                        // count จำนวน
                        var usercount = _context.Users.Where(m => m.Role_id == model.Role_id)
                            .Include(m => m.UserProvince)
                            .Where(m => m.SideId == model.SideId)
                            .Where(x => x.UserProvince.Any(x => x.ProvinceId == model.UserProvinceId)).Count();
                        var num2 = "0";
                        //ชื่อ ด้าน
                        var sidedata = _context.Sides.Find(model.SideId);
                        //ชื่อ จังหวัด
                        var provincedata = _context.Provinces.Find(model.UserProvinceId);

                        //ถ้าเกิดยังไม่มี username ในด้านนี้
                        if (usercount == 0)
                        {
                            num = "0" + 1.ToString();

                            Username = sidedata.ShortnameEN + "_" + provincedata.ShortnameEN + num;
                        }
                        else
                        {
                            for (var i = 1; i <= usercount; i++)
                            {

                                if (i >= 10)
                                {
                                    num = i.ToString();
                                }
                                else
                                {
                                    num = "0" + i.ToString();
                                }

                                //เอาUsername ไปเช็คในระบบ
                                var CheckUsername = sidedata.ShortnameEN + "_" + provincedata.ShortnameEN + num;
                                var usercount2 = _context.Users
                                    .Where(m => m.UserName == CheckUsername)
                                    .Where(m => m.Role_id == model.Role_id).Count();

                                //ถ้าไม่มีในระบบ
                                if (usercount2 == 0)
                                {

                                    Username = sidedata.ShortnameEN + "_" + provincedata.ShortnameEN + num;

                                    break;
                                }
                                else
                                {
                                    var num3 = i + 1;
                                    if (num3 >= 10)
                                    {
                                        num2 = num3.ToString();
                                    }
                                    else
                                    {
                                        num2 = "0" + num3.ToString();
                                    }
                                    Username = sidedata.ShortnameEN + "_" + provincedata.ShortnameEN + num2;
                                }

                            }
                        }

                    }

                }
                else if (model.Role_id == 9)
                {

                    //เช้คว่าเลือกมาแบบหลายจังหวัดหรือไม่
                    if (model.UserProvince.Count() > 1)
                    {

                        var check = _context.Users
                                    .Where(m => m.Id == editId && m.UserProvince.Count > 1)
                                    .Where(m => m.DepartmentId == model.DepartmentId); ;

                        //ถ้าไม่มีการแก้ไข เลือกจังหวัดเยอะกว่า1
                        if (check.Count() >= 1)
                        {
                            Username = check.Select(m => m.UserName).FirstOrDefault();
                        }
                        //ถ้ามีการแก้ไข เลือกจังหวัดเยอะกว่า1
                        else
                        {

                            // count จำนวน
                            var usercount = _context.Users
                                .Where(m => m.Role_id == model.Role_id && m.UserProvince.Count > 1)
                                .Where(m => m.DepartmentId == model.DepartmentId).Count();
                            var num2 = "0";
                            //ชื่อกรม
                            var departmentdata = _context.Departments.Find(model.DepartmentId);

                            //ถ้าเกิดยังไม่มี username ในหลายจังหวัดนี้
                            if (usercount == 0)
                            {
                                num = "0" + 1.ToString();

                                Username = departmentdata.ShortnameEN + "_" + "reg" + num;
                            }
                            else
                            {
                                for (var i = 1; i <= usercount; i++)
                                {

                                    if (i >= 10)
                                    {
                                        num = i.ToString();
                                    }
                                    else
                                    {
                                        num = "0" + i.ToString();
                                    }

                                    //เอาUsername ไปเช็คในระบบ
                                    var CheckUsername = departmentdata.ShortnameEN + "_" + "reg" + num;
                                    var usercount2 = _context.Users.Where(m => m.UserName == CheckUsername).Where(m => m.Role_id == model.Role_id).Count();

                                    //ถ้าไม่มีในระบบ
                                    if (usercount2 == 0)
                                    {

                                        Username = departmentdata.ShortnameEN + "_" + "reg" + num;

                                        break;
                                    }
                                    else
                                    {
                                        var num3 = i + 1;
                                        if (num3 >= 10)
                                        {
                                            num2 = num3.ToString();
                                        }
                                        else
                                        {
                                            num2 = "0" + num3.ToString();
                                        }
                                        Username = departmentdata.ShortnameEN + "_" + "reg" + num2;
                                    }

                                }
                            }

                        }

                    }
                    //มีจังหวัดเดียว
                    else
                    {
                        foreach (var item2 in model.UserProvince)
                        {

                            var check = _context.Users.Where(m => m.Id == editId && m.UserProvince.Count <= 1)
                                     .Include(m => m.UserProvince)
                                     .Where(x => x.UserProvince.Any(x => x.ProvinceId == item2))
                                     .Where(m => m.DepartmentId == model.DepartmentId);

                            //ถ้าไม่มีการแก้ไข จังหวัด
                            if (check.Count() >= 1)
                            {
                                Username = check.Select(m => m.UserName).FirstOrDefault();
                            }
                            //ถ้ามีการแก้ไข จังหวัด
                            else
                            {

                                // count จำนวน
                                var usercount = _context.Users.Where(m => m.Role_id == model.Role_id && m.UserProvince.Count <= 1)
                                 .Include(m => m.UserProvince)
                                  .Where(x => x.UserProvince.Any(x => x.ProvinceId == item2))
                                  .Where(m => m.DepartmentId == model.DepartmentId).Count();
                                var num2 = "0";
                                //ชื่อกรม
                                var departmentdata = _context.Departments.Find(model.DepartmentId);
                                //ชื่อจังหวัด
                                long setid = 0;
                                setid = item2;
                                var provincedata = _context.Provinces.Find(setid);

                                //ถ้าเกิดยังไม่มี username ในหลายจังหวัดนี้
                                if (usercount == 0)
                                {
                                    num = "0" + 1.ToString();

                                    Username = departmentdata.ShortnameEN + "_" + provincedata.ShortnameEN + num;
                                }
                                else
                                {
                                    for (var i = 1; i <= usercount; i++)
                                    {

                                        if (i >= 10)
                                        {
                                            num = i.ToString();
                                        }
                                        else
                                        {
                                            num = "0" + i.ToString();
                                        }

                                        //เอาUsername ไปเช็คในระบบ
                                        var CheckUsername = departmentdata.ShortnameEN + "_" + provincedata.ShortnameEN + num;
                                        var usercount2 = _context.Users.Where(m => m.UserName == CheckUsername).Where(m => m.Role_id == model.Role_id).Count();

                                        //ถ้าไม่มีในระบบ
                                        if (usercount2 == 0)
                                        {

                                            Username = departmentdata.ShortnameEN + "_" + provincedata.ShortnameEN + num;

                                            break;
                                        }
                                        else
                                        {
                                            var num3 = i + 1;
                                            if (num3 >= 10)
                                            {
                                                num2 = num3.ToString();
                                            }
                                            else
                                            {
                                                num2 = "0" + num3.ToString();
                                            }
                                            Username = departmentdata.ShortnameEN + "_" + provincedata.ShortnameEN + num2;
                                        }

                                    }
                                }

                            }

                        }

                    }

                }

            }
            //สร้าง username เอง
            else
            {
                Username = model.UserName;
            }


            //<!-- file -->
            if (!Directory.Exists(_environment.WebRootPath + "//imgprofile//"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//imgprofile//"); //สร้าง Folder Upload ใน wwwroot
            }

            var filePath = _environment.WebRootPath + "//imgprofile//";
            if (model.files != null)
            {
                foreach (var formFile in model.files.Select((value, index) => new { Value = value, Index = index }))
                {
                    var random = RandomString(10);
                    string filePath2 = formFile.Value.FileName;
                    string filename = Path.GetFileName(filePath2);
                    string ext = Path.GetExtension(filename);

                    System.Console.WriteLine("testuser1 : ");

                    if (formFile.Value.Length > 0)
                    {
                        using (var stream = System.IO.File.Create(filePath + random + filename))
                        {
                            await formFile.Value.CopyToAsync(stream);
                        }

                        //<!-- ลบไฟล์ออกจากโฟลเดอร์ -->
                        string fullPath = _environment.WebRootPath + "//imgprofile//" + model.Img;
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }

                        userdata.Img = random + filename;
                        System.Console.WriteLine("testuser2 : ");
                    }
                }
            }
            //<!-- END file -->

            //<!-- file2 -->
            if (model.Formprofile == 1) // 1 คือแก้ไขจากตัวuser เอง คือส่วนลายเซ็น
            {
                if (!Directory.Exists(_environment.WebRootPath + "//Signature//"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "//Signature//"); //สร้าง Folder Upload ใน wwwroot
                }

                var filePath3 = _environment.WebRootPath + "//Signature//";
                if (model.files2 != null)
                {
                    foreach (var formFile in model.files2.Select((value, index) => new { Value = value, Index = index }))
                    {
                        var random = RandomString(10);
                        string filePath4 = formFile.Value.FileName;
                        string filename = Path.GetFileName(filePath4);
                        string ext = Path.GetExtension(filename);

                        System.Console.WriteLine("testuser1.2 : ");

                        if (formFile.Value.Length > 0)
                        {
                            using (var stream = System.IO.File.Create(filePath3 + random + filename))
                            {
                                await formFile.Value.CopyToAsync(stream);
                            }
                            //<!-- ลบไฟล์ออกจากโฟลเดอร์ -->
                            string fullPath = _environment.WebRootPath + "//Signature//" + model.Signature;
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }

                            userdata.Signature = random + filename;
                            System.Console.WriteLine("testuser2.2 : ");
                        }
                    }
                }
                //<!-- END file2 -->
            }


            if (model.Formprofile == 1) // 1 คือแก้ไขจากตัวuser เอง
            {
                userdata.Prefix = model.Prefix;
                userdata.Name = model.Firstnameth + ' ' + model.Lastnameth;
                userdata.Firstnameth = model.Firstnameth;
                userdata.Lastnameth = model.Lastnameth;
                userdata.Position = model.Position;
                userdata.PhoneNumber = model.PhoneNumber;
                System.Console.WriteLine("testuser3 : ");
            }
            else // แอดมินแก้ไขไห้
            {

                userdata.DistrictId = model.DistrictId;
                userdata.ProvinceId = model.ProvinceId;
                userdata.SubdistrictId = model.SubdistrictId;
                userdata.MinistryId = model.MinistryId;
                userdata.DepartmentId = model.DepartmentId;
                userdata.ProvincialDepartmentId = model.ProvincialDepartmentId;
                userdata.SideId = model.SideId;
                userdata.FiscalYearId = model.FiscalYearId;
                userdata.Position = model.Position;
                userdata.Prefix = model.Prefix;
                userdata.Name = model.Firstnameth + ' ' + model.Lastnameth;
                userdata.Firstnameth = model.Firstnameth;
                userdata.Lastnameth = model.Lastnameth;
                userdata.Role_id = model.Role_id;
                userdata.Startdate = model.Startdate;
                userdata.Enddate = model.Enddate;
                userdata.Active = 1;
                userdata.Commandnumberdate = model.Commandnumberdate;//ลงวันที่คำสั่ง  
                userdata.Commandnumber = model.Commandnumber;
                //userdata.CreatedAt = model.CreatedAt;

                //ข้อมูลรอง
                userdata.Educational = model.Educational;
                userdata.Birthday = DateTime.Now;
                userdata.Officephonenumber = model.Officephonenumber;
                userdata.PhoneNumber = model.PhoneNumber;
                userdata.Telegraphnumber = model.Telegraphnumber;
                userdata.Housenumber = model.Housenumber;
                userdata.Rold = model.Rold;
                userdata.Alley = model.Alley;
                userdata.Postalcode = model.Postalcode;
                userdata.Autocreateuser = model.Autocreateuser;
                System.Console.WriteLine("testuser3.5 : " + Username);
                userdata.UserName = Username;
                userdata.Pw = passwordrandom;
                System.Console.WriteLine("testuser4 : " + model.SideId);

                // <!-- ลบเขตก่อน -->
                var deleteuserregiondata = _context.UserRegions.Where(m => m.UserID == editId);
                _context.UserRegions.RemoveRange(deleteuserregiondata);
                _context.SaveChanges();
                System.Console.WriteLine("testuser5 : ");

                var deleteuserprovincedata = _context.UserProvinces.Where(m => m.UserID == editId);
                _context.UserProvinces.RemoveRange(deleteuserprovincedata);
                _context.SaveChanges();
                System.Console.WriteLine("testuser6 : ");
                // <!-- END ลบเขตก่อน -->


                int tt = 0;
                List<FiscalYearRelation> termsList = new List<FiscalYearRelation>();

                //สำหรับกรณีของ role ผู้ตรวจเขต
                if (model.Role_id == 3 || model.Role_id == 6 || model.Role_id == 8 || model.Role_id == 10)
                {
                    foreach (var item in model.UserRegion)
                    {
                        var userregiondata = new UserRegion
                        {
                            UserID = editId,
                            RegionId = item
                        };
                        System.Console.WriteLine("testuser8 :");
                        _context.UserRegions.Add(userregiondata);
                        _context.SaveChanges();


                        var FiscalYearF = _context.FiscalYears.First();

                        //yochigang แก้ไข 20200915
                        var userprovince = _context.FiscalYearRelations
                                    .Where(m => m.RegionId == item && m.FiscalYearId == model.FiscalYearId)
                                    .ToList();


                        foreach (var item2 in userprovince)
                        {
                            termsList.Add(item2);
                        }
                        tt++;
                        System.Console.WriteLine("testuser10 :");

                    }
                }
                FiscalYearRelation[] terms = termsList.ToArray();
                System.Console.WriteLine("testuser10.2 :");
                if (terms.Count() != 0)
                {
                    System.Console.WriteLine("testuser10.3 :" + editId);
                    foreach (var x in terms)
                    {
                        System.Console.WriteLine("testuser10.4 :" + x.ProvinceId);
                        var userprovincedata = new UserProvince
                        {
                            UserID = editId,
                            ProvinceId = x.ProvinceId
                        };
                        _context.UserProvinces.Add(userprovincedata);
                        _context.SaveChanges();
                    }
                }

                //// role ที่ไม่ไช้เขตตรวจ
                if (model.Role_id == 1 || model.Role_id == 2 || model.Role_id == 4 || model.Role_id == 5
                     || model.Role_id == 7 || model.Role_id == 9)
                {


                    var userregiondata = new UserRegion
                    {
                        UserID = editId,
                        RegionId = 1
                    };
                    System.Console.WriteLine("testuser11 :");
                    _context.UserRegions.Add(userregiondata);
                    _context.SaveChanges();


                    if (model.Role_id == 9)
                    {
                        foreach (var item in model.UserProvince)
                        {
                            var userprovincedata = new UserProvince
                            {
                                UserID = editId,
                                ProvinceId = item
                            };
                            System.Console.WriteLine("testuser : 12.1");
                            _context.UserProvinces.Add(userprovincedata);
                            _context.SaveChanges();
                        }
                    }
                    else
                    {
                        var userprovincedata = new UserProvince
                        {
                            UserID = editId,
                            ProvinceId = model.UserProvinceId
                        };
                        System.Console.WriteLine("testuser : 12.2");
                        _context.UserProvinces.Add(userprovincedata);
                        _context.SaveChanges();
                    }

                    System.Console.WriteLine("testuser13: ");
                }
            }



            _context.Entry(userdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            System.Console.WriteLine("testuser13.2 : " + userdata.UserName);


            if (model.Formprofile != 1) // 1 คือแก้ไขจากตัวuser เอง
            {
                var tresult = await _userManager.RemovePasswordAsync(userdata);
                await _userManager.AddPasswordAsync(userdata, passwordrandom);
            }

            //<!-- ปามมาไหม่ -->
            //var change = _userManager.GenerateChangeEmailTokenAsync(userdata, model.Email).Result;
            //await _userManager.ChangeEmailAsync(userdata, model.Email, change);
            //<!-- END ปามมาไหม่ -->

            System.Console.WriteLine("testuser14 : " + Username);
            return Ok(new { status = true, password = passwordrandom });
        }

        //ปาม2020
        //<!-- resetpassword -->
        [HttpPut("api/[controller]/resetpassword")]
        public async Task<IActionResult> resetpassword([FromForm] UserViewModel model)
        {

            System.Console.WriteLine("momo"+model.Id);
            var passwordrandom = RandomString(8);
            var userdata = _context.Users.Find(model.Id);
            userdata.Pw = passwordrandom;
            _context.Entry(userdata).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            //System.Console.WriteLine("momo" + id);
           // var passwordrandom = RandomString(8);
           // var userdata = _context.Users.Find(id);

            var tresult = await _userManager.RemovePasswordAsync(userdata);
            await _userManager.AddPasswordAsync(userdata, passwordrandom);

            return Ok(new { Id = model.Id });
        }
        //<!-- END resetpassword -->

        [Route("api/[controller]/{id}")]
        [HttpDelete]
        public void Delete(string id)
        {
            System.Console.WriteLine("userdelete : " + id);

            var userregiondata = _context.UserRegions.Where(m => m.UserID == id);
            _context.UserRegions.RemoveRange(userregiondata);
            _context.SaveChanges();

            var userprovincedata = _context.UserProvinces.Where(m => m.UserID == id);
            _context.UserProvinces.RemoveRange(userprovincedata);
            _context.SaveChanges();


            var userdata = _context.ApplicationUsers.Find(id);
            _context.ApplicationUsers.Remove(userdata);
            _context.SaveChanges();
        }

        [HttpGet("api/[controller]/[action]/{id}/{ministryId}")]
        public IEnumerable<ApplicationUser> getuserSameMinistry(long id, long ministryId)
        {
            var users = _context.Users
                .Include(s => s.UserRegion)
                .ThenInclude(r => r.Region)
                .Include(s => s.UserProvince)
                .ThenInclude(r => r.Province)
                .Include(s => s.Province)
                .Include(s => s.Ministries)
                .Include(x => x.Departments)
                .Include(x => x.ProvincialDepartments)
                .Where(m => m.Role_id == id && m.MinistryId == ministryId)
                .Where(m => m.Active == 1)
                .Where(m => m.Email != "admin@inspec.go.th").OrderByDescending(m => m.CreatedAt);

            return users;
        }

        [HttpGet("api/[controller]/[action]/{id}/{departmentId}")]
        public IEnumerable<ApplicationUser> getuserSameDepartment(long id, long departmentId)
        {
            var users = _context.Users
                .Include(s => s.UserRegion)
                .ThenInclude(r => r.Region)
                .Include(s => s.UserProvince)
                .ThenInclude(r => r.Province)
                .Include(s => s.Province)
                .Include(s => s.Ministries)
                .Include(x => x.Departments)
                .Include(x => x.ProvincialDepartments)
                .Where(m => m.Role_id == id && m.DepartmentId == departmentId)
                .Where(m => m.Active == 1)
                .Where(m => m.Email != "admin@inspec.go.th").OrderByDescending(m => m.CreatedAt);

            return users;
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> Create()
        {
            string result = string.Empty;
            // Console.WriteLine("gogogogogo");
            if (_context.Users.Count() == 0)
            {
                UserViewModel[] users = {
                      new UserViewModel {UserName ="admin@inspec.go.th", Email = "admin@inspec.go.th", Name ="Super Admin",MinistryId =1,DepartmentId=1,ProvincialDepartmentId=1, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "Super Admin ",Prefix = "นาย",Role_id =1,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "",PhoneNumber = "",Telegraphnumber = "",Housenumber = "",Rold = "",Alley = "", Postalcode = "",SideId = 1, Img = "user.png",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {1},UserProvince = new List<int>(){1} },
                      new UserViewModel {UserName ="inspect_Role2@inspec.go.th", Email = "inspect_Role2@inspec.go.th", Name ="Centraladmin",MinistryId =1,DepartmentId=1,ProvincialDepartmentId=1, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "Centraladmin ",Prefix = "นาย",Role_id =2,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "",PhoneNumber = "",Telegraphnumber = "",Housenumber = "",Rold = "",Alley = "", Postalcode = "",SideId = 1, Img = "user.png",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {1},UserProvince = new List<int>(){1} },
                      new UserViewModel {UserName ="inspect_Role3@inspec.go.th", Email = "inspect_Role3@inspec.go.th", Name ="Inspector",MinistryId =1,DepartmentId=1,ProvincialDepartmentId=1, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "Inspector ",Prefix = "นาย",Role_id =3,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "",PhoneNumber = "",Telegraphnumber = "",Housenumber = "",Rold = "",Alley = "", Postalcode = "",SideId = 1, Img = "user.png",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {1},UserProvince = new List<int>(){1} },
                      new UserViewModel {UserName ="inspect_Role4@inspec.go.th", Email = "inspect_Role4@inspec.go.th", Name ="Provincialgovernor",MinistryId =1,DepartmentId=1,ProvincialDepartmentId=1, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "Provincialgovernor ",Prefix = "นาย",Role_id =4,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "",PhoneNumber = "",Telegraphnumber = "",Housenumber = "",Rold = "",Alley = "", Postalcode = "",SideId = 1, Img = "user.png",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {1},UserProvince = new List<int>(){1} },
                      new UserViewModel {UserName ="inspect_Role5@inspec.go.th", Email = "inspect_Role5@inspec.go.th", Name ="Adminprovince",MinistryId =1,DepartmentId=1,ProvincialDepartmentId=1, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "Adminprovince ",Prefix = "นาย",Role_id =5,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "",PhoneNumber = "",Telegraphnumber = "",Housenumber = "",Rold = "",Alley = "", Postalcode = "",SideId = 1, Img = "user.png",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {1},UserProvince = new List<int>(){1} },
                      new UserViewModel {UserName ="inspect_Role6@inspec.go.th", Email = "inspect_Role6@inspec.go.th", Name ="InspectorMinistry",MinistryId =1,DepartmentId=1,ProvincialDepartmentId=1, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "InspectorMinistry ",Prefix = "นาย",Role_id =6,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "",PhoneNumber = "",Telegraphnumber = "",Housenumber = "",Rold = "",Alley = "", Postalcode = "",SideId = 1, Img = "user.png",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {1},UserProvince = new List<int>(){1} },
                      new UserViewModel {UserName ="inspect_Role7@inspec.go.th", Email = "inspect_Role7@inspec.go.th", Name ="publicsector",MinistryId =1,DepartmentId=1,ProvincialDepartmentId=1, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "publicsector ",Prefix = "นาย",Role_id =7,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "",PhoneNumber = "",Telegraphnumber = "",Housenumber = "",Rold = "",Alley = "", Postalcode = "",SideId = 1, Img = "user.png",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {1},UserProvince = new List<int>(){1} },
                      new UserViewModel {UserName ="inspect_Role8@inspec.go.th", Email = "inspect_Role8@inspec.go.th", Name ="president",MinistryId =1,DepartmentId=1,ProvincialDepartmentId=1, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "president ",Prefix = "นาย",Role_id =8,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "",PhoneNumber = "",Telegraphnumber = "",Housenumber = "",Rold = "",Alley = "", Postalcode = "",SideId = 1, Img = "user.png",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {1},UserProvince = new List<int>(){1} },
                      new UserViewModel {UserName ="inspect_Role9@inspec.go.th", Email = "inspect_Role9@inspec.go.th", Name ="Examination",MinistryId =1,DepartmentId=1,ProvincialDepartmentId=1, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "Examination ",Prefix = "นาย",Role_id =9,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "",PhoneNumber = "",Telegraphnumber = "",Housenumber = "",Rold = "",Alley = "", Postalcode = "",SideId = 1, Img = "user.png",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {1},UserProvince = new List<int>(){2} },
                      new UserViewModel {UserName ="inspect_Role10@inspec.go.th", Email = "inspect_Role10@inspec.go.th", Name ="InspectorDepartment",MinistryId =1,DepartmentId=1,ProvincialDepartmentId=1, DistrictId =1,ProvinceId =1,SubdistrictId =1,Position = "InspectorDepartment ",Prefix = "นาย",Role_id =10,Educational = "",Birthday =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null),Officephonenumber = "",PhoneNumber = "",Telegraphnumber = "",Housenumber = "",Rold = "",Alley = "", Postalcode = "",SideId = 1, Img = "user.png",Active =1,CreatedAt = DateTime.Now,Startdate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), Enddate =DateTime.ParseExact("1957/10/22 00:00:00", "yyyy/MM/dd HH:mm:ss", null), UserRegion = new List<int>() {1},UserProvince = new List<int>(){1} }
                };
                foreach (var item in users)
                {
                    var user = new ApplicationUser
                    {
                        UserName = item.UserName,
                        Email = item.Email,
                        DepartmentId = item.DepartmentId,
                        ProvincialDepartmentId = item.ProvincialDepartmentId,
                        DistrictId = item.DistrictId,
                        ProvinceId = item.ProvinceId,
                        SubdistrictId = item.SubdistrictId,
                        MinistryId = item.MinistryId,
                        Role_id = item.Role_id,
                        SideId = item.SideId,
                        Pw = "Admin@12345678",
                    };
                    //Console.WriteLine ("department1");
                    var success = await _userManager.CreateAsync(user, "Admin@12345678").ConfigureAwait(false);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
                    await _userManager.ConfirmEmailAsync(user, code).ConfigureAwait(false);


                    //user.DepartmentId = item.DepartmentId;
                    user.Position = item.Position;
                    user.Prefix = item.Prefix;
                    user.Name = item.Name;
                    user.Educational = item.Educational;
                    user.Birthday = item.Birthday;
                    user.Officephonenumber = item.Officephonenumber;
                    user.PhoneNumber = item.PhoneNumber;
                    user.Telegraphnumber = item.Telegraphnumber;
                    user.Housenumber = item.Housenumber;
                    user.Alley = item.Alley;
                    user.Postalcode = item.Postalcode;
                    user.Rold = item.Rold;
                    user.Img = item.Img;
                    user.CreatedAt = item.CreatedAt;
                    user.Startdate = item.Startdate;
                    user.Enddate = item.Enddate;
                    user.Active = 1;
                    user.FiscalYearId = 1;
                    user.Commandnumberdate = null;
                    user.Autocreateuser = 1; //20200823

                    _context.Entry(user).State = EntityState.Modified;
                    _context.SaveChanges();
                    Console.Write("userrun");
                    foreach (var item2 in item.UserRegion)
                    {
                        var userregiondata = new UserRegion
                        {
                            UserID = user.Id,
                            RegionId = item2
                        };
                        _context.UserRegions.Add(userregiondata);
                        _context.SaveChanges();
                    }

                    foreach (var item3 in item.UserProvince)
                    {
                        var userprovincedata = new UserProvince
                        {
                            UserID = user.Id,
                            ProvinceId = item3
                        };
                        _context.UserProvinces.Add(userprovincedata);
                        _context.SaveChanges();
                    }

                }
                return Ok("Success");
            }
            else
            {
                return Ok("Fail");
            }
        }

        [HttpGet("api/[controller]/province/{id}")]
        public IEnumerable<UserProvince> getprovince(string id)
        {

            var provinces = _context.UserProvinces
                .Include(m => m.Province)
                .Where(m => m.UserID == id)
                .ToList();

            return provinces;
        }
        [HttpGet("api/get_role/{id}")]
        public IActionResult test(string id)
        {
            var user = _userManager.Users.Where(m => m.Id == id)
                   .Include(m => m.Departments)
                   //.ThenInclude(m=>m.)
                   .FirstOrDefault();

            var proviceDepart = _context.ProvincialDepartment
            .Where(m => m.DepartmentId == user.DepartmentId).FirstOrDefault();
            var test = new { user, proviceDepart };
            return Ok(new { user, proviceDepart });
        }

        [HttpGet("api/provicedepart/{id}")]
        public IActionResult proviceDepart(string id)
        {
            var user = _userManager.Users.Where(m => m.Id == id)
                   .Include(m => m.Departments)
                   //.ThenInclude(m=>m.)
                   .FirstOrDefault();

            var proviceDepart = _context.ProvincialDepartment
            .Where(m => m.DepartmentId == user.DepartmentId).FirstOrDefault();
            return Ok(proviceDepart);
        }

        [HttpGet("api/[controller]/[action]/{id}")]
        public IActionResult getplancount(string id)
        {
            var users = _context.CentralPolicyUsers
                .Where(m => m.UserId == id)
                .Where(m => m.Status == "รอการตอบรับ")
                .ToArray();
            //var users2 = users.DistinctBy(m => m.InspectionPlanEventId);
            //.GroupBy(m => m.InspectionPlanEventId)

            return Ok(users);
        }

        // <!-- test excel -->
        [HttpGet("api/[controller]/[action]")]
        public IActionResult getexceluserrole8()
        {


            var users = _context.Users;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("VicePrimeMinister");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = "Username";
                foreach (var momo in users)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = momo.Email;
                    worksheet.Cell(currentRow, 2).Value = momo.UserName;
                }
                System.Console.WriteLine("momomo : " + "789");
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "VicePrimeMinister.xlsx");
                }
            }
        }
        // <!-- END test excel -->

    }

    internal class Momo
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }
}