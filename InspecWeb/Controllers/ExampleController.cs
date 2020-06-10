using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InspecWeb.ViewModel;

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        [HttpPost("example")]
        public IActionResult example([FromBody] DataExample data)
        {

            return Ok(new { data });
        }

    }
}