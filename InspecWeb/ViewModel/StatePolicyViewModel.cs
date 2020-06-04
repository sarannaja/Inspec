using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Collections;

namespace InspecWeb.ViewModel
{
    public class StatePolicyViewModel
    {
        public string GangId { get; set; }
        
        public string Name { get; set; }
        
        public string No { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public string RoundNo { get; set; }

        public DateTime Date { get; set; }

        public List<IFormFile> files { get; set; }

    }
}
