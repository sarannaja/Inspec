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

        [Description("ไฟล์เอกสาร Word")]
        public string FileWord { get; set; }

        [Description("ไฟล์เอกสาร Excel")]
        public string FileExcel { get; set; }

        [Description("คนอัพโหลด")]
        public string CreateBy { get; set; }

        [Description("คำสั่งการ")]
        public string Command { get; set; }

        [Description("คนสั่งการ")]
        public string Commander { get; set; }
    }
}
