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

        [ForeignKey("TrainingPhase")]
        [Description("FK: ช่วงหลักสูตรอบรม")]
        public long TrainingPhaseId { get; set; }

        public virtual TrainingProgramFile TrainingProgramFiles { get; set; }
        public virtual TrainingPhase TrainingPhase { get; set; }
        public virtual TrainingProgramLoginQRCode TrainingProgramLoginQRCodes { get; set; }

        [Description("วันที่กำหนดการ")]
        [DataType(DataType.Date)]
        public DateTime ProgramDate { get; set; }

        [Description("เวลาเริ่มกำหนดการ")]
        public string MinuteStartDate { get; set; }

        [Description("เวลาสิ้นสุดกำหนดการ")]
        public string MinuteEndDate { get; set; }

        [ForeignKey("TrainingProgramType")]
        [Description("ประเภท")]
        public long ProgramType { get; set; }
        public virtual TrainingProgramType TrainingProgramTypes { get; set; }

        [Description("หัวเรื่อง")]
        public string ProgramTopic { get; set; }

        [Description("กิจกรรม")]
        public string ProgramDetail { get; set; }

        [Description("สถานที่")]
        public string ProgramLocation { get; set; }

        [Description("การแต่งกาย")]
        public string ProgramToDress { get; set; }

        [Description("วันที่สร้าง")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        public ICollection<TrainingProgramLecturer> TrainingProgramLecturers { get; set; }
        public ICollection<TrainingProgramFile> TrainingProgramFiles { get; set; }

    }
}