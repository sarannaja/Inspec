using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalOrganizationController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        public ExternalOrganizationController(IHttpClientFactory clientFactory)
        {
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

    }
}