using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("ImportReports")]
    [Description("ตารางนำเข้ารายงาน")]
    public class ImportReport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("FiscalYear")]
        [Description("FK: ปีงบประมาณ")]
        public long FiscalYearId { get; set; }
        public virtual FiscalYear FiscalYear { get; set; }

        [ForeignKey("Region")]
        [Description("FK: เขตตรวจราขการ")]
        public long RegionId { get; set; }
        public virtual Region Region { get; set; }

        [ForeignKey("Province")]
        [Description("FK: จังหวัด")]
        public long ProvinceId { get; set; }
        public virtual Province Province { get; set; }

        //[ForeignKey("CentralPolicyEvent")]
        //[Description("FK: แผนการตรวจ")] public long centralPolicyEventId { get; set; }
        //public virtual CentralPolicyEvent CentralPolicyEvent { get; set; }

        public string CentralPolicyType { get; set; }
        public string ReportType { get; set; }
        public string InspectionRound { get; set; }
        public string MonitoringTopics { get; set; }
        public string DetailReport { get; set; }
        public string Suggestion { get; set; }
        public string Command { get; set; }

        [ForeignKey("User")]
        [Description("FK: User")]
        public string CreatedBy { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreateAt { get; set; }

        public string Status { get; set; }

        [ForeignKey("Commander")]
        [Description("FK: Commander")]
        public string SendCommander { get; set; }
        public virtual ApplicationUser Commander { get; set; }

        public ICollection<ImportReportGroup> ImportReportGroups { get; set; }
        public ICollection<ReportCommander> ReportCommanders { get; set; }
        public ICollection<ImportReportFile> ImportReportFiles { get; set; }
    }
}
