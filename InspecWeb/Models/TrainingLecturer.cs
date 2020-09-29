using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        [Description("ชื่อวิทยากร")]
        public string LecturerName { get; set; }

        [Required]
        [Description("หมายเลขโทรศัพท์")]
        public string Phone { get; set; }

        [Description("อีเมล์")]
        public string Email { get; set; }

        [Description("ประวัติการศึกษา")]
        public string Education { get; set; }

        [Description("ประวัติการทำงาน")]
        public string WorkHistory { get; set; }

        [Description("ประสบการณ์บรรยาย")]
        public string Experience { get; set; }

        [Description("วันที่เพิ่ม")]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

    }
}