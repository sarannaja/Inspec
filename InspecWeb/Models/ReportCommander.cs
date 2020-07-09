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

        [ForeignKey("ImportReport")]
        [Description("FK: ImportReport")]
        public long ImportReportId { get; set; }
        public virtual ImportReport ImportReport { get; set; }

        public string Status { get; set; }

        [Description("คำสั่งการ")]
        public string Command { get; set; }

        [ForeignKey("User")]
        [Description("FK: User")]
        public string UserCommanderId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreateAt { get; set; }
    }
}
