using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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


        public ExternalOrganizationController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IHttpClientFactory clientFactory)
        {
            _context = context;
            _userManager = userManager;
            _clientFactory = clientFactory;
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
                var prov = new {province = Province.Name};
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




    }
}