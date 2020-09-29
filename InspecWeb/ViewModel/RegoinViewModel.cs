using System.Collections.Generic;

namespace InspecWeb.ViewModel
{
    public class RegoinViewModel
    {
        public class RegoinData
        {
            public string name { get; set; }
            public Provinces[] provinces { get; set; }
        }

        public class Provinces
        {
            public string name { get; set; }
        }

        public class Data
        {
            public List<RegoinData> data { get; set; }
        }


    }
}
