using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace InspecWeb.ViewModel
{
    public class ExportReportViewModel
    {
        [JsonProperty(PropertyName = "typeId")]
        public string typeId { get; set; }
        [JsonProperty(PropertyName = "centralPolicyId")]
        public string centralPolicyId { get; set; }
        [JsonProperty(PropertyName = "userId")]
        public string userId { get; set; }
        [JsonProperty(PropertyName = "centralPolicyProvinceId")]
        public long centralPolicyProvinceId { get; set; }
        [JsonProperty(PropertyName = "reportId")]
        public long reportId { get; set; }
        [JsonProperty(PropertyName = "electronicBookId")]
        public long electronicBookId { get; set; }

        [JsonProperty(PropertyName = "subjectData")]
        public string[] subjectData { get; set; }

        public reportData[] reportData { get; set; }

        public reportData2[] reportData2 { get; set; }

        public printReport[] printReport { get; set; }

        public printReport2[] printReport2 { get; set; }

        public allReport[] allReport { get; set; }

        [JsonProperty(PropertyName = "reportType")]
        public string reportType { get; set; }

        [JsonProperty(PropertyName = "reportDepartment")]
        public string reportDepartment { get; set; }

        [JsonProperty(PropertyName = "reportRegion")]
        public string reportRegion { get; set; }

        [JsonProperty(PropertyName = "reportRegionId")]
        public long reportRegionId { get; set; }

        [JsonProperty(PropertyName = "reportProvince")]
        public string reportProvince { get; set; }

        [JsonProperty(PropertyName = "reportDate")]
        public DateTime reportDate { get; set; }

        [JsonProperty(PropertyName = "startDate")]
        public DateTime startDate { get; set; }

        [JsonProperty(PropertyName = "reportZone")]
        public string reportZone { get; set; }

        public long TrainingId { get; set; }

        public allReportRateLogin[] allReportRateLogin { get; set; }

        [JsonProperty(PropertyName = "trainingName")]
        public string trainingName { get; set; }

        [JsonProperty(PropertyName = "trainingGen")]
        public long trainingGen { get; set; }

        [JsonProperty(PropertyName = "trainingYear")]
        public long trainingYear { get; set; }

        public allReportTrainingRegister[] allReportTrainingRegister { get; set; }
    }

    public class reportData
    {
        //public string subject { get; set; }
        //public string department { get; set; }
        //public string detail { get; set; }
        //public string suggestion { get; set; }
        //public string problem { get; set; }
        //public string opinionPeople { get; set; }

        [JsonProperty(PropertyName = "subject")]
        public string subject { get; set; }

        [JsonProperty(PropertyName = "department")]
        public string department { get; set; }

        [JsonProperty(PropertyName = "detail")]
        public string detail { get; set; }

        [JsonProperty(PropertyName = "suggestion")]
        public string suggestion { get; set; }

        [JsonProperty(PropertyName = "problem")]
        public string problem { get; set; }

        [JsonProperty(PropertyName = "opinionPeople")]
        public string opinionPeople { get; set; }

        [JsonProperty(PropertyName = "reportId")]
        public long reportId { get; set; }
    }

    public class reportData2
    {
        [JsonProperty(PropertyName = "centralPolicy")]
        public string centralPolicy { get; set; }

        [JsonProperty(PropertyName = "department")]
        public string department { get; set; }

        [JsonProperty(PropertyName = "fiscalYear")]
        public string fiscalYear { get; set; }

        [JsonProperty(PropertyName = "region")]
        public string region { get; set; }

        [JsonProperty(PropertyName = "province")]
        public string province { get; set; }

        public tableData[] tableData { get; set; }
    }

    public class tableData
    {
        [JsonProperty(PropertyName = "subject")]
        public string subject { get; set; }
    }

    public class printReport
    {
        [JsonProperty(PropertyName = "inspectorName")]
        public string inspectorName { get; set; }
        [JsonProperty(PropertyName = "inspectorDescription")]
        public string inspectorDescription { get; set; }
        [JsonProperty(PropertyName = "inspectorSign")]
        public string inspectorSign { get; set; }
        [JsonProperty(PropertyName = "approve")]
        public string approve { get; set; }

    }

    public class printReport2
    {
        [JsonProperty(PropertyName = "departmentName")]
        public string departmentName { get; set; }
        [JsonProperty(PropertyName = "department")]
        public string department { get; set; }
        [JsonProperty(PropertyName = "departmentDescription")]
        public string departmentDescription { get; set; }
        [JsonProperty(PropertyName = "departmentSign")]
        public string departmentSign { get; set; }
        [JsonProperty(PropertyName = "departmentDate")]
        public DateTime departmentDate { get; set; }
    }

    public class allReport
    {
        [JsonProperty(PropertyName = "dateReport")]
        public DateTime dateReport { get; set; }

        [JsonProperty(PropertyName = "subject")]
        public string subject { get; set; }

        [JsonProperty(PropertyName = "createBy")]
        public string createBy { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string status { get; set; }

        [JsonProperty(PropertyName = "command")]
        public string command { get; set; }

        [JsonProperty(PropertyName = "dateCommand")]
        public DateTime dateCommand { get; set; }

        [JsonProperty(PropertyName = "provinceReport")]
        public string provinceReport { get; set; }
    }

    public class allReportRateLogin
    {
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

        [JsonProperty(PropertyName = "position")]
        public string position { get; set; }

        [JsonProperty(PropertyName = "department")]
        public string department { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string phone { get; set; }

        [JsonProperty(PropertyName = "count")]
        public long count { get; set; }

        [JsonProperty(PropertyName = "countCourse")]
        public long countCourse { get; set; }

        [JsonProperty(PropertyName = "rateCourse")]
        public float rateCourse { get; set; }
    }

    public class allReportTrainingRegister
    {
        [JsonProperty(PropertyName = "generation")]
        public string generation { get; set; }

        [JsonProperty(PropertyName = "year")]
        public string year { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

        [JsonProperty(PropertyName = "detail")]
        public string detail { get; set; }

        [JsonProperty(PropertyName = "start")]
        public DateTime start { get; set; }

        [JsonProperty(PropertyName = "end")]
        public DateTime end { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string location { get; set; }

        [JsonProperty(PropertyName = "count")]
        public long count { get; set; }

        [JsonProperty(PropertyName = "approveCount")]
        public long approveCount { get; set; }
    }

    public class testExport
    {
        [JsonProperty(PropertyName = "reportType")]
        public string reportType { get; set; }

        [JsonProperty(PropertyName = "reportRegion")]
        public string reportRegion { get; set; }

        public allReport[] allReport { get; set; }
    }
}
