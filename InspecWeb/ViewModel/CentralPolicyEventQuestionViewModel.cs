using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace InspecWeb.ViewModel
{
    public class CentralPolicyEventQuestionViewModel
    {
       
        public long cenproid { get; set; }

        public long planid { get; set; }

        public string question { get; set; }

        public DateTime notificationdate { get; set; }

        public DateTime deadlinedate { get; set; }

    }
}
