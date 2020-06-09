using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    [Table("ReportCommanders")]
    [Description("ตารางไฟล์สมุดตรวจ")]
    public class ReportCommander
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [Required]
        [Description("ชื่อประเด็น")]
        public string Subject { get; set; }

        [Description("ประเภทการออกรายงาน")]
        public string TypeExport { get; set; }

        [Description("ประเภทรายงาน")]
        public string TypeReport { get; set; }

        [Description("ชื่อไฟล์เอกสาร")]
        public string FileName { get; set; }
    }
}
