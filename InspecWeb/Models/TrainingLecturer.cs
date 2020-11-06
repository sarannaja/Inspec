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
    [Table("TrainingLecturers")]
    [Description("ตารางกำหนดการหลักสูตรการอบรม")]
    public class TrainingLecturer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("PK")]
        public long Id { get; set; }

        [ForeignKey("TrainingLecturerType")]
        [Description("ประเภทวิทยากร")]
        public long LecturerType { get; set; }
        public virtual TrainingLecturerType TrainingLecturerTypes { get; set; }

        [Required]
        [Description("ชื่อวิทยากร")]
        public string LecturerName { get; set; }

        [Required]
        [Description("หมายเลขโทรศัพท์")]
        public string Phone { get; set; }

        [Description("รูปภาพ")]
        public string ImageProfile { get; set; }

        [Description("อีเมล์")]
        public string Email { get; set; }

        [Description("ประวัติการศึกษา")]
        public string Education { get; set; }

        [Description("ประวัติการทำงาน")]
        public string WorkHistory { get; set; }

        [Description("ประสบการณ์บรรยาย")]
        public string Experience { get; set; }

        [Description("ข้อมูลเพิ่มเติม")]
        public string DetailPlus { get; set; }
        

        [Description("วันที่เพิ่ม")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

    }
}