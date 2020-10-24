using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
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
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IMailService mailService;
        // private static ApplicationDbContext _context;
        public UtinityController(
            // ApplicationDbContext context,
            IMailService mailService,
            IServiceScopeFactory scopeFactory
        )
        {
            this.mailService = mailService;
            this.scopeFactory = scopeFactory;
            // _context = context;
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

                throw ex;
            }

        }

        [HttpPost("send2")]
        public async Task<IActionResult> SendMail2([FromForm] WelcomeRequest request)
        {
            try
            {
                var send = new WelcomeRequest
                {
                    ToEmail = "k12club@hotmail.com",
                    UserName = "ปาล์มทดสอบ"
                };
                await mailService.SendWelcomeEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        //ตัวอย่างการใช้ cronjob
        public void Process()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var menu = dbContext.Menu
             .Where(m => m.Role_id == 1).FirstOrDefault();

                Console.WriteLine("example dbcontext show first menu id " + menu.Id);
            }


            // return Ok(menu);
            // Console.WriteLine("ta kai yung hen log nee is saran yung mai dai tum cronjob 5555+ as " + DateTime.Now.ToString("F"));
        }

    }

}