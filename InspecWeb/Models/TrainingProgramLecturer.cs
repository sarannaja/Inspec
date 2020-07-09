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
    [Table("TrainingProgramLecturers")]
    [Description("ตารางกำหนดการหลักสูตรการอบรม")]
    public class TrainingProgramLecturer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("TrainingProgram")]
        [Description("FK:")]
        public long TrainingProgramId { get; set; }
        public virtual TrainingProgram TrainingProgram { get; set; }

        [ForeignKey("TrainingLecturer")]
        [Description("FK: วิทยากร")]
        public long TrainingLecturerId { get; set; }
        public virtual TrainingLecturer TrainingLecturer { get; set; }
    }
}