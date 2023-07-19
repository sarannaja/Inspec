using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspecWeb.Models
{
    [Table("OldReportFiles")]
    [Description("ตารางไฟล์รายงานย้อนหลัง")]
    public class OldReportFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("OldReport")]
        [Description("FK: รายงานย้อนหลัง")]
        public long OldReportId { get; set; }

        public virtual OldReport OldReport { get; set; }

        [Required]
        [Description("ชื่อไฟล์")]
        public string Name { get; set; }

        [Description("ประเภท")]
        public string Type { get; set; }

        [Description("คำอธิบายรูปภาพ")]
        public string Description { get; set; }
    }
}
