using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    /// <summary>
    /// เนื้อหาของสื่อสิ่งพิมพ์
    /// </summary>
    [Table("TrainingSummaryReports")]
    [Description("ตารางสรุปผลการฝึกอบรมทั้งหลักสูตร")]
    public class TrainingSummaryReport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("TrainingPhase")]
        [Description("FK: หลักสูตรอบรมช่วง")]
        public long TrainingId { get; set; }
        public virtual Training Training { get; set; }


        [Required]
        [Description("คำอธิบายรายงาน")]
        public string Detail { get; set; }

        [Required]
        [Description("รูปภาพ")]
        public string File { get; set; }


        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

    }
}