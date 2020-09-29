using System;

namespace InspecWeb.ViewModel
{
    public class ExportCalendarViewModel
    {
        //public string type { get; set; }

        public long regionId { get; set; }
        public long provinceId { get; set; }
        public long departmentId { get; set; }
        public string peopleId { get; set; }
        public DateTime? date { get; set; }
        public reportCalendarData[] reportCalendarData { get; set; }
    }

    public class reportCalendarData
    {
        public long index { get; set; }
        public string title { get; set; }
        public string startDate { get; set; }
        public string status { get; set; }
        public string namecreatedby { get; set; }
        public string phonenumbercreatedby { get; set; }
        public string nameinvited { get; set; }
        public string province { get; set; }
    }

}
