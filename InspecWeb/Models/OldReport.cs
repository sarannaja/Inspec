using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("OldReports")]
    [Description("ตารางรายงานย้อนหลัง")]
    public class OldReport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("User")]
        [Description("FK: User")]
        public string CreatedBy { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Description("ปีงบประมาณ")]
        public string Year { get; set; }
        //public ICollection<CentralPolicyUser> CentralPolicyUsers { get; set; }

        [Description("ประเภทแผนการตรวจ")]
        public string CentralPolicyType { get; set; }


        [Description("เรื่อง")]
        public string Name { get; set; }

        
        [Description("ประเภทรายงาน")]
        public string ReportType { get; set; }

        public ICollection<OldReportFile> OldReportFiles { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        [Description("รอบการตรวจ")]
        public string Round { get; set; }
    }
}
