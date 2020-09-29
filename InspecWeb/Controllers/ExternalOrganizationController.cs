using ClosedXML.Excel; //excel
using CorePush.Apple;
using CorePush.Google;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using InspecWeb.ViewModel.Otps;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO; //excel
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xceed.Document.NET;
using Xceed.Words.NET;
using static InspecWeb.ViewModel.ExternalOtpsViewModel;

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalOrganizationController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private static UserManager<ApplicationUser> _userManager;
        private static ApplicationDbContext _context;
        public static IWebHostEnvironment _environment;
        public ExternalOrganizationController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IHttpClientFactory clientFactory,
            IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _clientFactory = clientFactory;
            _environment = environment;
        }

        // GET api/values/5
        [HttpGet("gcc")]
        public IActionResult OnGet()
        {
            List<Ggc> model = null;
            var client = new HttpClient();
            var task = client.GetAsync("http://203.113.14.20:3000/ggcservice")
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<List<Ggc>>(jsonString.Result);

                });

            task.Wait();

            return Ok(model);

        }

        // GET api/values/5
        [HttpGet("otps/regions")]
        public IActionResult OnGetOtpsRegions()
        {
            List<OtpsRegions> model = null;
            var client = new HttpClient();
            var task = client.GetAsync("http://203.113.14.20:3000/testservice/otps/regions")
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<List<OtpsRegions>>(jsonString.Result);
                });
            task.Wait();
            return Ok(model);

        }
        // GET api/values/5
        [HttpGet("otps/regions2")]
        public IActionResult OnGetRegionOtps()
        {
            List<RegionOtps> model = null;
            var client = new HttpClient();
            var task = client.GetAsync("https://api.otps.go.th/api/Regions")
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<List<RegionOtps>>(jsonString.Result);
                });
            task.Wait();
            return Ok(model);

        }

        // GET api/values/5
        [HttpGet("otps/provinces")]
        public IActionResult OnGetOtpsProvinces()
        {
            List<OtpsProvinces> model = null;
            var client = new HttpClient();
            var task = client.GetAsync("https://api.otps.go.th/api/Provinces")
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<List<OtpsProvinces>>(jsonString.Result);
                });
            task.Wait();
            return Ok(model);

        }

        [HttpGet("otps/provinces/{Id}")]
        public IActionResult OnGetOtpsProvince(int Id)
        {
            OtpsProvinceFiscalYearsList model = null;
            var client = new HttpClient();
            var task = client.GetAsync("https://api.otps.go.th/api/Provinces/" + Id)
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;

                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<OtpsProvinceFiscalYearsList>(jsonString.Result);
                });
            task.Wait();
            return Ok(model);

        }

        // GET api/values/5
        //โครงการที่ยังไม่เสร็จ บลา บลา
        [HttpGet("otps/ministers")]
        public IActionResult OnGetOtpsMinisters()
        {
            List<OtpsMinisters> model = null;
            var client = new HttpClient();
            var task = client.GetAsync("https://api.otps.go.th/api/Ministers")
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<List<OtpsMinisters>>(jsonString.Result);
                });
            task.Wait();
            return Ok(model);

        }

        [HttpGet("otps/cabinets")]
        public IActionResult OnGetOtpsCabinets()
        {
            List<Cabinets> model = null;
            var client = new HttpClient();
            var task = client.GetAsync("https://api.otps.go.th/api/Cabinets")
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<List<Cabinets>>(jsonString.Result);
                });
            task.Wait();
            return Ok(model);

        }

        [HttpGet("gcc-opm/{provinceId}/{representId}")]
        public IActionResult OnGetGccOpm(int provinceId, int representId)
        {
            List<GgcService> model = null;
            var client = new HttpClient();
            var task = client.GetAsync("http://203.113.14.20:3000/ggcservice/withimage/" + provinceId + '/' + representId)
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<List<GgcService>>(jsonString.Result);
                });
            task.Wait();
            return Ok(model);

        }

        [HttpGet("gcc/provinces")]
        public IActionResult OnGetGccProvice()
        {
            List<GgcProvince> model = null;
            var client = new HttpClient();
            var task = client.GetAsync("http://203.113.14.20:3000/ggcservice/getprovince")
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<List<GgcProvince>>(jsonString.Result);
                });
            task.Wait();
            return Ok(model);

        }

        [HttpGet("gcc/wara")]
        public IActionResult OnGetGccWara()
        {
            List<GgcWara> model = null;
            var client = new HttpClient();
            var task = client.GetAsync("http://203.113.14.20:3000/ggcservice/wara")
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<List<GgcWara>>(jsonString.Result);
                });
            task.Wait();
            return Ok(model);

        }

        [HttpGet("opm-1111")]
        public IActionResult OnGetOpm()
        {
            List<Opm1111> model = null;
            var client = new HttpClient();
            var task = client.GetAsync("http://203.113.14.20:3000/testservice2/opm")
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<List<Opm1111>>(jsonString.Result);
                });
            task.Wait();
            return Ok(model);

        }

        // GET api/values/5
        [HttpGet("otps/region/{id}")]
        public IActionResult OnGetRegionOtps(int id)
        {
            NewRegion model = null;
            var client = new HttpClient();
            var task = client.GetAsync("https://api.otps.go.th/api/Regions/" + id)
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<NewRegion>(jsonString.Result);
                });
            task.Wait();
            return Ok(model);
        }

        [HttpGet("otps/provinces2")]
        public IActionResult OnGetProvincesOtps()
        {
            List<OtpsProvince> model = null;
            var client = new HttpClient();
            var task = client.GetAsync("https://api.otps.go.th/api/Provinces")
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<List<OtpsProvince>>(jsonString.Result);
                    // model = JsonConvert.DeserializeObject<List<QuickType.Province>>(jsonString.Result);
                });
            task.Wait();
            return Ok(model);
        }

        [HttpGet("otps/provinces2/{Id}")]
        public IActionResult OnGetOtpsProvince2(int Id)
        {
            OtpsProvinceFiscalYearsList model = null;
            var client = new HttpClient();
            var task = client.GetAsync("https://api.otps.go.th/api/Provinces/ " + Id)
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;

                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<OtpsProvinceFiscalYearsList>(jsonString.Result);
                });
            task.Wait();
            return Ok(model);

        }

        [HttpGet("opm/userprovince/{id}")]
        public IActionResult OngetOpmUser(string id)
        {
            var user = _userManager.Users.Where(m => m.Id == id)
                //    .Include(m => m.Departments)
                //.ThenInclude(m=>m.)
                .FirstOrDefault();

            var userProvince = _context.UserProvinces
                .Where(user => user.UserID == id)
                // .Where(p => p.ProvinceId == 1)
                // .Select(s => s.ProvinceId)
                .ToList();
            List<OpmUserProvince> opmUserProvincesAll = new List<OpmUserProvince>();
            foreach (var item in userProvince)
            {
                var Province = _context.Provinces
                    .Where(w => w.Id == item.ProvinceId)
                    .First();
                ProvinceKeyword ProvinceS = SearchProvince(Province.Name);
                Console.WriteLine(ProvinceS.Id);
                var prov = new { province = Province.Name };
                List<OpmUserProvince> opmUserProvinces = OpmOpmUserProvince(ProvinceS.Id);
                for (int i = 0; i < opmUserProvinces.Count; i++)
                {
                    opmUserProvincesAll.Add(opmUserProvinces[i]);
                }
            }
            OpmUserProvince[] terms = opmUserProvincesAll.ToArray();

            return Ok(terms);

        }

        public ProvinceKeyword SearchProvince(string provinceName)
        {
            ProvinceKeyword ProvinceS = null;
            var client = new HttpClient();
            var task = client.GetAsync("http://203.113.14.20:3000/testservice/opm/province/key/ " + provinceName)
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;

                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    ProvinceS = JsonConvert.DeserializeObject<ProvinceKeyword>(jsonString.Result);
                });
            task.Wait();
            return ProvinceS;
        }

        public List<OpmUserProvince> OpmOpmUserProvince(long ID)
        {
            List<OpmUserProvince> OpmUserProvince = null;
            var client = new HttpClient();
            var task = client.GetAsync("http://203.113.14.20:3000/testservice/opm/province/" + ID)
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;

                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    OpmUserProvince = JsonConvert.DeserializeObject<List<OpmUserProvince>>(jsonString.Result);
                });
            task.Wait();
            return OpmUserProvince;
        }

        [HttpGet("opm/case/{id}")]
        public OpmCase OpmCase(string id)
        {
            OpmCase OpmUserProvince = null;
            var client = new HttpClient();
            var task = client.GetAsync("http://203.113.14.20:3000/testservice/opm/case/" + id)
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;

                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    OpmUserProvince = JsonConvert.DeserializeObject<OpmCase>(jsonString.Result);
                });
            task.Wait();
            return OpmUserProvince;
        }

        [HttpGet("provinceall")]
        public IActionResult GetAll()
        {
            //สร้ าง array dotet core
            List<ProvinceRegion> termsList = new List<ProvinceRegion>();
            var provinces = _context.Provinces.ToList();
            for (int i = 0; i < provinces.Count(); i++)
            {
                long region = provinces[i].ProvincesGroupId;
                var test = _context.ProvincesGroups.Find(region);
                termsList.Add(new ProvinceRegion { name = provinces[i].Name, region = test.Name });
            }

            // You can convert it back to an array if you would like to
            ProvinceRegion[] terms = termsList.ToArray();
            return Ok(terms);
        }

        [HttpGet("mobilenotification/{userId}")]
        public IActionResult SendNotification(string userId)
        {
            // IActionResult OpmUserProvince = null;
            var mobileObj = _context.UserTokenMobiles
                            .Where(w => w.UserID == userId)
                            .ToArray();
            foreach (var item in mobileObj)
            {
                var client = new HttpClient();
                var task = client.GetAsync("http://203.113.14.20:3000/inspecnotification/" + item.DeviceType + "/" + item.Token)
                    .ContinueWith((taskwithresponse) =>
                    {
                        var response = taskwithresponse.Result;

                        var jsonString = response.Content.ReadAsStringAsync();
                        jsonString.Wait();
                        // OpmUserProvince = JsonConvert.DeserializeObject<OpmCase>(jsonString.Result);
                    });
                task.Wait();
            }

            return Ok(mobileObj);
        }
        [HttpGet("mobilenotification2/{userId}/{message}")]
        public async Task<IActionResult> SendNotificationMobile(string userId, string message)
        {
            // IActionResult OpmUserProvince = null;
            var mobileObj = _context.UserTokenMobiles
                            .Where(w => w.UserID == userId)
                            .ToArray();

            foreach (var item in mobileObj)
            {
                var p8privateKey = "MIGTAgEAMBMGByqGSM49AgEGCCqGSM49AwEHBHkwdwIBAQQgpaXr/CZEsUSHeky8aQQ6teO4cDSsLwVq3j7PWdpr6wOgCgYIKoZIzj0DAQehRANCAAQe9meUPmcbinwleRVTxlolByUcfJjX9uxhYie57KxZJZZEfYrM4/U/IAcDu3EiWWBoKYmsVtDMTBGbFTfVCUvo";
                if (item.DeviceType == "ios")
                {
                    using (var apn = new ApnSender(p8privateKey, "7D37LMN3C4", "RVLQ6T4Y33", "inspec.opm", ApnServerType.Production))
                    {
                        // await apn.SendAsync(item.Token,message );
                        await apn.SendAsync(new { alert = message }, item.Token);
                    }

                }
                else
                {
                    using (var fcm = new FcmSender("AAAAzLUSLHk:APA91bEPP7_VRyQf7nFxKPTpZ6BYZ_yk7A0sXEWTOo--4i8kykZpmZnxzneKB5jdkRj8hJIPGu7Nfsvtu81YM2bZ3zFGOS8oTV_QGPWMcuUimytm8GOOjA9OT6_4nxLLQ1oPZOEep8Jb", "879211195513"))
                    {
                        await fcm.SendAsync(item.Token, message);
                    }
                }

            }

            return Ok(mobileObj);
        }

        // <!-- excel คณะรัฐบาล-->
        [HttpGet("excelOtpsMinisters")]
        public IActionResult excelOtpsMinisters()
        {

            List<OtpsMinisters> model = null;
            var client = new HttpClient();
            var task = client.GetAsync("https://api.otps.go.th/api/Ministers")
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<List<OtpsMinisters>>(jsonString.Result);
                });
            task.Wait();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("ข้อมูลรายชื่อคณะรัฐมนตรี");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "รายชื่อคณะรัฐมนตรี";
                worksheet.Cell(currentRow, 2).Value = "ตำแหน่ง";
                foreach (var data in model)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = data.Name;
                    worksheet.Cell(currentRow, 2).Value = data.Position;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "OtpsMinisters.xlsx");
                }
            }
        }
        // <!-- END excel คณะรัฐบาล-->

        [HttpGet("worddistrict")]
        public IActionResult worddistrict()
        {

            List<ExternalOrganizationNew> model = new List<ExternalOrganizationNew>();
            var client = new HttpClient();
            var task = client.GetAsync("https://api.otps.go.th/api/Ministers")
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<List<ExternalOrganizationNew>>(jsonString.Result);
                });
            task.Wait();



            System.Console.WriteLine("1 : ");

            if (!Directory.Exists(_environment.WebRootPath + "//reportOtpsMinisters//")) //ถ้ามีไฟล์อยู่แล้ว
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//reportOtpsMinisters//"); //สร้าง Folder reportexecutive ใน wwwroot
            }

            var filePath = _environment.WebRootPath + "/reportOtpsMinisters/"; // เก็บไฟล์ logo 
            var filename = "OtpsMinisters" + ".docx"; // ชื่อไฟล์
            var createfile = filePath + filename; //
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("2");

            using (DocX document = DocX.Create(createfile)) //สร้าง
            {


                System.Console.WriteLine("3");

                // Add a title
                document.InsertParagraph("ข้อมูลรายชื่อคณะรัฐมนตรี").FontSize(16d)
                    .SpacingBefore(15d)
                    .SpacingAfter(15d)
                    .Bold() //ตัวหนา
                    .Alignment = Alignment.center;



                int dataCount = 0;
                dataCount = model.Count; //เอาที่ select มาใช้
                dataCount += 1;
                System.Console.WriteLine("Data Count: " + dataCount);
                // Add a table in a document of 1 row and 3 columns.
                var columnWidths = new float[] { 100f, 300f, 300f };
                var t = document.InsertTable(dataCount, columnWidths.Length);

                System.Console.WriteLine("4");

                // Set the table's column width and background 
                t.SetWidths(columnWidths);
                t.AutoFit = AutoFit.Contents;

                var row = t.Rows.First();

                row.Cells[0].Paragraphs.First().Append("ลำดับที่");
                row.Cells[1].Paragraphs.First().Append("รายชื่อคณะรัฐมนตรี");
                row.Cells[2].Paragraphs.First().Append("ตำแหน่ง");


                // Add rows in the table.
                int j = 0;
                for (int i = 0; i < model.Count; i++)
                {
                    j += 1;

                    System.Console.WriteLine("5: " + j);
                    t.Rows[j].Cells[0].Paragraphs[0].Append(j.ToString());
                    t.Rows[j].Cells[1].Paragraphs[0].Append(model[i].Name.ToString());
                    t.Rows[j].Cells[2].Paragraphs[0].Append(model[i].Position.ToString());


                }

                // Set a blank border for the table's top/bottom borders.
                var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                //t.SetBorder(TableBorderType.Bottom, blankBorder);
                //t.SetBorder(TableBorderType.Top, blankBorder);

                System.Console.WriteLine("6");
                document.Save(); //save เอกสาร
                Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");

                return Ok(new { data = filename });
            }
        }

        //<!-- ระบบพี่คัง ปามมาทำ -->
        [HttpGet("wordotps")]
        public IActionResult wordotps()
        {
            //before List<ExternalOrganizationNew> model = null;
            List<ExternalOrganizationNew> model = new List<ExternalOrganizationNew>();
            var client = new HttpClient();
            var task = client.GetAsync("https://api.otps.go.th/api/Ministers")
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    model = JsonConvert.DeserializeObject<List<ExternalOrganizationNew>>(jsonString.Result);
                });
            task.Wait();



            System.Console.WriteLine("1 : ");

            if (!Directory.Exists(_environment.WebRootPath + "//reportOtpsMinisters//")) //ถ้ามีไฟล์อยู่แล้ว
            {
                Directory.CreateDirectory(_environment.WebRootPath + "//reportOtpsMinisters//"); //สร้าง Folder reportexecutive ใน wwwroot
            }

            var filePath = _environment.WebRootPath + "/reportOtpsMinisters/"; // เก็บไฟล์ logo 
            var filename = "OtpsMinisters" + ".docx"; // ชื่อไฟล์
            var createfile = filePath + filename; //
            var myImageFullPath = filePath + "logo01.png";

            System.Console.WriteLine("2");

            using (DocX document = DocX.Create(createfile)) //สร้าง
            {


                System.Console.WriteLine("3");

                // Add a title
                document.InsertParagraph("ข้อมูลรายชื่อคณะรัฐมนตรี").FontSize(16d)
                    .SpacingBefore(15d)
                    .SpacingAfter(15d)
                    .Bold() //ตัวหนา
                    .Alignment = Alignment.center;


                int dataCount = 0;
                dataCount = model.Count; //เอาที่ select มาใช้
                dataCount += 1;
                System.Console.WriteLine("Data Count: " + dataCount);
                // Add a table in a document of 1 row and 3 columns.
                var columnWidths = new float[] { 100f, 300f, 300f };
                var t = document.InsertTable(dataCount, columnWidths.Length);

                System.Console.WriteLine("4");

                // Set the table's column width and background 
                t.SetWidths(columnWidths);
                t.AutoFit = AutoFit.Contents;
                var row = t.Rows.First();

                row.Cells[0].Paragraphs.First().Append("ลำดับที่");
                row.Cells[1].Paragraphs.First().Append("รายชื่อคณะรัฐมนตรี");
                row.Cells[2].Paragraphs.First().Append("ตำแหน่ง");


                // Add rows in the table.
                int j = 0;
                for (int i = 0; i < model.Count; i++)
                {
                    j += 1;

                    System.Console.WriteLine("5: " + j);
                    t.Rows[j].Cells[0].Paragraphs[0].Append(j.ToString());
                    t.Rows[j].Cells[1].Paragraphs[0].Append(model[i].Name.ToString());
                    t.Rows[j].Cells[2].Paragraphs[0].Append(model[i].Position.ToString());


                }

                // Set a blank border for the table's top/bottom borders.
                var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                //t.SetBorder(TableBorderType.Bottom, blankBorder);
                //t.SetBorder(TableBorderType.Top, blankBorder);

                System.Console.WriteLine("6");
                document.Save(); //save เอกสาร
                Console.WriteLine("\tCreated: InsertHorizontalLine.docx\n");

                return Ok(new { data = filename });
            }
        }



    }
}

public class GoogleNotification
{
    public class DataPayload
    {
        // Add your custom properties as needed
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    [JsonProperty("priority")]
    public string Priority { get; set; } = "high";

    [JsonProperty("data")]
    public DataPayload Data { get; set; }
}

public class AppleNotification
{
    public class ApsPayload
    {
        [JsonProperty("alert")]
        public string AlertBody { get; set; }
    }

    // Your custom properties as needed

    [JsonProperty("aps")]
    public ApsPayload Aps { get; set; }
}