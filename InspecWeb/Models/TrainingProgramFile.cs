using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InspecWeb.Models
{
    [Table("TrainingProgramFiles")]
    [Description("ตารางไฟล์บันทึกการดำเนินการตามข้อสั่งการ")]
    public class TrainingProgramFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("TrainingProgram")]
        [Description("FK: ตารางช่วงหลักสูตรการอบรม")]
        public long TrainingProgramId { get; set; }

        public virtual TrainingProgram TrainingProgram { get; set; }

        [Required]
        [Description("ชื่อไฟล์")]
        public string Name { get; set; }

    }
}
