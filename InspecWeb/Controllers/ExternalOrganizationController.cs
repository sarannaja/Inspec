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
        [HttpGet]
        public async Task<IActionResult> OnGet()
        {
            List<Ggc> model = null;
            var client = new HttpClient();
            var task = client.GetAsync("http://localhost:3000/ggcservice")
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


    }
}