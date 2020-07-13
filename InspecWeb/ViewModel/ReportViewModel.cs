using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspecWeb.ViewModel
{
    public class ReportViewModel
    { 

    }

    public class reportperformance
    {
        public long provinceid { get; set; }

        public long centralpolicyid { get; set; }

        public long subjectid { get; set; }

        public string reporttype { get; set; }
    }

    public class reportsuggestions
    {
        public long provinceid { get; set; }

        public long centralpolicyid { get; set; }

        public long subjectid { get; set; }

        public string reporttype { get; set; }
    }
}
