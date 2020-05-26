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
    [Table("TrainingPrograms")]
    [Description("ตารางกำหนดการหลักสูตรการอบรม")]
    public class TrainingProgram
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("Training")]
        [Description("FK: หลักสูตรอบรม")]
        public long TrainingId { get; set; }
        public virtual Training Training { get; set; }

        [Required]
        [Description("ชื่อวิทยากร")]
        public string LecturerName { get; set; }

        [Required]
        [Description("กิจกรรม")]
        public string ProgramDetail { get; set; }

        [Description("วันที่กำหนดการ")]
        [DataType(DataType.Date)]
        public DateTime ProgramDate { get; set; }

        [Description("เวลาเริ่มกำหนดการ")]
        [DataType(DataType.Date)]
        public DateTime MinuteStartDate { get; set; }

        [Description("เวลาสิ้นสุดกำหนดการ")]
        [DataType(DataType.Date)]
        public DateTime MinuteEndDate { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

    }
}