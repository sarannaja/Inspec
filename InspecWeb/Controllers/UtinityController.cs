using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Services;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtinityController : ControllerBase
    {
        private readonly IMailService mailService;
        public UtinityController(
            IMailService mailService
        )
        {
            this.mailService = mailService;
        }

        // POST: ตัวอย่างการส่ง email 
        [HttpPost("send")]
        public async Task<IActionResult> SendMail([FromForm] MailRequest request)
        {
            try
            {
                var send = new MailRequest
                {
                    ToEmail = "k12club@hotmail.com",
                    Subject = "Test Subject",
                    Body = "Test Body"
                };
                await mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        //cronjob เดี๋ยยวค่อยอธิบาย
        public void Process()
        {
            Console.WriteLine("ta kai yung hen log nee is saran yung mai dai tum cronjob 5555+ as " + DateTime.Now.ToString("F"));
        }

    }

}