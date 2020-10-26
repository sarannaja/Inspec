using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace InspecWeb.ViewModel
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        
        public List<IFormFile> Attachments { get; set; }
    }

     public class WelcomeRequest
    {
        public string ToEmail { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }

        
    }
}
